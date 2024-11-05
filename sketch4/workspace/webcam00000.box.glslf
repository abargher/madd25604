#version 410

layout(location=0) out vec4 _output;

uniform sampler2D inputTexture;
uniform vec2 clock_time;
in vec2 tc;

void main()
{
	vec2 t_flip = vec2(tc.x, 1-tc.y);
	vec4 c = texture(inputTexture, t_flip);
	_output  = vec4(c.x * tan(tc.x * 10), c.y * sin(clock_time.x / 10), c.z * sin(c.y * 100),1);

}