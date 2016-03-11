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
     class GameStateManager {

        //attributes
        public Thread backgroundThread;
        public Texture2D startButton;
        public Rectangle startButtonPosition;
        //public Texture2D ResumeButton;
        //public Rectangle ResumeButtonPosition;
        public Texture2D exitButton;
        public Rectangle exitButtonPosition;
        public Texture2D loadScreen;
        public Vector2 loadScreenPos;

        public Texture2D startMenuBackground;

        public List<string> states;
        public bool isLoading = false;
        public string gameState;
        //define attributes

        public GameStateManager(int screenWidth, int screenHeight, ContentManager content) {
            states = new List<string>();
            states.Add("LOADING");
            states.Add("PLAYING");
            states.Add("PAUSED");
            states.Add("STARTMENU");
            gameState = "";
            startButton = content.Load<Texture2D>("startButton");
            exitButton = content.Load<Texture2D>("exitButton");
            loadScreen = content.Load<Texture2D>("loadinggraphic");
            startMenuBackground = content.Load<Texture2D>("startMenuBackground");
            startButtonPosition = new Rectangle(screenWidth / 2 - screenWidth / 10, screenHeight * 4 / 10, screenWidth / 5, screenHeight / 8);
            exitButtonPosition = new Rectangle(screenWidth / 2 - screenWidth / 10, screenHeight * 6 / 10, screenWidth / 5, screenHeight / 8);
            loadScreenPos = new Vector2((screenWidth / 2) - (loadScreen.Width / 2), (screenHeight / 2) - (loadScreen.Height / 2));
            
        }
        public void CheckGameState() {
            if (states.Contains(gameState.ToUpper()) == false) {
                throw (new GameStateNotFoundException(gameState));
            } 
        }
        //draw load screen
        public void DrawLoad(SpriteBatch sb) {
            sb.Draw(loadScreen, loadScreenPos, Color.Cyan);
        }
        public void StartGame() {
            try {
                gameState = "Playing";
                CheckGameState();
            }catch(GameStateNotFoundException e) {
                Console.WriteLine(e.ToString());
                gameState = "";
            }
            isLoading = true;
        }
        //method for mouse on main menu
        public Rectangle MouseClicked(int x, int y, Game1 game) {
            Rectangle mouseClickRect = new Rectangle(x, y, 1, 1);
            Rectangle startbuttonRect = new Rectangle((int)startButtonPosition.X, (int)startButtonPosition.Y, 300, 108);
            Rectangle exitbuttonRect = new Rectangle((int)exitButtonPosition.X, (int)exitButtonPosition.Y, 600, 192);

            if (gameState == "StartMenu") {

                //player clicks start
                if (mouseClickRect.Intersects(startbuttonRect)) {
                    try {
                        gameState = "Loading";
                        CheckGameState();
                    } catch (GameStateNotFoundException e) {
                        Console.WriteLine(e.ToString());
                        gameState = "";
                    }

                    isLoading = true;
                }
                //player exits game
                else if (mouseClickRect.Intersects(exitbuttonRect)) {
                    game.Exit();
                }
            }

            //puased screen method
            if (gameState == "Paused") {
                if (mouseClickRect.Intersects(exitbuttonRect)) {
                    game.Exit();
                }
            }
            return mouseClickRect;
        }

        public bool updateState(KeyboardState State, KeyboardState oldState) {
            if (gameState == "Playing" && State.IsKeyDown(Keys.Escape) && oldState.IsKeyUp(Keys.Escape)) {
                try {
                    gameState = "Paused";
                    CheckGameState();
                } catch (GameStateNotFoundException e) {
                    Console.WriteLine(e.ToString());
                    gameState = "";
                }
                return false;
            }
            else if (gameState == "Paused") {
                if (State.IsKeyDown(Keys.Escape)&& oldState.IsKeyUp(Keys.Escape)) {
                    try {
                        gameState = "Playing";
                        CheckGameState();
                    } catch (GameStateNotFoundException e) {
                        Console.WriteLine(e.ToString());
                        gameState = "";
                    }
                }
                return false;
            }
            if (gameState == "Loading" && isLoading) {
                isLoading = true;
                return true;
            }
            return false;
        }
    }
}
