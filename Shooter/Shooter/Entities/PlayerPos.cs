using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shooter.Entities {
    //this class calculates the player coordinates for rendering to the screen
    static class PlayerPos {
        public static Rectangle CalcRectangle(double globalX, double globalY, double playerX, double playerY, int tileSize, int xOffset, int yOffset) {
            return new Rectangle((int)(((globalX + playerX) * tileSize)) + xOffset, (int)(((globalY + playerY) * tileSize)) + yOffset, tileSize, tileSize);
        }

        public static double CalcDirection(double mouseX, double mouseY, double globalX, double globalY, double playerX, double playerY, int tileSize) {
            return (double)Math.Atan2((double)mouseY - (int)(((globalY + playerY) * tileSize)), (double)mouseX - (int)(((globalX + playerX) * tileSize)));
        }

    }
}
