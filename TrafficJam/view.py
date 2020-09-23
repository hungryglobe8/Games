import pygame, button, view, math
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
# Define some colors
BLACK    = (   0,   0,   0)
WHITE    = ( 255, 255, 255)
GREEN    = (   0, 255,   0)
RED      = ( 255,   0,   0)
BLUE     = (   0,   0, 255)

def game(size, grid, show_buttons=False):
    # Game window size.
    screen = pygame.display.set_mode(size)

    # Set up controller.
    controller = Controller(grid)

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
        screen.fill(WHITE)
    
        # --- Drawing code should go here
        for car in grid.cars:
            pygame.draw.rect(screen, car.color, grid.cars[car])
        if show_buttons:
            for elm in button.buttons:
                elm.draw(screen)
            last_click = controller.last_click
            if isinstance(last_click, button.CarInfo):
                draw_box(screen, grid, pos, last_click.color, last_click.type, last_click.size)
        draw_grid(screen, grid)

        # --- Go ahead and update the screen with what we've drawn.
        pygame.display.flip()
    
        # --- Limit to 60 frames per second
        clock.tick(60)

def draw_box(screen, grid, mouse_pos, color, orient, size):
    game_coor = Grid.location_to_coordinate(mouse_pos[0], mouse_pos[1])
    if (orient == VerticalCar):
        car = VerticalCar(grid, game_coor, size, color)
    elif (orient == HorizontalCar):
        car = HorizontalCar(grid, game_coor, size, color)

    if car.is_within_grid():
        draw_car(screen, car) 

def draw_car(screen, vehicle):
    for coor in vehicle.coordinates:
        window_coor = Grid.transform_point_to_game(coor)
        pygame.draw.rect(screen, vehicle.color, [window_coor.x, window_coor.y, Grid.square_size, Grid.square_size])
        
def draw_grid(screen, grid):
    block_size = Grid.square_size
    for x in range(1,grid.width + 1):
        for y in range(1,grid.height + 1):
            rect = pygame.Rect(x * block_size, y * block_size, block_size, block_size)
            pygame.draw.rect(screen, BLACK, rect, 3)
    if grid.exit is not None:
        exit_coor = Grid.transform_point_to_game(grid.exit)
        pygame.draw.lines(screen, GREEN, False, \
            [(exit_coor.x, exit_coor.y + Grid.square_size), (exit_coor.x + Grid.square_size, exit_coor.y + Grid.square_size)], 3)