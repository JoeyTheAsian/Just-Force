using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Shooter.MapClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shooter.Entities {
    class PickUpItem {
        protected Texture2D itemTexture;
        protected string itemType;
        protected Coord location;

        //PickUpItem's texture
        public Texture2D ItemTexture {
            get { return itemTexture; }
            set { itemTexture = value; }
        }

        //PickUpItem's item type
        public string ItemType {
            get { return itemType; }
            set { itemType = value; }
        }
        //PickUpItem's location
        public Coord Location {
            get { return location; }
            set { location = value; }
        }

        public PickUpItem(ContentManager content, string t, string type, double x, double y) {
            itemTexture = content.Load<Texture2D>(t);
            itemType = type;
            location = new Coord(x, y);
        }

        public bool CheckCollide(Entity e) {
            //Gets the boolean values of the the corresponding four corners of the item
                bool topLeftCorner = (e.Loc.X - 0.5 <= this.location.X + 0.25 && e.Loc.X + 0.5 >= this.location.X + 0.25 && e.Loc.Y - 0.5 <= this.location.Y + 0.25 && e.Loc.Y + 0.5 >= this.location.Y + 0.25);    
                bool topRightCorner = (e.Loc.X - 0.5 <= this.location.X + 0.75 && e.Loc.X + 0.5 >= this.location.X + 0.75 && e.Loc.Y - 0.5 <= this.location.Y + 0.25 && e.Loc.Y + 0.5 >= this.location.Y + 0.25);
                bool botLeftCorner = (e.Loc.X - 0.5 <= this.location.X + 0.25 && e.Loc.X + 0.5 >= this.location.X + 0.25 && e.Loc.Y - 0.5 <= this.location.Y + 0.75 && e.Loc.Y + 0.5 >= this.location.Y + 0.75);
                bool botRightCorner = (e.Loc.X - 0.5 <= this.location.X + 0.75 && e.Loc.X + 0.5 >= this.location.X + 0.75 && e.Loc.Y - 0.5 <= this.location.Y + 0.75 && e.Loc.Y + 0.5 >= this.location.Y + 0.75);

            //Checks if any corners collide
            if (topLeftCorner || topRightCorner || botLeftCorner || botRightCorner && e.IsPlayer) {
                    return true;
                }
            //Returns none if there is no collision
            return false;
        }
    }
}
