using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shooter.Controls {
    class Weapon {
        //Texture for displaying in the HUD
        protected Texture2D texture;
        //each int in the list signifies how full that magazine is
        protected List<int> ammo;
        //if the gun is fully automatic
        protected bool auto;
        //randomness of the fire pattern
        protected double spread;
        //shots per second
        protected int fireRate;

        public Weapon(ContentManager content) {
            auto = false;
            ammo = new List<int>();
            for(int i = 0; i < 4; i++) {
                ammo.Add(12);
            }
            fireRate = 4;
            spread = 0;
            texture = content.Load<Texture2D>("Gun");
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
