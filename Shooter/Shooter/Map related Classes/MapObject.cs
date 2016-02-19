using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shooter.MapClasses {
    class MapObject {
        protected Texture2D objTexture;
        bool collision;
        public MapObject(ContentManager content) {
            objTexture = content.Load<Texture2D>("NoTexture");
            collision = false;
        }
        public MapObject(ContentManager content, bool c, string texture) {
            try {
                objTexture = content.Load<Texture2D>(texture);
            }catch(ContentLoadException e) {
                Console.WriteLine(e.ToString());
                Console.WriteLine("File Not Found: " + texture + ", Defaulting to NoTexture");
                objTexture = content.Load<Texture2D>("NoTexture");
            }
            collision = c;
        }
    }
}
