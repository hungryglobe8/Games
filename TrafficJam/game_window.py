from car import HorizontalCar, VerticalCar
from coordinate import Coordinate
from grid import Grid
import view
'''
Project description here.
'''
# Game window size.
size = (700, 700)
# Set up game.
grid = Grid(10, 10)
car1 = HorizontalCar(grid, Coordinate(0, 0), 2, view.BLUE)
car2 = HorizontalCar(grid, Coordinate(3, 0), 3, view.GREEN)
car3 = VerticalCar(grid, Coordinate(5, 5), 2, view.RED)
grid.add_car(car1)
grid.add_car(car2)
grid.add_car(car3)
grid.add_exit(Coordinate(5, -1))

view.game(size, grid)