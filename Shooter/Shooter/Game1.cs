//ONLY USE LIBRARIES YOU NEED TO USE TO REDUCE RAM USAGE AND INITIALIZATION TIME
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Shooter.Controls;
using Shooter.Entities;
using Shooter.MapClasses;
using Shooter.Testing_Tools;
using System;
using System.Collections.Generic;
using System.Threading;
using Shooter.GameStates;

namespace Shooter {
    ///main type for the game

    public class Game1 : Game {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Creates a new spritefont
        SpriteFont arial;

        //A list for all of the soundeffects so we can add more in as we go
        Dictionary<string,SoundEffect> soundEffects;

        //A queue for all soundeffects to be played
        private Queue<SoundEffect> curSounds;
        private Queue<Entity> sprites;

        //A list of all the projectiles
        private List<Projectile> projectiles;

        //A keyboard state object to get the keyboard old keyboard state
        KeyboardState oldState;
        MouseState oldMState;

        //FPS related objects
        private FPSHandling FPSHandler = new FPSHandling();

        //Origin vector and rotation variable for rotating the object
        Vector2 originPos;

        //input objects
        private double YVelocity;
        private double XVelocity;
        private Movement movement;

        //Height and width of the monitor
        private int screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        private int screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;

        //Camera object
        private Camera c;

        //New game console object
        private GameConsole consoleTool;

        //TEMPORARY ASSET OBJECTS________________________________________________________________

        private Character player;
        private Map m;

        private TileBounds tb;

        //Temp enemy
        private List<Character> enemies;
        //connor's menu implementation___________
        GameStateManager g;
        //HUD assets
        private Texture2D healthBar;
        private Texture2D health;
        //game time
        protected double time;
        
        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;

            //Initializes the list of sound effects
            soundEffects = new Dictionary<string,SoundEffect>();

            //Initializes the state objects
            oldState = Keyboard.GetState();
            oldMState = Mouse.GetState();

            this.IsMouseVisible = true;
            //set window size to screen size
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.PreferredBackBufferWidth = screenWidth;

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            // TODO: Add your initialization logic here
            //g = new GameStateClass(screenWidth,screenHeight, Content);
            //enable mouse pointer
            IsMouseVisible = true;
            g = new GameStateManager(screenWidth, screenHeight, Content);
            
            try {
                g.gameState = "StartMenu";
                g.CheckGameState();
            } catch (GameStateNotFoundException e) {
                Console.WriteLine(e.ToString());
                g.gameState = "";
            }

            base.Initialize();

            //set the game update rate to 120 hz
            base.TargetElapsedTime = System.TimeSpan.FromSeconds(1.0f / 120.0f);

            //Initializes console tool with fps
            consoleTool = new GameConsole(FPSHandler);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //HUD content
            healthBar = Content.Load<Texture2D>("healthBar");
            health = Content.Load<Texture2D>("health");
            //Loads in the arial font file
            arial = Content.Load<SpriteFont>("Arial20Bold");

            //adds and loads the first soundeffect object object, a gunshot
            //The gunshot file is a public domain file
            soundEffects.Add("gunshot", Content.Load<SoundEffect>("gunshot"));
            soundEffects.Add("emptyClick", Content.Load<SoundEffect>("emptyclick"));

            //create default tilebounds object
            tb = new TileBounds();

            //create map and pass in contentmanager
            m = new Map(Content);

            //creates the player
            player = new Character(Content);

            //creates the currently pending sound queue
            curSounds = new Queue<SoundEffect>();
            //creates the currently pending entity queue
            sprites = new Queue<Entity>();

            //Create the list of projectiles
            projectiles = new List<Projectile>();

            //load up initial camera
            c = new Camera(screenHeight, screenWidth);

            player.Loc.Y = (c.camPos.Y + (screenHeight / 2)) / m.TileSize;
            player.Loc.X = (c.camPos.X + (screenWidth / 2)) / m.TileSize;


            //movement object, set max velocity and acceleration here
            double maxVelocity =(10.0 / m.TileSize);
            double acceleration = ((70.0 / m.TileSize) / 1000);
            movement = new Movement(maxVelocity, acceleration);

            //use this.Content to load your game content here
            //Sets up the origin postion based off the rectangle position
            originPos = new Vector2(player.EntTexture.Width / 2f, player.EntTexture.Width / 2f);
            
            //Creates temp enemy
            enemies = new List<Character>();

