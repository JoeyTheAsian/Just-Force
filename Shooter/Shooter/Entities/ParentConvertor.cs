using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Shooter.Entities {
    //changes an entity child class back to an entity class for rendering
    class ParentConvertor {
        public Entity ToEntity(Character c, ContentManager content) {
           return new Entity(content, c.Loc.X, c.Loc.Y, c.EntTexture.ToString());
        }
        public Entity ToEntity(Projectile p, ContentManager content) {
            return new Entity(content, p.Loc.X, p.Loc.Y, p.EntTexture.ToString());
        }
    }
}
