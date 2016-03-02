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
        private int health;
        private int maxHealth;
        private int stamina;
        private Weapon weapon;

        //properties
        public int Health {
            get { return health; }
            set { health = value; }
        }
        public int MaxHealth {
            get { return maxHealth; }
            set { maxHealth = value; }
        }
        public int Stamina {
            get { return stamina; }
            set { stamina = value; }
        }
        public Weapon Weapon {
            get {
                return weapon;
            }
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
            stamina = 0;
        }

        public Character(ContentManager content, double x, double y, string t, Rectangle r) : base(content, x, y, t, r)
        {
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
        }

        public Character(ContentManager content, double x, double y, double dir, string t, bool c, Rectangle r) : base(content, x, y, t, r)
        {
            try {
                entTexture = content.Load<Texture2D>(t);
            } catch (FileNotFoundException) {
                entTexture = content.Load<Texture2D>("NoTexture");
                Console.WriteLine(t + "Not found. Using default texture.");
            }

            //Set health
            health = 1;
            maxHealth = health;

            loc.X = x;
            loc.Y = y;
            collision = c;
            if (dir > 360 || dir < 0) {
                direction = 0;
            } else {
                direction = dir;
            }
        }

        public bool CheckHealth(){
            //True if the target is still alive
            if(health > 0){
                return true;
            }
            //False if the target is dead
            return false;
        }
    }
}
