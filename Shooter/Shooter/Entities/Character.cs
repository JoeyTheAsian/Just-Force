using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Shooter.MapClasses;
using Shooter.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace Shooter.Entities {
    class Character : Entity {
        protected int health;
        protected int maxHealth;
        protected double stamina;
        protected Weapon weapon;
        protected bool isSprinting;
        protected bool isMeleeing;
        protected ContentManager cont;
        //Animation Variables
        protected int numOfFrames = 9;
        protected int framesElapsed;
        protected int timePerFrame = 100;
        protected int frame = 0;
        protected int frameLevel = 1;


        //properties
        public int Health {
            get { return health; }
            set { health = value; }
        }
        public int MaxHealth {
            get { return maxHealth; }
            set { maxHealth = value; }
        }
        public double Stamina {
            get { return stamina; }
            set { stamina = value; }
        }
        public Weapon Weapon {
            get { return weapon; }
            set { weapon = value; }
        }

        public bool IsSprinting {
            get { return isSprinting; }
            set { isSprinting = value; }
        }

        public bool IsMeleeing {
            get { return isMeleeing; }
            set { isMeleeing = value; }
        }

        public int NumOfFrames {
            get { return numOfFrames; }
            set { numOfFrames = value; }
        }

        public int FramesElapsed {
            get { return framesElapsed; }
            set { framesElapsed = value; }
        }

        public int TimePerFrame {
            get { return timePerFrame; }
            set { timePerFrame = value; }
        }

        public int Frame {
            get { return frame; }
            set { frame = value; }
        }

        public int FrameLevel {
            get { return frameLevel; }
            set { frameLevel = value; }
        }

        public Character(ContentManager content) : base(content) {
            loc = new Coord();
            entTexture = content.Load<Texture2D>("NoTexture");
            collision = false;

            //default character creates default weapon which is a pistol
            weapon = new Weapon(content);
            //Set Health
            maxHealth = 20;
            health = maxHealth;
            //set stamina
            stamina = 100;
            cont = content;
        }

        public Character(ContentManager content, double x, double y, string t, Rectangle r) : base(content, x, y, t, r) {
            //try to set texture to specified name
            try {
                entTexture = content.Load<Texture2D>(t);
            } catch (FileNotFoundException) {
                entTexture = content.Load<Texture2D>("NoTexture");
                Console.WriteLine(t + "Not found. Using default texture.");
            }
            //set coordinates
            loc.X = x;
            loc.Y = y;
            direction = 0.0;

            //non-collidable object by default
            collision = false;

            //Set health
            health = 20;
            maxHealth = health;

            //set stamina
            stamina = 100;

            cont = content;
        }

        public Character(ContentManager content, double x, double y, double dir, string t, bool c, Rectangle r) : base(content, x, y, t, r) {
            try {
                entTexture = content.Load<Texture2D>(t);
            } catch (FileNotFoundException) {
                entTexture = content.Load<Texture2D>("NoTexture");
                Console.WriteLine(t + "Not found. Using default texture.");
            }

            //Set health
            health = 20;
            maxHealth = health;

            //set stamina
            stamina = 100;
            cont = content;
            loc.X = x;
            loc.Y = y;
            collision = c;
            if (dir > 360 || dir < 0) {
                direction = 0;
            } else {
                direction = dir;
            }
        }

        public bool CheckHealth() {
            //True if the target is still alive
            if (health > 0) {
                return true;
            }
            //False if the target is dead
            return false;
        }

        public bool CheckStamina() {
            //Return true if stamina is over 0
            if (stamina > 0) {
                return true;
            }
            //Else return false
            return false;
        }

        public void UpdateStamina(int time) {
            //Sets the stamina to a max of 100
            if (stamina >= 100) {
                stamina = 100;
            }
            //checks to see if the stamina is not max, that the player is not sprinting
            if (stamina < 100 && !isSprinting) {
                //Increases stamina
                stamina += time / 2500.0;
                //Removes stamina if the player is sprinting and if stamina is postive
            } else if (isSprinting && stamina > 0) {
                stamina -= time / 4000.0;
                //If the charge delay is active then decrements it
            }
        }

        public void UpdateAnimation(double time) {
            //If the player is meleeing do the animation
            if (isMeleeing) {
                framesElapsed = (int)(time / timePerFrame);
                frame = framesElapsed % numOfFrames;
                //If the player has finished the animation then break out of melee
                if (frame > numOfFrames - 2) {
                    isMeleeing = false;
                    frameLevel = weapon.Level;
                    frame = 0;
                }
            } else if (weapon.isReloading) {
                framesElapsed = (int)(time / timePerFrame);
                frame = framesElapsed % numOfFrames;

                if (frame > numOfFrames - 2) {
                    weapon.isReloading = false;
                    frame = 0;
                }
            }
        }
    }

}

