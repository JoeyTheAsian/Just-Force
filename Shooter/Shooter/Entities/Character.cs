using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Shooter.MapClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Shooter.Entities {
    class Character : Entity {
        private int health;
        private int stamina;


        public Character(ContentManager content) : base(content) {
            loc = new Coord();
            entTexture = content.Load<Texture2D>("NoTexture");
            collision = false;
        }

        //properties
        public int Health{
            get{ return health;}
            set{ health = value;}
        }
        public int Stamina {
            get { return stamina; }
            set { stamina = value; }
        }

        public Character(ContentManager content, double x, double y, string t): base(content, x, y, t) {
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
        }
        
        public Character(ContentManager content, double x, double y, double dir, string t, bool c): base(content, x, y, t) {
            try {
                entTexture = content.Load<Texture2D>(t);
            } catch (FileNotFoundException) {
                entTexture = content.Load<Texture2D>("NoTexture");
                Console.WriteLine(t + "Not found. Using default texture.");
            }
            loc.X = x;
            loc.Y = y;
            collision = c;
            if (dir > 360 || dir < 0) {
                direction = 0;
            } else {
                direction = dir;
            }
        }
        public Projectile Shoot(ContentManager content) {
            Projectile p = new Projectile(content, loc.X, loc.Y, this.Direction, 40.0, "Bullet", true);
            return p;
        }
    }
}
