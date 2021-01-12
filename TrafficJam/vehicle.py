from coordinate import Coordinate

class Vehicle:
    def __init__(self, grid, coordinates, color=(200, 200, 200)):
        # add vehicle to a grid
        self.grid = grid
        # color should be tuple with 3 elements
        if (len(color) != 3):
            raise ValueError("Color should be 3-tuple.")
        self.color = color
        # check size
        if (len(coordinates) != 2 and len(coordinates) != 3):
            raise ValueError("Size should be two or three.")
        self.size = len(coordinates)
        if not any(isinstance(coor, Coordinate) for coor in coordinates):
            raise TypeError("All values in coordinates should be type(Coordinate).")
        self.coordinates = coordinates

    def collides_with(self, other_vehicle):
        ''' 
        Returns True if any coordinates are shared between two vehicles. 
        Returns False if vehicles are the same. 
        '''
        if self == other_vehicle:
            return False
        return any(coor in other_vehicle.coordinates for coor in self.coordinates)
        
    def is_within_grid(self):
        ''' Check if all of a cars coordinates are within the grid. If not, return false. '''
        x_max = self.grid.width
        y_max = self.grid.height
        for coor in self.coordinates:
            if not coor.within_range(0, x_max, 0, y_max):
                return False
        return True

    def increase_pos(self):
        '''
        Move a vehicle one direction along its track.
        (0, 0) -> (1, 0)
        (0, 0) -> (0, 1)
        '''
        raise NotImplementedError()

    def decrease_pos(self):
        '''
        Move a vehicle the other direction along its track.
        (1, 0) -> (0, 0)
        (0, 1) -> (0, 0)
        '''
        raise NotImplementedError()