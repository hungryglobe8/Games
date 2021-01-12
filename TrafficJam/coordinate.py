class Coordinate():
    def __init__(self, x, y):
        self.x = x
        self.y = y
    
    def __str__(self):
        ''' Print coordinate in (x, y) notation. '''
        return f"({self.x}, {self.y})"
    
    def __eq__(self, other):
        return self.x == other.x and self.y == other.y

    def shift_left(self): 
        ''' Coordinate (0, 0) becomes (-1, 0) '''
        self.x -= 1

    def shift_right(self): 
        ''' Coordinate (0, 0) becomes (1, 0) '''
        self.x += 1

    def shift_up(self): 
        ''' Coordinate (0, 0) becomes (0, -1) '''
        self.y -= 1

    def shift_down(self): 
        ''' Coordinate (0, 0) becomes (0, 1) '''
        self.y += 1

    def extend_right(self, size):
        ''' 
        Given a size, return an immutable list of coordinates stemming right from self.x and self.y. 
        For example, extend_right(3) from (0, 0) returns:
            ((0, 0), (1, 0), (2, 0))
        '''
        coors = [Coordinate(self.x, self.y)]
        for num in range(1, size):
            coors.append(Coordinate(self.x + num, self.y))
        return coors

    def extend_down(self, size):
        ''' 
        Given a size, return an immutable list of coordinates stemming down from self.x and self.y. 
        For example, extend_down(2) from (0, 0) returns:
            ((0, 0), (0, 1))
        '''
        coors = [Coordinate(self.x, self.y)]
        for num in range(1, size):
            coors.append(Coordinate(self.x, self.y + num))
        return coors

    def within_range(self, x_min, x_max, y_min, y_max):
        '''
        Determine if a coordinate falls in the range of (0, x_max) and (0, y_max),
        with x_max and y_max being exclusive.
        '''
        return self.x in range(x_min, x_max) and self.y in range(y_min, y_max)