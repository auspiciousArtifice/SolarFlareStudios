# SolarFlareStudios :stars: :video_game:


## Group Members

Ashleigh Paige Ryan, aryan34@gatech.edu, aryan34

Destini Deinde-Smith, dds7@gatech.edu , dds7

Isabelle Steffens, isteffens3@gatech.edu, isteffens3

Harry Wang, hwang703@gatech.edu, hwang703

Ben Le, ben3@gatech.edu, ben3


## Game Overview

The purpose of the game is to reach the end of the level by swinging on asteroids
and dashing through the air. Instructions for gameplay are provided below and
also in an Instructions scene automatically loaded prior to gameplay.

## Installation Requirements 

There are no special requirements, but **it is highly recommended to play this game with a mouse.**

## Gameplay Instructions

### Pausing

Press escape to pause the game at any time. This will provide a refresher of the
game instructions. You can also exit the game or restart your current level from
the pause screen.

### Character Controls

#### Basic Controls

You can move the character using WASD or the arrow keys. To move faster on the
ground while running press 'shift'. Pressing the space bar will allow you to
jump. While in the air, you can dash by right-clicking on your mouse or pressing
alt.

#### Grappling

A core mechanic for moving through this game is swinging on asteroids. There is
an always present crosshair in the center of the screen. Aligning this crosshair
with a "hookable object" like an asteroid will cause the crosshair to focus.

When aiming at a hookable objects, left-click (press m1) to fire the grappling
hook. If the hook is attached to a hookable object, the player will begin being pulled towards the hooked object. Pressing left-click while being pulled will stop the pull. Otherwise, the player will stop being pulled at a certain distance away from the hookable object. While attached to a hookable object with the grappling hook, pressing 'Q'
will lengthen the grapple and 'E' will shorten it. Once connected, you'll need
to click again or press the spacebar to be released by the grappling hook. Dashing (right-click) will also release you from the grappling hook with added force in the direction the camera is facing.

#### Fighting

Another mechanic of the game is to fight AI enemies. You can do this by pressing
1 to switch from grappling mode to fight mode. Once in fight mode, clicking will
allow you to swing your sword and potentially hit an enemy. You can swing your
sword as many times as necessary. Pressing 1 at any time will toggle you back to
grappling mode.

### Health and Lives

You will have 5 lives in this game. You lose a life by falling off a platform
into infinite space, failing to reach the end of the game before the countdown
timer ends, and when your health is depleted.

Health is impacted by AI enemies. When Ai enemies attack you they decrease your
health. When your health is completely depleted you will die and lose a life.

If you die with no lives remaining, then you will lose the entire game.

### Timer

The timer shows the amount of time you have remaining to complete this level
before dying and losing a life.

### Scoring

On the game HUD there is a score indicator. In this section we will describe how
to increase your score throughout the game and bonus points awarded after
winning the game.

#### Collectables and Enemies

The score displayed throughout the game can be increased by picking up coin
collectables and by killing AI enemies. The coins are a safe way to increase
your score and award fewer points than killing an enemy. Be on the lookout for
special collectables to boost your score!

#### End Game Bonuses

At the end of the game, there are two point-based bonuses for winning. The first comes
from the time remaining on the clock. The second is awarded based on the number
of lives left when you win the game.

### Navigating the 3D Platform Environment

There will be multiple ways to reach the end of the level. It is up to you to
figure out how to get there.

As you travel the level, there will be a variety of challenges beyond the basic
challenge of swinging. For example, there are moving platforms, and tight gaps
to swing through. Additionally, the AI enemies will attempt to kill you.

## Gameplay Requirements

### Game Feel

This is a game feel game because we have a clearly defined and achievable goal. We communicate success with coins and a win state. We communicate failure when you die. We immerse the player in a reward based experience with getting points by doing several actions in the world, like collecting coins and damaging/killing enemies. At any point you can pause and exit the game. After winning and losing the game, you are permitted to restart or quit the game.
The game mechanics will feel very natural for someone that has played a game with a keyboard and mouse since it follows the common commands used by such games.

### Precursors to Fun Gameplay 

The player is told their goal at the start of the game. They have multiple choices as to which path they can take to the end of the game, with some paths being faster, and others offering more coins, but also more enemies. They can choose to try to complete the game quickly to get a high score, or try to get a high score by taking time to collect coins and defeat enemies. The game gets more difficult as it progresses to coincide with player learning.

### 3D Character / Vehicle with Real-Time Control

