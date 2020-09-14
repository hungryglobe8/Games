from vehicle import VehicleInterface
class HorizontalCar(VehicleInterface):
    '''
    Create a horizontal car that is part of a grid system. Must be initialized with: 
        top-left location (as Coordinate),
        size (between 2 and 3)
    Can move left or right on a grid, but not up and down.
    '''
    def __init__(self, grid, coor, size, color=(200, 200, 200)):
        VehicleInterface.__init__(self, grid, coor, size, color)
        # Save all coordinates of car.
        self.coordinates = coor.extend_right(size)

    def set_init_position(self, start):
        VehicleInterface.set_init_position()

    def increase_pos(self):
        for coor in self.coordinates:
            coor.shift_right()

    def decrease_pos(self):
        for coor in self.coordinates:
            coor.shift_left()

class VerticalCar(VehicleInterface):
    '''
    Create a vertical car that is part of a grid system. Must be initialized with: 
        top-left location (as Coordinate),
        size (between 2 and 3)
    Can move up or down on a grid, but not left and right.
    '''
    def __init__(self, grid, coor, size, color=(200, 250, 250)):
        VehicleInterface.__init__(self, grid, coor, size, color)
        # Save all coordinates of car.
        self.coordinates = coor.extend_down(size)

    def increase_pos(self):
        super().move(self.start.down(), self.start.down().extend_down)

    def decrease_pos(self):
        super().move(self.start.up(), self.start.up().extend_down)