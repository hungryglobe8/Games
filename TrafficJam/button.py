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

class CarInfo():
    def __init__(self, car_type, color, size):
        self.type = car_type
        self.color = color
        self.size = size

class ToggleButton():
    '''
    A toggle button defines the relationship between groups of buttons where only one can be selected at a time.
    Clicking one toggle button will select it and remove focus from its fellow toggles.

    initial_selection - the initial button from the toggles to be shiny (active)
    button_dict - a dictionary of all buttons and the corresponding options that they connect to.
    '''
    def __init__(self, *args):
        self.buttons = args
        self.selected = args[0]
        self.selected.shiny = True

    def which_toggled(self):
        return self.selected

    def draw(self, screen):
        ''' Draw both buttons. '''
        for button in self.buttons:
            button.draw(screen)

    def on_button(self, pos):
        ''' Check whether a given position is on either button. '''
        return any(button.on_button(pos) for button in self.buttons)

    def toggle(self, pos):
        ''' Click one of the buttons in toggle button. Deactivate the others. '''
        for button in self.buttons:
            if button.on_button(pos):
                self.selected = button
                button.shiny = True
            else:
                button.shiny = False

# Wrap all button data in a class.
class Button():
    def __init__(self, x, y, w, h, text, colour=None, info=None):
        if colour is None:
            colour = COLOR_LIGHT

        self.normal_colour = colour
        self.x = x
        self.y = y
        self.w = w
        self.h = h
        self.font = pygame.font.SysFont('arial', 20)
        self.text = text
        self.info = info
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
        
state = HorizontalCar

# # Make list of buttons in the program
size = (700, 700)
center = size[0] / 4
bottom = size[1] - 130
make_car_button = Button(center, bottom, 100, 50, "New Car")
bottom += 60
horizontal_button = Button(center - 60, bottom, 80, 25, "horizontal", info=HorizontalCar)
vertical_button = Button(center + 80, bottom, 80, 25, "vertical", info=VerticalCar)
toggle1 = ToggleButton(horizontal_button, vertical_button)
size_two_button = Button(center - 60, bottom + 35, 80, 25, "2", info=2)
size_three_button = Button(center + 80, bottom + 35, 80, 25, "3", info=3)
toggle2 = ToggleButton(size_two_button, size_three_button)

buttons= [make_car_button, toggle1, toggle2]

# Mouse click handler
def update_click(mouse_pos, click):
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
                # Get car options.
                orientation = toggle1.which_toggled().info
                size = toggle2.which_toggled().info
                return CarInfo(orientation, make_car_button.normal_colour, size)
            # toggle buttons
            else:
                button.toggle(mouse_pos)

def random_color():
    return (random.randint(0, 255), random.randint(0, 255), random.randint(0, 255))