import view
from grid import Grid
'''
Game editor allows users to make their own traffic jam games,
test them out, make random boards, or save and load grids.
'''
# Game window size.
size = (700, 700)
# Set up game.
grid = Grid(5, 5)

view.game(size, grid, show_buttons=True)