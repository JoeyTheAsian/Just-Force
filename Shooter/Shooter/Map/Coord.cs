using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shooter {
    //Coordinate class, object type that stores a pair of coordinates on the global map
    class Coord {
        //x and y values
        double x;
        double y;
        //default constructor
        public Coord() {
            x = 0.0;
            y = 0.0;
        }
        //parameterized constructors
        public Coord(int X, int Y) {
            x = X;
            y = Y;
        }
        public Coord(double X, double Y) {
            x = X;
            y = Y;
        }
        //properties
        public double X{
            get{
                return x;
            }set{
                x = value;
            }
        }
        public double Y{
            get {
                return y;
            }set{
                y = value;
            }
        }

    }
}
