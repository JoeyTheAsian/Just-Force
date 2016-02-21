using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Shooter.GameStates {
     class GameStateClass {

        //attributes
        public Game1 game = new Game1();
        public Gamestate gamestate;
        public Thread backgroundThread;
        public Texture2D startButton;
        public Vector2 startButtonPosition;
        public Texture2D exitButton;
        public Vector2 exitButtonPosition;
        public Texture2D loadscreen;
        public Vector2 loadscreenPos;
        public bool isloading = false;
        public enum Gamestate {
            StartMenu,
            Loading,
            Playing,
            Paused
        }

        //define attributes

        public GameStateClass(int screenWidth, int screenHeight, ContentManager content) {
            startButton = content.Load<Texture2D>("button-start");
            exitButton = content.Load<Texture2D>("exit-button");
            loadscreen = content.Load<Texture2D>("loadinggraphic");
            startButtonPosition = new Vector2((screenWidth / 2) - 150, 200);
            exitButtonPosition = new Vector2((screenWidth / 2) - 290, 400);
            loadscreenPos = new Vector2((screenWidth / 2) - (loadscreen.Width / 2), (screenHeight / 2) - (loadscreen.Height / 2));
            
        }
        
        //draw load screen
        public static void DrawLoad(SpriteBatch sb, Texture2D load, Vector2 loadpos) {
            sb.Draw(load, loadpos, Color.Cyan);
        }
        public static void LoadGame(Gamestate gameState, bool isload) {
            gameState = Gamestate.Playing;
            isload = true;
        }
        //method for mouse on main menu
        public static Rectangle MouseClicked(int x, int y, Gamestate gameState, Vector2 start, Vector2 exit, bool isload) {
            Rectangle mouseClickRect = new Rectangle(x, y, 10, 10);
            if (gameState == Gamestate.StartMenu) {

                

                Rectangle startbuttonRect = new Rectangle((int)start.X, (int)start.Y, 300, 108);
                Rectangle exitbuttonRect = new Rectangle((int)exit.X, (int)exit.Y, 600, 192);

                //player clicks start
                if (mouseClickRect.Intersects(startbuttonRect)) {

                    gameState = Gamestate.Loading;

                    //gameState = Gamestate.Playing;

                    isload = true;
                }
                //player exits game
                else if (mouseClickRect.Intersects(exitbuttonRect)) {
                    Environment.Exit(0);
                }
                
            }
            return mouseClickRect;
        }

        public void updateState(Gamestate gameState, KeyboardState State, bool isload, Thread background) {

            if (gameState == Gamestate.Playing) {
                if (State.IsKeyDown(Keys.Escape)) {
                    gameState = Gamestate.Paused;
                }
            } else if (gameState == Gamestate.Paused) {
                if (State.IsKeyDown(Keys.Escape)) {
                    gameState = Gamestate.Playing;
                }
            }

            if (gameState == Gamestate.Playing && isload) {

                game.LoadGame();
                isload = false;
            }

            if (gameState == Gamestate.Loading && isload) {

                background = new Thread(game.LoadGame);
                isload = true;

                background.Start();
            }
        }
    }
}
