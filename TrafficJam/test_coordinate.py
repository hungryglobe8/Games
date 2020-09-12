from coordinate import Coordinate

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

def test_coordinate_within_coordinate_list():
    coor_list = make_zero_coordinate().extend(3, "horizontal")

    assert Coordinate(0, 0) in coor_list

def test_any_coordinate_from_list_within_other_coordinate_list():
    coor_list = make_zero_coordinate().extend(3, "horizontal")
    other_list = make_zero_coordinate().extend_down(2)

    assert Coordinate.shared_coordinate(coor_list, other_list)

def test_extend_horizontal():
    coor = make_zero_coordinate()

    coors = coor.extend(2, "horizontal")

    assert coors == [Coordinate(0, 0), Coordinate(1, 0)]

def test_extend_vertical():
    coor = make_zero_coordinate()

    coors = coor.extend_down(3)

    assert coors == (Coordinate(0, 0), Coordinate(0, 1), Coordinate(0, 2))

def test_get_left_coordinate():
    coor = make_zero_coordinate()

    assert coor.left() == Coordinate(-1, 0)

def test_get_right_coordinate():
    coor = make_zero_coordinate()

    assert coor.right() == Coordinate(1, 0)

def test_get_up_coordinate():
    coor = make_zero_coordinate()

    assert coor.up() == Coordinate(0, -1)

def test_get_down_coordinate():
    coor = make_zero_coordinate()

    assert coor.down() == Coordinate(0, 1)