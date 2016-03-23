using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Shooter.Entities {
    class Enemy : Character {
        //Default constructor for normal enemies
        public Enemy(ContentManager content, double x, double y, string t, Rectangle r) : base(content, x, y, t, r)
        {
            //try to set texture to specified name
            try {
                entTexture = content.Load<Texture2D>(t);
            } catch (FileNotFoundException) {
                entTexture = content.Load<Texture2D>("NoTexture");
                Console.WriteLine(t + "Not found. Using default texture.");
            }
            
            //collidable object by default
            collision = true;

            //Set health
            health = 4;
            maxHealth = health;
        }
    }
}
