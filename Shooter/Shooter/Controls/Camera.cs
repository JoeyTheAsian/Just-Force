using Shooter.MapClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shooter.Controls {
    class Camera {
        public Coord camPos;
        //screenshake bool
        public bool screenShake;
        public int shakeDur;
        public int xOffset;
        public int yOffset;
        public int mousexOffset;
        public int mouseyOffset;
        private int screenWidth;
        private int screenHeight;

        public Camera(int w, int h) {
            screenShake = false;
            shakeDur = 0;
            xOffset = 0;
            yOffset = 0;
            camPos = new Coord(0, 0);
            screenWidth = w;
            screenHeight = h;
        }
        public void UpdateCamera(int elapsedTime, double X, double Y, int tileSize, Coord mouse, Coord origin) {
            xOffset = 0;
            yOffset = 0;
            //Mouse averaging
            mousexOffset = (int)(-1 * (mouse.X - origin.X) / 2);
            mouseyOffset = (int)(-1 * (mouse.Y - origin.Y) / 2);
            //SCREEN SHAKE 
            if (screenShake == true && shakeDur < 12) {
                xOffset += 2;
                shakeDur += elapsedTime;
            } else if (screenShake == true && shakeDur >= 12 && shakeDur < 37) {
                xOffset -= 2;
                shakeDur += elapsedTime;
            } else if (screenShake == true && shakeDur >= 37 && shakeDur < 50) {
                xOffset += 2;
                shakeDur += elapsedTime;
            } else if (shakeDur >= 50) {
                xOffset = 0;
                yOffset = 0;
                screenShake = false;
                shakeDur = 0;
            }
        }
    }
}
