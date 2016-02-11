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


namespace Shooter {
    ///main type for the game

    public class Game1 : Game {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Creates a new spritefont
        SpriteFont arial;

        //A list for all of the soundeffects so we can add more in as we go
        List<SoundEffect> soundEffects;

        //A keyboard state object to get the keyboard old keyboard state
        KeyboardState oldState;

        //FPS related objects
        private FPSHandling FPSHandler = new FPSHandling();

        //Origin vector and rotation variable for rotating the object
        Vector2 originPos;
        float rotate;

        //control related objects
        private double MoveFactor;
        private double Maxvelocity;

        //Height and width of the monitor
        private int ScreenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        private int ScreenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;

        //TEMPORARY ASSET OBJECTS________________________________________________________________
        private Entity sprite;
        private Rectangle r = new Rectangle(0, 0, 200, 200);
        private Map m;
        private Coord global;
        private TileBounds tb;
        //_______________________________________________________________________________________

        //game time
        protected double time;

        public Game1() {

            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;

            //Initializes the list of sound effects
            soundEffects = new List<SoundEffect>();

            //Initializes the keyboard state object
            oldState = Keyboard.GetState();

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
            sprite = new Entity(Content);

            //Loads in the arial font file
            arial = Content.Load<SpriteFont>("Arial20Bold");

            //adds and loads the first soundeffect object object, a gunshot
            //The gunshot file is a public domain file
            soundEffects.Add(Content.Load<SoundEffect>("gunshot"));

            //create default tilebounds object
            tb = new TileBounds();

            //create map and pass in contentmanager
            m = new Map(Content);

            //set global coordinates to default (this would be the starting point in the game)
            global = new Coord(0,0);

            sprite.Loc.Y = sprite.Loc.Y = global.Y + (ScreenHeight / 2) / m.TileSize;
            sprite.Loc.X = sprite.Loc.X = global.X + (ScreenWidth / 2) / m.TileSize;
            MoveFactor = 10.0 / m.TileSize; //Pixels moved over tilesize
            Maxvelocity = 20.0 / m.TileSize;
            //create texture map the same size as map object and copy over textures

            //use this.Content to load your game content here

            //Sets up the origin postion based off the rectangle position
            originPos = new Vector2((int)(((global.X + sprite.Loc.X) * m.TileSize)), (int)(((global.Y + sprite.Loc.Y) * m.TileSize)));
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

            //UPDATE LOGIC_____________________________________________________________________________________________________________

            //CONTROLS_____________________________________
            //WASD movement controls
                if (oldState.IsKeyDown(Keys.W))
                {

                    global.Y += MoveFactor;
                    sprite.Loc.Y -= MoveFactor;

                }
                if (oldState.IsKeyDown(Keys.A))
                {

                    global.X += MoveFactor;
                    sprite.Loc.X -= MoveFactor;

                }
                if (oldState.IsKeyDown(Keys.S))
                {

                    global.Y -= MoveFactor;
                    sprite.Loc.Y += MoveFactor;

                }
                if (oldState.IsKeyDown(Keys.D))
                {

                    global.X -= MoveFactor;
                    sprite.Loc.X += MoveFactor;

                }
                if (oldState.IsKeyDown(Keys.LeftShift) || oldState.IsKeyDown(Keys.RightShift))
                {
                     MoveFactor = Maxvelocity;
                }
                else
                {
                MoveFactor = 10.0 / m.TileSize;
                }
            //Checks to see if the key is just pressed and not held down
            if (state.IsKeyDown(Keys.X) && oldState.IsKeyUp(Keys.X))
            {
                //Plays a new instance of the first audio file which is the gunshot
                soundEffects[0].CreateInstance().Play();
            }

            //Updates the old state with what the current state is
            oldState = state;

            //Updates the rotation position by getting the angle between two points
            rotate = (float)Math.Atan2(mState.Y - originPos.Y, mState.X - originPos.X);

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

            //use Tilebounds findBounds method to find the tiles that are actually in the game window, pass in all the values it needs to calculate
            tb.findBounds(global.X, global.Y, m.TileSize, m.TileMap.GetLength(0), m.TileMap.GetLength(1) , ScreenWidth, ScreenHeight);

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
            for (int i = 0; i < 1; i++){
                spriteBatch.Draw(sprite.EntTexture, new Rectangle((int)(((global.X + sprite.Loc.X) * m.TileSize)), (int)(((global.Y + sprite.Loc.Y) * m.TileSize)), m.TileSize, m.TileSize), null, Color.White, rotate, originPos, SpriteEffects.None, 0);
            }

            //Draws a spritefont at postion 0,0 on the screen
            spriteBatch.DrawString(arial, "FPS: " + FPSHandler.AvgFPS + " " + originPos, new Vector2(0, 0), Color.Yellow);

            //add frame to frame counter
            FPSHandler.frames++;
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
