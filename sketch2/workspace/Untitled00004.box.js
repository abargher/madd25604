var ToAxi = Java.type("trace.ToAxi")
var PythonHead = Java.type("trace.video.PythonHead")
// **change this according to your OS and according to where you put pythonhead**
// PythonHead.start(_, "/Users/alec/CMSC/madd25604/sketch2/venv/bin/python3", "/Users/alec/CMSC/madd25604/sketch2/pythonhead", ["--do_lines", "--shrink", "0.2", "--video", "1", "--do_flow"])

PythonHead.start(_, "/Users/alec/CMSC/madd25604/sketch2/venv/bin/python3", "/Users/alec/CMSC/madd25604/sketch2/pythonhead", 
["--shrink", "0.5",
 "--video", "1",
 "--do_pose",
 "--do_background",
 "--compute_contours",
"--do_lines",
 "--do_flow",
])
var frame_count = 0

var afx = 0
var afy = 0

_r = () => {
	_.lines.clear()
	var posepoints = PythonHead.now.posepoints
// 	PythonHead.now.posepoints

	var f1 = new FLine()
	var p = PythonHead.now.posepoints

	var x_off = 0.01
	var y_off = -0.3
	for (var i = 0; i < p.length; i+=2)
	{
		f1.moveTo(p[i][0]+x_off, p[i][1]+y_off)
		f1.lineTo(p[i+1][0]+x_off, p[i+1][1]+y_off)
	}

	f1.pointed = true
	f1.pointSize=5
	_.lines.f1 = f1 * 1000
//BACKGROUNDS

	var t = PythonHead.now.contours
	if (t)
	{

		for(var i=0;i<t.length;i++)
		{
			var f = new FLine()
			var track = t[i]

			if (track.length<40) continue

			f.moveTo(track[0][0]*960, track[0][1]*540)
			for(var j=1;j<track.length;j+=1)
			{
				f.lineTo(track[j][0]*960, track[j][1]*540)
			}

			f = f * scale(1.2).pivot(f.center())
			f.color = vec(0,0,0,0.3)
			f.strokeColor = vec(0,0,0,1)
			f.filled=false
			_.lines.add(f)
			f = f * rotate(10.2).pivot(f.center())
			f.color = vec(0,0,0,0.3)
			f.strokeColor = vec(0,0,0,1)
			f.filled=false
			_.lines.add(f)
		}

}
	
//IDK MORE LINES??
// make a new line
	var f3 = new FLine()
	// grab the 'lines' for the current moment
	var l = PythonHead.now.lines
	// if there are any lines, then draw all of them
	if (l)
	{
		// grab the flow amount
		var fx = PythonHead.now.mean_motion_x*200
		var fy = PythonHead.now.mean_motion_y*-200

		// and smooth out the noise in it
		afx = fx*0.1 + 0.9*afx
		afy = fy*0.1 + 0.9*afy

		for(var i=0;i<l.length;i++)
		{
			// list of list of lists!
			// the first point
			a = vec(l[i][0][0], l[i][0][1])
			// the second point
			b = vec(l[i][1][0], l[i][1][1])

			len = (a-b).length()
			// scale them up so we can see them
			a = a * vec(960, 540)
			b = b * vec(960, 540)

			// random flow-based jitter
			a.x += afx*100*Math.random()
			b.x += afx*100*Math.random()

			a.y += afy*100*Math.random()
			b.y += afy*100*Math.random()

			perp = a-b
			perp = vec(perp.y, -perp.x).normalize()*1


			f3.moveTo((a-b)*2+a)
			f3.last().color=vec(0,0,0,1)
			f3.lineTo((b-a)*2+b)
			f3.last().color=vec(0,0,0,1)

			// render as weird triangle things
			for(var n=0;n<30;n++)
			{
				a = (a-b)*(1+len*n*0.003) + b 
				a.noise(3)
				b.noise(0.5)
				a = a + perp
				f3.moveTo(a)
				f3.last().color=vec(0,0,0,0.1)
				//f3.last().color=vec(len*3,len*3,len*3,0.1)
				f3.lineTo(b)
				f3.last().color=vec(0,0,0,0.3)
			}
		}

	}
	_.lines.f3 = f3

}

