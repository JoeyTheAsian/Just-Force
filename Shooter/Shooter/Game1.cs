//ONLY USE LIBRARIES YOU NEED TO USE TO REDUCE RAM USAGE AND INITIALIZATION TIME
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Shooter.Entities;
using Shooter.MapClasses;
using Shooter.Tools;
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
        List<SoundEffect> soundEffects;

        //A queue for all soundeffects to be played
        private Queue<SoundEffect> curSounds;
        private Queue<Entity> sprites;

        //A keyboard state object to get the keyboard old keyboard state
        KeyboardState oldState;
        MouseState oldMState;

        //FPS related objects
        private FPSHandling FPSHandler = new FPSHandling();

        //Origin vector and rotation variable for rotating the object
        Vector2 originPos;

        //control related objects
        private double Maxvelocity;
        private double curVelocity;
        private double acceleration;

        //Height and width of the monitor
        private int ScreenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        private int ScreenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;

        //ParentConvertor object, converts child objects of entity to entity objects for drawing to the screen
        ParentConvertor convertor = new ParentConvertor();

        //TEMPORARY ASSET OBJECTS________________________________________________________________

        private Character player;
        private Map m;
        private Coord global;
        private TileBounds tb;

        private Gamestate gamestate;
        private Thread backgroundThread;

        private Texture2D startButton;
        private Vector2 startButtonPosition;
        private Texture2D exitButton;
        //connor's menu implementation_____________________________________________
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

            //enable mouse pointer
            IsMouseVisible = true;

            //set position of start button
            startButtonPosition = new Vector2((ScreenWidth / 2) - 150, 200);
            exitButtonPosition = new Vector2((ScreenWidth / 2) - 290, 400);

            //set game to start on start menu
            gamestate = Gamestate.StartMenu;

            base.Initialize();

            //set the game update rate to 120 hz
            base.TargetElapsedTime = System.TimeSpan.FromSeconds(1.0f / 120.0f);
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

            //set global coordinates to default (this would be the starting point in the game)
            global = new Coord(0,0);

            player.Loc.Y = player.Loc.Y = global.Y + (ScreenHeight / 2) / m.TileSize;
            player.Loc.X = player.Loc.X = global.X + (ScreenWidth / 2) / m.TileSize;
            Maxvelocity = 20.0 / m.TileSize;
            acceleration = ((25.0 / m.TileSize) / 1000);
            //create texture map the same size as map object and copy over textures

            //use this.Content to load your game content here

            //Sets up the origin postion based off the rectangle position
            originPos = new Vector2((int)(((global.X + player.Loc.X) * m.TileSize)), (int)(((global.Y + player.Loc.Y) * m.TileSize)));
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
            if (oldState.IsKeyDown(Keys.Escape) && state.IsKeyUp(Keys.Escape))
                Exit();

            if (oldMState.LeftButton == ButtonState.Pressed && mState.LeftButton == ButtonState.Released) {

                MouseClicked(mState.X, mState.Y);
            }

            if (gamestate == Gamestate.Playing && isloading) {

                LoadGame();
                isloading = false;
            }

            if(gamestate == Gamestate.Loading && !isloading) {

                backgroundThread = new Thread(LoadGame);
                isloading = true;

                backgroundThread.Start();
            }
            //UPDATE LOGIC_____________________________________________________________________________________________________________

            //CONTROLS_____________________________________
            //WASD movement controls

            //______________________W KEY_____________________________
            if (state.IsKeyDown(Keys.W)){

                if ((state.IsKeyDown(Keys.W) == true) && (oldState.IsKeyDown(Keys.W) == false)) {
                    curVelocity = 0;
                }

                if(curVelocity < Maxvelocity) {
                    
                    curVelocity += (gameTime.ElapsedGameTime.Milliseconds * acceleration);
                    global.Y += (curVelocity);
                    player.Loc.Y -= (curVelocity);
                }
            }

            //__________________________________A KEY___________________________________
            if (state.IsKeyDown(Keys.A)) {

                if ((state.IsKeyDown(Keys.A) == true) && (oldState.IsKeyDown(Keys.A) == false)) {
                    curVelocity = 0;
                }

                if (curVelocity < Maxvelocity) {

                    curVelocity += (gameTime.ElapsedGameTime.Milliseconds * acceleration);
                    global.X += (curVelocity);
                    player.Loc.X -= (curVelocity);
                }
            }
            //__________________________________S KEY_____________________________________
            if (state.IsKeyDown(Keys.S)) {

                if ((state.IsKeyDown(Keys.S) == true) && (oldState.IsKeyDown(Keys.S) == false)) {
                    curVelocity = 0;
                }

                if (curVelocity < Maxvelocity) {

                    curVelocity += (gameTime.ElapsedGameTime.Milliseconds * acceleration);
                    global.Y -= (curVelocity);
                    player.Loc.Y += (curVelocity);
                }
            }
            //_______________________________D KEY_______________________
            if (state.IsKeyDown(Keys.D)) {

                if ((state.IsKeyDown(Keys.D) == true) && (oldState.IsKeyDown(Keys.D) == false)) {
                    curVelocity = 0;
                }

                if (curVelocity < Maxvelocity) {

                    curVelocity += (gameTime.ElapsedGameTime.Milliseconds * acceleration);
                    global.X -= (curVelocity);
                    player.Loc.X += (curVelocity);
                }
            }
            else if (state.IsKeyUp(Keys.W) && state.IsKeyUp(Keys.S) && state.IsKeyUp(Keys.A) && state.IsKeyUp(Keys.D) && curVelocity > 0) {
                curVelocity -= gameTime.ElapsedGameTime.Milliseconds * acceleration;

            }
            if (curVelocity < 0) {
                curVelocity = 0;
            }
            /*if (state.IsKeyDown(Keys.A)){

                global.X += MoveFactor;
                player.Loc.X -= MoveFactor;
            }
            if (state.IsKeyDown(Keys.S)){

                global.Y -= MoveFactor;
                player.Loc.Y += MoveFactor;
            }
            if (state.IsKeyDown(Keys.D)){
                global.X -= MoveFactor;
                player.Loc.X += MoveFactor;
            }
            if (state.IsKeyDown(Keys.LeftShift) || state.IsKeyDown(Keys.RightShift)){
                 MoveFactor = Maxvelocity;
            }
            else{
            MoveFactor = 10.0 / m.TileSize;
            }*/

            //Checks to see if the key is just pressed and not held down
            if (oldMState.LeftButton == ButtonState.Pressed && mState.LeftButton == ButtonState.Released){
                //Plays a new instance of the first audio file which is the gunshot
                curSounds.Enqueue(soundEffects[0]);

            }

            //Updates the old state with what the current state is
            oldState = state;
            oldMState = mState;

            //Updates the rotation position by getting the angle between two points
            player.Direction = (double)Math.Atan2(mState.Y - originPos.Y, mState.X - originPos.X);

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
        //___________________________________________________________________________
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

            //draw in the start menu
            if(gamestate == Gamestate.StartMenu) {
                spriteBatch.Draw(startButton, startButtonPosition, Color.White);
                spriteBatch.Draw(exitButton, exitButtonPosition, Color.White);
            }

            if(gamestate == Gamestate.Loading) {

                spriteBatch.Draw(loadscreen, new Vector2((ScreenWidth / 2) - (loadscreen.Width / 2), (ScreenHeight / 2) - (loadscreen.Height / 2)), Color.YellowGreen);
            }

            //draw the game while it is being played
            if (gamestate == Gamestate.Playing) {

                //use Tilebounds findBounds method to find the tiles that are actually in the game window, pass in all the values it needs to calculate
                tb.findBounds(global.X, global.Y, m.TileSize, m.TileMap.GetLength(0), m.TileMap.GetLength(1), ScreenWidth, ScreenHeight);

                //draw the TileMap THIS MUST COME FIRST__________________________________________________________________________
                //loop through only the tiles that are actually in the window with bounds in tilebounds object
                for (int i = tb.Xmin; i < tb.Xmax; i++) {
                    for (int j = tb.Ymin; j < tb.Ymax; j++) {
                        //draw the tile
                        spriteBatch.Draw(m.TileMap[i, j],
                                        //Width value and Height values are translated to pixel units + the position of the tile on the actual gridmap + .5 to account for rounding errors
                                        new Rectangle((int)((global.X * m.TileSize) + (i * m.TileSize) + .5),
                                                      (int)((global.Y * m.TileSize) + (j * m.TileSize) + .5),
                                                      m.TileSize, m.TileSize), Color.White);
                    }
                }

                //draw entities___________________________________________________________________________________________________
                //draw the player model
                for (int i = 0; i < 1; i++) {
                    spriteBatch.Draw(player.EntTexture, new Rectangle((int)(((global.X + player.Loc.X) * m.TileSize)), (int)(((global.Y + player.Loc.Y) * m.TileSize)), m.TileSize, m.TileSize), null, Color.White, (float)player.Direction, originPos, SpriteEffects.None, 0);
                }

                //Draws a spritefont at postion 0,0 on the screen
                spriteBatch.DrawString(arial, "FPS: " + FPSHandler.AvgFPS + " " + originPos, new Vector2(0, 0), Color.Yellow);

                //play all enqueued sound effects
                for (int i = 0; i < curSounds.Count; i++) {
                    curSounds.Dequeue().Play();
                }
                //add frame to frame counter
                FPSHandler.frames++;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        void LoadGame() {
            
            //wait one seconds
            Thread.Sleep(1000);

            //start playing game
            gamestate = Gamestate.Playing;
            isloading = true;

        }

       /* public void LoadGame() {
            startButtonPosition = new Vector2
        }*/
    }
}
