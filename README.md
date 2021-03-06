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
<li>Stress tested RAM capacity, game manages to load 10k x 10k TileMap and render with no issues. This means ambient occlusion is working properly. This also means, don't make maps more than 10k x 10k tiles. Though that should be obvious</li></ul>


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

<b>12:34 am 2-29-16: Austin Ford</b>
<ul><li>Changed collision to use rectangle intersect methods</li>
<li>Added rectangle property to all entities and their constructors</li>
<li>Add checkCollide method and coordinates to map objects</li></ul>

<b>12:19 am 3-1-16: Joey Tong</b>
<ul><li>Implemented weapon fire rate</li>
<li>Implemented reloading and ammo HUD elements</li>
<li>Changed implementation of entity drawing to enqueue all entity children to a sprites class for drawing</li>
<li>Added emptyClick sound, sound of the gun when firing empty</li>
<li>Moved Shoot() method from character to weapon</li>
<li>Added various methods and variables to weapon class for fire rate & reload functionality</li>
</ul>

<b> 4:48 pm 3/3/2016: Connor Cummings</b>
<ul><li>added panel, scrollbars, tile selector, and user input boxes for map editor</li>
<li>added textures to tile selection buttons</li>
<li>created event handlers for</li>
<li>	inputting grid and tile size</li>
<li>	painting grid to panel</li>
<li>	painting textures to button</li>
<li>started events for</li>
<li>	scrollbars</li>
<li>	saving texture to cursor when tile selection is made</li></ul>

<b> 3:00 pm 3/4/2016: Anthony Olivares</b>
<ul><li>Updated map editor with methods to read and write data to a .dat</li>
<li>Updated game map classes with a constructor that reads from a file created by the map editor</li></ul>

<b> 5:21 pm 3/4/2016: Connor Cummings</b>
<ul><li> added further methods for selecting tiles yet it still doesn't work</li>
<li> created method for getting box the mouse is clicking in the picturebox</li>
<li>needs to fixing to get grid, textures and selectoring working correclty</li></ul>

<b> 10:43 am 3/7/2016: Connor Cummings</b>
<ul><li>added FormatExceptions for input boxes for rows, columns, height, width for in map editor</li></ul>

<b> 6:17 pm 3/9/2016: Connor Cummings</b>
<ul><li>changed the look of the pause screen and added an exit button</li>
<li>added a Quit method after Draw in Game1 to exit game from other classes. But its not implemented anywhere</li></ul>

<b> 2:38 pm 3/11/2016: Connor Cummings</b>
<ul><li>fixed code for Exit buttons. Exit buttons on start and pause menu now exit program</li></ul>

<b> 3:52 pm 3/11/2016: Anthony Olivares</b>
<ul><li>added code so that the map editor creates a 2d string array version of the map that contains texture names in the same locations as the textures are stored. This array is read from to create the file that is saved by it.</li>
<li>The load map button has also had its method altered to function this way - it reads in the strings from the .dat file, puts them back into the string array, and then loads the bitmaps with those names and puts them in the editor's map.</li></ul>

<b> 4:00 pm 3/11/16: Joey Tong</b>
<ul><li>Fixed issue regarding diagonal movement being faster</li>
<li>Added tommy gun (submachinegun) UI texture</li>
</ul>

<b> 3:21 am 3/13/16: Joey Tong</b>
<ul><li>General Code cleanup, moved HUD drawing procedure to external class</li>
<li>Added comments where needed</li>
<li>Removed unused values and calculations in camera class</li>
<li>added drawing procedure for objectMap</li>
</ul>

<b> 10:45 am 3/14/2016: Connor Cummings</b>
<ul><li>added curype to Mapeditor to choose between textures and objects</li>
<li>removed classes that weren't needed</li>
<li>added one gameobject for now</li>
<li>gave game objects their own table</li></ul>

<b> 10:44 am 3/14/16: Anthony Olivares </b>
<ul><li>Commented the file io code in the map editor</li>
<li>Fixed initialization of string arrays used in file io in the map editor</li>
<li>Map editor saving method can be optimised better, will clean up the code more later</li></ul>


