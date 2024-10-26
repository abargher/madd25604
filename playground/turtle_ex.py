import turtle
# from turtle import Screen, Turtle
# from random import randint

# WIDTH, HEIGHT = 640, 480
# CURSOR_SIZE = 20

# def rand_move():
#     turtle.goto(randint(CURSOR_SIZE - WIDTH//2, WIDTH//2 - CURSOR_SIZE), randint(CURSOR_SIZE - HEIGHT//2, HEIGHT//2 - CURSOR_SIZE))

#     if turtle.points < 3:
#         screen.ontimer(rand_move, 1000)  # delay in milliseconds
#     else:
#         turtle.home()
#         turtle.write("WOW! You're good at catching me!", align='center', font=('Arial', 16, 'bold'))
#         turtle.hideturtle()

# def catch(x, y):
#     turtle.write("Ah!", font=('Arial', 14, 'normal'))
#     turtle.points += 1

# screen = Screen()
# screen.setup(WIDTH, HEIGHT)

# turtle = Turtle()
# turtle.shape('turtle')
# turtle.color('red')
# turtle.speed('fastest')
# turtle.penup()
# turtle.onclick(catch)

# turtle.points = 0  # user defined property

# rand_move()

# screen.mainloop()

bob = turtle.Turtle()
def golden_rectangle(length):
    golden_ratio = 1.618
    for _ in range (100):
        l1 = length / golden_ratio
        l2 = l1 / golden_ratio
        for i in range(2):
            bob.forward(l1)
            bob.right(90)
            bob.forward(l2)
            bob.right(90)

golden_rectangle(100)
