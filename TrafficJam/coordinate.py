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
        coors = [self]
        for num in range(1, size):
            coors.append(Coordinate(self.x + num, self.y))
        return tuple(coors)


    def extend_down(self, size):
        ''' 
        Given a size, return an immutable list of coordinates stemming down from self.x and self.y. 
        For example, extend_down(2) from (0, 0) returns:
            ((0, 0), (0, 1))
        '''
        coors = [self]
        for num in range(1, size):
            coors.append(Coordinate(self.x, self.y + num))
        return tuple(coors)


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

    @staticmethod
    def shared_coordinate(coor_list1, coor_list2):
        ''' Returns true if any coordinates are shared between lists. '''
        return any(coor in coor_list1 for coor in coor_list2)

square_size = 100
origin = Coordinate(50,50)

def transform_car_to_game(car):
    start = transform_point_to_game(car.coordinates[0])
    height = width = square_size
    if car.orientation == "horizontal":
        width *= car.size
    elif car.orientation == "vertical":
        height *= car.size 
    else:
        raise ValueError()
    return [start.x, start.y, width, height]


def transform_point_to_game(coor):
    '''
    Transform a point to the game window's coordinate system.
    (0,0) => (50, 50)
    (1,1) => (150, 150)
    '''
    x = (coor.x * square_size) + origin.x
    y = (coor.y * square_size) + origin.y
    return Coordinate(x, y)
    
def location_to_coordinate(x, y):
    '''
    Transform a location to the grid's coordinate system.
    '''
    new_x = (x - origin.x) // square_size
    new_y = (y - origin.x) // square_size
    return Coordinate(new_x, new_y)