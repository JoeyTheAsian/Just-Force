# Just Force
<b>10:28 am 2-1-16: Joey Tong</b> <ul><li>Created repository</li></ul>

<b>12:24 pm 2-5-16: Joey Tong</b> <ul><li>Game skeleton, Rendering sprite, basic WASD movement</li></ul>

<b>4:27 am 2-6-16: Joey Tong</b> <ul><li> Implemented FPS tracking, Entity class</li>
<li> added Character & Projectile Entity children classes</li>
<li> added Coord (coordinate) class and Map class</li>
<li> Created Developer's Console (windows console)</li>
<li> Added "NoTexture.png" texture and loaded to content</li>
<li> Implemented autosize window</li>
<li> Set game refresh rate to 120hz</li>
<li> Removed placeholder "sm.png" texture</li></ul>


<b>10:55PM 2-6-16: Joey Tong</b> <ul><li>Fixed FPS tracking issue where it was tracking game logic refresh rate instead of fps</li>
<li> added "ConcreteTexture.png" and loaded asset to content</li>
<li> partially implemented Map class and sloppily rendered a bitmap with no objects onto the game window</li>
<li> continued implementation of Entity child classes</li>
<li> Stress tested game rendering, maxes out at about 500 entities on screen while maintaining constant 55 fps and 120 hz game refresh rate</li></ul>


<b>3:59 pm 2-7-16 Anthony Olivares</b>
<ul><li>Added a story section to the design document <b>Issue: File was not pushed to repo</b></li></ul>


<b>7:30pm 2-7-16: Joey Tong</b><ul><li>fixed implementation of map class to be more useful and changed map rendering pattern</li>
<li>added and updated several tile textures: Concrete.png, Asphalt.png, ConcreteCorner.png, ConcreteEdge,png, LaneLine.png</li></ul>


<b>11:20 am 2-8-16: Joey Tong</b>
<ul><li>Implemented Map scrolling (during class in lab)</li></ul>


<b>12:30 am 2-9-16: Joey Tong</b>
<ul><li>Fixed optimization issues with rendering TileMap and implemented ambient occlusion</li>
<li>Stress tested RAM capacity, game manages to load 10k x 10k TileMap and render with no issues. This means ambient occlusion is working properly. This also means, don't make maps more than 10k x 10k tiles. Though that should be obvious</li>


<b>10:07 pm 2-9-16: Austin Ford</b>
<ul><li>Added sprite font and a list for holding sound effects for the time being</li>
<li>Press "x" to hear the sound effect, right now it is just a gunshot</li>
<li>Added two spritefonts so far, both arial for the time being, one in size 14 and the other in size 20</li>
<li>The text "Testing text" appears at point (0,0) and remains there as the map moves</li>
<li>Also changed the key input to allow for keypress and for when the keys are held down, the sound and escape are keypress while movement is still keydown</li></ul>
<b>12:57 pm 2-10-16: Joey Tong</b>
<ul><li>Moved implementation of ambient occlusion calculations to separate class called "TileBounds.cs"</li>
<li>Changed spriteFont render on top left to show current FPS</li>
<li>Changed default background color from CornFlowerBlue to Black</li></ul>


<b>11:11 pm 2-10-2016: Connor Cummings</b>
<ul><li>Implemented Sprint Button (Press and hold Shift)</li>
<li>Created MaxVelocity variable to use for sprint and normal movment acceleration </li></ul>


<b>8:08 pm 2-10-16: Austin Ford</b>
<ul><li>Added to rotation to the player</li>
<li>Added two new variables, one for origin, one for rotation</li>
<li>Changed the spritebatch draw to alllow for rotation</li></ul>


<b>6:49 pm 2-10-16: Joey Tong</b>
<ul><li>Added curSounds queue and modified implementation of sound effects to be able to play any number of sounds that are enqueued</li>
<li>changed sprite entity to an entity queue that contains all the pending entities to be rendered</li>
<li>created a "Character" object called player which now holds all of the player data</li>
<li>changed activation button for sound effect to LMB</li>
<li>added "ParentConvertor" that has methods that take a child of entity and convert it to an entity object for rendering</li>
<li>added Shoot method to character that creates a new projectile</li></ul>


<b>7:43 pm 2-11-16 Anthony Olivares</b>
<ul><li>Actual changes to design doc. Added names of locations and character for story, level ideas, enemy types, marked items I feel are required for the game</li></ul>


<b>11:04 pm 2-11-16 Anthony Olivares</b>
<ul><li>Contents of design doc migrated to design.txt, contents revised in terms of story and level design.</li></ul>


<b>11:12 am 2-12-16 Anthony Olivares</b>
<ul><li>Updated design.txt with a section for assets we would need for confirmed levels as well as those that would be needed for other levels, added game name section</li></ul>


