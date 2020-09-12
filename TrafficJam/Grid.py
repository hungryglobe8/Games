from car import VerticalCar, HorizontalCar
from coordinate import Coordinate

class Grid():
    # Class variables
    square_size = 50
    origin = Coordinate(50,50)

    def __init__(self, width, height):
        self.width = width
        self.height = height
        self.cars = dict()
        self.occupied_squares = list()
        
    def within_grid(self, coor):
        x = coor.x
        y = coor.y
        if (x < 0 or x > self.width or y < 0 or y > self.height):
            return False
        else:
            return True

    def point_within_grid(self, point):
        if (point < 0 or point > self.width):
            return False
        else:
            return True

    def add_loc(self, coor):
        self.occupied_squares.append(coor)

    def remove_loc(self, coor):
        self.occupied_squares.remove(coor)

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
        # append adds as list, not elements
        self.occupied_squares.extend(car.coordinates)

        res = ""
        for square in self.occupied_squares:
            res += str(square)
        print(res)


    def attempt_move(self, old_coor, new_coor, car):
        '''
        TODO: mouse must be on box, new_coor must be open
        '''
        if (not self.within_grid(new_coor)):
            return

        if (new_coor.x > car.coordinates[-1] or new_coor.y > car.coordinates[-1]):
            car.increase_pos()
            for existing_car in self.cars.keys():
                if car.collides_with(existing_car):
                    car.decrease_pos()
        
        elif (new_coor.x < car.coordinates[0] or new_coor.y < car.coordinates[0]):
            car.decrease_pos()
            for existing_car in self.cars.keys():
                if car.collides_with(existing_car):
                    car.increase(pos)

        else:
            return

        # remove old coordinates
        for coor in car.coordinates:
            self.occupied_squares.remove(coor)
            
        self.update_car_location(car)
            
            
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