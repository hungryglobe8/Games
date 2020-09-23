import pygame
# pylint: disable=no-member
import math, random
import button
from car import HorizontalCar, VerticalCar
from coordinate import Coordinate
from grid import Grid
pygame.init()

class Controller():
    def __init__(self):
        # Set up a grid to interact with the game.
        self.grid = Grid(5, 5)
        self.grid_size = [50, 50, 500, 500]

        self.selection = None
        self.last_click = None

    def attempt_drag(self, mouse_pos, car):
        new_coor = Grid.location_to_coordinate(mouse_pos[0], mouse_pos[1])
        self.grid.drag_vehicle(new_coor, car)
        
    def add_car_to_game(self, mouse_pos, car):
        pos = Grid.location_to_coordinate(mouse_pos[0], mouse_pos[1])
        self.grid.add_car(car.type(self.grid, pos, car.size, car.color))

    def within_grid(self, mouse_pos):
        x = mouse_pos[0]
        y = mouse_pos[1]
        return x in range(self.grid_size[0], self.grid_size[0] + self.grid_size[2]) and \
            y in range(self.grid_size[1], self.grid_size[1] + self.grid_size[3])

    def clicked_region(self, mouse_pos, car_shape):
        '''
        Returns whether a user's mouse is within a rectangle's area.
        '''
        x = mouse_pos[0]
        y = mouse_pos[1]
        width = range(car_shape[0], car_shape[0] + car_shape[2])
        height = range(car_shape[1], car_shape[1] + car_shape[3])
        return x in width and y in height

    def mouse_down(self, mouse_pos):
        # Reset last car click.
        if isinstance(self.last_click, button.CarInfo) and self.within_grid(mouse_pos):
            self.add_car_to_game(mouse_pos, self.last_click)
            self.last_click = None
        else:
            for car, loc in self.grid.cars.items():
                if self.clicked_region(mouse_pos, loc):
                    print(f"Mouse is in {car.color} region.")
                    self.selection = car
                    break
        button.update_click(mouse_pos, True)

    def mouse_up(self, mouse_pos):
        self.last_click = button.update_click(mouse_pos, False)
        self.selection = None