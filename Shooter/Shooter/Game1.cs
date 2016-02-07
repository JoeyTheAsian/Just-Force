//ONLY USE LIBRARIES YOU NEED TO USE TO REDUCE RAM USAGE AND INITIALIZATION TIME
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Shooter.Entities;
using Shooter.GameMap;
using System;
using System.Collections.Generic;

namespace Shooter {
    ///main type for the game

    public class Game1 : Game {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //FPS related objects
        private FPSHandling FPSHandler = new FPSHandling();


        //TEMPORARY ASSET OBJECTS________________________________________________________________
        private Entity sprite;
        private Rectangle r = new Rectangle(0,0,200,200);
        private Map m = new Map();
        private Texture2D[,] CurrentMap;
        //_______________________________________________________________________________________

        //Height and width of the monitor
        private int ScreenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        private int ScreenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;

        //game time
        protected double time;

        public Game1() {

            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;

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
            //create texture map the same size as map object and copy over textures
            
            //use this.Content to load your game content here
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
            //exit the window with esc key
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //UPDATE LOGIC_____________________________________________________________________________________________________________

            //CONTROLS_____________________________________
            //WASD movement controls
            if (Keyboard.GetState().IsKeyDown(Keys.W)) {
                r.Y -= 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A)) {
                r.X -= 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S)) {
                r.Y += 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D)) {
                r.X += 2;
            }

            //update current fps sample
            if(gameTime.TotalGameTime.TotalMilliseconds % 1000 == 0) {
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
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //drawing code
            spriteBatch.Begin();

            //draw the map THIS MUST COME BEFORE ALL ENTITIES_________________________________________________________________
            for (int i = 0; i < m.TileMap.GetLength(0); i++) {
                for (int j = 0; j < m.TileMap.GetLength(1); j++) {
                    //super sloppy code, just for testing purposes
                    spriteBatch.Draw(Content.Load<Texture2D>(m.TileMap[i, j]),
                        new Rectangle(100 * i, 100 * j, 100, 100), Color.White);
                }
            }
            //draw entities___________________________________________________________________________________________________
            //draw the temporary player 
                spriteBatch.Draw(sprite.EntTexture, r, Color.White);
            //add frame to frame counter
            FPSHandler.frames++;
            spriteBatch.End();

            base.Draw(gameTime);

        }
    }
}
