_.time.frame.x = 200
// animates time to the right slowly
var time = 0
_t()
while(_.wait())
{
	time = (time + 1) % 100
    _.time.frame.x += 0.1
	if (_.time.frame.x >= 300) {
		_.time.frame.x = 200;
	}
	_.redraw()
}