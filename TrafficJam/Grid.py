import Car, Coordinate

class Grid():
    # Class variables
    square_size = 100
    origin = Coordinate.Coordinate(50,50)

    def __init__(self, width, height):
        self.width = width
        self.height = height
        self.cars = list()
        
    def within_grid(self, coor):
        x = coor.x
        y = coor.y
        if (x < 0 or x > self.width or y < 0 or y > self.height):
            return False
        else:
            return True

    def add_car(self, car):
        self.cars.append(car)

def game_window_coordinates(coor):
    '''
    Transform a grid's coordinates to the game window's coordinates.
    (0,0) => (50, 50)
    (1,1) => (150, 150)
    '''
    x = (coor.x * Grid.square_size) + Grid.origin.x
    y = (coor.y * Grid.square_size) + Grid.origin.y
    return Coordinate.Coordinate(x, y)

def mouse_to_grid(mouse_pos):
    '''
    Transform a mouse position to a grid's coordinate system.
    '''
    x = (mouse_pos[0] - Grid.origin.x) // Grid.square_size
    y = (mouse_pos[1] - Grid.origin.x) // Grid.square_size
    return Coordinate.Coordinate(x, y)