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
        List<SoundEffect> soundEffects;

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
        private int ScreenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        private int ScreenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;


        //New game console object
        GameConsole consoleTool;

        //TEMPORARY ASSET OBJECTS________________________________________________________________

        private Character player;
        private Map m;
        private Coord global;
        private TileBounds tb;
        //screenshake bool
        private bool screenShake = false;
        private int shakeDur = 0;
        private int xOffset = 0;
        private int yOffset = 0;
        //Temp enemy
        private List<Character> enemies;
        private Character enemy;
        //connor's menu implementation_____________________________________________
        
       // private GameStateClass g;
        //_________________________________________________________________________
        private Gamestate gamestate;
        private Thread backgroundThread;

        private Texture2D startButton;
        private Vector2 startButtonPosition;
        private Texture2D exitButton;

        private Vector2 exitButtonPosition;

        private Texture2D loadscreen;
        private bool isloading = false;
        //_________________________________________________________________________
        enum Gamestate {
            StartMenu,
            Loading,
            Playing,
            Paused
        }
        //_______________________________________________________________________________________

        //game time
        protected double time;
        
        public Game1() {

            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;

            //Initializes the list of sound effects
            soundEffects = new List<SoundEffect>();

            //Initializes the state objects
            oldState = Keyboard.GetState();
            oldMState = Mouse.GetState();

            this.IsMouseVisible = true;
            //set window size to screen size
            graphics.PreferredBackBufferHeight = ScreenHeight;
            graphics.PreferredBackBufferWidth = ScreenWidth;

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
            //g = new GameStateClass(ScreenWidth,ScreenHeight, Content);
            //enable mouse pointer
            IsMouseVisible = true;

            startButtonPosition = new Vector2((ScreenWidth / 2) - 150, 200);
            exitButtonPosition = new Vector2((ScreenWidth / 2) - 290, 400);
            //set game to start on start menu
            gamestate = Gamestate.StartMenu;

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

        
            //load in textures for start and exit buttons 
           startButton = Content.Load<Texture2D>("button-start");
           exitButton = Content.Load<Texture2D>("exit-button");
           loadscreen = Content.Load<Texture2D>("loadinggraphic");

            //Loads in the arial font file
            arial = Content.Load<SpriteFont>("Arial20Bold");

            //adds and loads the first soundeffect object object, a gunshot
            //The gunshot file is a public domain file
            soundEffects.Add(Content.Load<SoundEffect>("gunshot"));

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

            //set global coordinates to default (this would be the starting point in the game)
            global = new Coord(0,0);

            player.Loc.Y = (global.Y + (ScreenHeight / 2)) / m.TileSize;
            player.Loc.X = (global.X + (ScreenWidth / 2)) / m.TileSize;


            //movement object, set max velocity and acceleration here
            double maxVelocity =(10.0 / m.TileSize);
            double acceleration = ((70.0 / m.TileSize) / 1000);
            movement = new Movement(maxVelocity, acceleration);

            //use this.Content to load your game content here
            //Sets up the origin postion based off the rectangle position
            originPos = new Vector2(player.EntTexture.Width / 2f, player.EntTexture.Width / 2f);
            
            //Creates temp enemy
            enemies = new List<Character>();
            enemies.Add(new Character(Content, (global.X + 100) / m.TileSize, (global.Y + 100) / m.TileSize, "NoTexture"));
            enemies.Add(new Character(Content, (global.X + 400) / m.TileSize, (global.Y + 100) / m.TileSize, "NoTexture"));
            enemies.Add(new Character(Content, (global.X + 800) / m.TileSize, (global.Y + 100) / m.TileSize, "NoTexture"));

            //g = new GameStateClass(ScreenWidth, ScreenHeight, Content);
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

            //exit the window with esc key
            //Checks to see if the key is just pressed and not held down
            
            if(oldState.IsKeyDown(Keys.OemTilde) && state.IsKeyUp(Keys.OemTilde))
            {
                consoleTool.OpenInput();
            }
            if (oldMState.LeftButton == ButtonState.Pressed && mState.LeftButton == ButtonState.Released) {

                MouseClicked(mState.X,mState.Y);
                //GameStateClass.MouseClicked(mState.X, mState.Y, g.gamestate, g.startButtonPosition, g.exitButtonPosition, g.isloading);
            }

           // g.updateState(g.gamestate, state, g.isloading, g.backgroundThread);
            if(gamestate == Gamestate.Playing) {
                if (state.IsKeyDown(Keys.Escape)) {
                    gamestate = Gamestate.Paused;
                }
            }else if(gamestate == Gamestate.Paused) {
                if (state.IsKeyDown(Keys.Escape)) {
                    gamestate = Gamestate.Playing;
                }
            }

            if (gamestate == Gamestate.Playing && isloading) {

                LoadGame();
                isloading = false;
            }

            if(gamestate == Gamestate.Loading && isloading) {

                backgroundThread = new Thread(LoadGame);
                isloading = true;

                backgroundThread.Start();
            }
            //UPDATE LOGIC_____________________________________________________________________________________________________________

            //CONTROLS_____________________________________

            //WASD movement controls

            //update sprint
            movement.UpdateSprint(state, oldState, m.TileSize);
            
            //update the current velocity
            XVelocity = movement.UpdateX(XVelocity, gameTime.ElapsedGameTime.Milliseconds, state);
            YVelocity = movement.UpdateY(YVelocity, gameTime.ElapsedGameTime.Milliseconds, state);



            //update the screen & player positions
            player.Loc.X -= XVelocity;
            player.Loc.Y -= YVelocity;

            global.X += XVelocity;
            global.Y += YVelocity;


            //Left mouse button to shoot
            //Checks to see if the key is just pressed and not held down
            if (oldMState.LeftButton == ButtonState.Pressed && mState.LeftButton == ButtonState.Released){
                //Plays a new instance of the first audio file which is the gunshot
                curSounds.Enqueue(soundEffects[0]);
                projectiles.Add(player.Shoot(Content));
                screenShake = true;
            }

            //SCREEN SHAKE 
            if(screenShake == true && shakeDur < 12) {
                xOffset += 2;
                shakeDur += gameTime.ElapsedGameTime.Milliseconds;
            }else if(screenShake == true && shakeDur >= 12 && shakeDur < 37) {
                xOffset -= 2;
                shakeDur += gameTime.ElapsedGameTime.Milliseconds;
            }else if (screenShake == true && shakeDur >= 37 && shakeDur < 50) {
                xOffset += 2;
                shakeDur += gameTime.ElapsedGameTime.Milliseconds;
            }else if (shakeDur >= 50){
                xOffset = 0;
                yOffset = 0;
                screenShake = false;
                shakeDur = 0;
            }

            //updates projectiles and checks collision
            for (int i = 0; i < projectiles.Count; i++) { 
                if(projectiles[i].CheckRange() == true) {
                    projectiles.Remove(projectiles[i]);
                    i--;
                } else {
                    projectiles[i].UpdatePos(gameTime.ElapsedGameTime.Milliseconds, m.TileSize);
                    //Checks if any projectiles collide with any enemies
                    for (int k = 0; k < enemies.Count; k++)
                    {
                        if (projectiles[i].CheckHit(enemies[k]))
                        {
                            projectiles.RemoveAt(i);
                            i--;
                            break;
                        }
                    }
                }
            }

            //Temp: Checks to see if the enemy is hit
            for (int k = 0; k < enemies.Count; k++){
                if (!enemies[k].CheckHealth())
                {
                    enemies.RemoveAt(k);
                    k--;
                }
            }
            
            //Updates the rotation position by getting the angle between two points
            player.Direction = PlayerPos.CalcDirection(mState.X, mState.Y, global.X, global.Y, player.Loc.X, player.Loc.Y, m.TileSize);
            
            //Updates the old state with what the current state is
            oldState = state;
            oldMState = mState;

            //Enqueue player to be rendered
            sprites.Enqueue(player);

            //update current fps sample
            if (gameTime.TotalGameTime.TotalMilliseconds % 1000 == 0) {
                FPSHandler.AddSample(FPSHandler.frames);
                FPSHandler.frames = 0;
                //update FPS
                FPSHandler.UpdateFPS();
            }
        }

        //___________________________________________________________________________MOVE THIS CODE TO AN EXTERNAL CLASS
        //method for mouse on main menu
        void MouseClicked(int x, int y) {

            if (gamestate == Gamestate.StartMenu) {

                Rectangle mouseClickRect = new Rectangle(x, y, 10, 10);

                Rectangle startbuttonRect = new Rectangle((int)startButtonPosition.X, (int)startButtonPosition.Y, 300, 108);
                Rectangle exitbuttonRect = new Rectangle((int)exitButtonPosition.X, (int)exitButtonPosition.Y, 600, 192);

                //player clicks start
                if (mouseClickRect.Intersects(startbuttonRect)) {

                    // gamestate = Gamestate.Loading;

                    gamestate = Gamestate.Playing;

                    isloading = true;
                }
                //player exits game
                else if (mouseClickRect.Intersects(exitbuttonRect)) {
                    Exit();
                }
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

            switch (gamestate) {
//____________________DRAW START MENU____________________________________________________________________
                case Gamestate.StartMenu:
                    spriteBatch.Draw(startButton, startButtonPosition, Color.White);
                    spriteBatch.Draw(exitButton, exitButtonPosition, Color.White);
                    break;
//____________________DRAW LOAD SCREEN____________________________________________________________________
                case Gamestate.Loading:
                    //GameStateClass.DrawLoad(spriteBatch, g.loadscreen, g.loadscreenPos);
                    spriteBatch.Draw(loadscreen, new Vector2((ScreenWidth / 2) - (loadscreen.Width / 2), (ScreenHeight / 2) - (loadscreen.Height / 2)), Color.Cyan);
                    break;
//____________________DRAW PAUSE MENU____________________________________________________________________
                case Gamestate.Paused:
                    GraphicsDevice.Clear(Color.Blue);
                    break;
//____________________DRAW GAME ASSETS____________________________________________________________________
                case Gamestate.Playing:
                    //use Tilebounds findBounds method to find the tiles that are actually in the game window, pass in all the values it needs to calculate
                    tb.findBounds(global.X, global.Y, m.TileSize, m.TileMap.GetLength(0), m.TileMap.GetLength(1), ScreenWidth, ScreenHeight);

                    //draw the TileMap THIS MUST COME FIRST__________________________________________________________________________
                    //loop through only the tiles that are actually in the window with bounds in tilebounds object
                    for (int i = tb.Xmin; i < tb.Xmax; i++) {
                        for (int j = tb.Ymin; j < tb.Ymax; j++) {
                            //draw the tile
                            spriteBatch.Draw(m.TileMap[i, j],
                                            //Width value and Height values are translated to pixel units + the position of the tile on the actual gridmap + .5 to account for rounding errors
                                            new Rectangle((int)((global.X * m.TileSize) + (i * m.TileSize) + .5 + xOffset),
                                                          (int)((global.Y * m.TileSize) + (j * m.TileSize) + .5 + yOffset),
                                                          m.TileSize, m.TileSize), Color.White);
                        }
                    }

                    //draw entities___________________________________________________________________________________________________
                    for (int i = 0; i < projectiles.Count; i++) {
                        spriteBatch.Draw(projectiles[i].EntTexture, new Rectangle((int)((global.X + projectiles[i].Loc.X) * m.TileSize), (int)((global.Y + projectiles[i].Loc.Y) * m.TileSize), m.TileSize, m.TileSize), null, Color.White, (float)projectiles[i].Direction, new Vector2(projectiles[i].EntTexture.Width / 2f, projectiles[i].EntTexture.Width / 2f), SpriteEffects.None, 0);
                    }

                    //Draws the temp enemy
                    for (int k = 0; k < enemies.Count; k++) {
                        spriteBatch.Draw(enemies[k].EntTexture, PlayerPos.CalcRectangle(global.X, global.Y, enemies[k].Loc.X, enemies[k].Loc.Y, m.TileSize, xOffset, yOffset), Color.White);
                    }

                    //draw the player model
                    spriteBatch.Draw(player.EntTexture, PlayerPos.CalcRectangle(global.X, global.Y, player.Loc.X, player.Loc.Y, m.TileSize, xOffset, yOffset), null, Color.White, (float)player.Direction, originPos, SpriteEffects.None, 0);

                    //Draws a spritefont at postion 0,0 on the screen
                    spriteBatch.DrawString(arial, "FPS: " + FPSHandler.AvgFPS, new Vector2(0, 0), Color.Yellow);

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

        public void LoadGame() {
            //start playing game
            
            gamestate = Gamestate.Playing;
            isloading = true;

        }
    }
}
