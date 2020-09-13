import pygame
import math
from button import *
from car import HorizontalCar, VerticalCar
from coordinate import Coordinate
from grid import Grid
from pygame.locals import (
    MOUSEBUTTONDOWN, MOUSEBUTTONUP, QUIT, MOUSEMOTION, KEYDOWN
)
'''
Project description here.
'''
# Define some colors
BLACK    = (   0,   0,   0)
WHITE    = ( 255, 255, 255)
GREEN    = (   0, 255,   0)
RED      = ( 255,   0,   0)
BLUE     = (   0,   0, 255)

# # Define a font
# smallfont = pygame.font.SysFont('Corbel', 35)
# # Render text
# text = smallfont.render('New Car', True, color)

def attempt_drag(mouse_pos, car, grid):
    car_loc = Grid.transform_car_to_game(car)
    old_coor = Grid.location_to_coordinate(car_loc[0], car_loc[1])
    new_coor = Grid.location_to_coordinate(mouse_pos[0], mouse_pos[1])
    
    grid.drag_car(old_coor, new_coor, car)

def draw_grid(grid):
    block_size = Grid.square_size
    for x in range(1,grid.width + 1):
        for y in range(1,grid.height + 1):
            rect = pygame.Rect(x * block_size, y * block_size, block_size, block_size)
            pygame.draw.rect(screen, BLACK, rect, 2)

def clicked_region(mouse_pos, car_shape):
    '''
    Returns whether a user's mouse is within a rectangle's area.
    '''
    x = mouse_pos[0]
    y = mouse_pos[1]
    width = range(car_shape[0], car_shape[0] + car_shape[2])
    height = range(car_shape[1], car_shape[1] + car_shape[3])
    return x in width and y in height

def round_down(x, init_pos, size):
    return int(math.ceil((x - size/2)/ size)) * size - init_pos

# Game window size.
size = (700, 700)
screen = pygame.display.set_mode(size)

# Window title.
pygame.display.set_caption("TrafficJam")

# Loop until the user clicks the close button.
done = False
 
# Used to manage how fast the screen updates.
clock = pygame.time.Clock()

# Keep track of rectangle locations.
grid = Grid(10, 10)
button1 = Button(600, 100, 50, 50, "New Car")

cars = dict()
selection = None
mouse_down = drag = False
# -------- Main Program Loop -----------
while not done:
    # --- Main event loop
    pos = pygame.mouse.get_pos()
    x = pos[0]
    y = pos[1]
    for event in pygame.event.get(): # User did something
        if event.type == QUIT: # If user clicked close
            done = True # Flag that we are done so we exit this loop
        elif event.type == MOUSEBUTTONDOWN:
            print("User pressed a mouse button")
            for color, car in cars.items():
                if clicked_region(pos, grid.cars[car]):
                    print(f"Mouse is in {color} region.")
                    selection = color
                    break
            if button1.on_button(pos):
                print("User clicked button")
            mouse_down = True
        elif event.type == MOUSEBUTTONUP:
            print("User released mouse")
            selection = None
            mouse_down = False
            drag = False
        
        if mouse_down and event.type == MOUSEMOTION:
            #attempt_drag(pygame.mouse.get_pos())
            drag = True
 
    # --- Game logic should go here
    if selection is not None:
        #print(f"{selection} is selected")
        attempt_drag(pos, cars[selection], grid)

    # First, clear the screen to white. Don't put other drawing commands
    # above this, or they will be erased with this command.
    screen.fill(WHITE)
 
    # --- Drawing code should go here
    draw_grid(grid)
    button1.draw(screen)

    # --- Go ahead and update the screen with what we've drawn.
    pygame.display.flip()
 
    # --- Limit to 60 frames per second
    clock.tick(60)