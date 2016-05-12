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
using System.IO;
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

        private Single volume;

        //Height and width of the monitor
        //private int screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        //private int screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        //private int screenHeight = 1015;
        //private int screenWidth = 1920;
        private int screenHeight = 768;
        private int screenWidth = 1024;
        //Camera object
        private Camera c;

        //New game console object
        private GameConsole consoleTool;

        //TEMPORARY ASSET OBJECTS________________________________________________________________

        private Character player;
        private Map m;
        Random rng = new Random();
        private bool isDrawing = true;

        private TileBounds tb;
        Thread loadingThread;
        //Temp enemy
        private List<Enemy> enemies;
        private List<PickUpItem> Items;
        //connor's menu implementation___________
        GameStateManager g;
        //Cursor texture
        private Texture2D cursor;
        private int cursorX;
        private int cursorY;
        //HUD assets
        private Texture2D healthBar;
        private Texture2D health;
        private Texture2D stamina;
        private Texture2D bar;
        private Texture2D[] skillIcons;
        //game time
        protected double time;
        //Current level variable
        private int currentLevel = 0;
        private int numOfLevels = 5;
        private string[,] levelInfo;
        private int timer = 0;
        private int tranTimer = 3000;
        private string wepUnl = "";
        private SoundEffect deathSound;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;

            //Initializes the list of sound effects
            soundEffects = new Dictionary<string, SoundEffect>();

            //Initializes the state objects
            oldState = Keyboard.GetState();
            oldMState = Mouse.GetState();

            this.IsMouseVisible = false;
            //set window size to screen size
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.PreferredBackBufferWidth = screenWidth;

            volume = 1.0f;
            Content.RootDirectory = "Content";
        }


        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            g = new GameStateManager(screenWidth, screenHeight, Content, currentLevel);
            cursor = Content.Load<Texture2D>("Cursor");
            cursorX = Mouse.GetState().X;
            cursorY = Mouse.GetState().Y;
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

            loadingThread = new Thread(LoadGame);
            loadingThread.Start();
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
            //update mouse position
            cursorX = mState.X;
            cursorY = mState.Y;

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
                        if (methodCall[0].Equals("CREATENORMALENEMY")) {
                            CreateEnemy.CreateNormalEnemy(ref enemies, Content, c, m, double.Parse(methodCall[1]), double.Parse(methodCall[2]), 0);
                        } else if (methodCall[0].Equals("CREATERIOTENEMY")) {
                            CreateEnemy.CreateRiotEnemy(ref enemies, Content, c, m, double.Parse(methodCall[1]), double.Parse(methodCall[2]) , 0);
                        } else if (methodCall[0].Equals("createhealthkit")) {
                            CreateItems.CreateHealthKit(Content, Items, double.Parse(methodCall[1]), double.Parse(methodCall[2]));
                        } else if (methodCall[0].Equals("createammokit")) {
                            if (methodCall[methodCall.Length - 1].Equals("pistol")) {
                                CreateItems.CreatePistolAmmo(Content, Items, double.Parse(methodCall[1]), double.Parse(methodCall[2]));
                            } else if (methodCall[methodCall.Length - 1].Equals("rifle")) {
                                CreateItems.CreateRifleAmmo(Content, Items, double.Parse(methodCall[1]), double.Parse(methodCall[2]));
                            } else if (methodCall[methodCall.Length - 1].Equals("shotgun")) {
                                CreateItems.CreateShotgunAmmo(Content, Items, double.Parse(methodCall[1]), double.Parse(methodCall[2]));
                            } else if (methodCall[methodCall.Length - 1].Equals("smg")) {
                                CreateItems.CreateSMGAmmo(Content, Items, double.Parse(methodCall[1]), double.Parse(methodCall[2]));
                            }
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
                g.MouseClicked(mState.X, mState.Y, this, ref currentLevel, ref enemies, ref Items, ref projectiles, ref timer, Content, ref player, ref wepUnl);
            }
            //when loading, updatestate returns true, use that to start new thread
            bool newThread = g.updateState(state, oldState);
            if (newThread == true) {
                g.gameState = "Loading";
                g.StartGame();
            }

            //UPDATE GAME LOGIC IF NOT PAUSED_____________________________________________________________________________________________________________
            if (g.gameState.Equals("Playing")) {
                if (enemies.Count == 0) {
                    if (oldState.IsKeyDown(Keys.Right) && state.IsKeyUp(Keys.Right)) {
                        if (currentLevel != numOfLevels) {
                            g.levelClears[currentLevel] = 1;
                            currentLevel++;
                            enemies.Clear();
                            Items.Clear();
                            projectiles.Clear();
                            timer = 0;
                            g.gameState = "LevelSwitch";
                            g.saveLevelClears();
                            if (currentLevel < Shooting.weapons.Length && !Shooting.weapons[currentLevel].IsAcquired) {
                                wepUnl = "Looks like I found a new weapon, the " + Shooting.weapons[currentLevel].Name;
                                Shooting.weapons[currentLevel].IsAcquired = true;
                            } else {
                                wepUnl = "";
                            }

                        } else {
                            g.levelClears[g.levelClears.Length - 1] = 1;
                            enemies.Clear();
                            Items.Clear();
                            projectiles.Clear();
                            g.saveLevelClears();
                            g.gameState = "Victory";
                        }
                    }
                }
                //CONTROLS_____________________________________

                //movement controls (W,A,S,D, Shift)
                //update X & Y instantanous velocities and checks sprint  
                movement.XVelocity = movement.UpdateX(movement.XVelocity, gameTime.ElapsedGameTime.Milliseconds, state);
                movement.YVelocity = movement.UpdateY(movement.YVelocity, gameTime.ElapsedGameTime.Milliseconds, state);
                //Sprint and stamina checks
                movement.UpdateSprint(state, oldState, m.TileSize, player);
                player.UpdateStamina(gameTime.TotalGameTime.Milliseconds);

                //Checks for player collision with mapobjects
                m.CheckArea(player, c);

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
                        player.Weapon.Reload(curSounds);
                        player.Frame = 0;
                        player.NumOfFrames = 9;
                        player.TimePerFrame = (int)(player.Weapon.ReloadTime / player.NumOfFrames);
                    }
                }
                player.UpdateAnimation(gameTime.TotalGameTime.TotalMilliseconds);
                Shooting.Stab(player, state, oldState, Content, c, m.TileSize, projectiles, curSounds);
                SkillSystem.UseSkill(player, state, oldState, gameTime.TotalGameTime.Milliseconds);
                //update camera
                c.UpdateCamera(gameTime.ElapsedGameTime.Milliseconds, mState.X - originPos.X, mState.Y - originPos.Y, m.TileSize,
                            new Coord(mState.X, mState.Y), new Coord((player.Loc.X - c.camPos.X) * m.TileSize, (player.Loc.Y - c.camPos.Y) * m.TileSize));

                //updates projectiles and checks collision
                for (int i = 0; i < projectiles.Count; i++) {
                    //checks if the projectile has exceeded its maximum range
                    if(projectiles[i] != null)
                    {
                        if (projectiles[i].CheckRange() == true)
                        {
                            projectiles.Remove(projectiles[i]);
                            i--;
                        }
                        else {
                            projectiles[i].UpdatePos(gameTime.ElapsedGameTime.Milliseconds, m.TileSize);
                            if (m.CheckArea(projectiles[i], c) != null && m.CheckArea(projectiles[i], c).Equals("hit") && !projectiles[i].IsRifleRound)
                            {
                                projectiles.RemoveAt(i);
                                i--;
                                break;
                            }
                            if (!SkillSystem.skills[2].Active && enemies.Count > 0 && projectiles[i].CheckHit(player, enemies[0].Weapon))
                            {
                                projectiles.RemoveAt(i);
                                curSounds.Enqueue(Content.Load<SoundEffect>("playerHit"));
                                i--;
                                break;
                            }
                            //Checks if any projectiles collide with any enemies
                            for (int k = 0; k < enemies.Count; k++)
                            {
                                if (projectiles[i].CheckHit(enemies[k], player.Weapon))
                                {
                                    projectiles.RemoveAt(i);
                                    i--;
                                    break;
                                }
                            }
                        }
                    
                    }
                }
                //update AI for all enemies
                for (int i = 0; i < enemies.Count; i++) {
                    if (enemies[i] != null) {
                        if (enemies[i].UpdateAI(ref m, gameTime.ElapsedGameTime.Milliseconds, player.Loc)) {
                            projectiles.Add(enemies[i].Weapon.Shoot(Content, enemies[i], c, m.TileSize, enemies[i]));
                            enemies[i].Weapon.Ammo[0]++;
                            SoundEffect TempSound;
                            soundEffects.TryGetValue("gunshot", out TempSound);
                            try {
                                curSounds.Enqueue(TempSound);
                            } catch (NullReferenceException e) {
                                Console.WriteLine("Sound Effect not found or implemented");
                                Console.WriteLine(e.StackTrace);
                            }
                        }
                    }
                }

                //enqueue enemies
                if (isDrawing) {
                    for (int k = 0; k < enemies.Count; k++) {
                        sprites.Enqueue(ParentConvertor.ToEntity(enemies[k], Content));
                        isDrawing = false;
                        //spriteBatch.Draw(enemies[k].EntTexture, PlayerPos.CalcRectangle(c.camPos.X, c.camPos.Y, enemies[k].Loc.X, enemies[k].Loc.Y, m.TileSize, c.xOffset, c.yOffset), Color.White);
                    }
                } else {
                    isDrawing = true;
                }
                //Temp: Checks to see if the enemy is hit
                for (int k = 0; k < enemies.Count; k++) {
                    if (!enemies[k].CheckHealth()) {
                        CreateItems.CreateRandomItem(rng, Content, Items, enemies[k].Loc.X, enemies[k].Loc.Y, player);
                        enemies.RemoveAt(k);
                        k--;
                    }
                }

                //Checks if the player is dead
                if (player.Health <= 0) {
                    g.saveLevelClears();
                    curSounds.Clear();
                    deathSound.Play();
                    g.gameState = "Death";
                }

                CreateItems.CheckItemCollisions(Items, player, Content, curSounds);

                //Updates the rotation position by getting the angle between two points
                player.Direction = PlayerPos.CalcDirection(mState.X, mState.Y, c.camPos.X, c.camPos.Y, player.Loc.X, player.Loc.Y, m.TileSize);


            }

            if (g.gameState == "LevelSwitch") {
                timer += gameTime.ElapsedGameTime.Milliseconds;
                if (timer >= tranTimer) {
                    m = new Map(Content, "level" + currentLevel + ".dat", c, player, enemies, screenWidth);
                    g.gameState = "Playing";
                }
            }

            //UPDATE LOGIC FOR SOUND OPTIONS______________________________________________________________________________________________________________
            if (g.gameState == "SoundsMenu") {
                if (state.IsKeyDown(Keys.Right) && oldState.IsKeyUp(Keys.Right)) {
                    volume += 0.1f;
                    if (volume > 1.0f) {
                        volume = 1.0f;
                    }
                    if (volume < 0.0f) {
                        volume = 0.0f;
                    }
                    SoundEffect shot;
                    soundEffects.TryGetValue("gunshot", out shot);
                    curSounds.Enqueue(shot);
                    curSounds.Dequeue().Play(volume, 0.0f, 0.0f);
                }

                if (state.IsKeyDown(Keys.Left) && oldState.IsKeyUp(Keys.Left)) {
                    volume = volume - 0.1f;
                    if (volume > 1.0f) {
                        volume = 1.0f;
                    }
                    if (volume < 0.0f) {
                        volume = 0.0f;
                    }
                    SoundEffect shot;
                    soundEffects.TryGetValue("gunshot", out shot);
                    curSounds.Enqueue(shot);
                    curSounds.Dequeue().Play(volume, 0.0f, 0.0f);
                }
            }

            //END OF GAME LOGIC_____________________________________________________________________________________________________________
            if (gameTime.TotalGameTime.TotalMilliseconds % 1000 == 0) {
                FPSHandler.AddSample(FPSHandler.frames);
                FPSHandler.frames = 0;
                //update FPS
                FPSHandler.UpdateFPS();
            }
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
                    spriteBatch.Draw(g.levelSelectButton, g.levelSelectButtonPosition, Color.White);
                    spriteBatch.Draw(g.exitButton, g.exitButtonPosition, Color.White);
                    break;
                //____________________DRAW OPTIONS MENU___________________________________________________________________
                case "OptionsMenu":
                    spriteBatch.Draw(g.startMenuBackground, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    spriteBatch.Draw(g.soundsButton, g.soundsButtonPosition, Color.White);
                    spriteBatch.Draw(g.graphicsButton, g.graphicsButtonPosition, Color.White);
                    spriteBatch.Draw(g.backButton, g.backButtonPosition, Color.White);
                    spriteBatch.Draw(g.controlButton, g.controlButtonPosition, Color.White);
                    break;
                //____________________DRAW LEVEL SELECT OPTIONS MENU__________________________________________________________
                case "LevelSelect":
                    spriteBatch.Draw(g.startMenuBackground, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    spriteBatch.Draw(g.backButton, g.backButtonPosition, Color.White);
                    if (g.levelClears[0] != 0) {
                        spriteBatch.Draw(g.levelIcons[0], g.levelRect[0], Color.White); //level 1
                    } else {
                        spriteBatch.Draw(g.levelIcons[0], g.levelRect[0], Color.Red); //level 1
                    }
                    if (g.levelClears[1] != 0) {
                        spriteBatch.Draw(g.levelIcons[1], g.levelRect[1], Color.White); //level 2
                    } else {
                        spriteBatch.Draw(g.levelIcons[1], g.levelRect[1], Color.Red); //level 2
                    }
                    if (g.levelClears[2] != 0) {
                        spriteBatch.Draw(g.levelIcons[2], g.levelRect[2], Color.White); //level 3
                    } else {
                        spriteBatch.Draw(g.levelIcons[2], g.levelRect[2], Color.Red); //level 3
                    }
                    if (g.levelClears[3] != 0) {
                        spriteBatch.Draw(g.levelIcons[3], g.levelRect[3], Color.White); //level 3
                    } else {
                        spriteBatch.Draw(g.levelIcons[3], g.levelRect[3], Color.Red); //level 3
                    }
                    if (g.levelClears[4] != 0) {
                        spriteBatch.Draw(g.levelIcons[4], g.levelRect[4], Color.White); //level 3
                    } else {
                        spriteBatch.Draw(g.levelIcons[4], g.levelRect[4], Color.Red); //level 3
                    }
                    spriteBatch.Draw(health, g.levelRect[5], Color.White); //level 6
                    spriteBatch.Draw(health, g.levelRect[6], Color.White); //level 7
                    spriteBatch.Draw(health, g.levelRect[7], Color.White); //level 8
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
                    spriteBatch.DrawString(arial, "Use the Left and Right arrow keys to increase or decrease the volume", new Vector2(screenWidth / 4, screenHeight / 2), Color.DarkRed);
                    break;
                //____________________DRAW Controls OPTIONS MENU____________________________________________________________
                case "Controls":
                    spriteBatch.Draw(g.controls, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    spriteBatch.Draw(g.backButton, g.controlBackButtonPosition, Color.White);
                    break;
                //____________________DRAW LOAD SCREEN____________________________________________________________________
                case "Loading":
                    g.DrawLoad(spriteBatch);
                    spriteBatch.Draw(g.loadScreen, new Vector2((screenWidth / 2) - (g.loadScreen.Width / 2), (screenHeight / 2) - (g.loadScreen.Height / 2)), Color.Cyan);
                    break;

                //___________________DRAW DEATH SCREEN_____________________________________________________________________
                case "Death":
                    spriteBatch.Draw(g.deathBackground, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    spriteBatch.Draw(g.backButton, g.backButtonPosition, Color.White);
                    break;
                //___________________DRAW DEATH SCREEN_____________________________________________________________________
                case "Case":
                    spriteBatch.Draw(g.caseBackground, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    spriteBatch.Draw(g.startButton, g.rightStartButton, Color.White);
                    break;
                //___________________DRAW DEATH SCREEN_____________________________________________________________________
                case "Victory":
                    spriteBatch.Draw(g.closedBackground, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    spriteBatch.Draw(g.startButton, g.rightStartButton, Color.White);
                    break;
                //____________________DRAW PAUSE MENU____________________________________________________________________
                case "Paused":
                    //temp test
                    spriteBatch.Draw(g.startMenuBackground, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    spriteBatch.Draw(g.resumeButton, g.resumeButtonPosition, Color.White);
                    spriteBatch.Draw(g.optionsButton, g.optionsButtonPosition, Color.White);
                    spriteBatch.Draw(g.exitButton, g.exitButtonPosition, Color.White);
                    break;
                //____________________DRAW Level Transition____________________________________________________________________
                case "LevelSwitch":
                    spriteBatch.DrawString(arial, "Level " + currentLevel, new Vector2((screenWidth - arial.MeasureString("Level " + currentLevel).X) / 2, (screenHeight * 2) / 10), Color.White);
                    spriteBatch.DrawString(arial, levelInfo[currentLevel - 1, 0], new Vector2((screenWidth - arial.MeasureString(levelInfo[currentLevel - 1, 0]).X) / 2, ((screenHeight * 2) / 10) + arial.MeasureString("Level " + currentLevel).Y), Color.White);
                    spriteBatch.DrawString(arial, wepUnl, new Vector2((screenWidth - arial.MeasureString(wepUnl).X) / 2, ((screenHeight * 2) / 10) + arial.MeasureString("Level " + currentLevel).Y * 3), Color.White);
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
                            /*spriteBatch.Draw(m.TileMap[i, j],
                                            //Width value and Height values are translated to pixel units + the position of the tile on the actual gridmap + .5 to account for rounding errors
                                            new Rectangle((int)((c.camPos.X * m.TileSize) + (i * m.TileSize) + .5 + c.xOffset),
                                                          (int)((c.camPos.Y * m.TileSize) + (j * m.TileSize) + .5 + c.yOffset),
                                                          m.TileSize, m.TileSize), Color.White);
                                                          */
                            spriteBatch.Draw(m.TileMap[i, j], new Rectangle((int)((c.camPos.X * m.TileSize) + (i * m.TileSize) + .5 + c.xOffset),
                                                          (int)((c.camPos.Y * m.TileSize) + (j * m.TileSize) + .5 + c.yOffset),
                                                          m.TileSize, m.TileSize), null, Color.White, m.TileRot[i, j] * -1.5708f, new Vector2((m.TileMap[i, j].Width / 2f), (m.TileMap[i, j].Width / 2f)), SpriteEffects.None, 0);
                        }
                    }
                    //loop through only the tiles that are actually in the window with bounds in tilebounds object
                    for (int i = tb.Xmin; i < tb.Xmax; i++) {
                        for (int j = tb.Ymin; j < tb.Ymax; j++) {
                            if (m.ObjectMap[i, j] != null) {
                                /*spriteBatch.Draw(m.ObjectMap[i, j].ObjTexture,
                                                //Width value and Height values are translated to pixel units + the position of the tile on the actual gridmap + .5 to account for rounding errors
                                                new Rectangle((int)((c.camPos.X * m.TileSize) + (i * m.TileSize) + .5 + c.xOffset),
                                                                (int)((c.camPos.Y * m.TileSize) + (j * m.TileSize) + .5 + c.yOffset),
                                                                m.TileSize, m.TileSize), Color.White);
                                                                */
                                spriteBatch.Draw(m.ObjectMap[i, j].ObjTexture, new Rectangle((int)((c.camPos.X * m.TileSize) + (i * m.TileSize) + .5 + c.xOffset),
                                                                (int)((c.camPos.Y * m.TileSize) + (j * m.TileSize) + .5 + c.yOffset),
                                                                m.TileSize, m.TileSize), null, Color.White, m.ObjRot[i, j] * -1.5708f, new Vector2((m.ObjectMap[i, j].ObjTexture.Width / 2f), (m.ObjectMap[i, j].ObjTexture.Width / 2f)), SpriteEffects.None, 0);
                            }
                        }
                    }
                    //Draws the Items in the list
                    for (int u = 0; u < Items.Count; u++) {
                        spriteBatch.Draw(Items[u].ItemTexture, PlayerPos.CalcRectangle(c.camPos.X, c.camPos.Y, Items[u].Location.X, Items[u].Location.Y, m.TileSize, c.xOffset, c.yOffset), Color.White);
                    }

                    //draw projectiles
                    for (int i = 0; i < projectiles.Count; i++) {
                        //Updates the projectiles' rectangle property
                        if (projectiles[i] != null) {
                            projectiles[i].rectangle = new Rectangle((int)((c.camPos.X + projectiles[i].Loc.X) * m.TileSize), (int)((c.camPos.Y + projectiles[i].Loc.Y) * m.TileSize), m.TileSize, m.TileSize);
                            spriteBatch.Draw(projectiles[i].EntTexture, projectiles[i].rectangle, null, Color.White, (float)projectiles[i].Direction, new Vector2(projectiles[i].EntTexture.Width / 2f, projectiles[i].EntTexture.Width / 2f), SpriteEffects.None, 0);
                        }
                    }

                    //Draws the temp enemies queued to the sprites list
                    for (int j = 0; j < sprites.Count; j++) {
                        //temp
                        Entity e = sprites.Dequeue();
                        //draw enemy
                        spriteBatch.Draw(e.EntTexture, PlayerPos.CalcRectangle(c.camPos.X, c.camPos.Y, e.Loc.X, e.Loc.Y, m.TileSize, c.xOffset, c.yOffset),
                                                                                null, Color.White, (float)e.Direction, new Vector2(e.EntTexture.Width / 2f, e.EntTexture.Height / 2f), SpriteEffects.None, 0);
                    }
                    //draw the player model
                    spriteBatch.Draw(player.EntTexture, PlayerPos.CalcRectangle(c.camPos.X, c.camPos.Y, player.Loc.X, player.Loc.Y,
                                                                                m.TileSize, c.xOffset, c.yOffset),
                                                                                new Rectangle(player.Frame * 200, player.FrameLevel * 200, 200, 200), Color.White, (float)player.Direction, originPos, SpriteEffects.None, 0);




                    //Draws a spritefont at postion 0,0 on the screen
                    spriteBatch.DrawString(arial, "FPS: " + FPSHandler.AvgFPS, new Vector2(0, 0), Color.Yellow);
                    //Draw the HUD, moved calculations to external static class
                    HUD.DrawHUD(player, ref spriteBatch, screenHeight, screenWidth, arial, health, healthBar, stamina, skillIcons);

                    //play all enqueued sound effects
                    for (int i = 0; i < curSounds.Count; i++) {
                        curSounds.Dequeue().Play(volume, 0f, 0f);
                    }
                    m.sounds.Clear();

                    //Draws if level is clear
                    if (enemies.Count == 0) {
                        spriteBatch.Draw(bar, new Rectangle(0, (int)(screenHeight - arial.MeasureString("Height").Y * 3), (int)(arial.MeasureString("These boys are on the highway to hellll").X), (int)(arial.MeasureString("Height").Y * 3)), Color.Gray);
                        spriteBatch.DrawString(arial, levelInfo[currentLevel - 1, 1], new Vector2(0, (int)(screenHeight - arial.MeasureString("Height").Y * 3)), Color.White);
                        spriteBatch.DrawString(arial, levelInfo[currentLevel - 1, 2], new Vector2(0, (int)(screenHeight - arial.MeasureString("Height").Y * 2)), Color.White);
                        spriteBatch.DrawString(arial, levelInfo[currentLevel - 1, 3], new Vector2(0, (int)(screenHeight - arial.MeasureString("Height").Y)), Color.White);
                    }
                    //add frame to frame counter
                    break;
            }
            //Draw the cursor
            spriteBatch.Draw(cursor, new Rectangle(cursorX - cursor.Width / 2, cursorY - cursor.Height / 2, cursor.Width, cursor.Height), Color.Magenta);
            spriteBatch.End();
            //update current fps sample
            FPSHandler.frames++;

            base.Draw(gameTime);
        }

        protected void LoadGame() {
            //HUD content
            healthBar = Content.Load<Texture2D>("healthBar");
            health = Content.Load<Texture2D>("health");
            stamina = Content.Load<Texture2D>("stamina");
            bar = Content.Load<Texture2D>("bar");
            skillIcons = new Texture2D[3];
            skillIcons[0] = Content.Load<Texture2D>("OVC_Icon");
            skillIcons[1] = Content.Load<Texture2D>("PERSV_Icon");
            skillIcons[2] = Content.Load<Texture2D>("RHNO_Icon");
            //Loads in the arial font file
            arial = Content.Load<SpriteFont>("Arial20Bold");

            //adds and loads the first soundeffect object object, a gunshot
            //The gunshot file is a public domain file
            soundEffects.Add("gunshot", Content.Load<SoundEffect>("gunshot"));
            soundEffects.Add("emptyClick", Content.Load<SoundEffect>("emptyclick"));

            //create default tilebounds object
            tb = new TileBounds();

            //create map and pass in contentmanager
            //m = new Map(Content, screenWidth, screenHeight);

            //m = new Map(Content, "TestMap.dat", c, player, enemies, screenWidth);
            //creates the player (test texture)
            player = new Character(Content, 10, 10, 0, "Pistol_Player", true, new Rectangle(0, 0, 0, 0));
            player.Health = 10;
            player.MaxHealth = 10;
            //creates the currently pending sound queue
            curSounds = new Queue<SoundEffect>();
            //creates the currently pending entity queue
            sprites = new Queue<Entity>();

            //Create the list of projectiles
            projectiles = new List<Projectile>();

            //load up initial camera
            c = new Camera(screenHeight, screenWidth);
            player.Loc.Y = (c.camPos.Y + (screenHeight / 2)) / (screenWidth / 20);
            player.Loc.X = (c.camPos.X + (screenWidth / 2)) / (screenWidth / 20);
            enemies = new List<Enemy>();
            //LOAD MAP HERE______

            m = new Map(Content, "level4.dat", c, player, enemies, screenWidth);
            player.IsPlayer = true;

            //movement object, set max velocity and acceleration here
            double maxVelocity = (4.7 / m.TileSize);
            double acceleration = ((80.0 / m.TileSize) / 1000);
            movement = new Movement(maxVelocity, acceleration, m.TileSize);

            //use this.Content to load your game content here
            //Sets up the origin postion based off the rectangle position
            originPos = new Vector2(player.EntTexture.Width / 2f, player.EntTexture.Width / 2f);

            //Creates temp enemy
            Items = new List<PickUpItem>();
            //Creates enemies to check
            // CreateEnemy.CreateNormalEnemy(ref enemies, Content, c, m, 4, 1);
            //CreateEnemy.CreateNormalEnemy(ref enemies, Content, c, m, 8, 1);
            //CreateEnemy.CreateNormalEnemy(ref enemies, Content, c, m, 12, 1);
            //CreateEnemy.CreateRiotEnemy(ref enemies, Content, c, m, 16, 1);
            //Creates the weapons for the player
            Shooting.CreateWeapons(Content);
            SkillSystem.CreateSkills(Content, player);
            //Starts the player with the first weapon
            player.Weapon = Shooting.weapons[1];
            SkillSystem.skills[0].Obtained = true;
            SkillSystem.skills[1].Obtained = true;
            SkillSystem.skills[2].Obtained = true;
            currentLevel = 1;
            //Creates the names of the levels
            StreamReader levelLoader = new StreamReader("../../../Content/levelInfo.txt");
            levelInfo = new string[8,4];
            for(int i = 0; i < levelInfo.GetLength(0); i++) {
                for(int j = 0; j < levelInfo.GetLength(1); j++) {
                    levelInfo[i, j] = levelLoader.ReadLine();
                }
            }
            levelLoader.Close();
            deathSound = Content.Load<SoundEffect>("deathSound");
            player.EntTexture = Content.Load<Texture2D>("Player_Sheet");
            g.gameState = "StartMenu";
        }
    }
}
