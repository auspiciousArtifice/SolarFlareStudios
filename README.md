# SolarFlareStudios

The purpose of the game is to reach the end of the level by swining on asteroids
and dashing through the air. Instructions for gameplay are provided below and
also in an Instructions scene automoatically loaded prior to gameplay.

It is highly recommmended to play this game with a mouse.

## Pausing

Press escape to pause the game at any time. This will provide a refresher of the
game instructions. You can also exit the game or restart your current level from
the pause screen.

## Character Controls

### Basic Controls

You can move the character using WASD or the arrow keys. To move faster on the
ground while running press 'shift'. Pressing the space bar will allow you to
jump. While in the air, you can dash by right-clicking on your mouse or pressing
alt.

### Grappling

A core mechanic for moving through this game is swinging on asteriods. There is
an always present crosshair in the center of the screen. Alighing this crosshair
with a "hookable object" like an asteroid will cause the crosshair to focus.

When aiming at a hookable objects, click (or press m1) to fire the grappling
hook. While attached to a hookable object with the grappling hook, pressing 'Q'
will lengthen the grapple and 'E' will shorten it. Once connected, you'll need
to click again to be released by the grappling hook. Dashing (right-click) will
also release you from the grappling hook with added force.

### Fighing

Another mechanic of the game is to fight AI enemies. You can do this by pressing
1 to switch from grappling mode to fight mode. Once in fight mode, clicking will
allow you to swing your sword and potentially hit an enemy. You can swing your
sword as many times as necessary. Pressing 1 at any time will toggle you back to
grappling mode.

## Health and Lives

You will have 5 lives in this game. You lose a life by falling off a platform
into infinite space, failing to reach the end of the game before the countdown
timer ends, and when your health is depleted.

Health is impacted by AI enemies. When Ai enemies attack you they decrease your
health. When your health is completely depleted you will die and lose a life.

If you die with no lives remaining, then you will lose the entire game.

## Timer

The timer shows the amount of time you have remaining to complete this level
before dying and losing a life.

## Scoring

On the game HUD there is a score indicator. In this section we will describe how
to increase your score throughout the game and bonus points awarded after
winning the game.

### Collectables and Enemies

The score displayed throughout the game can be increased by picking up coin
collectables and by killing AI enemies. The coins are a safe way to increase
your score and award fewer points than killing an enemy. Be on the look out for
special collectables to boost your score!

### End Game Bonuses

At the end of the game, there are two point bonuses for winning. The first comes
from the time remaining on the clock. The second is awarded based on the number
of lives left when you win the game.


## Navigating the 3D Platform Environment

There will be multiple ways to reach the end of the level. It is up to you to
figure out how to get there.

As you travel the level, there will be variety of challenges beyond the basic
challenge of swinging. For example, there are moving platforms, and tight gaps
to swing through. Additionally, the AI enemies will attempt to kill you.



The game also contains a singleton for the game manager that controls the game
state and persistence of things on the screen, such as the amount of lives.



## Known Bugs << update!! >>
If pulled to close to hookable object, then the player loses gravity.
After releasing the grappling hook, the player only retain velocity in the y direction.
Currently, restarting the game does not work.
