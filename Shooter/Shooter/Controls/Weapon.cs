using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shooter.Controls {
    class Weapon {
        //Texture for displaying weapon in the HUD
        protected Texture2D texture;
        //each int in the list signifies how full that magazine is
        protected List<int> ammo;
        //if the gun is fully automatic
        protected bool auto;
        //randomness of the fire pattern
        protected double spread;
        //shots per second
        protected int fireRate;
        //name of gun (gun type)
        protected string name;

        private Random r = new Random();
        //default weapon is a pistol
        public Weapon(ContentManager content) {
            auto = false;
            ammo = new List<int>();
            for(int i = 0; i < 4; i++) {
                ammo.Add(12);
            }
            fireRate = 4;
            //randomness in fire pattern is a 10 degree arc
            spread = 10;
            name = "Pistol";
            texture = content.Load<Texture2D>("Pistol");
        }

        public Weapon(ContentManager content, List<int> a, bool au, double spr, int fr, string t) {
            fireRate = fr;
            ammo = a;
            auto = au;
            spread = spr;
            texture = content.Load<Texture2D>(t);
        }

        public int FireRate {
            get { return fireRate; }
        }
        public bool Auto {
            get { return auto; }
        }
        public double Spread {
            get { return spread; }
        }
        public List<int> Ammo {
            get { return ammo; }
        }
        public string Name {
            get {
                return name;
            }
        }
        public Texture2D Texture {
            get {
                return texture;
            }
        }
        public double GetSpread() {
            //returns a random integer that will be the offset of the bullet direction
            return r.Next((int)-spread/2, (int)spread/2);
        }
        //Checks if gun is ready to be fired agaim
        //takes an int (milliseconds) of the time since the last shot was fired
        public bool CheckFireRate(int timeElapsed) {
            if(timeElapsed >= 1000/ fireRate) {
                return true;
            } else {
                return false;
            }
        }
    }
}
