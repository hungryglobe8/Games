import Coordinate

class Car():
    
    def __init__(self, grid, location, orientation, size):
        # add cars to a specific grid
        self.grid = grid
        # check type of coordinate
        if (type(location) != Coordinate):
            raise TypeError()
        # check size
        if (size != 2 and size != 3):
            raise ValueError("Size should be two or three")
        # orientation is checked in extend.
        self.coordinates = location.extend(size, orientation)
        self.size = size