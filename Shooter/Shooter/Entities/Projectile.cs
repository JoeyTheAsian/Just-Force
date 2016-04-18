using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Shooter.MapClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Shooter.Controls;

namespace Shooter.Entities {
    class Projectile : Entity {
        protected double range;
        protected double velocity;
        protected double distTraveled;
        protected bool isRifleRound;

        public bool IsRifleRound {
            get { return isRifleRound; }
            set { isRifleRound = value; }
        }

        public Projectile(ContentManager content) : base(content) {
            loc = new Coord();
            entTexture = content.Load<Texture2D>("NoTexture");
            collision = false;
            direction = 0.0;
            velocity = .50;
            range = 10.0;
            isRifleRound = false;
        }

        //parameters: pass in game content manager, x coord, y coord, texture file name
        public Projectile(ContentManager content, double x, double y, string t, Rectangle r) : base(content, x, y, t, r) {
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
            isRifleRound = false;
        }
        //constructor: x coord, y coord, direction, velocity, texture file, collision
        public Projectile(ContentManager content, double x, double y, double dir, double v, string t, bool c, Rectangle r, double rng, bool rifle) : base(content, x, y, dir, t, c, r) {
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
            range = rng;
            isRifleRound = rifle;
        }
        public void UpdatePos(double timeElapsed, int tileSize) {
            double x = (Math.Cos(Direction) * velocity * timeElapsed) / tileSize;
            double y = (Math.Sin(Direction) * velocity * timeElapsed) / tileSize;
            Loc.X += x;
            Loc.Y += y;
            distTraveled += Math.Sqrt(x * x + y * y);
        }
        public bool CheckRange() {
            if (distTraveled >= range) {
                //if the bullet has traveled the distance of its effective range, return true
                return true;
            } else {
                //if it hasn't, return false
                return false;
            }
        }

        public bool CheckHit(Character e, Weapon w) {
            //Converts the direction from radians to degrees
            double directionDegrees = this.direction * 57.2958;

            //Checks collision based on the direction the projectile is rotated to
            //Checks angles in the fourth quadrant
            if (directionDegrees > 270 && directionDegrees <= 360) {
                if (this.loc.X + 1 < e.Loc.X + 1 && this.loc.X + 1 > e.Loc.X && this.loc.Y + 1 < e.Loc.Y + 1 && this.loc.Y + 1 > e.Loc.Y) {
                    //Decrements the character's health
                    e.Health -= w.Damage;
                    return true;
                }
            }
            //Checks angles in the third quadrant
            else if (directionDegrees > 180 && directionDegrees <= 270) {
                if (this.loc.X < e.Loc.X + 1 && this.loc.X > e.Loc.X && this.loc.Y + 1 < e.Loc.Y + 1 && this.loc.Y + 1 > e.Loc.Y) {
                    //Decrements the character's health
                    e.Health -= w.Damage;
                    return true;
                }
            }
            //Checks angles in the second quadrant
            else if (directionDegrees > 90 && directionDegrees <= 180) {
                if (this.loc.X < e.Loc.X + 1 && this.loc.X > e.Loc.X && this.loc.Y < e.Loc.Y + 1 && this.loc.Y > e.Loc.Y) {
                    //Decrements the character's health
                    e.Health -= w.Damage;
                    return true;
                }
            }
            //Checks angles in the first quadrant
            else {
                if (this.loc.X < e.Loc.X + 1 && this.loc.X > e.Loc.X && this.loc.Y < e.Loc.Y + 1 && this.loc.Y > e.Loc.Y) {
                    //Decrements the character's health
                    e.Health -= w.Damage;
                    return true;
                }
            }

            //Else returns false if no collision occurs
            return false;
        }
    }
}
