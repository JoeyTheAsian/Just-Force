using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Shooter.Controls;
using Shooter.MapClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shooter.Entities {
    static class CreateEnemy {
        private static Random rng = new Random();
        //Creates a single normal enemy
        public static void CreateNormalEnemy(ref List<Enemy> enemies, ContentManager Content, Camera c, Map m, double x, double y, float dir) {
            Enemy e = new Enemy(Content, x, y, "NoTexture", PlayerPos.CalcRectangle(c.camPos.X, c.camPos.Y, x, y, m.TileSize, c.xOffset, c.yOffset), dir);
            e.EntTexture = Content.Load<Texture2D>("Enemy" + rng.Next(1, 4));
            enemies.Add(e);
        }

        //Creates a single riot enemy
        public static void CreateRiotEnemy(ref List<Enemy> enemies, ContentManager Content, Camera c, Map m, double x, double y, float dir) {
            enemies.Add(new RiotEnemy(Content, x, y, "NoTexture", PlayerPos.CalcRectangle(c.camPos.X, c.camPos.Y, x, y, m.TileSize, c.xOffset, c.yOffset), dir));
        }
    }
}
