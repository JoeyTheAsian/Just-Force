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




        //Height and width of the monitor
        private int ScreenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        private int ScreenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;

        //TEMPORARY ASSET OBJECTS________________________________________________________________
        private Entity sprite;
        private Rectangle r = new Rectangle(0, 0, 200, 200);
        private Map m;
        private Coord global;
        private double MoveFactor;
        //_______________________________________________________________________________________

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

            m = new Map(Content);

            global = new Coord(0,0);

            sprite.Loc.Y = sprite.Loc.Y = global.Y + (ScreenHeight / 2) / m.TileSize;
            sprite.Loc.X = sprite.Loc.X = global.X + (ScreenWidth / 2) / m.TileSize;
            MoveFactor = 5.0 / m.TileSize; //Pixels moved over tilesize
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
                global.Y += MoveFactor;
                sprite.Loc.Y -= MoveFactor;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A)) {

                global.X += MoveFactor;
                sprite.Loc.X -= MoveFactor;

            }
            if (Keyboard.GetState().IsKeyDown(Keys.S)) {

                global.Y -= MoveFactor;
                sprite.Loc.Y += MoveFactor;

            }
            if (Keyboard.GetState().IsKeyDown(Keys.D)) {

                global.X -= MoveFactor;
                sprite.Loc.X += MoveFactor;

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
                    spriteBatch.Draw(m.TileMap[i, j],
                        new Rectangle((int)((global.X*m.TileSize)+ (i * m.TileSize) + .5), (int)((global.Y*m.TileSize) + (j * m.TileSize) + .5), m.TileSize, m.TileSize), Color.White);
                }
            }
            //draw entities___________________________________________________________________________________________________
            //draw the temporary player 
            for (int i = 0; i < 1; i++){
                spriteBatch.Draw(sprite.EntTexture, new Rectangle((int)(((global.X + sprite.Loc.X) * m.TileSize)), (int)(((global.Y + sprite.Loc.Y) * m.TileSize)), m.TileSize, m.TileSize), Color.White);
            }
            Console.WriteLine(global.X + "," + global.Y);
            Console.WriteLine(sprite.Loc.X + "," + sprite.Loc.Y);
            //add frame to frame counter
            FPSHandler.frames++;
            spriteBatch.End();

            base.Draw(gameTime);

        }
    }
}
