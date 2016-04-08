using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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
        private int visionRange;
        //how far the enemy can hear gunshots
        private int scanRange;

        //Default constructor for normal enemies
        public Enemy(ContentManager content, double x, double y, string t, Rectangle r) : base(content, x, y, t, r) {
            //try to set texture to specified name
            try {
                entTexture = content.Load<Texture2D>(t);
            } catch (FileNotFoundException) {
                entTexture = content.Load<Texture2D>("NoTexture");
                Console.WriteLine(t + "Not found. Using default texture.");
            }

            //collidable object by default
            collision = true;

            //Set health
            health = 4;
            maxHealth = health;
            visionRange = 5;
            scanRange = 5;
        }
        public void UpdateAI() {

        }
        //Checks all adjacent tiles and returns list of valid tiles sorted by estimated path length
        public List<Node> checkAdjacent(Node curNode, Coord end, ref Map m, ref List<Node>[,] nodeMap) {
            int X = (int)curNode.location.X;
            int Y = (int)curNode.location.Y;

            List<Node> adjacentNodes = new List<Node>();
            bool temp;
            //check for valid tiles and add them to list
            for (int i = X - 1; i < X + 2; i++) {
                for (int j = Y - 1; j < Y + 2; Y++) {
                    //account for null case
                    if (m.ObjectMap[i, j] == null) {
                        temp = false;
                    } else {
                        temp = m.ObjectMap[i, j].collision;
                    }
                    //check that the coord checked isn't the current position & it isn't collidable
                    if (temp != true && (i != X || j != Y)) {
                        //add a new valid adjacent node to the list
                        adjacentNodes.Add(new Node(new Coord(i, j), temp, end, curNode));
                    }
                }
            }

            //sort the list by the estimated length of the path
            adjacentNodes.Sort((x, y) => x.estLength.CompareTo(y.estLength));
            foreach(Node n in adjacentNodes) {
                n.state = Node.NodeState.Open;
            }
            return adjacentNodes;
        }
        /*
        public List<Coord> FindPath(ref Map m, Node curNode, Coord startPos, Coord endPos, List<Node>[,] nodeMap) {
            if(curNode == null) {
                curNode = new Node(new Coord((int)(startPos.X - .5), (int)(startPos.Y - .5)), false, endPos, null);
            }
            Coord endTile = new Coord((int)(endPos.X - .5), (int)(endPos.Y - .5));
            if (curNode.location != endTile) {
                List<Node> adjNodes = checkAdjacent(curNode, endPos, ref m, ref nodeMap);
                if(adjNodes.Count == 0) {
                    //return empty list if the path doesn't reach the destination & there's no more valid paths
                    return new List<Coord>();
                }
                foreach(Node n in adjNodes) {
                    //tested nodes are marked as closed
                    n.state = Node.NodeState.Closed;
                    List<Coord> result = FindPath(ref m, n, startPos, endPos, nodeMap);
                    //if the last value in the resulting path is the end tile, return the path
                    if (result[result.Count - 1] == endTile) {
                        return result;
                    }
                }
            } else {
                
            }
        }*/

    }
}
