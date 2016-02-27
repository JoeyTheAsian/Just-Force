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

        public Camera() {
            screenShake = false;
            shakeDur = 0;
            xOffset = 0;
            yOffset = 0;
            camPos = new Coord(0, 0);
        }
        public void UpdateCamera(int elapsedTime, double X, double Y, int tileSize) {
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
