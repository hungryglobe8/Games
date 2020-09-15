from vehicle import Vehicle
class HorizontalCar(Vehicle):
    '''
    Create a horizontal car that is part of a grid system. Must be initialized with: 
        top-left location (as Coordinate),
        size (between 2 and 3)
    Can move left or right on a grid, but not up and down.
    '''
    def __init__(self, grid, coor, size, color=(200, 200, 200)):
        Vehicle.__init__(self, grid, coor.extend_right(size), size, color)

    def increase_pos(self):
        for coor in self.coordinates:
            coor.shift_right()

    def decrease_pos(self):
        for coor in self.coordinates:
            coor.shift_left()

class VerticalCar(Vehicle):
    '''
    Create a vertical car that is part of a grid system. Must be initialized with: 
        top-left location (as Coordinate),
        size (between 2 and 3)
    Can move up or down on a grid, but not left and right.
    '''
    def __init__(self, grid, coor, size, color=(200, 250, 250)):
        Vehicle.__init__(self, grid, coor.extend_down(size), size, color)

    def increase_pos(self):
        for coor in self.coordinates:
            coor.shift_up()
    def decrease_pos(self):
        for coor in self.coordinates:
            coor.shift_down()