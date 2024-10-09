
var code = __.children.draw_100.code

var FieldLinesAndJavaText = Java.type("field.graphics.FLinesAndJavaText")


_.lines.clear()
var i = 0;
for (const s of code.split(/\r?\n/)) {
	var toText = new FieldLinesAndJavaText()
	toText.moveTo((50*i) % 500, (26*i) % 250);
	var txt = toText.flineForText(s, "Iosevka", 12, 0);
	txt.filled = true
	_.lines.add(txt)
	i = i + 1;
}

// var f = toText.flineForText(code, "Iosevka", 24, 0)
// f.filled = true
// _.lines.f = f
_.redraw()