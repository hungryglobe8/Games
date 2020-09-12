from grid import Grid
from coordinate import Coordinate
import test_car as t

def test_init():
    assert Grid(5, 5) is not None

def make_normal_grid():
    return Grid(5, 5)

def test_add_car():
    grid = make_normal_grid()

    grid.add_car(t.basic_horizontal_car(grid))
    
    assert len(grid.cars) == 1

def test_add_two_horizontal_cars():
    grid = make_normal_grid()

    grid.add_car(t.basic_horizontal_car(grid, coor=Coordinate(0, 0)))
    grid.add_car(t.basic_horizontal_car(grid, coor=Coordinate(0, 1)))

    assert len(grid.cars.keys()) == 2

def test_add_two_cars_occupying_same_space_returns_false():
    grid = make_normal_grid()

    grid.add_car(t.basic_horizontal_car(grid))
    grid.add_car(t.basic_vertical_car(grid))
    
    assert len(grid.cars.items()) == 1
    assert len(grid.occupied_squares) == 2
