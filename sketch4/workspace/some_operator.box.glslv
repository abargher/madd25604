#version 410
layout(location=0) in vec3 position;

out vec2 tc;
// out float time;

void main()
{
	gl_Position = ( vec4(position*2 - vec3(1,1,0), 1.0));

	tc = position.xy;
	// time = _t();
	
}