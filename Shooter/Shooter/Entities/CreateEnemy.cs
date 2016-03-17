using Microsoft.Xna.Framework.Content;
using Shooter.Controls;
using Shooter.MapClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shooter.Entities {
    static class CreateEnemy {

        //Creates a single normal enemy
        public static void CreateNormalEnemy(List<Enemy> enemies, ContentManager Content, Camera c, Map m, double x, double y) {
            enemies.Add(new Enemy(Content, x, y, "NoTexture", PlayerPos.CalcRectangle(c.camPos.X, c.camPos.Y, x, y, m.TileSize, c.xOffset, c.yOffset)));
        }

        //Creates a single riot enemy
        public static void CreateRiotEnemy(List<Enemy> enemies, ContentManager Content, Camera c, Map m, double x, double y) {
            enemies.Add(new RiotEnemy(Content, x, y, "NoTexture", PlayerPos.CalcRectangle(c.camPos.X, c.camPos.Y, x, y, m.TileSize, c.xOffset, c.yOffset)));
        }
    }
}
