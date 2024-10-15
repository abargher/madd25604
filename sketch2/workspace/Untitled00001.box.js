var OpenPose = Java.type("trace.mocap.OpenPose")

var op = new OpenPose("/Users/marc/Documents/theron_playground/marc_1/output", 1920, 1920)

var f = new FLine()
f.color=vec(1,1,1,1)

var layer = _.stage.withName("asdf")
layer.is3D = true
layer.makeKeyboardCamera()
layer.vrDefaults()


layer.camera.position = vec(0.1, 0.05, -0.2)
layer.camera.target = vec(0, 0.05, 0)


_.stage.lines.clear()
layer.lines.clear()
layer.lines.f = f

for(var frame of op.frames)
{
	for(var person of frame.people)
	{
		// draw point 4 (right hand)
		var p = person.points[4]
		
		if (p.z>0)
		{
			f.lineTo(p.x/1000, p.y/1000)
		}
		
		f = f + vec(0, 0, 0.01)
		layer.lines.f = f
		
		for(var person of frame.people)
		{
			var b = new FLine()
			for(var point of person.points)
			{
				if (point.z>0)
					b.smoothTo(point.x/1000, point.y/1000)
			}
			b.color=vec(1,1,1,0.3)
			layer.lines.body = b

		}
	}
	_.stage.frame(0.05)
}