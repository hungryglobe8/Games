from coordinate import Coordinate

orientations = {"h": "horizontal", "v": "vertical"}

class Car():
    '''
    Create a car that is part of a grid system. Must be initialized with: 
        top-left location (as Coordinate),
        orientation (can be marked with h : horizontal or v : vertical),
        size (between 2 and 3)
    Can be moved around a grid according to its orientation.
    '''
    
    def __init__(self, grid, location, orientation, size):
        # add cars to a specific grid
        self.grid = grid
        # check type of coordinate
        if (type(location) != Coordinate):
            raise TypeError(location)
        self.start = location
        # check size
        if (size != 2 and size != 3):
            raise ValueError("Size should be two or three")
        self.size = size
        # orientation is checked in extend.
        self.coordinates = location.extend(size, orientation)
        self.orientation = orientation

    def move(self, coor):
        ''' Update the start coordinate and list of coordinates associated with a car. '''
        self.start = coor
        self.coordinates = self.start.extend(self.size, self.orientation)

    def move_left(self):
        if self.orientation != "horizontal":
            return
        self.move(Coordinate(self.start.x - 1, self.start.y))

    def move_right(self):
        if self.orientation != "horizontal":
            return
        self.move(Coordinate(self.start.x + 1, self.start.y))
    
    def move_up(self):
        if self.orientation != "vertical":
            return
        self.move(Coordinate(self.start.x, self.start.y - 1))

    def move_down(self):
        if self.orientation != "vertical":
            return
        self.move(Coordinate(self.start.x, self.start.y + 1))