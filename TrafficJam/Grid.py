from car import Car
from coordinate import Coordinate

class Grid():
    # Class variables
    square_size = 100
    origin = Coordinate(50,50)

    def __init__(self, width, height):
        self.width = width
        self.height = height
        self.cars = list()
        self.occupied_squares = list()
        
    def within_grid(self, coor):
        x = coor.x
        y = coor.y
        if (x < 0 or x > self.width or y < 0 or y > self.height):
            return False
        else:
            return True

    def add_loc(self, coor):
        self.occupied_squares.append(coor)

    def remove_loc(self, coor):
        self.occupied_squares.remove(coor)

    def add_car(self, car):
        self.cars.append(car)

    @staticmethod
    def game_window_coordinates(coor):
        '''
        Transform a grid's coordinates to the game window's coordinates.
        (0,0) => (50, 50)
        (1,1) => (150, 150)
        '''
        x = (coor.x * Grid.square_size) + Grid.origin.x
        y = (coor.y * Grid.square_size) + Grid.origin.y
        return Coordinate(x, y)
        
    @staticmethod
    def location_to_coordinate(x, y):
        '''
        Transform a location to the grid's coordinate system.
        '''
        new_x = (x - Grid.origin.x) // Grid.square_size
        new_y = (y - Grid.origin.x) // Grid.square_size
        return Coordinate(new_x, new_y)