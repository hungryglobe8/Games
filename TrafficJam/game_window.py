import pygame
# pylint: disable=no-member
import math
import button
import view
from controller import Controller
from car import HorizontalCar, VerticalCar
from coordinate import Coordinate
from grid import Grid
from tkinter import Tk
from tkinter import messagebox

Tk().wm_withdraw() #to hide the main window
pygame.init()
from pygame.constants import (
    MOUSEBUTTONDOWN, MOUSEBUTTONUP, QUIT, MOUSEMOTION, KEYDOWN
)
'''
Project description here.
'''
# Game window size.
size = (700, 700)
screen = pygame.display.set_mode(size)

# Set up controller.
grid = Grid(10, 10)
controller = Controller(grid)
car1 = HorizontalCar(grid, Coordinate(0, 0), 2, view.BLUE)
car2 = HorizontalCar(grid, Coordinate(3, 0), 3, view.GREEN)
car3 = VerticalCar(grid, Coordinate(5, 5), 2, view.RED)
grid.add_car(car1)
grid.add_car(car2)
grid.add_car(car3)
grid.add_exit(Coordinate(5, -1))

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
    if grid.game_over:
        messagebox.showinfo('You won!!', "Congratulations")
        grid.game_over = False

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