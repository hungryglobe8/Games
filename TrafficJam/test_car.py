from car import VerticalCar, HorizontalCar
from vehicle import Vehicle
from grid import Grid
from coordinate import Coordinate
import pytest

def basic_vehicle(grid=Grid(5, 5), coordinates=[Coordinate(0, 0), Coordinate(1, 1)]):
    return Vehicle(grid, coordinates)

class TestVehicle:
    def test_init_vehicle(self):
        assert basic_vehicle() is not None
        
    def test_invalid_location(self):
        with pytest.raises(TypeError):
            basic_vehicle(coordinates=(0, 0))

    def test_invalid_size(self):
        short_list = [Coordinate(0, 0)]
        long_list = [Coordinate(0, 0), Coordinate(1, 1), Coordinate(2, 2), Coordinate(3, 3)]
        
        with pytest.raises(ValueError):
            basic_vehicle(coordinates=short_list)
        with pytest.raises(ValueError):
            basic_vehicle(coordinates=long_list)

    def test_vehicle_collision_on_same_grid(self):
        grid = Grid(5, 5)
        vehicle1 = basic_vehicle(grid)
        vehicle2 = Vehicle(grid, [Coordinate(1, 1), Coordinate(1, 2)])

        assert vehicle1.collides_with(vehicle2)
        assert vehicle2.collides_with(vehicle1)

    def test_vehicle_collision_from_diff_grids(self):
        vehicle1 = basic_vehicle()
        vehicle2 = Vehicle(Grid(10, 10), [Coordinate(1, 1), Coordinate(1, 2)])

        assert vehicle1.collides_with(vehicle2)
        assert vehicle2.collides_with(vehicle1)

    @pytest.mark.parametrize("test_coors", [[Coordinate(-1, 0), Coordinate(0, 0)], [Coordinate(0, -1), Coordinate(0, 0)], [Coordinate(6, 5), Coordinate(5, 5)], [Coordinate(5, 5), Coordinate(5, 6)]])
    def test_vehicle_outside_grid(self, test_coors):
        vehicle1 = basic_vehicle(coordinates=test_coors)

        assert not vehicle1.is_within_grid()

def basic_horizontal_car(grid=Grid(5, 5), coor=Coordinate(0, 0), size=2):
    return HorizontalCar(grid, coor, size)

class TestHorizontalCar:
    def test_init_horizontal_car(self):
        assert basic_horizontal_car() is not None

    def test_type_horizontal_car(self):
        isinstance(basic_horizontal_car, Vehicle)

    def test_horizontal_car_coors(self):
        exp = basic_horizontal_car().coordinates
        assert exp == [Coordinate(0, 0), Coordinate(1, 0)]

    def test_decrease_pos(self):
        sut = basic_horizontal_car()

        sut.decrease_pos()

        assert sut.coordinates == [Coordinate(-1, 0), Coordinate(0, 0)]

    def test_increase_pos(self):
        sut = basic_horizontal_car()

        sut.increase_pos()

        assert sut.coordinates == [Coordinate(1, 0), Coordinate(2, 0)]

def basic_vertical_car(grid=Grid(5, 5), coor=Coordinate(0, 0), size=2):
    return VerticalCar(grid, coor, size)

class TestVerticalCar:
    def test_init_vertical_car(self):
        assert basic_vertical_car() is not None

    def test_type_vertical_car(self):
        isinstance(basic_vertical_car, Vehicle)

    def test_vertical_car_coors(self):
        exp = basic_vertical_car().coordinates
        assert exp == [Coordinate(0, 0), Coordinate(0, 1)]

    def test_decrease_pos(self):
        sut = basic_vertical_car()

        sut.decrease_pos()
        
        assert sut.coordinates == [Coordinate(0, -1), Coordinate(0, 0)]

    def test_increase_pos(self):
        sut = basic_vertical_car()
        
        sut.increase_pos()
        
        assert sut.coordinates == [Coordinate(0, 1), Coordinate(0, 2)]

def test_vehicle_collision():
    car1 = basic_horizontal_car()
    car2 = basic_vertical_car()

    assert car1.collides_with(car2)

def test_long_vehicle_collision():
    car1 = basic_horizontal_car(size=3)
    car2 = basic_vertical_car(size=3)

    assert car1.collides_with(car2)