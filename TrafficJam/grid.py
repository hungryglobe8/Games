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
        self.exit = None
        self.game_over = False

    def add_car(self, car=None):
        if car == None:
            print("Randomly generate car")
            return True

        # Do not add car if it is outside of the grid.
        if not car.is_within_grid():
            return False

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
        '''
        Try to move a car in a given direction. If the car ends up out of bounds,
        or colliding with another car which already exists, undo the move and don't
        update the grid's list of cars.
        '''
        move()
        # Check for victory.
        if self.exit is not None and self.exit in car.coordinates:
            self.update_car_location(car)
            self.game_over = True
            return
        if not car.is_within_grid():
            # or any(car.collides_with(existing_car) for existing_car in self.cars.keys()):
            # Undo move.
            reverse()
            return
        for existing_car in self.cars.keys():
            if car.collides_with(existing_car):
                # Undo move.
                reverse()
                return
        # Update grid positions.
        self.update_car_location(car)

    def drag_vehicle(self, new_coor, car):
        '''
        Attempts to drag a vehicle towards a specific location. Only tries to move car
        one tile at a time. Any collision will result in the complete stop of the car.
        Accepts vehicles of types HorizontalCar and VerticalCar
        '''
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

    def add_exit(self, coor):
        ''' Create an exit for the main car. Must be outside of main grid, but bordering it. '''
        if coor.within_range(0, self.width, 0, self.height):
            print("Exit must be outside of grid.")
            return
        if not coor.within_range(-1, self.width + 1, -1, self.height + 1):
            print("Exit is too far away from grid.")
            return
        # Only change exit if conditions above are not triggered.
        self.exit = coor
            
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
        Transform a grid's coordinates to the window's position.
        (0,0) => (50, 50)
        (1,1) => (150, 150)
        '''
        x = (coor.x * Grid.square_size) + Grid.origin.x
        y = (coor.y * Grid.square_size) + Grid.origin.y
        return Coordinate(x, y)
        
    @staticmethod
    def mouse_to_coordinate(pos):
        '''
        Transform a window position to the grid's coordinate system.
        (50, 50) => (0, 0)
        (150, 150) => (1, 1)
        '''
        x = pos[0]
        y = pos[1]
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