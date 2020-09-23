import pygame
# pylint: disable=no-member
import math, random
import button
from controller import Controller
import view
pygame.init()
from pygame.constants import (
    MOUSEBUTTONDOWN, MOUSEBUTTONUP, QUIT, MOUSEMOTION, KEYDOWN
)
'''
Game editor allows users to make their own traffic jam games,
test them out, make random boards, or save and load grids.
'''
# Game window size.
size = (700, 700)
screen = pygame.display.set_mode(size)

# Set up controller.
controller = Controller()
grid = controller.grid

# Window title.
pygame.display.set_caption("TrafficJam")

# Loop until the user clicks the close button.
done = False
 
# Used to manage how fast the screen updates.
clock = pygame.time.Clock()
# -------- Main Program Loop -----------
while not done:
    # --- Main event loop
    pos = pygame.mouse.get_pos()
    for event in pygame.event.get(): # User did something
        if event.type == QUIT: # If user clicked close
            done = True # Flag that we are done so we exit this loop
        elif event.type == MOUSEBUTTONDOWN:
            controller.mouse_down(pos)
        elif event.type == MOUSEBUTTONUP:
            controller.mouse_up(pos)
 
    # --- Game logic should go here
    if controller.selection is not None:
        controller.attempt_drag(pos, controller.selection)

    # Clear the screen to white.
    screen.fill(view.WHITE)
 
    # --- Drawing code should go here
    for elm in button.buttons:
        elm.draw(screen)
    for car in grid.cars:
        pygame.draw.rect(screen, car.color, grid.cars[car])
    view.draw_grid(screen, grid)

    last_click = controller.last_click
    if isinstance(last_click, button.CarInfo):
        view.draw_box(screen, grid, pos, last_click.color, last_click.type, last_click.size)

    # --- Go ahead and update the screen with what we've drawn.
    pygame.display.flip()
 
    # --- Limit to 60 frames per second
    clock.tick(60)