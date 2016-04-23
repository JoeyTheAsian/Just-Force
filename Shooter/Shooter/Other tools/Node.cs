using System;
using Shooter.MapClasses;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shooter.Other_tools {
    class Node {
        public Coord location;
        public bool collidable;
        //length of the current path
        public double pathLength;
        //length of straight line from node to finish
        public double bestLength;
        public  double estLength;
        public Node parent;
        public enum NodeState {
            Untested,
            Open,
            Closed    
        };
        public NodeState state;
        public Node(Coord loc, bool col, Coord end, Node par){
            collidable = col;
            location = loc;
            parent = par;
            bestLength = Math.Abs(end.X - loc.X) + Math.Abs(end.Y - loc.Y);
            pathLength = 0;
            Node temp = this;
            while(temp.parent != null) {
                pathLength += (float)Math.Sqrt(Math.Pow((temp.location.X - temp.parent.location.X), 2.0) 
                                   + Math.Pow((temp.location.Y - temp.parent.location.Y), 2.0));
                temp = temp.parent;
            }
            estLength = bestLength + pathLength;
            state = NodeState.Untested;
        }
        public void UpdateDist(Coord loc, Coord end) {
            bestLength = Math.Sqrt((Math.Pow(Math.Abs(end.X - loc.X), 2) + Math.Pow(Math.Abs(end.Y - loc.Y), 2)));
            double total = 0;
            Node temp = this;
            while (temp.parent != null) {
                //get the total distance traveled along the current path
                total += (float)Math.Sqrt(Math.Pow((temp.location.X - temp.parent.location.X), 2.0) + Math.Pow((temp.location.Y - temp.parent.location.Y), 2.0));
                temp = temp.parent;
            }
            pathLength = total;
            estLength = pathLength + bestLength;
        }
        public double GetNewPathDist(Node p) {
            float total = 0.0f;
            Node temp = this;
            temp.parent = p;
            while (temp.parent != null) {
                //get the total distance traveled along the new path
                total += (float)Math.Sqrt(Math.Pow((temp.location.X - temp.parent.location.X), 2.0) + Math.Pow((temp.location.Y - temp.parent.location.Y), 2.0));
                temp = temp.parent;
            }
            return total;
        }
    }
}
