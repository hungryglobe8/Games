from coordinate import Coordinate

class VehicleInterface:

    def __init__(self, grid, coor, size):
        # add vehicle to a grid
        self.grid = grid
        # check type of coor
        if (type(coor) != Coordinate):
            raise TypeError(coor)
        self.start = coor
        # check size
        if (size != 2 and size != 3):
            raise ValueError("Size should be two or three")
        self.size = size

    def move(self, start, extend):
        ''' Update the start coordinate and list of coordinates associated with a vehicle. '''
        self.start = start
        self.coordinates = extend(self.size)

    def collides_with(self, other_vehicle):
        ''' Returns true if any coordinates are shared between two vehicles. '''
        return any(coor in other_vehicle.coordinates for coor in self.coordinates)


    def increase_pos(self):
        raise NotImplementedError()

    def decrease_pos(self):
        raise NotImplementedError()
