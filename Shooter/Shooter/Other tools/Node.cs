using System;
using Shooter.MapClasses;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shooter.Other_tools {
    class Node {
        public Coord Location;
        public bool collidable;
        public float pathLength;
        public float bestLength;
        public float estLength;
        public Node parent;
        //public NodeState state;
    }
}
