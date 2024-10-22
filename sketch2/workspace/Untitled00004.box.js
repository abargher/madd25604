var PythonHead = Java.type("trace.video.PythonHead")
// **change this according to your OS and according to where you put pythonhead**
// PythonHead.start(_, "/Users/alec/CMSC/madd25604/sketch2/venv/bin/python3", "/Users/alec/CMSC/madd25604/sketch2/pythonhead", ["--do_lines", "--shrink", "0.2", "--video", "1", "--do_flow"])

PythonHead.start(_, "/Users/alec/CMSC/madd25604/sketch2/venv/bin/python3", "/Users/alec/CMSC/madd25604/sketch2/pythonhead", 
["--shrink", "0.5",
 "--video", "1",
 "--do_pose"])
_r = () => {
	var posepoints = PythonHead.now.posepoints
// 	PythonHead.now.posepoints

	var f = new FLine()

// 	if (posepoints.length[0]) {
		var p = PythonHead.now.posepoints
		PythonHead.now.posepoints
		for(var i=0;i<p.length;i++)
		{
			f.lineTo(p[i][0], p[i][1])
		}
		f.pointed = true
		f.pointSize=5
		_.lines.f = f * 300
// 	}

}


