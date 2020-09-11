from coordinate import Coordinate
from car import Car
from grid import Grid
import pytest

def basic_car(grid=Grid(5, 5), coor=Coordinate(0, 0), orient="horizontal", size=2):
    return Car(grid, coor, orient, size)

def test_init():
    assert basic_car() is not None

# grid test
# def test_invalid_grid_size():
#     with pytest.raises(ValueError):
#         assert basic_car(grid = Grid(-1, 5))

def test_invalid_location():
    with pytest.raises(TypeError):
        basic_car(coor=(0, 0))

def test_invalid_orientation():
    with pytest.raises(ValueError):
        basic_car(orient="horiz")

def test_invalid_size():
    with pytest.raises(ValueError):
        basic_car(size=1)
    with pytest.raises(ValueError):
        basic_car(size=4)

def test_move_left():
    sut = basic_car()

    sut.move_left()

    assert sut.coordinates == [Coordinate(-1, 0), Coordinate(0, 0)]

def test_move_right():
    sut = basic_car()

    sut.move_right()

    assert sut.coordinates == [Coordinate(1, 0), Coordinate(2, 0)]

def test_move_up():
    sut = basic_car(orient="vertical")

    sut.move_up()

    assert sut.coordinates == [Coordinate(0, -1), Coordinate(0, 0)]

def test_move_down():
    sut = basic_car(orient="vertical")

    sut.move_down()

    assert sut.coordinates == [Coordinate(0, 1), Coordinate(0, 2)]

def test_cannot_move_horizontal_car_up_or_down():
    sut = basic_car(orient="horizontal")
    expected_coors = [Coordinate(0, 0), Coordinate(1, 0)]

    sut.move_up()
    assert sut.coordinates == expected_coors
    sut.move_down()
    assert sut.coordinates == expected_coors

def test_cannot_move_vertical_car_left_or_right():
    sut = basic_car(orient="vertical")
    expected_coors = [Coordinate(0, 0), Coordinate(0, 1)]

    sut.move_left()
    assert sut.coordinates == expected_coors
    sut.move_right()
    assert sut.coordinates == expected_coors