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
        public Texture2D startButton;
        public Rectangle startButtonPosition;
        //public Texture2D ResumeButton;
        //public Rectangle ResumeButtonPosition;
        public Texture2D exitButton;
        public Rectangle exitButtonPosition;
        public Texture2D loadScreen;
        public Vector2 loadScreenPos;

        public Texture2D optionsButton;
        public Rectangle optionsButtonPosition;
        public Texture2D levelSelectButton;
        public Rectangle levelSelectButtonPosition;
        public Texture2D soundsButton;
        public Rectangle soundsButtonPosition;
        public Texture2D graphicsButton;
        public Rectangle graphicsButtonPosition;
        public Texture2D backButton;
        public Rectangle backButtonPosition;
        public Texture2D resumeButton;
        public Rectangle resumeButtonPosition;

        public Texture2D startMenuBackground;

        public List<string> states;
        public bool isLoading = false;
        public string gameState;
        public string lastState; // holds whether the player went to options from pause menu or main menu
        //define attributes

        public GameStateManager(int screenWidth, int screenHeight, ContentManager content) {
            states = new List<string>();
            states.Add("LOADING");
            states.Add("PLAYING");
            states.Add("PAUSED");
            states.Add("STARTMENU");
            states.Add("OPTIONSMENU");
            states.Add("LEVELSELECT");
            states.Add("SOUNDSMENU");
            states.Add("GRAPHICSMENU");
            gameState = "";

            // main menu buttons and their positions
            startButton = content.Load<Texture2D>("startButton");
            exitButton = content.Load<Texture2D>("exitButton");
            loadScreen = content.Load<Texture2D>("loadinggraphic");
            levelSelectButton = content.Load<Texture2D>("levelSelect");
            startMenuBackground = content.Load<Texture2D>("startMenuBackground");

            startButtonPosition = new Rectangle(screenWidth / 2 - screenWidth / 10, screenHeight * 3 / 11, screenWidth / 5, screenHeight / 8);
            exitButtonPosition = new Rectangle(screenWidth / 2 - screenWidth / 10, screenHeight * 9 / 11, screenWidth / 5, screenHeight / 8);
            levelSelectButtonPosition = new Rectangle(screenWidth / 2 - screenWidth / 10, screenHeight * 5 / 11, screenWidth / 5, screenHeight / 8);
            loadScreenPos = new Vector2((screenWidth / 2) - (loadScreen.Width / 2), (screenHeight / 2) - (loadScreen.Height / 2));

            // initialise buttons for options menu and their positions
            optionsButton = content.Load<Texture2D>("Options");
            soundsButton = content.Load<Texture2D>("Sounds");
            graphicsButton = content.Load<Texture2D>("Graphics");
            backButton = content.Load<Texture2D>("Back");

            optionsButtonPosition = new Rectangle(screenWidth / 2 - screenWidth / 10, screenHeight * 7 / 11, screenWidth / 5, screenHeight / 8);
            soundsButtonPosition = new Rectangle(screenWidth / 2 - screenWidth / 10, screenHeight * 4 / 10, screenWidth / 5, screenHeight / 8);
            graphicsButtonPosition = new Rectangle(screenWidth / 2 - screenWidth / 10, screenHeight * 6 / 10, screenWidth / 5, screenHeight / 8);
            backButtonPosition = new Rectangle(screenWidth / 2 - screenWidth / 10, screenHeight * 8 / 10, screenWidth / 5, screenHeight / 8);

            resumeButton = content.Load<Texture2D>("Resume");
            resumeButtonPosition = new Rectangle(screenWidth / 2 - screenWidth / 10, screenHeight * 4 / 10, screenWidth / 5, screenHeight / 8);

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
            } catch(GameStateNotFoundException e) {
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
                // player clicked on options
                else if (mouseClickRect.Intersects(optionsButtonPosition)) {
                    try {
                        gameState = "OptionsMenu";
                        lastState = "StartMenu";
                        CheckGameState();
                    } catch (GameStateNotFoundException e) {
                        Console.WriteLine(e.ToString());
                        gameState = "";
                    }
                } else if (mouseClickRect.Intersects(levelSelectButtonPosition)) {
                    try {
                        gameState = "LevelSelect";
                        lastState = "StartMenu";
                        CheckGameState();
                    } catch (GameStateNotFoundException e) {
                        Console.WriteLine(e.ToString());
                        gameState = "";
                    }
                }
            }
            //level select menu
            else if (gameState == "LevelSelect") {
                // back button clicked
                if (mouseClickRect.Intersects(backButtonPosition)) {
                    try {
                        gameState = lastState;
                        CheckGameState();
                    } catch (GameStateNotFoundException e) {
                        Console.WriteLine(e.ToString());
                        gameState = "";
                        lastState = "";
                    }
                }
            }
            //puased screen
            else if (gameState == "Paused") {
                if (mouseClickRect.Intersects(exitbuttonRect)) {
                    game.Exit();
                } else if (mouseClickRect.Intersects(optionsButtonPosition)) {
                    try {
                        gameState = "OptionsMenu";
                        lastState = "Paused";
                        CheckGameState();
                    } catch (GameStateNotFoundException e) {
                        Console.WriteLine(e.ToString());
                        gameState = "";
                    }
                } else if (mouseClickRect.Intersects(resumeButtonPosition)) {
                    try {
                        gameState = "Playing";
                        CheckGameState();
                    } catch (GameStateNotFoundException e) {
                        Console.WriteLine(e.ToString());
                        gameState = "";
                    }
                }
            }

            //options screen method
            else if (gameState == "OptionsMenu") {
                // back button clicked
                if (mouseClickRect.Intersects(backButtonPosition)) {
                    try {
                        gameState = lastState;
                        CheckGameState();
                    } catch (GameStateNotFoundException e) {
                        Console.WriteLine(e.ToString());
                        gameState = "";
                        lastState = "";
                    }
                }
                // sounds button clicked
                else if (mouseClickRect.Intersects(soundsButtonPosition)) {
                    try {
                        gameState = "SoundsMenu";
                    } catch (GameStateNotFoundException e) {
                        Console.WriteLine(e.ToString());
                        gameState = "";
                    }
                }
                // graphics button clicked
                else if (mouseClickRect.Intersects(graphicsButtonPosition)) {
                    try {
                        gameState = "GraphicsMenu";
                    } catch (GameStateNotFoundException e) {
                        Console.WriteLine(e.ToString());
                        gameState = "";
                    }
                }
            }

            // Sounds menu
            else if (gameState == "SoundsMenu") {
                if (mouseClickRect.Intersects(backButtonPosition)) {
                    try {
                        gameState = "OptionsMenu";
                    } catch (GameStateNotFoundException e) {
                        Console.WriteLine(e.ToString());
                        gameState = "";
                    }
                }
            }

            // graphics menu
            else if (gameState == "GraphicsMenu") {
                if (mouseClickRect.Intersects(backButtonPosition)) {
                    try {
                        gameState = "OptionsMenu";
                    } catch (GameStateNotFoundException e) {
                        Console.WriteLine(e.ToString());
                        gameState = "";
                    }
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
