import pygame
# pylint: disable=no-member
import math
from button import Button
from car import HorizontalCar, VerticalCar
from coordinate import Coordinate
from grid import Grid
pygame.init()
from pygame.constants import (
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

def handle_button_clicks(mouse_pos, click):
    for button in buttons:
        if button.on_button(mouse_pos):
            # normal button
            if button is button1:
                button.shiny = click
                # activate car thingy
            # toggle buttons
            if button in car_orientation_buttons:
                index = car_orientation_buttons.index(button)
                other_button = car_orientation_buttons[(index + 1) % 2]
                if not button.shiny:
                    button.shiny = True
                    other_button.shiny = False
            if button in size_buttons:
                index = size_buttons.index(button)
                other_button = size_buttons[(index + 1) % 2]
                if not button.shiny:
                    button.shiny = True
                    other_button.shiny = False


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
center = size[0] / 4
bottom = size[1] - 130
button1 = Button(center, bottom, 100, 50, "New Car")
bottom += 60
horizontal_button = Button(center - 60, bottom, 80, 25, "horizontal")
vertical_button = Button(center + 80, bottom, 80, 25, "vertical")
size_two_button = Button(center - 60, bottom + 35, 80, 25, "2")
size_three_button = Button(center + 80, bottom + 35, 80, 25, "3")
horizontal_button.shiny = True
size_two_button.shiny = True

cars = dict()
buttons= [button1, horizontal_button, vertical_button, size_two_button, size_three_button]
car_orientation_buttons = (horizontal_button, vertical_button)
size_buttons = (size_two_button, size_three_button)
selection = None
mouse_down = drag = False
# -------- Main Program Loop -----------
while not done:
    # --- Main event loop
    pos = pygame.mouse.get_pos()
    x = pos[0]
    y = pos[1]
    print(f"({x},{y})")
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
            handle_button_clicks(pos, True)
            mouse_down = True
        elif event.type == MOUSEBUTTONUP:
            print("User released mouse")
            handle_button_clicks(pos, False)
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
    for button in car_orientation_buttons:
        button.draw(screen)
    for button in size_buttons:
        button.draw(screen)

    # --- Go ahead and update the screen with what we've drawn.
    pygame.display.flip()
 
    # --- Limit to 60 frames per second
    clock.tick(60)