            enemies.Add(new Character(Content, (c.camPos.X + 100) / m.TileSize, (c.camPos.Y + 100) / m.TileSize, "NoTexture", PlayerPos.CalcRectangle(c.camPos.X, c.camPos.Y, (c.camPos.X + 100) / m.TileSize, (c.camPos.Y + 100) / m.TileSize, m.TileSize, c.xOffset, c.yOffset)));
            enemies.Add(new Character(Content, (c.camPos.X + 400) / m.TileSize, (c.camPos.Y + 100) / m.TileSize, "NoTexture", PlayerPos.CalcRectangle(c.camPos.X, c.camPos.Y, (c.camPos.X + 400) / m.TileSize, (c.camPos.Y + 100) / m.TileSize, m.TileSize, c.xOffset, c.yOffset)));
            enemies.Add(new Character(Content, (c.camPos.X + 800) / m.TileSize, (c.camPos.Y + 100) / m.TileSize, "NoTexture", PlayerPos.CalcRectangle(c.camPos.X, c.camPos.Y, (c.camPos.X + 800) / m.TileSize, (c.camPos.Y + 100) / m.TileSize, m.TileSize, c.xOffset, c.yOffset)));

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {

            //Creates another keyboard & mouse state objects to hold the new states
            KeyboardState state = Keyboard.GetState();
            MouseState mState = Mouse.GetState();
            if (oldState.IsKeyDown(Keys.OemTilde) && state.IsKeyUp(Keys.OemTilde)) {
                consoleTool.OpenInput();
            }

            if (oldMState.LeftButton == ButtonState.Pressed && mState.LeftButton == ButtonState.Released) {
                g.MouseClicked(mState.X, mState.Y, this);
            }
            //when loading, updatestate returns true, use that to start new thread
            bool newThread = g.updateState(state, oldState);
            if (newThread == true) {
                g.backgroundThread = new Thread(new ThreadStart(g.StartGame));
                g.backgroundThread.Start();
            }
            //UPDATE GAME LOGIC IF NOT PAUSED_____________________________________________________________________________________________________________
            if (g.gameState != "Paused") {
                //CONTROLS_____________________________________

                //WASD movement controls

                //update sprint
                movement.UpdateSprint(state, oldState, m.TileSize);


                //update the screen & player positions
                player.Loc.X -= XVelocity;
                player.Loc.Y -= YVelocity;

                c.camPos.X += XVelocity;
                c.camPos.Y += YVelocity;



                bool temp = player.Weapon.CheckFireRate(gameTime.ElapsedGameTime.Milliseconds);
                //Left mouse button to shoot
                //Checks to see if the key is just pressed and not held down
                if (oldMState.LeftButton == ButtonState.Pressed && mState.LeftButton == ButtonState.Released) {
                    if (temp) {
                        SoundEffect TempSound;
                        //enqueue gunshot sound
                        //only shoot if not a null projectile
                        Projectile p = player.Weapon.Shoot(Content, player, c, m.TileSize);
                        if (p != null) {
                            projectiles.Add(p);
                            soundEffects.TryGetValue("gunshot", out TempSound);
                            curSounds.Enqueue(TempSound);
                            c.screenShake = true;
                        } else {
                            player.Weapon.Shoot(Content, player, c, m.TileSize);
                            //enqueue gun click sound if empty
                            soundEffects.TryGetValue("emptyClick", out TempSound);
                            curSounds.Enqueue(TempSound);
                        }
                    }
                }
                if (state.IsKeyDown(Keys.R) && oldState.IsKeyUp(Keys.R)) {
                    player.Weapon.Reload();
                }
                    //update camera
                    c.UpdateCamera(gameTime.ElapsedGameTime.Milliseconds, mState.X - originPos.X, mState.Y - originPos.Y, m.TileSize,
                               new Coord(mState.X, mState.Y), new Coord((player.Loc.X - c.camPos.X) * m.TileSize, (player.Loc.Y - c.camPos.Y) * m.TileSize));

                //updates projectiles and checks collision
                for (int i = 0; i < projectiles.Count; i++) {
                    if (projectiles[i].CheckRange() == true) {
                        projectiles.Remove(projectiles[i]);
                        i--;
                    } else {
                        projectiles[i].UpdatePos(gameTime.ElapsedGameTime.Milliseconds, m.TileSize);
                        //Checks if any projectiles collide with any enemies
                        for (int k = 0; k < enemies.Count; k++) {
                            if (projectiles[i].CheckHit(enemies[k])) {
                                projectiles.RemoveAt(i);
                                i--;
                                break;
                            }
                        }
                    }
                }
                //enqueue enemies
                for (int k = 0; k < enemies.Count; k++) {
                    sprites.Enqueue(ParentConvertor.ToEntity(enemies[k], Content));
                    //spriteBatch.Draw(enemies[k].EntTexture, PlayerPos.CalcRectangle(c.camPos.X, c.camPos.Y, enemies[k].Loc.X, enemies[k].Loc.Y, m.TileSize, c.xOffset, c.yOffset), Color.White);
                }
                //Temp: Checks to see if the enemy is hit
                for (int k = 0; k < enemies.Count; k++) {
                    if (!enemies[k].CheckHealth()) {
                        enemies.RemoveAt(k);
                        k--;
                    }
                }

                //Updates the rotation position by getting the angle between two points
                player.Direction = PlayerPos.CalcDirection(mState.X, mState.Y, c.camPos.X, c.camPos.Y, player.Loc.X, player.Loc.Y, m.TileSize);

            }
            
            //END OF GAME LOGIC_____________________________________________________________________________________________________________

            //Updates the old state with what the current state is
            oldState = state;
            oldMState = mState;

            //update current fps sample
            if (gameTime.TotalGameTime.TotalMilliseconds % 1000 == 0) {
                FPSHandler.AddSample(FPSHandler.frames);
                FPSHandler.frames = 0;
                //update FPS
                FPSHandler.UpdateFPS();
            }
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);

