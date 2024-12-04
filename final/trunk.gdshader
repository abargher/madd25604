shader_type canvas_item;
//The following functions are from:
//'Combustible Voronoi' https://www.shadertoy.com/view/4tlSzl
vec3 firePalette(float i){

    float T = 1400. + 2200.*i; // Temperature range (in Kelvin).
    vec3 L = vec3(8.15, 6.6, 5.8); // Red, green, blue wavelengths (in hundreds of nanometers).
    L = pow(L,vec3(4.0)) * (exp(1.43876719683e5/(T*L))-1.0);
    return 1.0-exp(-5e6/L); // Exposure level. Set to "50." For "70," change the "5" to a "7," etc.
}
vec3 hash33(vec3 p){

    float n = sin(dot(p, vec3(7, 157, 113)));
    return fract(vec3(2097152.011, 262144.984, 32768.115)*n);
}
float voronoi(vec3 p){

	vec3 b, r, g = floor(p);
	p = fract(p); // "p -= g;" works on some GPUs, but not all, for some annoying reason.

	float d = 0.5;

    for(float j = -1.; j <= 1.; j++) {
	    for(float i = -1.; i <= 1.; i++) {

		    b = vec3(i, j, -1.);
		    r = b - p + hash33(g+b);
		    d = min(d, dot(r,r));

		    b.z = 0.0;
		    r = b - p + hash33(g+b);
		    d = min(d, dot(r,r));

		    b.z = 1.;
		    r = b - p + hash33(g+b);
		    d = min(d, dot(r,r));

	    }
	}

	return d; // Range: [0, 1]
}

float noiseLayers(in vec3 p) {
    vec3 t = vec3(0., 0., 0.);

    const int iter = 5; // Just five layers is enough.
    float tot = 0.08, sum = 0., amp = 0.5; // Total, sum, amplitude.

    for (int i = 0; i < iter; i++) {
        tot += voronoi(p + t) * amp; // Add the layer to the total.
        p *= 2.0; // Position multiplied by two.
        t *= 1.5; // Time multiplied by less than two.
        sum += amp; // Sum of amplitudes.
        amp *= 0.5; // Decrease successive layer amplitude, as normal.
    }

    return tot/sum; // Range: [0, 1].
}
//=================================================================


void fragment()
{
    // Use UV coordinates in range x = [-0.25, 0.25], y = [-0.025, 0.025].
    vec2 uv = FRAGCOORD.xy/(1.0 / SCREEN_PIXEL_SIZE);
    uv *= vec2(2.0, 0.20);//scale vertically to replicate tree bark
    uv -= vec2(1.0, 0.1);
    uv *= 0.25;

    // Constructing the unit ray.
	vec3 rd = normalize(vec3(uv.x, uv.y, 3.1415926535898/8.));

    // Rotating the ray about the XY plane, to simulate a rolling camera.
	float cs = cos(-0.15), si = sin(0.25);
    // Apparently "r *= rM" can break in some older browsers.
	rd.xy = rd.xy*mat2(vec2(cs, -si), vec2(si, cs));

	// Passing a unit ray multiple into the Voronoi layer function, which
	// is nothing more than an fBm setup with some time dialation.
	float c = noiseLayers(rd*28.8);

	// Optional: Adding a bit of random noise for a subtle dust effect.
	c = max(c + dot(hash33(rd)*2.-1., vec3(0.015)), 0.);

    // Adjust color output to match bark.
    vec3 col =  (((vec3(0.5) - firePalette(c))*0.3) + vec3(0.120,0.055,0.40)) * vec3(7.3,1.65,0.85);
    col = vec3(dot(col.rgb, vec3(0.299, 0.587, 0.114)));//convert to grayscale

    vec4 base_color = vec4(0.5, 0.3, 0.2, 1.0);
	// Done.
	COLOR = vec4(col, 1.) * base_color;
    // COLOR = base_color;
}

void vertex() {
    //vec4 pos = MODEL_MATRIX * vec4(0.0, 0.0, 0.0, 1.0);
    //float time = TIME * speed + offset;
    ////float time = TIME * speed + pos.x * pos.y  ; not working when moving...
    //VERTEX.x += getWind(VERTEX.xy, UV, time);
}