<b> 11:48 pm 3/15/16: Joey Tong</b>
<ul><li>Added full compatibility with different resolution screens</li>
<li>Fixed tommy gun weapon indicator texture</li>
</ul>

<b> 9:22 pm 3/16/16: Joey Tong</b>
<ul><li>Fixed optimization and data handling issues with map editor</li>
</ul>

<b> 2:24 pm 3/17/16: Austin Ford</b>
<ul><li>Created a static class for creating, shooting and switching the player's weapon</li>
<li>Added sub classes to character for Enemies and Riot Enemies</li>
<li>Created a static class for creating Enemies</li>
<li>Fixed collison against map objects</li>
<li>Added a new constructor and fillAmmo method to the weapon class</li>
<li>Added the Tommy Gun</li>
<li>Changed game console class and added commands for it</li>
</ul>

<b> 11:33 pm 3/22/16: Joey Tong</b>
<ul><li>Added player textures "Pistol_Player" and "Submachine_Gun_Player"</li>
<li>Added UI textures for pause menu buttons</li>
<li>Added temporary test code to test player textures</li></ul>

<b> 10:46 am 3/28/2016: Connor Cummings</b>
<ul><li> Added regions for ease of reading the code</li>
<li> started an eraser tool</li></ul>

<b>10:49 am 3/30/2016: Anthony Olivares</b>
<ul><li> Updated list of assets we need for level 1 (look at the bottom of design.txt for the Assets section)</li></ul>

<b> 4:50 pm 4/1/2016: Connor Cummings</b>
<ul><li> added erasers for textures and game objects</li>
<li>encountered an issue where the left most column and the top most row of the editor can not be painted on<li>
<li>these spaces dont take textures or game objects</li></ul>

<b> 1:36 pm 4/2/2016: Anthony Olivares</b>
<ul><li> cleaned up code for saving and loading maps in editor</li>
<li> added code for saving object array</li>
<li> added in a textbox to name the map so that it saves to/ loads from a manually chosen file instead of just the default map.dat</li></ul>

<b> 9:46 am 4/4/16: Austin Ford</b>
<ul><li>Added stamina drain and recharge</li>
<li>Added two basic skills: Overcharged and Perseverance</li>
<li>Added base skill parent class and static class for using skills</li>
<li>Fixed sprint bug</li></ul>

<b> 8:56 pm 4/6/16: Joey Tong</b>
<ul><li>Began work on AI and pathfinding</li>
<li>Added Node class</li>
</ul>

<b> 3:48 pm 4/8/2016: Connor Cummings</b>
<ul><li> fill tool works with textures, objects and erasers</li></ul>

<b> 4:00 pm 4/8/2016: Anthony Olivares</b>
<ul><li> added option screen functionality / game states (can be accessed from main menu or pause menu) </li>
<li> added placeholder screens and states for sound options and graphics options </li> </ul>

<b> 2:58 pm 4/10/2016: Connor Cummings</b>
<ul><li> fixed naming conventions for buttons in map editor</li></ul>

<b> 9:33 am 4/11/16: Austin Ford</b>
<ul><li>Added pick up items and static class for creating them</li>
<li>Changed the skill system</li>
<li>Changed HUD to count total player mags and draw only first eight</li>
<li>Added a melee weapon class and a basic stab method</li></ul>

<b> 10:04 am 4/11/2016: Connor Cummings</b>
<ul><li>created entity array and entity string array for enimeies ane player spawn</li>
<li> added a player spawn tool to set point of entry in level</li></ul>

<b> 4:47 pm 4/13/16: Joey Tong</b>
<ul><li>Implemented A* pathfinding algorithm. (Unoptimized) </li>
<li>Temporary changes to map to test pathfinding</li>
</ul>

<b> 11:57 pm 4/13/16: Joey Tong</b>
<ul><li><b>Went to hell and back</b> (Optimization and best-case debugging to A* algorithm) </li>
<li>Partial implementation of basic movement AI and sound scanning</li>
</ul>

