from coordinate import Coordinate
from car import Car
from grid import Grid

def test_init():
    assert Car(Grid(5, 5), Coordinate(0, 0), "horizontal", 2) is not None

def test_invalid_location():
    assert Car(Grid(5, 5), (0, 0), "horizontal", 2) == 0