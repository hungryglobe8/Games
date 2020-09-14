from Engine.coordinate import Coordinate

class VehicleInterface:

    def __init__(self, grid, coor, size, color=(200, 200, 200)):
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
        
    def is_within_grid(self):
        ''' Check if all of a cars coordinates are within the grid. If not, return false. '''
        for coor in self.coordinates:
            x = coor.x
            y = coor.y
            if (x < 0 or x > self.grid.width - 1 or y < 0 or y > self.grid.height - 1):
                return False
        return True
        
    # def is_within_grid(self, coor):
    #     ''' Check whether a given coordinate is within the grid. '''
    #     x = coor.x
    #     y = coor.y
    #     return not (x < 0 or x > self.width - 1 or y < 0 or y > self.height - 1)

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
