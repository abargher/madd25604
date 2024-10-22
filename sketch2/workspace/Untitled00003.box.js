var ToAxi = Java.type("trace.ToAxi")

var f = new FLine()
var num = 400
var f = new FLine()

_.lines.clear()
var f2 = new FLine()
for(var i=0;i<num;i++)
{
	let alpha = i/num
	//let theta = Math.PI*2*alpha
	let theta = Math.PI*2*Math.random()
	
	var x = Math.sin(theta)*140
	var y = Math.cos(theta)*130

	var x2 = Math.sin(theta+1.0)*400
	var y2 = Math.cos(theta+1.0)*400

	f.moveTo(0,0)
	f.polarCubicTo(1,100,0.23,100, x, y)
	f.smoothTo(x2, y2)
	
	//f2.moveTo(0,0).lineTo(100,300)
	//f2.moveTo(x,y).lineTo(500,500)
	
}

//f2.pointed = true
//f2.pointSize = 10
_.lines.f = f
//_.lines.f2 = f2
_.redraw()

var toaxi = new ToAxi("/Users/alec/CMSC/madd25604/curvything.csv")
var paths = toaxi.sample(_.lines, 5.0)
toaxi.export(paths, 1.0, toaxi.fit(paths, vec(17, 11)), vec(0.5, 0.5), toaxi.min(paths))