using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Shooter.Entities {
    class RiotEnemy : Enemy{

        //Default constructor for riot enemies
        public RiotEnemy(ContentManager content, double x, double y, string t, Rectangle r, float direction) : base(content, x, y, t, r, direction)
        {
            //try to set texture to specified name
            try {
                entTexture = content.Load<Texture2D>(t);
            } catch (FileNotFoundException) {
                entTexture = content.Load<Texture2D>("RiotEnemy");
                Console.WriteLine(t + "Not found. Using default texture.");
            }

            //collidable object by default
            collision = true;

            //Set health
            //collidable object by default
            collision = true;
            aggro = false;
            //Set health
            health = 8;
            maxHealth = health;
            //set AI scan range
            visionRange = 9;
            scanRange = 10;
            turnSpeed = .01;
            scanArc = .1 * Math.PI;
            //convert to ms
            scanTime = 4 * 1000;
            heading = 0;
            //tiles per second
            speed = 3;
            weapon = new Controls.Weapon(content);
            weapon.FireRate = 1.4;
            this.direction = direction;
        }
    }
}
