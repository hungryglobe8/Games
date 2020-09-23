import pygame
import math, random
import button
from car import HorizontalCar, VerticalCar
from coordinate import Coordinate
from grid import Grid
pygame.init()
# Define some colors
BLACK    = (   0,   0,   0)
WHITE    = ( 255, 255, 255)
GREEN    = (   0, 255,   0)
RED      = ( 255,   0,   0)
BLUE     = (   0,   0, 255)

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


def random_color():
    return (random.randint(0, 255), random.randint(0, 255), random.randint(0, 255))
