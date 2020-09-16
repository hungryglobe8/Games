from coordinate import Coordinate
from grid import Grid
import test_car as t
import pytest

def test_init():
    assert Grid(5, 5) is not None

def make_normal_grid():
    return Grid(5, 5)

# @pytest.mark.parametrize("grid_size, expected_result", [(8, True), (6, False), (10, True)])
# def test_within_grid(grid_size, expected_result):
#     grid = Grid(grid_size, grid_size)

#     assert Coordinate(7,5).is_within(grid_size, grid_size) == expected_result

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

def test_add_exit():
    grid = make_normal_grid()

    grid.add_exit(Coordinate(-1, 0))

    assert grid.exit is not None

def test_win_game():
    grid = make_normal_grid()
    selected_car = t.basic_horizontal_car(grid)
    grid.add_car(selected_car)
    grid.add_exit(Coordinate(-1, 0))

    grid.drag_vehicle(Coordinate(-1, 0), selected_car)

    assert grid.game_over
