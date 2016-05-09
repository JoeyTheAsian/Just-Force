using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Shooter.Entities;
using Shooter.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace Shooter.GameStates
{
    class GameStateManager
    {

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
        public Texture2D deathBackground;
        public Rectangle controlButtonPosition;
        public Texture2D controlButton;
        public Texture2D controls;
        public Rectangle controlBackButtonPosition;
        public int[] levelClears;

        //Textures for level buttons
        public List<Texture2D> levelIcons;
        //Rectangles for level buttons
        public List<Rectangle> levelRect;

        public Texture2D startMenuBackground;

        public List<string> states;
        public bool isLoading = false;
        public string gameState;
        public string lastState; // holds whether the player went to options from pause menu or main menu
        //define attributes

        public GameStateManager(int screenWidth, int screenHeight, ContentManager content, int curLvl) {
            states = new List<string>();
            states.Add("LOADING");
            states.Add("PLAYING");
            states.Add("PAUSED");
            states.Add("STARTMENU");
            states.Add("OPTIONSMENU");
            states.Add("LEVELSELECT");
            states.Add("SOUNDSMENU");
            states.Add("GRAPHICSMENU");
            states.Add("LEVELSWITCH");
            gameState = "";

            // main menu buttons and their positions
            startButton = content.Load<Texture2D>("startButton");
            exitButton = content.Load<Texture2D>("exitButton");
            loadScreen = content.Load<Texture2D>("loadinggraphic");
            levelSelectButton = content.Load<Texture2D>("levelSelect");
            startMenuBackground = content.Load<Texture2D>("startMenuBackground");
            deathBackground = content.Load<Texture2D>("deathScreen");
            controlButton = content.Load<Texture2D>("controlsButton");

            startButtonPosition = new Rectangle(screenWidth / 2 - screenWidth / 10, screenHeight * 3 / 11, screenWidth / 5, screenHeight / 8);
            exitButtonPosition = new Rectangle(screenWidth / 2 - screenWidth / 10, screenHeight * 9 / 11, screenWidth / 5, screenHeight / 8);
            levelSelectButtonPosition = new Rectangle(screenWidth / 2 - screenWidth / 10, screenHeight * 5 / 11, screenWidth / 5, screenHeight / 8);
            loadScreenPos = new Vector2((screenWidth / 2) - (loadScreen.Width / 2), (screenHeight / 2) - (loadScreen.Height / 2));

            // initialise buttons for options menu and their positions
            optionsButton = content.Load<Texture2D>("Options");
            soundsButton = content.Load<Texture2D>("Sounds");
            graphicsButton = content.Load<Texture2D>("Graphics");
            backButton = content.Load<Texture2D>("Back");
            controls = content.Load<Texture2D>("tutorial");

            optionsButtonPosition = new Rectangle(screenWidth / 2 - screenWidth / 10, screenHeight * 7 / 11, screenWidth / 5, screenHeight / 8);
            controlButtonPosition = new Rectangle(screenWidth / 2 - screenWidth / 10, (int)(screenHeight * 4 / 10), screenWidth / 5, screenHeight / 8);
            soundsButtonPosition = new Rectangle(screenWidth / 2 - screenWidth / 10, (int)(screenHeight * 5.3 / 10), screenWidth / 5, screenHeight / 8);
            graphicsButtonPosition = new Rectangle(screenWidth / 2 - screenWidth / 10, (int)(screenHeight * 6.6 / 10), screenWidth / 5, screenHeight / 8);
            backButtonPosition = new Rectangle(screenWidth / 2 - screenWidth / 10, (int)(screenHeight * 8 / 10), screenWidth / 5, screenHeight / 8);
            controlBackButtonPosition = new Rectangle(screenWidth / 2 - screenWidth / 10, (int)(screenHeight * 9 / 10), screenWidth / 5, screenHeight / 8);

            resumeButton = content.Load<Texture2D>("Resume");
            resumeButtonPosition = new Rectangle(screenWidth / 2 - screenWidth / 10, screenHeight * 4 / 10, screenWidth / 5, screenHeight / 8);

            //Adds the icons
            levelIcons = new List<Texture2D>();
            levelIcons.Add(content.Load <Texture2D>("level1Icon"));
            levelIcons.Add(content.Load<Texture2D>("level2Icon"));
            levelIcons.Add(content.Load<Texture2D>("level3Icon"));

            //Adds the rectangles
            levelRect = new List<Rectangle>();
            levelRect.Add(new Rectangle((screenWidth / 10) + 50, screenHeight * 3 / 10, 350, 200)); //level 1
            levelRect.Add(new Rectangle(screenWidth / 3, screenHeight * 3 / 10, 350, 200)); //level 2
            levelRect.Add(new Rectangle((screenWidth * 4 / 7) - 55, screenHeight * 3 / 10, 350, 200)); //level 3
            levelRect.Add(new Rectangle(screenWidth * 3 / 4, screenHeight * 3 / 10, 350, 200)); //level 4
            levelRect.Add(new Rectangle((screenWidth / 10) + 50, screenHeight * 3 / 5, 350, 200)); //level 5
            levelRect.Add(new Rectangle(screenWidth / 3, screenHeight * 3 / 5, 350, 200)); //level 6
            levelRect.Add(new Rectangle((screenWidth * 4 / 7) - 55, screenHeight * 3 / 5, 350, 200)); //level 7
            levelRect.Add(new Rectangle(screenWidth * 3 / 4, screenHeight * 3 / 5, 350, 200)); //level 8
            levelClears = new int[8];
            levelClears[0] = 1;

            //
            try {
                BinaryReader levelReader = new BinaryReader(File.OpenRead("levelClear.dat"));
                for (int i = 0; i < levelClears.Length; i++) {
                    levelClears[i] = levelReader.ReadInt32();
                }
                levelReader.Close();
            } catch {
                saveLevelClears();
            }
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
            } catch (GameStateNotFoundException e) {
                Console.WriteLine(e.ToString());
                gameState = "";
            }
            isLoading = true;
        }
        //method for mouse on main menu
        public Rectangle MouseClicked(int x, int y, Game1 game, ref int currentLevel, ref List<Enemy> enemies, ref List<PickUpItem> Items, ref List<Projectile> projectiles, ref int timer, ContentManager Content, ref Character player, ref string wepUnl) {
            Rectangle mouseClickRect = new Rectangle(x, y, 1, 1);
            Rectangle startbuttonRect = new Rectangle((int)startButtonPosition.X, (int)startButtonPosition.Y, 300, 108);
            Rectangle exitbuttonRect = new Rectangle((int)exitButtonPosition.X, (int)exitButtonPosition.Y, 600, 192);

            if (gameState == "StartMenu") {
                //player clicks start
                if (mouseClickRect.Intersects(startbuttonRect)) {
                    try {
                        SkillSystem.CreateSkills(Content, player);
                        Shooting.CreateWeapons(Content);
                        player.Health = player.MaxHealth;
                        player.Stamina = 100;
                        wepUnl = "";
                        player.Weapon = Shooting.weapons[1];
                        player.FrameLevel = 1;
                        currentLevel = 1;
                        enemies.Clear();
                        Items.Clear();
                        projectiles.Clear();
                        timer = 0;
                        gameState = "LevelSwitch";
                        CheckGameState();
                    } catch (GameStateNotFoundException e) {
                        Console.WriteLine(e.ToString());
                        gameState = "";
                    }

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
                } else if (mouseClickRect.Intersects(levelRect[0]) || mouseClickRect.Intersects(levelRect[1]) || mouseClickRect.Intersects(levelRect[2])) {
                    Shooting.CreateWeapons(Content);
                    if (mouseClickRect.Intersects(levelRect[0]) && levelClears[0] != 0) {
                        currentLevel = 1;
                        SkillSystem.CreateSkills(Content, player);
                        player.Health = player.MaxHealth;
                        player.Stamina = 100;
                        wepUnl = "";
                        player.Weapon = Shooting.weapons[1];
                        player.FrameLevel = 1;
                        enemies.Clear();
                        Items.Clear();
                        projectiles.Clear();
                        timer = 0;
                        gameState = "LevelSwitch";
                        CheckGameState();
                    } else if (mouseClickRect.Intersects(levelRect[1]) && levelClears[1] != 0) {
                        currentLevel = 2;
                        Shooting.weapons[2].IsAcquired = true;
                        SkillSystem.CreateSkills(Content, player);
                        player.Health = player.MaxHealth;
                        player.Stamina = 100;
                        wepUnl = "";
                        player.Weapon = Shooting.weapons[1];
                        player.FrameLevel = 1;
                        enemies.Clear();
                        Items.Clear();
                        projectiles.Clear();
                        timer = 0;
                        gameState = "LevelSwitch";
                        CheckGameState();
                    } else if (mouseClickRect.Intersects(levelRect[2]) && levelClears[2] != 0) {
                        currentLevel = 3;
                        Shooting.weapons[2].IsAcquired = true;
                        Shooting.weapons[3].IsAcquired = true;
                        SkillSystem.CreateSkills(Content, player);
                        player.Health = player.MaxHealth;
                        player.Stamina = 100;
                        wepUnl = "";
                        player.Weapon = Shooting.weapons[1];
                        player.FrameLevel = 1;
                        enemies.Clear();
                        Items.Clear();
                        projectiles.Clear();
                        timer = 0;
                        gameState = "LevelSwitch";
                        CheckGameState();
                    }
                    
                }
            }
            //Death screen 
            else if (gameState == "Death") {
                if (mouseClickRect.Intersects(exitbuttonRect)) {
                    gameState = "StartMenu";
                    CheckGameState();
                }
            }
            //puased screen
            else if (gameState == "Paused") {
                if (mouseClickRect.Intersects(exitbuttonRect)) {
                    saveLevelClears();
                    gameState = "StartMenu";
                    CheckGameState();
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
                // controls button clicked
                else if (mouseClickRect.Intersects(controlButtonPosition)) {
                    try {
                        gameState = "Controls";
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


            // Controls menu
            else if (gameState == "Controls") {
                if (mouseClickRect.Intersects(controlBackButtonPosition)) {
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
            } else if (gameState == "Paused") {
                if (State.IsKeyDown(Keys.Escape) && oldState.IsKeyUp(Keys.Escape)) {
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

        //Method that saves the current level clears
        public void saveLevelClears() {
                Stream str = File.OpenWrite("levelClear.dat");
                BinaryWriter levelWriter = new BinaryWriter(str);
                for (int i = 0; i < levelClears.Length; i++) {
                    levelWriter.Write(levelClears[i]);
                }
                levelWriter.Close();
            
        }
    }
}
