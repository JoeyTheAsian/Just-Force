using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Shooter.Controls;
using Shooter.MapClasses;
using Shooter.Other_tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Shooter.Entities {
    class Enemy : Character {
        //Enemy unique data values
        //how far the enemy can see
        protected int visionRange;
        //how far the enemy can hear gunshots
        protected int scanRange;
        //the direction that the character is moving to, not necessarily facing
        protected double heading;
        //movement speed
        protected double speed;
        //the speed that the entity turns
        protected double turnSpeed;
        //the arc that the enemy pans
        protected double scanArc;
        //amount of time to do one whole scan
        protected double scanTime;
        //The timer for scanning
        protected double scanTimer;
        //boolean for aggression
        protected bool aggro;
        protected Coord playerLoc;
        //queue that holds the coordinates the npc is scheduled to move to
        private Queue<Coord> moveQueue = new Queue<Coord>();
        private Random random = new Random();
        //Default constructor for normal enemies
        public Enemy(ContentManager content, double x, double y, string t, Rectangle r, float direction) : base(content, x, y, t, r) {
            //try to set texture to specified name
            try {
                entTexture = content.Load<Texture2D>("Enemy" + random.Next(1, 3));
            } catch (FileNotFoundException) {
                entTexture = content.Load<Texture2D>("NoTexture");
                Console.WriteLine(t + "Not found. Using default texture.");
            }

            //collidable object by default
            collision = true;
            aggro = false;
            //Set health
            health = 4;
            maxHealth = health;
            //set AI scan range
            visionRange = 5;
            scanRange = 8;
            turnSpeed = .013;
            scanArc = .1 * Math.PI;
            //convert to ms
            scanTime = 2 * 1000;
            heading = 0;
            //tiles per second
            speed = 6;
            weapon = new Controls.Weapon(content);
            weapon.FireRate = 1;
            this.direction = direction;
        }
        public bool Move(double elapsedTime, Coord end) {
            if (loc.X > end.X - .05 && loc.X < end.X + .05 && loc.Y > end.Y - .05 && loc.Y < end.Y + .05) {
                return true;
            } else {
                Coord start = loc;
                double x = end.X - loc.X;
                double y = end.Y - loc.Y;
                double h = Math.Sqrt(x * x + y * y);
                double distance = speed * (elapsedTime / 1000);
                loc.X += distance * x / h;
                loc.Y += distance * y / h;
                //account for overshoot
                if (start.X < end.X) {
                    if (loc.X > end.X)
                        loc.X = end.X;
                }
                if (start.X > end.X) {
                    if (loc.X < end.X)
                        loc.X = end.X;
                }
                if (start.Y > end.Y) {
                    if (loc.Y < end.Y)
                        loc.Y = end.Y;
                }
                if (start.Y < end.Y) {
                    if (loc.Y > end.Y)
                        loc.Y = end.Y;
                }
                if (loc.X > end.X - .1 && loc.X < end.X + .1 && loc.Y > end.Y - .1 && loc.Y < end.Y + .1) {
                    return true;
                }
                //if the coordinates are close enough, return true to indicate that moving was successful
                else {
                    return false;
                }
            }
        }
        //rotates the character x degrees (positive for clockwise)
        public void Rotate(double deg) {
            if(direction > 1 * Math.PI) {
                //wrap angle
                direction = -1 * Math.PI + (direction - Math.PI);
            }
            if(direction < -1 * Math.PI) {
                direction = Math.PI + (direction + Math.PI);
            } else {
                direction += deg;
            }
        }
        public void RotateTo(double deg) {
            if(direction - deg < deg - direction) {
                Rotate(turnSpeed);
                if(direction < deg + (1.5 * turnSpeed) && direction > deg - (turnSpeed)){
                    direction = deg;
                }
            } else {
                Rotate(-2 * turnSpeed);
                if (direction < deg + (1.5 * turnSpeed) && direction > deg - (1.5 * turnSpeed)) {
                    direction = deg;
                }
            }
        }
        // Return True if the player is in the scan triangle
        public bool IsPointInPolygon(List<Coord> triangle, Coord player) {
            double totalAngle = 0;
            for(int i = 0; i < triangle.Count; i++) {
                int nextIndex = i + 1; 
                if (nextIndex >= triangle.Count) {
                    nextIndex = 0;
                }
                double distAB = Math.Sqrt(Math.Pow((player.X - triangle[i].X), 2) + Math.Pow((player.Y - triangle[i].Y), 2));
                double distAC = Math.Sqrt(Math.Pow((player.X - triangle[nextIndex].X), 2) + Math.Pow((player.Y - triangle[nextIndex].Y), 2));
                double distBC = Math.Sqrt(Math.Pow((triangle[i].X - triangle[nextIndex].X), 2) + Math.Pow((triangle[i].Y - triangle[nextIndex].Y), 2));
                totalAngle += Math.Acos((Math.Pow(distAB,2) + Math.Pow(distAC,2) - Math.Pow(distBC, 2)) / (2 *distAB * distBC));
            }
            return (Math.Abs(totalAngle) > 0.000001);
        }
        //returns true if the entity should shoot
        public bool UpdateAI(ref Map m, double elapsedTime, Coord player) {
            //define the triangle within which the character scans
            List<Coord> triangle = new List<Coord>();
            triangle.Add(loc);
            triangle.Add(new Coord((loc.X + Math.Cos(direction + scanArc) * visionRange), (loc.Y + Math.Sin(direction + scanArc) * visionRange)));
            triangle.Add(new Coord((loc.X + Math.Cos(direction - scanArc) * visionRange), (loc.Y + Math.Sin(direction - scanArc) * visionRange)));

            //update timer
            scanTimer += elapsedTime;
            if (moveQueue.Count > 0) {
                //Find the shortest way to turn to the direction it's headed in
                if (heading - direction <= Math.PI && heading - direction > 0) {
                    Rotate(10 * turnSpeed);
                    if (direction > heading) {
                        direction = heading;
                    }
                } else if (direction - heading >= -Math.PI && heading - direction <= 0) {
                    Rotate(10 * -turnSpeed);
                    if (direction < heading) {
                        direction = heading;
                    }
                }
                if (Move(elapsedTime, moveQueue.Peek())) {
                    Coord temp = new Coord(moveQueue.Peek().X - loc.X, moveQueue.Peek().Y - loc.Y);
                    heading = Math.Atan2(temp.Y, temp.X);
                    moveQueue.Dequeue();
                }
            } else {
                //if the player is not yet found, pan to look for him and listen for his shots
                if (!aggro) {
                    if (playerLoc != null) {
                        List<Coord> path = GetPath(loc, playerLoc, ref m);
                        foreach(Coord c in path) {
                            moveQueue.Enqueue(c);
                        }
                        playerLoc = null;
                    }
                    //pan around looking for player
                    if (scanTimer < scanTime / 4) {
                        Rotate(2 * turnSpeed);
                    } else if (scanTimer < scanTime * 3 / 4) {
                        Rotate(-2 * turnSpeed);
                    } else if (scanTimer < scanTime) {
                        Rotate(2 * turnSpeed);
                    } else {
                        scanTimer = 0;
                    }
                    //Pathfinding with sound
                    if (m.sounds.Count > 0) {
                        Coord start = m.sounds[m.sounds.Count - 1];
                        double dist = Math.Sqrt(Math.Pow(start.X - loc.X, 2) + Math.Pow(start.Y - loc.Y, 2));
                        if (dist < scanRange) {
                            //find a path to the sound and put it on move queue
                            moveQueue.Clear();
                            List<Coord> path = GetPath(loc, m.sounds[m.sounds.Count - 1], ref m);
                            foreach (Coord point in path) {
                                moveQueue.Enqueue(point);
                            }
                        }
                    }
                } else {
                    playerLoc = player;
                    direction = Math.Atan2(player.Y - loc.Y, player.X - loc.X);
                }
            }

            if (IsPointInPolygon(triangle, player)) {
                aggro = true;
                if (weapon.CheckFireRate(elapsedTime)) {
                    return true;
                } else {
                    return false;
                }
            } else {
                aggro = false;
                    return false;
            }
        }
        //Checks all adjacent tiles and returns list of valid tiles sorted by estimated path length
        public List<Node> checkAdjacent(Node curNode, Coord end, ref Node[,] nodeMap) {
            int X = (int)curNode.location.X;
            int Y = (int)curNode.location.Y;

            List<Node> validNodes = new List<Node>();

            //check for valid adjacent tiles and add them to list
            for (int i = X - 1; i < X + 2; i++) {
                for (int j = Y - 1; j < Y + 2; j++) {
                    //stay within boundaries of map and check if the tile is walkable
                    if (i > 0 && j > 0 && j < nodeMap.GetLength(1) && i < nodeMap.GetLength(0) && nodeMap[i,j].collidable == false) {
                        Node n = nodeMap[i, j];
                        if (n.state == Node.NodeState.Closed) {
                            //do nothing if the node is closed
                            continue;
                        }
                        // Already-open nodes are added to the list only if you find a shorter way to get there
                        if (n.state == Node.NodeState.Open) {
                            //if the currently stored path to that node is longer, override with shorter path
                            n.UpdateDist(n.location, end);
                            if (n.GetNewPathDist(curNode) < n.pathLength) {
                                n.parent = curNode;
                                n.UpdateDist(n.location, end);
                                validNodes.Add(n);
                            }
                        } 
                        //untested node case
                        else {
                            n.parent = curNode;
                            n.state = Node.NodeState.Open;
                            n.UpdateDist(n.location, end);
                            validNodes.Add(n);
                        }
                        //update node on map
                        nodeMap[i,j] = n;
                    }
                }
            }
            //sort the nodes by the least distance first
            validNodes = validNodes.OrderBy(o => o.estLength).ToList();
            return validNodes;
        }
        
        public bool Search(Node curNode, Coord startPos, Coord endPos,ref Node[,] nodeMap) {
            //all nodes added to a path are marked as closed
            curNode.state = Node.NodeState.Closed;
            //get sorted list of valid moves
            List<Node> validNodes = checkAdjacent(curNode, endPos, ref nodeMap);
            
            foreach (var nextNode in validNodes) {
                //if the next node you check is the end, return true
                if ((int)nextNode.location.X == (int)endPos.X && (int)nextNode.location.Y == (int)endPos.Y) {
                    return true;
                } else {
                    //recurse until dead end is met or end is reached
                    if (Search(nextNode, startPos, endPos, ref nodeMap)) {
                        return true;
                    }
                }
            }
            //if all paths are checked and cannot reach destination, return false
            return false;
        }
        //compiles the path
        public List<Coord> GetPath(Coord startPos, Coord endPos, ref Map m) {
            //round down coords to lock to tiles
            int startX = (int)(startPos.X);
            int startY = (int)(startPos.Y);
            int endX = (int)(endPos.X);
            int endY = (int)(endPos.Y);
            //create nodemap
            Node[,] nodeMap = new Node[m.ObjectMap.GetLength(0), m.ObjectMap.GetLength(1)];
            for (int x = 0; x < nodeMap.GetLength(0); x++) {
                for (int y = 0; y < nodeMap.GetLength(1); y++) {
                    if (m.ObjectMap[x, y] != null) {
                        nodeMap[x, y] = new Node(new Coord(x, y), m.ObjectMap[x, y].collision, new Coord(endX, endY), null);
                    } else {
                        nodeMap[x, y] = new Node(new Coord(x, y), false, new Coord(endX, endY), null);
                    }
                }
            }

            bool success = Search(nodeMap[startX, startY], new Coord(startX, startY), new Coord(endX, endY), ref nodeMap);
            if (success) {
                List<Coord> path = new List<Coord>();
                Node n = nodeMap[endX, endY];
                while (n.parent != null) {
                    path.Add(new Coord(n.location.X + (random.Next(-3, 3) * .1), n.location.Y + (random.Next(-3, 3) * .1)));
                    n = n.parent;
                }
                path.Reverse();
                return path;
            } else {
                //return empty list if search is not successful
                return new List<Coord>();
            }
        }
    }
}