<b> 9:30 pm 4/14/16: Joey Tong</b>
<ul><li>New Textures!</li>
<li>Fence textures</li>
<li>Car Textures</li>
<li>Overhauled Concrete textures</li>
<li>Overhauled Asphalt textures + lane lines</li>
<li>Building textures</li>
</ul>

<b> 12:00 am 4/15/2016: Connor Cummings</b>
<ul><li> created buttons for new textures and game objects</li></ul>

<b> 9:59 am 4/14/16: Joey Tong</b>
<ul><li>Gameplay tweaks & fixed AI to work for all currently spawned enemies</li>
</ul>

<b> 10:02 am 4/18/16: Austin Ford</b>
<ul><li>Added Shotugn and Rifle</li>
<li>Added HUD and ammo icons for Shotgun and Rifle</li>
<li>Pressing 'Q' now switches to previous weapon </li>
<li>Maps can now be read in</li></ul>

<b> 1:25 pm 4/18/16: Anthony Olivares </b>
<ul><li> Added the folder "LevelDesigns", and digital mock-ups for the layouts of levels 1 and 2 </li></ul>

<b> 3:23 pm 4/20/2016: Connor Cummings</b>
<ul><li>fixed rotation tool</li>
<li> added exception catches for textures and map objects</li></ul>

<b> 4:04 pm 4/20/2016: Anthony Olivares </b>
<ul><li>implemented the ability to change the game's volume in the sounds option menu</li></ul>

<b> 10:02 am 4/18/16: Austin Ford</b>
<ul><li>Added Shotugn ammo and Rifle ammo for pickup items</li>
<li>Animations for melee, pistol reloading and smg reloading</li>
<li>Added the basis for the third skill, which will grant invincibility while sprinting</li>
<li>Changed the console and added more commands</li></ul>

<b> 1:11 am 4/24/16: Joey Tong</b>
<ul><li>Forgot to add a few change posts this week</li>
<li>Added rotation/ panning to enemy AI</li>
<li>Added rotate method to enemy that wraps the direction</li>
<li>Implemented some fixes to AI not updating path mid movement</li>
<li>Added Level Select button texture and implemented LevelSelect gamestate & screen</li>
<li>Fixed FPS display bug that caused FPS to sporadically shoot up to 300</li>
<li>Added direction to parentconvertor for compatibility with draw procedure</li>
<li>Added weapons to enemies</li>
<li>Made enemies try to face the direction that they're moving in while still scanning</li>
<li>General bug fixes in map editor</li>
<li>Fixed indexing issues where some tools has problems drawing to some sections of the screen</li>
<li>Fixed an IndexOutOfBounds exception caused by fill tool</li>
<li>Fixed bug where fill tool mouse coordinates weren't cleared when tools were switched</li>
</ul>

<b> 3:36 pm 4/29/16: Austin Ford, Anthony Olivartes, Connor Cummings</b>
<ul><li>Made level 1 and added texture for the interior of building roofs, fixed some bugs in the map editor</li></ul>
<b> 4:00 pm 4/29/16: Joey Tong</b>
<ul><li>New Textures:</li>
<li>Slanted car</li>
<li>Dumpster</li>
<li>Stairs</li>
<li>Columns</li>
</ul>
<b> 2:30 am 5/1/16: Joey Tong</b>
<ul><li>Added vision cone and player detection to AI</li>
</ul>

<b> 4:01pm 5/2/2016: Connor Cummings</b>
<ul><li>added new textures to map editor</li></ul>

<b> 3:33pm 5/4/2016: Anthony Olivares</b>
<ul><li>updated design.txt with a list of assets needed for the office building levels</li></ul>

<b> 9:30 pm 5/4/16: Joey Tong</b>
<ul><li>New Textures:</li>
<li>Enemy1, Enemy2, Enemy3</li>
<li>Cursor.png</li>
<li>Finished coding AI, added some improvements and randomizations of path</li>
<li>Fixed many glitches related to rotation and detection in the AI</li>
<li>Changed cursor to custom cursor texture</li>
</ul>