Since our game is a 3D platformer, player control is a large aspect of gameplay. You can move our character with the standard WASD / arrows. You can also jump with spacebar, sprint with left-shift, and dash with right-click (or left alt). Grappling also introduces another level of character control. This control of the character is constant. Even when grappling, the player can use W and S to exert real time control in the direction the camera is facing, akin to swinging on a real rope. Additionally, using Q and E will extend and shorten the grappling hook’s rope respectively. When letting go of the grappling hook, the player’s momentum is maintained throughout the air, and when hooking onto a new object, the player can keep its old momentum upon starting swinging again. We also use root motion on the character’s ground animations and have low latency responsiveness with control inputs. The sprint animations makes use of blend trees on corners in order to minimize hoping between the forward/backward and left/right animations by mirroring the animations accordingly and blending between them. The camera is scripted to follow the character and adjust the player’s path when not aimed directly forward. For instance, on the ground, the player will rotate to face the direction the camera is facing in order to make movement more intuitive.
When the character jumps onto the ground, is damaged, heals, or dies we play sounds to give feedback to the player.

### 3D World with Physics and Spatial Simulation

The player can interact with crate and seesaw puzzle. There’s physics and momentum when swinging from asteroid to asteroid. There’s a red button that will lower a bridge blocking the player’s path. Also, there’s a boundary on the bottom of the level to kill the player if they fall. There are also various moving platforms for the player to get to. Additionally, there’s a moving hookable object that contains a heart.

### Real-time NPC Steering Behaviors / Artificial Intelligence

AI has states such as idle, patrolling, seeking player and fleeing from player. We have moving enemies and static ones. Moving enemies make use of waypoints, they will find the closest one to themselves until it is reached and then patrol to a different one. If the player gets too close, it will seek out the player but go back to patrolling if the player goes too far. Static enemies have the patrol feature not enabled, it will stay still and seek the player if it gets close and then stay in place after.
If the enemy is seeking the player and it gets close enough to the player it will “attempt” to attack it, it will remain in place until the attack animation is done and then try to get closer to the player. The sword of the enemy has a collider that will damage the player on touch given that it is doing the attack animation. 
If the enemy is alive and the health goes below (or equal) to half of the total due to player damaging it, the enemy will flee from the player until a set distance and then go back to patrolling once it is further enough (given it is a moving enemy) or stay in place (given it is a static enemy). While being damaged the enemy will remain in place for the time of the animation. If the enemy’s health goes to zero it will die and it’s body will remain there for the remainder of the game.

### Polish

We have a start menu and pause menu. The pause menu is always accessible and allows users to exit the game. All menus and text use the same consistent font style, which is in line with the space theme of the game. There are also post processing effects to enhance the game experience, such as making the collectible coins glow and adding a menacing atmosphere.
The instructions scene also introduces a level of polish and professionalism. The pause menu restates these instructions. Also, the instruction scenes have polished, blinking text to get the user’s attention.
There is also a mix of diegetic and nondiegetic sounds to immerse the player.

## Known Bugs

If pulled too close to hookable object, then the player loses gravity.
Holding ‘E’ when hooked and too close to the asteroid causes the player to shoot back out.
Colliding with a corner on the ground will cause player movement to bug out and use the airborne animator instead of the ground animator. Dashing upwards to get off the ground and landing again will restore movement. If the player switches to the sword when swinging, they cannot use the grappling mechanics until switching back.

## External Resources Used

### From Unity Asset Store
Metal Textures Pack
AsteroidPack
Gems Ultimate Pack
Katana
Seven Swords
Overwound Mars Pack
Starfield Skybox
CursorsAndCrosshairs

### From Mixamo.com
Characters:
Knight D Pelegrini
Crypto

All animations of main character and enemy.

### From Youtube Audio Library
Various diegetic sounds such as landing on ground and death scream
Background music

### From DaFont.com
Hyper Vyper Font Package (Iconian Fonts)

## Who Did What
**Harry:** Airborn player movement, dashing mechanic, grappling hook mechanics, hookable object modification to work with grappling, grappling visuals (rope, player model rotation in midair, etc), end of level platform and goal (lunar lander placement and animation)

**Isabelle:** Player movement, player health system, initial grappling hook system, player attack system, player’s animator with root motion and blend trees to prevent hoping.  Game manager. AI moving and attacking system, AI health system, AI animator. Some level interaction like moving with the moving platforms. Platform and items animations. Post processing and skybox lighting. Fade in and fade out of level. 

**Ben:** Added 3rd person camera, platforms, made the hookable objects/asteroids, created seesaw crate puzzle, added regaining health with heart collectible, worked on crosshair, worked on having the grapple “lock on” to asteroids when looked at, worked on button bridge system.

**Paige:** Did level design, platforms, created hollow platforms, added some animations, implemented power-up mechanic, portion of combat system, worked on bridge button system

**Destini:** Implemented all game UI including the menu scene, pause menu, HUD, game over, and game win screens. Created the instructions scene and implemented all necessary instructions and logic in this scene. Worked on score and time management, which included creating coin prefabs and implementing a singleton class to keep track of game score. Used event manager system to hook up sounds throughout game. Worked a bit on GameManager to make sure lives wouldn’t become negative and time out triggered player death.

## Scene to Open
Start by opening the Menu Scene.