            //drawing code
            spriteBatch.Begin();

            switch (g.gameState) {
                //____________________DRAW START MENU____________________________________________________________________
                case "StartMenu":
                    spriteBatch.Draw(g.startMenuBackground, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    spriteBatch.Draw(g.startButton, g.startButtonPosition, Color.White);
                    spriteBatch.Draw(g.exitButton, g.exitButtonPosition, Color.White);
                    break;
                //____________________DRAW LOAD SCREEN____________________________________________________________________
                case "Loading":
                    //GameStateClass.DrawLoad(spriteBatch, g.loadscreen, g.loadscreenPos);
                    spriteBatch.Draw(g.loadScreen, new Vector2((screenWidth / 2) - (g.loadScreen.Width / 2), (screenHeight / 2) - (g.loadScreen.Height / 2)), Color.Cyan);
                    break;
                //____________________DRAW PAUSE MENU____________________________________________________________________
                case "Paused":
                    //temp test
                    GraphicsDevice.Clear(Color.Gray);
                    spriteBatch.DrawString(arial, "Press Esc to Resume", new Vector2(screenWidth / 2 - screenWidth / 10, screenHeight * 4 / 10), Color.DarkRed);
                    spriteBatch.Draw(g.exitButton, g.exitButtonPosition, Color.White);
                    break;
                //____________________DRAW GAME ENVIRONMENT____________________________________________________________________
                case "Playing":
                    //use Tilebounds findBounds method to find the tiles that are actually in the game window, pass in all the values it needs to calculate
                    tb.findBounds(c.camPos.X, c.camPos.Y, m.TileSize, m.TileMap.GetLength(0), m.TileMap.GetLength(1), screenWidth, screenHeight);

                    //draw the TileMap THIS MUST COME FIRST__________________________________________________________________________
                    //loop through only the tiles that are actually in the window with bounds in tilebounds object
                    for (int i = tb.Xmin; i < tb.Xmax; i++) {
                        for (int j = tb.Ymin; j < tb.Ymax; j++) {
                            //draw the tile
                            spriteBatch.Draw(m.TileMap[i, j],
                                            //Width value and Height values are translated to pixel units + the position of the tile on the actual gridmap + .5 to account for rounding errors
                                            new Rectangle((int)((c.camPos.X * m.TileSize) + (i * m.TileSize) + .5 + c.xOffset),
                                                          (int)((c.camPos.Y * m.TileSize) + (j * m.TileSize) + .5 + c.yOffset),
                                                          m.TileSize, m.TileSize), Color.White);
                        }
                    }
                    //draw projectiles
                    for (int i = 0; i < projectiles.Count; i++) {
                        //Updates the projectiles' rectangle property
                        projectiles[i].rectangle = new Rectangle((int)((c.camPos.X + projectiles[i].Loc.X) * m.TileSize), (int)((c.camPos.Y + projectiles[i].Loc.Y) * m.TileSize), m.TileSize, m.TileSize);
                        spriteBatch.Draw(projectiles[i].EntTexture, projectiles[i].rectangle, null, Color.White, (float)projectiles[i].Direction, new Vector2(projectiles[i].EntTexture.Width / 2f, projectiles[i].EntTexture.Width / 2f), SpriteEffects.None, 0);
                    }
                    //Draws the temp enemies queued to the sprites list
                    for (int j = 0; j < sprites.Count; j++) {
                        //temp
                        Entity e = sprites.Dequeue();
                        //draw enemy
                        spriteBatch.Draw(e.EntTexture, PlayerPos.CalcRectangle(c.camPos.X, c.camPos.Y, e.Loc.X, e.Loc.Y, m.TileSize, c.xOffset, c.yOffset), Color.White);
                    }

                    //draw the player model
                    spriteBatch.Draw(player.EntTexture, PlayerPos.CalcRectangle(c.camPos.X, c.camPos.Y, player.Loc.X, player.Loc.Y, 
                                                                                m.TileSize, c.xOffset, c.yOffset), 
                                                                                null, Color.White, (float)player.Direction, originPos, SpriteEffects.None, 0);
                    //Draws the temp enemy
                    for (int k = 0; k < enemies.Count; k++)
                    {
                        //Updates the enemies' rectangle property
                        enemies[k].rectangle = PlayerPos.CalcRectangle(c.camPos.X, c.camPos.Y, enemies[k].Loc.X, enemies[k].Loc.Y, m.TileSize, c.xOffset, c.yOffset);
                    }

                    //draw the player model
                    //updates the player's rectangle property
                    player.rectangle = PlayerPos.CalcRectangle(c.camPos.X, c.camPos.Y, player.Loc.X, player.Loc.Y, m.TileSize, c.xOffset, c.yOffset);
                    spriteBatch.Draw(player.EntTexture, player.rectangle, null, Color.White, (float)player.Direction, originPos, SpriteEffects.None, 0);

                    //Draws a spritefont at postion 0,0 on the screen
                    spriteBatch.DrawString(arial, "FPS: " + FPSHandler.AvgFPS, new Vector2(0, 0), Color.Yellow);

                    //Draws HUD___________________________________________________________________________________________
                    //weapon indicator at bottom right
                    spriteBatch.Draw(player.Weapon.Texture, new Rectangle(screenWidth - player.Weapon.Texture.Width * 2/3,
                                                                          screenHeight - player.Weapon.Texture.Height * 2/3, 
                                                                          player.Weapon.Texture.Width / 3, 
                                                                          player.Weapon.Texture.Height / 3), Color.White);
                    //Weapon name
                    spriteBatch.DrawString(arial, player.Weapon.Name , new Vector2(screenWidth - player.Weapon.Texture.Width * 1 / 3, 
                                                                                   screenHeight - player.Weapon.Texture.Height * 1 / 3 - arial.MeasureString(player.Weapon.Name).Y), Color.Red);
                    //health bar background
                    spriteBatch.Draw(health, new Rectangle(screenWidth / 20 + (screenWidth / 100), 
                                                           screenHeight / 20 + (int)(screenHeight / 52), 
                                                           screenWidth / 4 - (screenWidth / 50), 
                                                           screenHeight / 15 - (int)(screenHeight / 30)), Color.Black);
                    //health
                    spriteBatch.Draw(health, new Rectangle(screenWidth / 20 + (screenWidth/100), 
                                                           screenHeight / 20 + (int)(screenHeight/52), 
                                                           (screenWidth / 4 - (screenWidth/50)) * (player.Health/player.MaxHealth), 
                                                           screenHeight / 15 - (int)(screenHeight / 30)), Color.Magenta);
                    //health bar
                    spriteBatch.Draw(healthBar, new Rectangle(screenWidth/20, screenHeight/20, screenWidth / 4, screenHeight / 15),Color.White);

                    //ammo
                    for(int i = 0; i < player.Weapon.Ammo.Count; i++) {
                        spriteBatch.Draw(health, new Rectangle(screenWidth * 6 / 7 - 2, 
                                                              (screenHeight / 18) + (i * 35) - 2,
                                                              player.Weapon.maxAmmo * 19 + 2,
                                                              31), Color.DarkGray);
                        for(int j = 0; j < player.Weapon.Ammo[i]; j++) {
                            spriteBatch.Draw(player.Weapon.AmmoTexture, 
                                            new Rectangle(screenWidth * 6 / 7 + (j * 19),
                                                         (screenHeight / 18) + (i * 35) - 2,
                                                          19, 31), Color.White);
                        }
                    }
                    spriteBatch.DrawString(arial, "Ammo Left: " + player.Weapon.TotalAmmo(), new Vector2(screenWidth * 6 / 7 - 2,
                                                                                                        (screenHeight / 18) - arial.MeasureString(" ").Y - 10) , Color.Red);
                    //___________________________________________________________________________________________

                    //play all enqueued sound effects
                    for (int i = 0; i < curSounds.Count; i++) {
                        curSounds.Dequeue().Play();
                    }
                    //add frame to frame counter
                    FPSHandler.frames++;
                    break;
            }
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