<b> 2:14 p 5/5/16: Austin Ford</b>
<ul><li>Created custom icons for the three skills and added them to the hud</li>
<li>Skill icons display which skill is active when skills may not be used</li>
<li>Added random pick up item drops from enemies</li>
<li>Added a level transition state that displays before a level</li>
<li>Add the ability to switch between levels after clearing an area of enemies</li>
<li>Add a visual prompt that tells the player when the area is clear</li>
<li>Weapon are now unlocked based on the current level the player is on </li>
<li>Added a stamina for the player's sprint and changed the display for overcharged health</li></ul>

<b> 3:53 pm 5/6/16: Anthony Olivares </b>
<ul><li>Added tutorial screen content</li>
<li>Balancing changes (reduced bullet speed, reduce starting magazines for SMG and Shotgun, reduced clip size of shotgun)</li></ul>

<b> 11:41 pm 5/8/16: Joey Tong</b>
<ul><li>Ai improvements</li>
<li>General stat balancing</li>
<li>Added riot enemy stat presets</li>
<li>removed debugging "writLine" calls</li>
<li>General Debugging, fixed bug that caused double draw calls for every entity</li>
<li>Added Riot Enemy Texture</li>
<li>Made game significantly harder</li>
</ul>

<b> 7:59 pm 5/9/16: Austin Ford</b>
<ul><li>Added in level clear status saving and loading</li>
<li>You can now only select levels that you have beaten</li>
<li>Levels that are not beaten are draw with a red overlay</li>
<li>Added a controls button to the options menu that takes you to the controls image</li></ul>

<b> 10:01 pm 5/10/16: Austin Ford</b>
<ul><li>Added sound effects for the player shooting all weapons</li>
<li>Added sound effects for the player reloading all weapons</li>
<li>Added sound effects for knifing</li></ul>

<b> 11:35 am 5/11/16: Austin Ford</b>
<ul><li>Added sound effects for the player getting hit and dying</li>
<li>Added sound effects for the player picking up ammo and health</li>
<li>Added rotation saving for enemies in the map editor</li>
<li>Added rotation loading for enemies</li></ul>

<b> 3:03 pm 5/11/16: Anthony Olivares</b>
<ul><li>Added levels 4 and 5</li>
<li>Fixed a bug in which enemies could run out of ammo, causing the game to crash (fixed by giving enemies infinite ammo)</li></ul>

<b> 3:35 pm 5/11/16: Anthony Connor Cummings</b>
<ul><li>Added new textures for map editor</li>
<li>changed look of texture and game object panels</li></ul>

<b> 4:12 pm 5/11/16: Austin Ford</b>
<ul><li>Added loading feature to the map editor</li>
<li>Addded door texture</li></ul>

<b> 5:20 pm 5/12/16: Austin Ford</b>
<ul><li>Added levels 4 and 5 to the game</li>
<li>Changed level select to scale with screen size</li>
<li>Created icons for levels for and five</li>
<li>Added levels 4 and 5 to the level select</li>
<li>Added a spot for the case file for the game</li>
<li>Added a victory screen the game</li></ul>

<b> 6:36 pm 5/12/16: Austin Ford</b>
<ul><li>Added the ability to change resolutions</li>
<li>Created icons for the diffrent resolutions</li></ul>

<b> 12:43 pm 5/13/16: Austin Ford</b>
<ul><li>Added a visual bar for volume control</li>
<li>Added a background song to the playing state of the game</li></ul>

<b> 2:58 pm 5/13/16: Anthony Olivares </b>
<ul><li>pushed levels 6, 7, and 8</li>
<li>Deleted the line tool from the map editor, since it didn't work</li>
<li>Pushed the User's Manual</li></ul>

<b> 3:39 pm 5/13/16: Anthony Olivares </b>
<ul><li>Fixed some errors in user's manual</li>
<li>Finalized the levelinfo.txt</li></ul>

<b>1:52 pm 5/14/16: Joey Tong</b>
<ul><li>Cleaned up directories and added milestone 4 files</li>
<li>Cleaned up code to use correct directories independent of source code files</li>
<li>Set resolution to autosize on startup</li>
<li>Game is done!</li></ul>