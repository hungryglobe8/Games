from coordinate import Coordinate

def make_zero_coordinate():
    return Coordinate(0, 0)

def test_init():
    assert Coordinate(0, 0) is not None

def test_zero_by_zero():
    coor = make_zero_coordinate()
    assert coor.x == 0 and coor.y == 0

def test_string():
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

def test_coordinate_within_coordinate_list():
    coor_list = make_zero_coordinate().extend(3, "horizontal")

    assert Coordinate(0, 0) in coor_list

def test_any_coordinate_within_coordinate_list():
    coor_list = make_zero_coordinate().extend(3, "horizontal")
    other_list = make_zero_coordinate().extend(2, "vertical")

    assert Coordinate.shared_coordinate(coor_list, other_list)

def test_extend_horizontal():
    coor = make_zero_coordinate()
    coors = coor.extend(2, "horizontal")
    assert coors == [Coordinate(0, 0), Coordinate(1, 0)]

def test_extend_vertical():
    coor = make_zero_coordinate()
    coors = coor.extend(3, "vertical")
    assert coors == [Coordinate(0, 0), Coordinate(0, 1), Coordinate(0, 2)]


