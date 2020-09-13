from vehicle import VehicleInterface

orientations = {"h": "horizontal", "v": "vertical"}
# Define some colors
BLACK    = (   0,   0,   0)
WHITE    = ( 255, 255, 255)
GREEN    = (   0, 255,   0)
RED      = ( 255,   0,   0)
BLUE     = (   0,   0, 255)

class HorizontalCar(VehicleInterface):
    '''
    Create a horizontal car that is part of a grid system. Must be initialized with: 
        top-left location (as Coordinate),
        size (between 2 and 3)
    Can move left or right on a grid, but not up and down.
    '''
    def __init__(self, grid, coor, size, color):
        VehicleInterface.__init__(self, grid, coor, size, color)
        # Save all coordinates of car.
        self.coordinates = coor.extend_right(size)

    def increase_pos(self):
        new_coor = self.start.right()
        super().move(new_coor, new_coor.extend_right)

    def decrease_pos(self):
        super().move(self.start.left(), self.start.left().extend_right)

class VerticalCar(VehicleInterface):
    '''
    Create a vertical car that is part of a grid system. Must be initialized with: 
        top-left location (as Coordinate),
        size (between 2 and 3)
    Can move up or down on a grid, but not left and right.
    '''
    def __init__(self, grid, coor, size, color):
        VehicleInterface.__init__(self, grid, coor, size, color)
        # Save all coordinates of car.
        self.coordinates = coor.extend_down(size)

    def increase_pos(self):
        super().move(self.start.down(), self.start.down().extend_down)

    def decrease_pos(self):
        super().move(self.start.up(), self.start.up().extend_down)