<b>5:43pm 2/12/16 Connor Cummings</b>
<ul><li>Acceleration has been implemented</li> 
<strike><li>ISSUE: the acceleration jumps</li> </strike>
<li>Gamestates and start menu are operation and started on loading screen</li></ul>


<b>6:30 pm 2-12-16: Joey Tong</b>
<ul><li>Fixed acceleration code</li>
<li>migrated acceleration to external class Movement.cs</li>
</ul>


<b>1:03 pm 2-14-16: Austin Ford</b>
<ul><li>Fixed mouse rotation and recentered the player</li>
<li>Added a basic console class with a few commands</li></ul>


<b>1:11 pm 2-17-16 Anthony Olivares</b>
<li>Updated design.txt with a rough draft for game exposition/intro dialogue.</li>


<b>11:41 am 2-19-16: Joey Tong</b>
<ul><li>Reimplemented sprint feature (lshift)</li>
<li>Fixed implementation of rotation</li>
<li>Implemented shooting, currently creates bullet on the player in the same direction as the player</li>
<li>Moved player position calculations to external static class called playerPos</li>
<li>Added Bullet.png asset </li>
<li>Added a projectiles list that contains all the projectiles on screen.</li>
<li>Added a loop that renders all projectiles to the screen</li></ul>


<b>12:15 pm 2-19-16: Joey Tong</b>
<ul><li>Created MapEditor windows form</li>
<li>added range value to projectile object</li></ul>


<b>12:53 pm 2-19-16 Anthony Olivares</b>
<ul><li>Updated design.txt with some important updates. Realized I forgot to move enemy types from the .docx over, so did that. Also updated skills further, and have now added stats for firerrate (in frames) and damage (numeric values) of all must-add weapons. Also added Health stat values for player and enemies, and stamina stat (max/recharge rate/usage speed) for player.</li></ul>


<b>1:51 pm 2-19-16: Joey Tong</b>
<ul><li>implemented bullet animations/rendering/ velocity/ range, etc.</li>
<li> Adjusted movement speed</li>
<li> fixed implementation of sprint (again)</li>
<li>Fixed implementation of bullet rotation</li></ul>


<b> 5:01 pm 2/19/2016: Connor Cummings</b>
<ul><li>Implemented a pause screen. It's just a blue screen activated by pressing P in game and exits the pause screen by pressing P again</li>
<li>Created a loading screen that appears for 1.5 seconds before game starts</li></ul> 


<b>8:04 pm 2-19-16 Anthony Olivares</b>
<ul><li>Fire rates in design.txt are now in seconds, as is stamina rates</li>
<li>Fixed up opening narration a bit</li>
<li>Added color and appearence ideas for enemies</li></ul>


<b>5:10 pm 2-20-16: Joey Tong</b>
<ul><li>Implemented Screen shake by adding a pixel offset to all rendered objects</li>
<li>Screen shakes when firing a projectile</li>
<li>Coded a new class, "Weapon.cs". Will handle all weapon related functionality</li></ul>


<b>1:00 pm 2-21-16 Joey Tong & Connor Cummings</b>
<ul><li>Fixed and moved implementation of gamestates to external class "GameStateManager"</li>
<li>Fixed bug in FPSHandler that caused inaccurate readings of fps</li></ul>


<b>8:09 pm 2-24-16: Austin Ford</b>
<ul><li>Fixed basic collision bug</li></ul>


<b>3:02 pm 2/25/2016: Connor Cummings</b>
<ul><li>added temporary assets for HUD (health, stamina, ammo)</li></ul>


<b>12:19 am 2-27-16: Joey Tong</b>
<ul><li>Moved ScreenShake code and all camera related variables to "Camera.cs"</li>
<li>Implemented bullet spread, pistol spread set to 10 degrees</li>
<li>created weapon object housed in character class</li>
<li>Added HUD component that displays current weapon type and name and drew it to the screen</li>
<li>moved and renamed global coord to Camera class as "camPos"</li>
<li>added silhouette of pistol (HUD texture) "Pistol.png"</li></ul>


<b>6:56 pm 2-28-16 Anthony Olivares</b>
<ul><li>Added FileHandling.cs to the map editor. Bare-bones and placeholder code for saving and reading in a map to/from .dat files.</li></ul>


<b>12:19 am 2-27-16: Joey Tong</b>
<ul><li>Added new textures for Health, pistol silhouette, start menu background, start menu buttons, bullet</li>
<li>Fixed implementation of health bar to actually display current player health</li>
<li>Fixed implementation of pausing to actually stop game loop</li>
<li>Fixed implementation of drawing HP bar & menu buttons to be rescalable</li></ul>
