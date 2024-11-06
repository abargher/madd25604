import math
import sys

import pygame as pg

WIDTH = 600
HEIGHT = 600
BACK_COLOR = (230, 230, 255)


def main(argv: list[str]) -> None:
    # pg setup
    pg.init()
    screen = pg.display.set_mode((WIDTH, HEIGHT))
    pg.display.set_caption("game!")
    screen.fill(BACK_COLOR)
    clock = pg.time.Clock()

    running = True

    while running:
        # poll for events
        # pg.QUIT event means the user clicked X to close your window
        for event in pg.event.get():
            if event.type == pg.QUIT:
                running = False

        # fill the screen with a color to wipe away anything from last frame
        pg.draw.rect(surface=screen, color="black", rect=(10, 10, 40, 40))

        # RENDER YOUR GAME HERE

        clock.tick(60)  # limits FPS to 60

        # flip() the display to put your work on screen
        pg.display.flip()


    pg.quit()

if __name__ == "__main__":
    main(sys.argv)
