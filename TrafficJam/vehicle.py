from coordinate import Coordinate

class VehicleInterface:

    def __init__(self, grid, coor, size, color):
        # add vehicle to a grid
        self.grid = grid
        # check type of coor
        if (type(coor) != Coordinate):
            raise TypeError(coor)
        self.start = coor
        # check size
        if (size != 2 and size != 3):
            raise ValueError("Size should be two or three.")
        self.size = size
        # color should be tuple with 3 elements
        if (len(color) != 3):
            raise ValueError("Color should be 3-tuple.")
        self.color = color

    def move(self, start, extend):
        ''' Update the start coordinate and list of coordinates associated with a vehicle. '''
        self.start = start
        self.coordinates = extend(self.size)

    def collides_with(self, other_vehicle):
        ''' 
        Returns True if any coordinates are shared between two vehicles. 
        Returns False if vehicles are the same. 
        '''
        if self == other_vehicle:
            return False
        return any(coor in other_vehicle.coordinates for coor in self.coordinates)

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
