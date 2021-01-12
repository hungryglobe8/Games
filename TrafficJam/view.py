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
DARKBLUE = (  12,  69, 121)
GOAL     = RED

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
        if grid.game_over:
            controller.selection = None
            messagebox.showinfo('You won!!', "Congratulations")
            grid.game_over = False

        # Clear the screen to white.
        screen.fill(WHITE)
    
        # --- Drawing code should go here
        # Handle buttons before drawing other things.
        if show_buttons:
            for elm in button.buttons:
                elm.draw(screen)
            last_click = controller.last_click
            if isinstance(last_click, button.CarInfo):
                draw_box(screen, grid, pos, last_click.color, last_click.type, last_click.size)
            elif last_click == "line":
                draw_line(screen, Grid.mouse_to_coordinate(pos), grid)
            elif last_click == "save":
                grid.save_grid("TrafficJam/Levels/Test/mytest.game")
            elif last_click == "load":
                grid = Grid.load_grid("TrafficJam/Levels/Test/mytest.game")

        for car in grid.cars:
            pygame.draw.rect(screen, car.color, grid.cars[car])

        draw_grid(screen, grid)

        # --- Go ahead and update the screen with what we've drawn.
        pygame.display.flip()
    
        # --- Limit to 60 frames per second
        clock.tick(60)

def draw_box(screen, grid, mouse_pos, color, orient, size):
    game_coor = Grid.mouse_to_coordinate(mouse_pos)
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
        
def draw_line(screen, grid_coor, grid):
    width = grid.width
    height = grid.height
    game_coor = Grid.transform_point_to_game(grid_coor)
    # bottom side
    if grid_coor.within_range(0, width, -1, 0):
        vertices = [(game_coor.x, game_coor.y + Grid.square_size), (game_coor.x + Grid.square_size, game_coor.y + Grid.square_size)]
    # right side
    elif grid_coor.within_range(-1, 0, 0, height):
        vertices = [(game_coor.x + Grid.square_size, game_coor.y), (game_coor.x + Grid.square_size, game_coor.y + Grid.square_size)]
    # top side
    elif grid_coor.within_range(0, width, height, height + 1):
        vertices = [(game_coor.x, game_coor.y), (game_coor.x + Grid.square_size, game_coor.y)]
    # left side
    elif grid_coor.within_range(width, width + 1, 0, height):
        vertices = [(game_coor.x, game_coor.y), (game_coor.x, game_coor.y + Grid.square_size)]
    else:
        return
    pygame.draw.lines(screen, GOAL, False, vertices, 5)

def draw_grid(screen, grid):
    block_size = Grid.square_size
    for x in range(1,grid.width + 1):
        for y in range(1,grid.height + 1):
            rect = pygame.Rect(x * block_size, y * block_size, block_size, block_size)
            pygame.draw.rect(screen, BLACK, rect, 3)
            
    if grid.exit is not None:
        draw_line(screen, grid.exit, grid)