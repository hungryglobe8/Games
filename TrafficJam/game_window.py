import pygame
import math
from car import Car
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

def attempt_drag(mouse_pos, car, grid):
    car_loc = Grid.transform_car_to_game(car)
    old_coor = Grid.location_to_coordinate(car_loc[0], car_loc[1])
    new_coor = Grid.location_to_coordinate(mouse_pos[0], mouse_pos[1])
    
    grid.attempt_move(old_coor, new_coor, car)

def draw_stick_figure(screen, x, y):
    # Head
    pygame.draw.ellipse(screen, BLACK, [1+x,y,10,10], 0)
 
    # Legs
    pygame.draw.line(screen, BLACK ,[5+x,17+y], [10+x,27+y], 2)
    pygame.draw.line(screen, BLACK, [5+x,17+y], [x,27+y], 2)
 
    # Body
    pygame.draw.line(screen, RED, [5+x,17+y], [5+x,7+y], 2)
 
    # Arms
    pygame.draw.line(screen, RED, [5+x,7+y], [9+x,17+y], 2)
    pygame.draw.line(screen, RED, [5+x,7+y], [1+x,17+y], 2)

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
size = (700, 500)
screen = pygame.display.set_mode(size)

# Window title.
pygame.display.set_caption("TrafficJam")

# Loop until the user clicks the close button.
done = False
 
# Used to manage how fast the screen updates.
clock = pygame.time.Clock()

# Keep track of rectangle locations.
grid = Grid(10, 10)
car1 = Car(grid, Coordinate(0, 0), "horizontal", 2)
car2 = Car(grid, Coordinate(3, 0), "horizontal", 3)
car3 = Car(grid, Coordinate(5, 5), "vertical", 2)
grid.add_car(car1)
grid.add_car(car2)
grid.add_car(car3)


cars = {"green": car1, "red": car2, "blue": car3}
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
    # if (drag):
    #     draw_stick_figure(screen, x, y)
    pygame.draw.rect(screen, GREEN, grid.cars[car1])
    pygame.draw.rect(screen, RED, grid.cars[car2])
    pygame.draw.rect(screen, BLUE, grid.cars[car3])

    # --- Go ahead and update the screen with what we've drawn.
    pygame.display.flip()
 
    # --- Limit to 60 frames per second
    clock.tick(60)