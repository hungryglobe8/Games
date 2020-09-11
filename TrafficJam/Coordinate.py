class Coordinate():
    def __init__(self, x, y):
        self.x = x
        self.y = y
    
    def __str__(self):
        ''' Print coordinate in (x, y) notation. '''
        return f"({self.x}, {self.y})"
    
    def __eq__(self, other):
        return self.x == other.x and self.y == other.y

    def extend(self, size, orientation):
        ''' 
        Given a size and orientation, return a list of coordinates stemming from self.x and self.y. 
        For example, extend(2, "horizontal") from (0, 0) returns:
            (0, 0), (1, 0)
        Calling extend(3, "vertical") from (0, 0) returns:
            (0, 0), (0, 1), (0, 2)
        '''
        coors = [(self)]
        if (orientation == "horizontal"):
            for num in range(1, size):
                coors.append(Coordinate(self.x + num, self.y))
        elif (orientation == "vertical"):
            for num in range(1, size):
                coors.append(Coordinate(self.x, self.y + num))
        else:
            raise ValueError("Orientation must be 'horizontal' or 'vertical'")
        return coors