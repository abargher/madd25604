#version 410

layout(location=0) out vec4 _output;

uniform sampler2D inputTexture;
uniform vec2 clock_time;
in vec2 tc;

float circle(vec2 _st, float _radius){
	vec2 len = _st-vec2(0.5);
	return 1.5-smoothstep(_radius-(_radius*0.01),
						_radius+(_radius*0.01),
						dot(len, len)*4.0);
}

vec2 tile(vec2 st, float scale){
	return fract(st * scale);
}

vec2 brickTile(vec2 _st, float _zoom, float time){
	_st *= _zoom;
	time = sin(time * 0.01)*5;
	
	float vert = 0;
	float horiz = 0;
	float t_fact = mod(time/100, 2);
	
	    if( fract(time)>0.5 ){
        if (fract( _st.y * 0.5) > 0.5){
            _st.x += fract(time)*2.0;
        } else {
            _st.x -= fract(time)*2.0;
        }
    } else {
        if (fract( _st.x * 0.5) > 0.5){
            _st.y += fract(time)*2.0;
        } else {
            _st.y -= fract(time)*2.0;
        }
    }
	
	
	return fract(_st);
}

void main()
{
	float time = clock_time.x;
		
	vec2 t_flip = vec2(tc.x, 1-tc.y);
	vec4 c = texture(inputTexture, t_flip);
	c = vec4(
		c.r*cos(texture(inputTexture, vec2(1- t_flip.x, t_flip.y)).b),
		c.g * sin(time / 10), 
		c.b * sin(c.g * 100),
		1.0
	);
	
	vec2 st = brickTile(t_flip,
						//(cos(time / 20) * 10) + 30,
						40,
					   	time);
	
	vec3 color = vec3(circle(st, 0.6)) * c.rgb;
	_output = vec4(color, 1.0);

}