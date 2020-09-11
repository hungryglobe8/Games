from grid import *
import test_car as t

def test_init():
    assert Grid(5, 5) is not None

def make_normal_grid():
    return Grid(5, 5)

def test_add_car():
    grid = make_normal_grid()

    grid.add_car(t.basic_car(grid))
    
    assert len(grid.cars) == 1

def test_add_two_horizontal_cars():
    grid = make_normal_grid()

    grid.add_car(t.basic_car(grid, coor=Coordinate(0, 0)))
    grid.add_car(t.basic_car(grid, coor=Coordinate(0, 1)))

    assert len(grid.cars.items) == 2

def test_add_two_cars_occupying_same_space_returns_false():
    grid = make_normal_grid()

    grid.add_car(t.basic_car(grid, orient="horizontal"))
    grid.add_car(t.basic_car(grid, orient="vertical"))
    
    assert len(grid.cars.items) == 1
    assert len(grid.occupied_squares) == 2

