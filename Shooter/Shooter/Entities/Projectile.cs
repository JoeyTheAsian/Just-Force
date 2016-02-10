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
    class Projectile :Entity{
        protected double velocity;
        public Projectile(ContentManager content): base(content) {
            loc = new Coord();
            entTexture = content.Load<Texture2D>("NoTexture");
            collision = false;
            direction = 0.0;
            velocity = 1.0;
        }
        //parameters: pass in game content manager, x coord, y coord, texture file name
        public Projectile(ContentManager content, double x, double y, string t): base(content, x, y, t) {
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
            velocity = 1.0;
            direction = 0.0;
        }
        //constructor: x coord, y coord, direction, velocity, texture file, collision
        public Projectile(ContentManager content, double x, double y, double dir, double v, string t, bool c): base(content, x, y, dir, t, c) {
            //try to set texture to specified name
            try {
                entTexture = content.Load<Texture2D>(t);
            } catch (FileNotFoundException) {
                entTexture = content.Load<Texture2D>("NoTexture");
                Console.WriteLine(t + "Not found. Using default texture.");
            }
            loc.X = x;
            loc.Y = y;
            collision = c;
            //direction can only be an angle from 0 - 360
            if (dir >= 360 || dir < 0) {
                throw new IndexOutOfRangeException();
            } else {
                direction = dir;
            }
            velocity = v;
        }
    }
}
