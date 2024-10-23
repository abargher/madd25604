var ToAxi = Java.type("trace.ToAxi")

// __.children.pythonhead.lines

var global_frame_count = __.children.pythonhead.frame_count

var toaxi = new ToAxi(`/Users/alec/CMSC/madd25604/sketch2/draw_out/frame_${global_frame_count}.csv`)

var paths = toaxi.sample(__.children.pythonhead.lines, 5.0)
toaxi.export(paths, 1.0, toaxi.fit(paths, vec(16, 10)), vec(0.5, 0.5), toaxi.min(paths))

__.children.pythonhead.frame_count++
