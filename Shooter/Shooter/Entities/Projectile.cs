using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Shooter.Entities {
    class Projectile :Entity{
        public Projectile(ContentManager content): base(content) {
            loc = new Coord();
            entTexture = content.Load<Texture2D>("NoTexture");
            collision = false;
        }
    }
}
