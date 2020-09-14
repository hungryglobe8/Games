import pygame
import random
from car import HorizontalCar, VerticalCar
pygame.init()
BLACK    = (   0,   0,   0)
WHITE    = ( 255, 255, 255)
# light shade of the button 
COLOR_LIGHT = (170,170,170)  
# dark shade of the button 
COLOR_DARK = (100,100,100)

# Wrap all button data in a class.
class Button():
    def __init__(self, x, y, w, h, text, colour=None):
        if colour is None:
            colour = COLOR_LIGHT

        self.normal_colour = colour
        self.x = x
        self.y = y
        self.w = w
        self.h = h
        self.font = pygame.font.SysFont('arial', 20)
        self.text = text
        self.shiny = False

    def draw(self, screen):
        if self.shiny:
            bg = COLOR_DARK
        else:
            bg = self.normal_colour

        surf = self.font.render(self.text, True, BLACK, bg)
        rect = (self.x, self.y, self.w, self.h)
        xo = self.x + (self.w - surf.get_width()) // 2
        yo = self.y + (self.h - surf.get_height()) // 2
        screen.fill(bg, rect)
        screen.blit(surf, (xo, yo))

    def on_button(self, pos):
        return self.x <= pos[0] and self.x + self.w > pos[0] and \
               self.y <= pos[1] and self.y + self.h > pos[1]

def draw_toggle(b_list, button1, screen):
    if button1 not in b_list:
        return

    index = b_list.index(button1)
    button2 = b_list[(index + 1) % 2]
    if not button1.shiny:
        button1.shiny = True
        button2.shiny = False

# # Make list of buttons in the program
size = (700, 700)
center = size[0] / 4
bottom = size[1] - 130
make_car_button = Button(center, bottom, 100, 50, "New Car")
bottom += 60
horizontal_button = Button(center - 60, bottom, 80, 25, "horizontal")
vertical_button = Button(center + 80, bottom, 80, 25, "vertical")
size_two_button = Button(center - 60, bottom + 35, 80, 25, "2")
size_three_button = Button(center + 80, bottom + 35, 80, 25, "3")
horizontal_button.shiny = True
size_two_button.shiny = True

buttons= [make_car_button, horizontal_button, vertical_button, size_two_button, size_three_button]
car_orientation_buttons = (horizontal_button, vertical_button)
size_buttons = (size_two_button, size_three_button)

# Mouse click handler
def update_click(mouse_pos, click, screen):
    """
    Mouse got clicked
    Returns whether a change occurred.
    """
    print("Button clicked from button")
    global disable_select
    for button in buttons:
        if button.on_button(mouse_pos):
            # make_car_button
            if button is make_car_button:
                button.shiny = click
                button.normal_colour = random_color()
                # activate car thingy
                car_size = 0
                if size_two_button.shiny:
                    car_size = 2
                else:
                    car_size = 3
                if horizontal_button.shiny:
                    # Hard with coordinate as part of constructor... When to fix?
                    # return HorizontalCar(grid, car_size, make_car_button.normal_colour)
                    return (make_car_button.normal_colour, "horizontal", car_size)
                elif vertical_button.shiny:
                    return (make_car_button.normal_colour, "vertical", car_size)
            # toggle buttons
            draw_toggle(car_orientation_buttons, button, screen)
            draw_toggle(size_buttons, button, screen)

def random_color():
    return (random.randint(0, 255), random.randint(0, 255), random.randint(0, 255))