var FieldLinesAndJavaText = Java.type("field.graphics.FLinesAndJavaText")

var toText = new FieldLinesAndJavaText()
var code = __.children.text_draw.code

_.lines.clear()
var code_lines = code.split(/\r?\n/)
var num_lines = code_lines.length

function weird_trig(q, i) {
	return Math.tan(Math.pow(q, 2) + Math.sin(q) + Math.log(q)) * i * 0.02
}

// Font parameters
var spacing = 40
var font_size = 36
var font_name = "SF Mono"

// Color parameters
var opacity = 0.2
var base_color = vec(0.1,0.02,0.15,opacity)
var red_adj = 0.02
var green_adj = 0.008
var blue_adj = 0.01

// Weird parameters
var fuzz = 4
var q_limit = 25.0
var point_size = 2
var chaos_scale = 0.05
var noise_scale = 4
var noise_limiter = 200

function weird_trig(q, i) {
	return (1 / Math.cos(Math.pow(q, 2) + 
					Math.sin(q) + 
					Math.tan(q_limit - q) * chaos_scale + 
					Math.log(q))) * 
			i * 
			chaos_scale
}

function variable_noise(i, lp) {
	return (i +
			lp +
			Math.sin(i / (num_lines / 2)) +
		   	Math.log(lp) * noise_scale) / noise_limiter
}

var q_func = weird_trig

// Worker function, draws a single line and its sublines, where one
// "line" is a line comprising a character of text
function draw_point_fuzz(m, i, line, f){
	m = new FLine().data("ml*", m.sampleByDistance(0.1))
	var c = m.cursor()
	var p = []
	
	for(var q = 0; q < q_limit; q++)
	{
		var alpha = (_t().doubleValue() * num_lines % 1)*q/q_limit
		var offset = vec(0, (i+1) * spacing)
		
		c.setD(c.lengthD()*alpha)
		
		// move to location
		p.push(c.position() + 
			   c.normal2().normalize()*q_func(q, i)*fuzz +
			   offset)
		
		// draw line to location
		p.push(c.position() +
			   c.tangent().normalize()*fuzz +
			   offset)
	}

	for (var item of p) {
		item.noise(variable_noise(i, p.length))
	}
	
	var fline = new FLine().data("ml*", p)
	var color = (base_color + (i * vec(red_adj, green_adj, blue_adj, 0)))
	
	fline.pointed = true
	fline.pointSize = point_size
	fline.color = color
	_.lines.add(fline)
}

_r = () => {
	var i = Math.floor(_t().doubleValue() * code_lines.length)

	var line = code_lines[i];
	var f = toText.flineForText(line, font_name, font_size, 0)

	for(var m of f.pieces()) {
		draw_point_fuzz(m, i, line, f);
	}
	_.redraw()
}