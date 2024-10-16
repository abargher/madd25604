var PythonHead = Java.type("trace.video.PythonHead")
// **change this according to your OS and according to where you put pythonhead**
// PythonHead.start(_, "/Users/alec/CMSC/madd25604/sketch2/venv/bin/python3", "/Users/alec/CMSC/madd25604/sketch2/pythonhead", ["--do_lines", "--shrink", "0.2", "--video", "1", "--do_flow"])

PythonHead.start(_, "/Users/alec/CMSC/madd25604/sketch2/venv/bin/python3", "/Users/alec/CMSC/madd25604/sketch2/pythonhead", 
["--shrink", "0.5",
 "--video", "1",
 "--do_pose"])

// do this every frame
_r = () => {
	var points = PythonHead.now.posepoints
	var width = Math.floor(Number(PythonHead.now.width))
	var height = Math.floor(Number(PythonHead.now.height))
	
	var draw_points = []
	for (var coord of points[0]) {
		var fx = Number(coord[0])
		var fy = Number(coord[1])
		var fz = Number(coord[2])
		var loc = vec(fx * width/2, fy * height/2)
		draw_points.push(loc)
	}
	
	var newline = new FLine().data("ml*", draw_points)
	newline.thicken = 10
	newline.color = vec(0,0,0,1)
	_.lines.f = newline
	
// 	var fx = PythonHead.now.mean_motion_x*10000
// 	var fy = PythonHead.now.mean_motion_y*10000

// 	var b = PythonHead.now.mean_red /255
// 	var g = PythonHead.now.mean_green /255
// 	var r = PythonHead.now.mean_blue /255

// 	var px = PythonHead.now.mean_motion_px*960
// 	var py = PythonHead.now.mean_motion_py*540

// 	var f = new FLine()

// 	f.moveTo(px, py)
// 	f.lineTo(px+fx, py+fy)

// 	f.thicken=10
// 	f.color = vec(r,g,b,1)

// 	_.lines.f = f
}