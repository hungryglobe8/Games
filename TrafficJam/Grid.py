from car import VerticalCar, HorizontalCar
from coordinate import Coordinate
import pickle

class Grid():
    # Class variables
    square_size = 50
    origin = Coordinate(50,50)

    def __init__(self, width, height):
        self.width = width
        self.height = height
        self.cars = dict()
        
    def within_grid(self, coor):
        x = coor.x
        y = coor.y
        if (x < 0 or x > self.width - 1 or y < 0 or y > self.height - 1):
            return False
        else:
            return True

    def point_within_grid(self, point):
        if (point < 0 or point > self.width):
            return False
        else:
            return True

    def add_car(self, car=None):
        if car == None:
            print("Randomly generate car")
            return True

        # Do not add car if space is already occupied.
        for existing_car in self.cars.keys():
            if car.collides_with(existing_car):
                return False
        else:
            self.update_car_location(car)
            return True

    def update_car_location(self, car):
        self.cars[car] = Grid.transform_car_to_game(car)   

    def attempt_move(self, car, move, reverse):
        move()
        for existing_car in self.cars.keys():
            if car.collides_with(existing_car):
                # Undo move.
                reverse()
                return
        # Update grid positions.
        self.update_car_location(car)

    def drag_car(self, old_coor, new_coor, car):
        '''
        TODO: mouse must be on box, new_coor must be open
        '''
        if (not self.within_grid(new_coor)):
            return

        if isinstance(car, HorizontalCar):
            if new_coor.x > car.coordinates[-1].x:
                self.attempt_move(car, car.increase_pos, car.decrease_pos)
            elif new_coor.x < car.coordinates[0].x:
                self.attempt_move(car, car.decrease_pos, car.increase_pos)
        elif isinstance(car, VerticalCar):
            if new_coor.y > car.coordinates[-1].y:
                self.attempt_move(car, car.increase_pos, car.decrease_pos)
            elif new_coor.y < car.coordinates[0].y:
                self.attempt_move(car, car.decrease_pos, car.increase_pos)
        else:
            raise ValueError("Vehicle is of an undefined type.")           
            
    @staticmethod
    def transform_car_to_game(car):
        start = Grid.transform_point_to_game(car.coordinates[0])
        height = width = Grid.square_size
        if isinstance(car, HorizontalCar):
            width *= car.size
        elif isinstance(car, VerticalCar):
            height *= car.size 
        else:
            raise ValueError()
        return [start.x, start.y, width, height]

    @staticmethod
    def transform_point_to_game(coor):
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

    @staticmethod
    def load_grid(file_name):
        '''
        Load data from a binary file storing an instance of a grid.
        The data has been serialized through a python package called pickle.
        '''
        f = open(file_name, "rb")
        grid = pickle.load(f)
        if not isinstance(grid, Grid):
            raise FileNotFoundError("This file does not contain a grid object as its first object.")
        f.close()
        return grid

    def save_grid(self, file_name):
        '''
        Dump grid data into a file with binary formatting.
        Can be retrieved with load_grid.
        '''
        f = open(file_name, "wb")
        pickle.dump(self, f) # serialize class object
        f.close()