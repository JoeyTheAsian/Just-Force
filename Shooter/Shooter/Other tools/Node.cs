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
        public float pathLength;
        //length of straight line from node to finish
        public float bestLength;
        public float estLength;
        public Node parent;
        public enum NodeState {
            Untested,
            Open,
            Closed    
        };
        public NodeState state;
        public Node(Coord loc, bool col, Coord end, Node par) {
            collidable = col;
            location = loc;
            parent = par;
            bestLength = (float)Math.Sqrt(Math.Pow((end.X - loc.X), 2.0) + Math.Pow((end.Y - loc.Y), 2.0));
            pathLength = 0f;
            Node temp = par;
            while(temp.parent != null) {
                pathLength += (float)Math.Sqrt(Math.Pow((temp.location.X - temp.parent.location.X), 2.0) 
                                   + Math.Pow((temp.location.Y - temp.parent.location.Y), 2.0));
                temp = temp.parent;
            }
            estLength = bestLength + pathLength;
            state = NodeState.Untested;
        }
    }
}
