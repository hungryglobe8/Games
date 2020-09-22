import pygame
# pylint: disable=no-member
import math, random
import button
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

def attempt_drag(mouse_pos, car):
    car_loc = Grid.transform_car_to_game(car)
    game_coor = Grid.location_to_coordinate(car_loc[0], car_loc[1])
    new_coor = Grid.location_to_coordinate(mouse_pos[0], mouse_pos[1])
    
    grid.drag_vehicle(new_coor, car)

def add_car_to_game(mouse_pos, car):
    pos = Grid.location_to_coordinate(mouse_pos[0], mouse_pos[1])
    grid.add_car(car.type(grid, pos, car.size, car.color))

def random_color():
    return (random.randint(0, 255), random.randint(0, 255), random.randint(0, 255))

def draw_grid(grid):
    block_size = Grid.square_size
    for x in range(1,grid.width + 1):
        for y in range(1,grid.height + 1):
            rect = pygame.Rect(x * block_size, y * block_size, block_size, block_size)
            pygame.draw.rect(screen, BLACK, rect, 3)

def within_grid(mouse_pos):
    x = mouse_pos[0]
    y = mouse_pos[1]
    return x in range(grid_size[0], grid_size[0] + grid_size[2]) and \
        y in range(grid_size[1], grid_size[1] + grid_size[3])

def draw_box(screen, mouse_pos, color, orient, size):
    game_coor = Grid.location_to_coordinate(mouse_pos[0], mouse_pos[1])
    if (orient == VerticalCar):
        car = VerticalCar(grid, game_coor, size, color)
    elif (orient == HorizontalCar):
        car = HorizontalCar(grid, game_coor, size, color)

    if car.is_within_grid():
        draw_car(screen, car) 

def draw_car(screen, car):
    for coor in car.coordinates:
        window_coor = Grid.transform_point_to_game(coor)
        pygame.draw.rect(screen, car.color, [window_coor.x, window_coor.y, Grid.square_size, Grid.square_size])

def clicked_region(mouse_pos, car_shape):
    '''
    Returns whether a user's mouse is within a rectangle's area.
    '''
    x = mouse_pos[0]
    y = mouse_pos[1]
    width = range(car_shape[0], car_shape[0] + car_shape[2])
    height = range(car_shape[1], car_shape[1] + car_shape[3])
    return x in width and y in height

# Game window size.
size = (700, 700)
grid_size = [50, 50, 500, 500]
screen = pygame.display.set_mode(size)

# Window title.
pygame.display.set_caption("TrafficJam")

# Loop until the user clicks the close button.
done = False
 
# Used to manage how fast the screen updates.
clock = pygame.time.Clock()

# Keep track of rectangle locations.
grid = Grid(5, 5)

selection = None
last_click = None
# WARNING BAD = same reference
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
            # Reset last car click.
            if last_click is not None:
                if within_grid(pos):
                    add_car_to_game(pos, last_click)
                last_click = None
            else:
                for car, loc in grid.cars.items():
                    if clicked_region(pos, loc):
                        print(f"Mouse is in {car.color} region.")
                        selection = car
                        break
            button.update_click(pos, True, screen)
            mouse_down = True
        elif event.type == MOUSEBUTTONUP:
            print("User released mouse")
            last_click = button.update_click(pos, False, screen)
            if isinstance(last_click, tuple):
                print(f"Button selected is {last_click[0]}")
            selection = None
            mouse_down = False
            drag = False
        
        if mouse_down and event.type == MOUSEMOTION:
            #attempt_drag(pygame.mouse.get_pos())
            drag = True
 
    # --- Game logic should go here
    if selection is not None:
        #print(f"{selection} is selected")
        attempt_drag(pos, selection)

    # First, clear the screen to white. Don't put other drawing commands
    # above this, or they will be erased with this command.
    screen.fill(WHITE)
 
    # --- Drawing code should go here
    for elm in button.buttons:
        elm.draw(screen)
    for car in grid.cars:
        pygame.draw.rect(screen, car.color, grid.cars[car])
    draw_grid(grid)

    if isinstance(last_click, button.CarInfo):
        draw_box(screen, pos, last_click.color, last_click.type, last_click.size)

    # --- Go ahead and update the screen with what we've drawn.
    pygame.display.flip()
 
    # --- Limit to 60 frames per second
    clock.tick(60)