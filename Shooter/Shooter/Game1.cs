//ONLY USE LIBRARIES YOU NEED TO USE TO REDUCE RAM USAGE AND INITIALIZATION TIME
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Shooter.Controls;
using Shooter.Entities;
using Shooter.GameStates;
using Shooter.MapClasses;
using Shooter.Testing_Tools;
using System;
using System.Collections.Generic;
using System.Threading;


namespace Shooter {
    ///main type for the game

    public class Game1 : Game {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Creates a new spritefont
        SpriteFont arial;

        //A list for all of the soundeffects so we can add more in as we go
        Dictionary<string, SoundEffect> soundEffects;

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
        private Movement movement;

        //Height and width of the monitor
        private int screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        private int screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        //private int screenHeight = 768;
        //private int screenWidth = 1024;
        //Camera object
        private Camera c;

        //New game console object
        private GameConsole consoleTool;

        //TEMPORARY ASSET OBJECTS________________________________________________________________

        private Character player;
        private Map m;

        private TileBounds tb;

        //Temp enemy
        private List<Enemy> enemies;
        private List<PickUpItem> Items;
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
            soundEffects = new Dictionary<string, SoundEffect>();

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
            consoleTool = new GameConsole();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            g.gameState = "Loading";
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
            m = new Map(Content, screenWidth, screenHeight);

            //creates the player (test texture)
            player = new Character(Content, 10, 10, 0, "Pistol_Player", true, new Rectangle(0, 0, 0, 0));

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
            player.IsPlayer = true;

            //movement object, set max velocity and acceleration here
            double maxVelocity = (5.4 / m.TileSize);
            double acceleration = ((80.0 / m.TileSize) / 1000);
            movement = new Movement(maxVelocity, acceleration, m.TileSize);

            //use this.Content to load your game content here
            //Sets up the origin postion based off the rectangle position
            originPos = new Vector2(player.EntTexture.Width / 2f, player.EntTexture.Width / 2f);

            //Creates temp enemy
            enemies = new List<Enemy>();
            Items = new List<PickUpItem>();
            //Creates enemies to check
            CreateEnemy.CreateNormalEnemy(ref enemies, Content, c, m, 4, 1);
            CreateEnemy.CreateNormalEnemy(ref enemies, Content, c, m, 8, 1);
            CreateEnemy.CreateNormalEnemy(ref enemies, Content, c, m, 12, 1);
            CreateEnemy.CreateRiotEnemy(ref enemies, Content, c, m, 16, 1);
            //Creates the weapons for the player
            Shooting.CreateWeapons(Content);
            SkillSystem.CreateSkills(Content, player);
            //Starts the player with the first weapon
            player.Weapon = Shooting.weapons[1];
            SkillSystem.skills[0].Obtained = true;
            SkillSystem.skills[1].Obtained = true;
            g.gameState = "StartMenu";
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

            //Checks for a console input
            if (oldState.IsKeyDown(Keys.OemTilde) && state.IsKeyUp(Keys.OemTilde)) {
                //Checks the command and does the neccessary method for that
                string command = "";
                //Loops until time to exit the console
                while (!command.Equals("exit")) {
                    command = consoleTool.OpenInput();
                    //Splits the method call into the method and parameter
                    string[] methodCall = command.Split('/');
                    //Checks to see the number of parameters
                    if (methodCall.Length > 2) {
                        if (methodCall[0].Equals("CreateNormalEnemy")) {
                            CreateEnemy.CreateNormalEnemy(ref enemies, Content, c, m, double.Parse(methodCall[1]), double.Parse(methodCall[2]));
                        } else if (methodCall[0].Equals("CreateRiotEnemy")) {
                            CreateEnemy.CreateRiotEnemy(ref enemies, Content, c, m, double.Parse(methodCall[1]), double.Parse(methodCall[2]));
                        }
                        //Else runs checks for the command
                    } else {
                        if (methodCall[0].Equals("player.weapon")) {
                            player.Weapon.FillAmmo();
                        } else if (methodCall[0].Equals("player.health")) {
                            player.Health = 10000000;
                        } else if (methodCall[0].Equals("UpdateFPS")) {
                            FPSHandler.UpdateFPS();
                        } else if (methodCall[0].Equals("printenemies")) {
                            foreach (Enemy e in enemies) {
                                Console.WriteLine("X: " + e.Loc.X + " Y: " + e.Loc.Y + " Health: " + e.Health);
                            }
                        }
                    }
                }
            }

