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
        protected ContentManager cont;


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

        public Character(ContentManager content) : base(content) {
            loc = new Coord();
            entTexture = content.Load<Texture2D>("NoTexture");
            collision = false;

            //default character creates default weapon which is a pistol
            weapon = new Weapon(content);
            //Set Health
            maxHealth = 1;
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
            health = 1;
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
            health = 1;
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
    }

}

