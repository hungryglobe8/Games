import Coordinate

def test_init():
    assert Coordinate.Coordinate(0, 0) is not None

def test_zero_by_zero():
    sut = Coordinate.Coordinate(0, 0)
    assert sut.x == 0 and sut.y == 0