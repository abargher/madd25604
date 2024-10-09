_.lines.clear()
var on = false
for (var i = 0; i < 100; i++) {
	if (!on){
		break;
	}
	var line = new FLine()
	line.color = vec(i + 60 /  256.0, (i + 25) / 200.0, (i / 50.0) % 20.0, 0.3)
	line.moveTo((50*i) % 500, (26*i) % 250);
	line.cubicTo(10*i % 500, 15 * i % 250, 200, i*40 % 400, (i*500 + 56) % 256, (i*40 + 6) % 250);
	// line.lineTo();
	_.lines.add(line);
}

