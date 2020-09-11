from coordinate import Coordinate

class Car():
    
    def __init__(self, grid, location, orientation, size):
        # add cars to a specific grid
        self.grid = grid
        # check type of coordinate
        if (type(location) != Coordinate):
            raise TypeError()
        self.start = location
        # check size
        if (size != 2 and size != 3):
            raise ValueError("Size should be two or three")
        self.size = size
        # orientation is checked in extend.
        self.coordinates = location.extend(size, orientation)
        self.orientation = orientation

    def move_left(self):
        # self.start = Coordinate(start.x - 1, coord.y)
        # self.coordinates = self.start.extend(self.size, self.orientation)
        for coord in self.coordinates:
            coord = Coordinate(coord.x - 1, coord.y)

    def move_right(self):
        for coord in self.coordinates:
            coord = Coordinate(coord.x + 1, coord.y)
    
    def move_up(self):
        for coord in self.coordinates:
            coord = Coordinate(coord.x, coord.y + 1)

    def move_down(self):
        for coord in self.coordinates:
            coord = Coordinate(coord.x, coord.y - 1)