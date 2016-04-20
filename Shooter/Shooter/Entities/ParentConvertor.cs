using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Shooter.Entities {
    //changes an entity child class back to an entity class for rendering
    static class ParentConvertor {
        public static Entity ToEntity(Character c, ContentManager content) {
            Entity newEnt;
            try {
                newEnt = new Entity(content, c.Loc.X, c.Loc.Y, c.Direction, c.EntTexture.ToString(), false, c.rectangle);
                return newEnt;
            }catch(NullReferenceException) {
                try {
                    newEnt = new Entity(content, c.Loc.X, c.Loc.Y, c.Direction, content.Load<Texture2D>("NoTexture").ToString(), false, c.rectangle);
                    return newEnt;
                }catch(NullReferenceException e) {
                    Console.WriteLine(e.ToString());
                    return null;
                }
            }
        }
        public static Entity ToEntity(Projectile p, ContentManager content) {
            Entity newEnt;
            try {
                newEnt = new Entity(content, p.Loc.X, p.Loc.Y,p.Direction, p.EntTexture.ToString(), true, p.rectangle);
                return newEnt;
            } catch (NullReferenceException) {
                try {
                    newEnt = new Entity(content, p.Loc.X, p.Loc.Y,p.Direction, content.Load<Texture2D>("NoTexture").ToString(), true, p.rectangle);
                    return newEnt;
                } catch (NullReferenceException e) {
                    Console.WriteLine(e.ToString());
                    return null;
                }
            }
        }
    }
}
