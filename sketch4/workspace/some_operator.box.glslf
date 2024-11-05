#version 410
${_._uniform_decl_}
layout(location=0) out vec4 _output;
in vec2 tc;



void main()
{
	_output  = vec4(1,1,1,1);
	_output = vec4(tc.x*5, tc.y * sin(tc.x), cos(tc.y), 1.0);
	
	
	
}