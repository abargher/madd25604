var speed = 0.5
var end = 620

_.time.frame.x = 0
while(_.wait())
{
    _.time.frame.x  = (_.time.frame.x + speed) % end
    _.redraw()
}