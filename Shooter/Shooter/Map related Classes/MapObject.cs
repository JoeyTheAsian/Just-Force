using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shooter.Entities;

namespace Shooter.MapClasses {
    class MapObject {
        protected Texture2D objTexture;
        bool collision;
        protected Coord loc;

        public Texture2D ObjTexture
        {
            get { return objTexture; }
        }
        public MapObject(ContentManager content, int x, int y) {
            objTexture = content.Load<Texture2D>("EmptyTile");
            collision = false;
            loc = new Coord(x, y);
        }
        public MapObject(ContentManager content, bool c, string texture, int x, int y) {
            try {
                objTexture = content.Load<Texture2D>(texture);
            }catch(ContentLoadException e) {
                Console.WriteLine(e.ToString());
                Console.WriteLine("File Not Found: " + texture + ", Defaulting to NoTexture");
                objTexture = content.Load<Texture2D>("EmptyTile");
            }
            collision = c;
            loc = new Coord(x, y);
        }

        /// <summary>
        /// Checks to see if there is collision
        /// </summary>
        /// <param name="e"></param>
        /// <param name="tileSize"></param>
        /// <returns></returns>
        public bool checkCollide(Entity e, int tileSize)
        {
            //First checks to see if the object has collision before testing for collision
            if (!collision)
            {
                return false;
            }
            //Creates a rectangle to compare the position against
            Rectangle r = new Rectangle((int)loc.X, (int)loc.Y, tileSize + 2, tileSize + 2);

            //Checks to see if one rectangle intersects with another one
            if(r.Intersects(e.rectangle))
            {
                return true;
            } else
            {
                return false;
            } 
        }
    }
}
