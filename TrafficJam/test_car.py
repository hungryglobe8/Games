from coordinate import Coordinate
from car import VerticalCar, HorizontalCar
from vehicle import VehicleInterface
from grid import Grid
import pytest

def basic_vehicle(grid=Grid(5, 5), coor=Coordinate(0, 0), size=2):
    return VehicleInterface(grid, coor, size)

class TestVehicle:
    def test_init_vehicle(self):
        assert basic_vehicle() is not None
        
    def test_invalid_location(self):
        with pytest.raises(TypeError):
            basic_vehicle(coor=(0, 0))

    def test_invalid_size(self):
        with pytest.raises(ValueError):
            basic_vehicle(size=1)
        with pytest.raises(ValueError):
            basic_vehicle(size=4)

def basic_horizontal_car(grid=Grid(5, 5), coor=Coordinate(0, 0), size=2):
    return HorizontalCar(grid, coor, size)

class TestHorizontalCar:
    def test_init_horizontal_car(self):
        assert basic_horizontal_car() is not None

    def test_type_horizontal_car(self):
        isinstance(basic_horizontal_car, VehicleInterface)

    def test_decrease_pos(self):
        sut = basic_horizontal_car()

        sut.decrease_pos()

        assert sut.coordinates == (Coordinate(-1, 0), Coordinate(0, 0))

    def test_increase_pos(self):
        sut = basic_horizontal_car()

        sut.increase_pos()

        assert sut.coordinates == (Coordinate(1, 0), Coordinate(2, 0))

def basic_vertical_car(grid=Grid(5, 5), coor=Coordinate(0, 0), size=2):
    return VerticalCar(grid, coor, size)

class TestVerticalCar:
    def test_init_vertical_car(self):
        assert basic_vertical_car() is not None

    def test_type_vertical_car(self):
        isinstance(basic_vertical_car, VehicleInterface)

    def test_decrease_pos(self):
        sut = basic_horizontal_car()

        sut.decrease_pos()

        assert sut.coordinates == (Coordinate(0, -1), Coordinate(0, 0))

    def test_increase_pos(self):
        sut = basic_horizontal_car()

        sut.increase_pos()

        assert sut.coordinates == (Coordinate(0, 1), Coordinate(0, 2))

def test_vehicle_collision():
    car1 = basic_horizontal_car()
    car2 = basic_vertical_car()

    assert car1.collides_with(car2)

def test_long_vehicle_collision():
    car1 = basic_horizontal_car(size=3)
    car2 = basic_vertical_car(coor=Coordinate(1, 0), size=3)

    assert car1.collides_with(car2)
