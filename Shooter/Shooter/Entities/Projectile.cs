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
        protected double range;
        protected double velocity;
        protected double distTraveled;
        public Projectile(ContentManager content): base(content) {
            loc = new Coord();
            entTexture = content.Load<Texture2D>("NoTexture");
            collision = false;
            direction = 0.0;
            velocity = .50;
            range = 10.0;
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
            velocity = .5;
            direction = 0.0;
            range = 10.0;
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
            if (dir >= 4 || dir < -4) { 
                direction = 0;
            } else {
                direction = dir;
            }
            velocity = v;
            range = 10.0;
        }
        public void UpdatePos(double timeElapsed, int tileSize) {
            double x = (Math.Cos(Direction) * velocity * timeElapsed) / tileSize;
            double y = (Math.Sin(Direction) * velocity * timeElapsed) / tileSize;
            Loc.X += x;
            Loc.Y += y;
            distTraveled += Math.Sqrt(x * x + y * y);
        }
        public bool CheckRange() {
            if(distTraveled >= range) {
                //if the bullet has traveled the distance of its effective range, return true
                return true;
            }else{
                //if it hasn't, return false
                return false;
            }
        }

        public bool CheckHit(Character target)
        {
            //Checks to see if the projectile has hit the target, deals damage and returns true 
            if((loc.X >= target.Loc.X) && (loc.X <= target.Loc.X + 1) && (loc.Y >= target.Loc.Y) && (loc.Y <= target.Loc.X + 1))
            {
                target.Health -= 1;
                return true;
            }

            //Else returns false
            return false;
        }
    }
}
