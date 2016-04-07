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
        public Enemy(ContentManager content, double x, double y, string t, Rectangle r) : base(content, x, y, t, r)
        {
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
        public List<Node> checkAdjacent(Node curNode, Coord end, ref Map m) {
            int X = (int)curNode.location.X;
            int Y = (int)curNode.location.Y;

            List<Node> adjacentNodes = new List<Node>();
            bool temp;
            //check for valid tiles and add them to list
            //account for null case
            if (m.ObjectMap[X + 1, Y] == null) temp = false;
            else temp = m.ObjectMap[X + 1, Y].collision;
            if (temp != false) {
                adjacentNodes.Add(new Node(new Coord(X + 1, Y), temp, end, curNode));
            }
            if (m.ObjectMap[X - 1, Y] == null) temp = false;
            else temp = m.ObjectMap[X - 1, Y].collision;
            if (temp != false) {
                adjacentNodes.Add(new Node(new Coord(X - 1, Y), temp, end, curNode));
            }
            if (m.ObjectMap[X + 1, Y + 1] == null) temp = false;
            else temp = m.ObjectMap[X + 1, Y + 1].collision;
            if (temp != false) {
                adjacentNodes.Add(new Node(new Coord(X + 1, Y + 1), temp, end, curNode));
            }
            if (m.ObjectMap[X - 1, Y + 1] == null) temp = false;
            else temp = m.ObjectMap[X - 1, Y + 1].collision;
            if (temp != false) {
                adjacentNodes.Add(new Node(new Coord(X - 1, Y + 1), temp, end, curNode));
            }
            if (m.ObjectMap[X - 1, Y - 1] == null) temp = false;
            else temp = m.ObjectMap[X - 1, Y - 1].collision;
            if (temp != false) {
                adjacentNodes.Add(new Node(new Coord(X - 1, Y - 1), temp, end, curNode));
            }
            if (m.ObjectMap[X, Y + 1] == null) temp = false;
            else temp = m.ObjectMap[X, Y + 1].collision;
            if (temp != false) {
                adjacentNodes.Add(new Node(new Coord(X, Y + 1), temp, end, curNode));
            }
            if (m.ObjectMap[X, Y - 1] == null) temp = false;
            else temp = m.ObjectMap[X, Y - 1].collision;
            if (temp != false) {
                adjacentNodes.Add(new Node(new Coord(X, Y - 1), temp, end, curNode));
            }
            //sort the list by the estimated length of the path
            adjacentNodes.Sort((x, y) => x.estLength.CompareTo(y.estLength));
            return adjacentNodes;
       }
       /*public Node FindPath(ref Map m, Node curNode, Coord curPos , Coord endPos) {
            bool isTilecollidable;
            if (m.ObjectMap[(int)(loc.X - .5), (int)(loc.Y - .5)] == null) {
                isTilecollidable = false;
            } else {
                isTilecollidable = m.ObjectMap[(int)(loc.X - .5), (int)(loc.Y - .5)].collision;
            }

        }*/
    }


}
