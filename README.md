# GDAPS-GAME
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
<li> Stress tested game rendering, maxes out at about 500 entities on screen while maintaining constant 55 fps and 120 hz game refresh rate</li>
<li> <b>Known Issue: frame rate is locked to about 55 and not very well optimized, we should consider unlocking</b></li></ul>              			  
                            
<b>3:59 pm 2-7-16 Anthony Olivares</b>
<ul><li>Added a story section to the design document <b>Issue: File was not pushed to repo</b></li></ul>
<b>7:30pm 2-7-16: Joey Tong</b><ul><li>fixed implementation of map class to be more useful and changed map rendering pattern</li>
<li>added and updated several tile textures: Concrete.png, Asphalt.png, ConcreteCorner.png, ConcreteEdge,png, LaneLine.png</li></ul>
<b>11:20 am 2-8-16: Joey Tong</b>
<ul><li>Implemented Map scrolling (during class in lab)</li>
<strike><li><b>Known Issue: Poor optimization of map rendering, need to implement ambient occlusion</b></ul></strike>
<b>12:30 am 2-9-16: Joey Tong</b>
<ul><li>Fixed optimization issues with rendering TileMap and implemented ambient occlusion</li>
<li>Stress tested RAM capacity, game manages to load 10k x 10k TileMap and render with no issues. This means ambient occlusion is working properly. This also means, don't make maps more than 10k x 10k tiles. Though that should be obvious</li>
<li><b>Issue: Game will need load screens in the near future as total assets being loaded onto the ram are increasing rapidly with implementation</b></li></ul>

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
