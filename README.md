# Games
This repo is a place for me (Casey Rand) to organize my projects and experiment with rebuilding various puzzle games in different languages.
## Minesweeper
The classic game we all know and love (or is it just me?) with a few additional features that increase playability.
* The first click of any game is guaranteed to be a zero. This starts you off with a small region from which to play rather than just one square. Don't waste it on a corner!
* A "reveal all" button appears after all your flags have been expended. A fast way to end the game... one way or another.
* Stats that prevail across runs showing percentage and can be reset.

## Sudoku
This sudoku app can generate fully complete sudoku grids according to a variety of rules and sizes.
It can also be used to solve partially filled in puzzles relatively quickly.
Unique features:
* Smart fill-in system, moving selection automatically to the next open square when valid.
* Highlights inconsistencies in grid as soon as they invalidate previous numbers. Also highlights the other number until the conflict is resolved.
* Generate or solve X Sudoku puzzles (only reasonable for sizes < 10)

Future goals:
* Remove numbers from generated puzzles, maintaining human solvability for easy, medium, and hard difficulties.
* Speed up X Sudoku generation.
* Come up with new Sudoku modes.