            if (oldMState.LeftButton == ButtonState.Released && mState.LeftButton == ButtonState.Pressed) {
                g.MouseClicked(mState.X, mState.Y, this);
            }
            //when loading, updatestate returns true, use that to start new thread
            bool newThread = g.updateState(state, oldState);
            if (newThread == true) {
                g.gameState = "Loading";
                g.StartGame();
            }
            //UPDATE GAME LOGIC IF NOT PAUSED_____________________________________________________________________________________________________________
            if (g.gameState != "Paused" && g.gameState != "Loading") {

                //CONTROLS_____________________________________

                //movement controls (W,A,S,D, Shift)
                //update X & Y instantanous velocities and checks sprint  
                movement.XVelocity = movement.UpdateX(movement.XVelocity, gameTime.ElapsedGameTime.Milliseconds, state);
                movement.YVelocity = movement.UpdateY(movement.YVelocity, gameTime.ElapsedGameTime.Milliseconds, state);
                //if (!(player.CheckStamina())) {
                //    movement.UpdateSprint(state, oldState, m.TileSize, false, player);
                // } else {
                //    movement.UpdateSprint(state, oldState, m.TileSize, true, player);
                // }
                movement.UpdateSprint(state, oldState, m.TileSize, player);
                player.UpdateStamina(gameTime.TotalGameTime.Milliseconds);

                //Checks for player collision with mapobjects
                string[] s = m.CheckArea(player);
                for (int i = 0; i < s.Length; i++) {
                    if (s[i] != null) {
                        if (s[i].Equals("Bottom") && movement.YVelocity > 0 || s[i].Equals("Top") && movement.YVelocity < 0) {
                            movement.YVelocity = -movement.YVelocity / 5;
                        }
                        if (s[i].Equals("Left") && movement.XVelocity < 0 || s[i].Equals("Right") && movement.XVelocity > 0) {
                            movement.XVelocity = -movement.XVelocity / 5;
                        }
                    }
                }

                //update the camera & player positions
                player.Loc.X -= movement.XVelocity;
                player.Loc.Y -= movement.YVelocity;

                c.camPos.X += movement.XVelocity;
                c.camPos.Y += movement.YVelocity;
                //_____________________________________________


                bool temp = player.Weapon.CheckFireRate(gameTime.ElapsedGameTime.Milliseconds);
                //check if gun is reloading
                if (!player.Weapon.Reloading(gameTime.ElapsedGameTime.Milliseconds)) {
                    //Shoots the player's weapon
                    Shooting.ShootWeapon(player, mState, oldMState, projectiles, temp, c, Content, curSounds, soundEffects, ref m);

                    //Switches the player's weapon
                    Shooting.SwitchWeapon(player, state, oldState);

                    if (state.IsKeyDown(Keys.R) && oldState.IsKeyUp(Keys.R)) {
                        player.Weapon.Reload();
                    }
                }
                Shooting.Stab(player, state, oldState, Content, c, m.TileSize, projectiles);
                SkillSystem.UseSkill(player, state, oldState, gameTime.TotalGameTime.Milliseconds);
                //update camera
                c.UpdateCamera(gameTime.ElapsedGameTime.Milliseconds, mState.X - originPos.X, mState.Y - originPos.Y, m.TileSize,
                            new Coord(mState.X, mState.Y), new Coord((player.Loc.X - c.camPos.X) * m.TileSize, (player.Loc.Y - c.camPos.Y) * m.TileSize));

                //updates projectiles and checks collision
                for (int i = 0; i < projectiles.Count; i++) {
                    //checks if the projectile has exceeded its maximum range
                    if (projectiles[i].CheckRange() == true) {
                        projectiles.Remove(projectiles[i]);
                        i--;
                    } else {
                        projectiles[i].UpdatePos(gameTime.ElapsedGameTime.Milliseconds, m.TileSize);
                        if (m.CheckArea(projectiles[i])[0] != null && m.CheckArea(projectiles[i])[0].Equals("hit") && !projectiles[i].IsRifleRound) {
                            projectiles.RemoveAt(i);
                            i--;
                            break;
                        }
                        //Checks if any projectiles collide with any enemies
                        for (int k = 0; k < enemies.Count; k++) {
                            if (projectiles[i].CheckHit(enemies[k], player.Weapon)) {
                                projectiles.RemoveAt(i);
                                i--;
                                break;
                            }
                        }
                    }
                }
                //update AI code
                for(int i = 0; i < enemies.Count; i++) {
                    if (enemies[i] != null) {
                        enemies[i].UpdateAI(ref m, gameTime.ElapsedGameTime.Milliseconds);
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

                if (oldState.IsKeyDown(Keys.X) && state.IsKeyUp(Keys.X)) {

                }

                //Updates the rotation position by getting the angle between two points
                player.Direction = PlayerPos.CalcDirection(mState.X, mState.Y, c.camPos.X, c.camPos.Y, player.Loc.X, player.Loc.Y, m.TileSize);


            }

            //END OF GAME LOGIC_____________________________________________________________________________________________________________

            //Updates the old input states with what the current states are
            oldState = state;
            oldMState = mState;
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
                    spriteBatch.Draw(g.optionsButton, g.optionsButtonPosition, Color.White);
                    spriteBatch.Draw(g.exitButton, g.exitButtonPosition, Color.White);
                    break;
                //____________________DRAW OPTIONS MENU___________________________________________________________________
                case "OptionsMenu":
                    spriteBatch.Draw(g.startMenuBackground, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    spriteBatch.Draw(g.soundsButton, g.soundsButtonPosition, Color.White);
                    spriteBatch.Draw(g.graphicsButton, g.graphicsButtonPosition, Color.White);
                    spriteBatch.Draw(g.backButton, g.backButtonPosition, Color.White);
                    break;
                //____________________DRAW GRAPHICS OPTIONS MENU__________________________________________________________
                case "GraphicsMenu":
                    spriteBatch.Draw(g.startMenuBackground, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    spriteBatch.Draw(g.backButton, g.backButtonPosition, Color.White);
                    break;
                //____________________DRAW SOUNDS OPTIONS MENU____________________________________________________________
                case "SoundsMenu":
                    spriteBatch.Draw(g.startMenuBackground, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    spriteBatch.Draw(g.backButton, g.backButtonPosition, Color.White);
                    break;
                //____________________DRAW LOAD SCREEN____________________________________________________________________
                case "Loading":
                    g.DrawLoad(spriteBatch);
                    spriteBatch.Draw(g.loadScreen, new Vector2((screenWidth / 2) - (g.loadScreen.Width / 2), (screenHeight / 2) - (g.loadScreen.Height / 2)), Color.Cyan);
                    break;
                //____________________DRAW PAUSE MENU____________________________________________________________________
                case "Paused":
                    //temp test
                    GraphicsDevice.Clear(Color.Gray);
                    spriteBatch.DrawString(arial, "Press Esc to Resume", new Vector2(screenWidth / 2 - screenWidth / 10, screenHeight * 4 / 10), Color.DarkRed);
                    spriteBatch.Draw(g.optionsButton, g.optionsButtonPosition, Color.White);
                    spriteBatch.Draw(g.exitButton, g.exitButtonPosition, Color.White);
                    break;
                //____________________DRAW GAME ENVIRONMENT____________________________________________________________________
                case "Playing":
                    //use Tilebounds findBounds method to find the tiles that are actually in the game window, pass in all the values it needs to calculate
                    tb.findBounds(c.camPos.X, c.camPos.Y, m.TileSize, m.TileMap.GetLength(0), m.TileMap.GetLength(1), screenWidth, screenHeight);

                    //draw the Map THIS MUST COME FIRST__________________________________________________________________________
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
                    //loop through only the tiles that are actually in the window with bounds in tilebounds object
                    for (int i = tb.Xmin; i < tb.Xmax; i++) {
                        for (int j = tb.Ymin; j < tb.Ymax; j++) {
                            if (m.ObjectMap[i, j] != null) {
                                spriteBatch.Draw(m.ObjectMap[i, j].ObjTexture,
                                                //Width value and Height values are translated to pixel units + the position of the tile on the actual gridmap + .5 to account for rounding errors
                                                new Rectangle((int)((c.camPos.X * m.TileSize) + (i * m.TileSize) + .5 + c.xOffset),
                                                                (int)((c.camPos.Y * m.TileSize) + (j * m.TileSize) + .5 + c.yOffset),
                                                                m.TileSize, m.TileSize), Color.White);
                            }
                        }
                    }
                    //Draws the Items in the list
                    for (int u = 0; u < Items.Count; u++) {
                        spriteBatch.Draw(Items[u].ItemTexture, PlayerPos.CalcRectangle(c.camPos.X, c.camPos.Y, Items[u].Location.X, Items[u].Location.X, m.TileSize, c.xOffset, c.yOffset), Color.White);
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
                    if (Items.Count > 0) {
                        spriteBatch.Draw(Items[0].ItemTexture, PlayerPos.CalcRectangle(c.camPos.X, c.camPos.Y, Items[0].Location.X, Items[0].Location.Y, m.TileSize, c.xOffset, c.yOffset), Color.White);
                    }//draw the player model
                    spriteBatch.Draw(player.EntTexture, PlayerPos.CalcRectangle(c.camPos.X, c.camPos.Y, player.Loc.X, player.Loc.Y,
                                                                                m.TileSize, c.xOffset, c.yOffset),
                                                                                null, Color.White, (float)player.Direction, originPos, SpriteEffects.None, 0);
                    //Draws the temp enemy
                    for (int k = 0; k < enemies.Count; k++) {
                        //Updates the enemies' rectangle property
                        enemies[k].rectangle = PlayerPos.CalcRectangle(c.camPos.X, c.camPos.Y, enemies[k].Loc.X, enemies[k].Loc.Y, m.TileSize, c.xOffset, c.yOffset);
                    }


                    //draw the player model
                    //updates the player's rectangle property
                    player.rectangle = PlayerPos.CalcRectangle(c.camPos.X, c.camPos.Y, player.Loc.X, player.Loc.Y, m.TileSize, c.xOffset, c.yOffset);
                    spriteBatch.Draw(player.EntTexture, player.rectangle, null, Color.White, (float)player.Direction, originPos, SpriteEffects.None, 0);

                    //Draws a spritefont at postion 0,0 on the screen
                    spriteBatch.DrawString(arial, "FPS: " + FPSHandler.AvgFPS, new Vector2(0, 0), Color.Yellow);
                    //Draw the HUD, moved calculations to external static class
                    HUD.DrawHUD(player, ref spriteBatch, screenHeight, screenWidth, arial, health, healthBar);

                    //play all enqueued sound effects
                    for (int i = 0; i < curSounds.Count; i++) {
                        curSounds.Dequeue().Play();
                    }
                    m.sounds.Clear();
                    break;
            }

            //add frame to frame counter
            FPSHandler.frames++;
            spriteBatch.End();
            //update current fps sample
            if (gameTime.TotalGameTime.TotalMilliseconds % 1000 == 0) {
                FPSHandler.AddSample(FPSHandler.frames);
                FPSHandler.frames = 0;
                //update FPS
                FPSHandler.UpdateFPS();
            }
            base.Draw(gameTime);
        }
    }
}
