var max_time = 1000000

while(_.wait()) {
	__.children.webcam.shader.clock_time = vec((__.children.webcam.shader.clock_time.x + 0.1) % max_time, 0)
}