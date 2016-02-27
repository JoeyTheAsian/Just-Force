using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shooter.MapClasses {
    class TileBounds {
        int xmax;
        int ymax;
        int xmin;
        int ymin;

        public int Xmax {
            get { return xmax; }
        }public int Ymax {
            get { return ymax; }
        }public int Xmin {
            get { return xmin; }
        }public int Ymin {
            get { return ymin; }
        }

        //takes the inverted sign of coordinates x,y because c.camPos coordinates are inverted.
        //Minimum bound is where the screen begins, maximum bound is where the screen ends. Implementation of ambient occlusion to save memory
        //+/- 1.5 tile buffer to make sure non-whole edges of screen are never empty and account for rounding errors

        public void findBounds(double x, double y, int tileSize, int mapWidth, int mapHeight, int screenWidth, int screenHeight) {
            if ((int)((x * -1) + (screenWidth / tileSize) + 1.5) < mapWidth) {
                xmax = (int)((x * -1) + (screenWidth / tileSize) + 1.5);
            } else {
                xmax = mapWidth;
            }
            if ((int)((y * -1) + (screenHeight / tileSize) + 1.5) < mapHeight) {
                ymax = (int)((y * -1) + (screenHeight / tileSize) + 2.5);
            } else {
                ymax = mapHeight;
            }
            //set min
            if ((int)((x * -1)) > 0) {
                xmin = (int)((x * -1) - 1.5);
            } else {
                xmin = 0;
            }
            if ((int)((y * -1)) > 0) {
                ymin = (int)((y * -1) - 1.5);
            } else {
                ymin = 0;
            }
        }

    }
}
