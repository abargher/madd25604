var clock_time = 0

while (_.wait()) {
	clock_time = (clock_time + 1) % 100
}