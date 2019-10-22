# SolarFlareStudios

The purpose of the game is to swing towards the end of the level. You can do this by aiming at the floating, white rectangular prisims. These will be named "hookable objects". While aiming at a hookable objects, press mouse 1 (m1) to fire the grappling hook. Once connected, you'll need to press m1 again to stop being pulled. Pressing 'W' and 'S' will allow you to swing forward and backward while hooked. If you press m1, you will be released. Additionally, if you press mouse 2, you will dash forward. Pressing shift will allow to sprint.

The game also contains a singleton for the game manager that controls the game state and persistence of things on the screen, such as the amount of lives between levels.

Pressing escape will pause the game. For now, pressing backspace will show a win screen.

There will be multiple ways to reach the end of the level. It is up to you to figure out how to get there. There is a timer, so the longer you take, the worse performance will be. Additionaly, there are coins to collect that will help boost your score.

As you travel the level, there will be variety of challenges beyond the basic challenge of swinging. For example, there are moving platforms, and tight gaps to swing through. Additionally, there are enemies that will attempt to kill you.

For the alpha, pressing 'H' will change the scene so the player is next to an AI enemy.

There are some known bugs with the alpha.
If pulled to close to hookable object, then the player loses gravity.
After releasing the grappling hook, the player only retain velocity in the y direction.
Currently, restarting the game and respawning does not work.
