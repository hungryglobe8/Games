from coordinate import Coordinate
import pytest

def make_zero_coordinate():
    return Coordinate(0, 0)

def test_init():
    assert Coordinate(0, 0) is not None

def test_zero_by_zero():
    coor = make_zero_coordinate()
    assert coor.x == 0 and coor.y == 0

def test_coordinate_to_string():
    coor = make_zero_coordinate()
    assert str(coor) == "(0, 0)"

def test_coordinate_equality():
    coor1 = make_zero_coordinate()
    coor2 = make_zero_coordinate()
    assert coor1 == coor2

def test_coordinate_inequality():
    coor1 = Coordinate(5, 5)
    coor2 = Coordinate(3, 3)
    assert coor1 != coor2

@pytest.mark.parametrize("test_coor", [Coordinate(-1, 0), Coordinate(0, -1), Coordinate(6, 5), Coordinate(5, 6)])
def test_coordinate_within_range(test_coor):
    assert not test_coor.within_range(0, 0, 5, 5)

def test_coordinate_within_coordinate_list():
    coor_list = make_zero_coordinate().extend_right(3)

    assert Coordinate(0, 0) in coor_list

def test_extend_horizontal():
    coor = make_zero_coordinate()

    coors = coor.extend_right(2)

    assert coors == [Coordinate(0, 0), Coordinate(1, 0)]

def test_extend_vertical():
    coor = make_zero_coordinate()

    coors = coor.extend_down(3)

    assert coors == [Coordinate(0, 0), Coordinate(0, 1), Coordinate(0, 2)]

def test_shift_left():
    coor = make_zero_coordinate()

    coor.shift_left()

    assert coor == Coordinate(-1, 0)

def test_shift_right():
    coor = make_zero_coordinate()

    coor.shift_right()

    assert coor == Coordinate(1, 0)

def test_shift_up():
    coor = make_zero_coordinate()

    coor.shift_up()

    assert coor == Coordinate(0, -1)

def test_shift_down():
    coor = make_zero_coordinate()

    coor.shift_down()

    assert coor == Coordinate(0, 1)

def test_shift_group_of_coordinates():
    coor = make_zero_coordinate()
    coors = coor.extend_down(3)

    for coor in coors:
        coor.shift_down()

    assert coors == [Coordinate(0, 1), Coordinate(0, 2), Coordinate(0, 3)]