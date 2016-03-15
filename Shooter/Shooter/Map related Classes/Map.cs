using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Shooter.Entities;

namespace Shooter.MapClasses {
    class Map {
        protected Texture2D[,] tileMap;
        protected MapObject[,] objectMap;
        //tiles are 100px by 100px, use this variable for everything related to tile scaling
        int tileSize;

        //default map constructor, makes a concrete bitmap with nothing on it
        public Map(ContentManager content, int screenWidth, int screenHeight) {
            tileSize = screenWidth / 20;
            tileMap = new Texture2D[100, 100];
            objectMap = new MapObject[tileMap.GetLength(0),tileMap.GetLength(1)];
            //loops through entire tileMap array and sets each value to concrete
            for (int i = 0; i < tileMap.GetLength(0); i++) {
                for (int j = 0; j < tileMap.GetLength(1); j++) {
                    tileMap[i, j] = content.Load<Texture2D>("ConcreteCorner");
                }
            }
            //loops through objectMap array and sets edges to noTexture
            for (int i = 0; i < objectMap.GetLength(0); i++) {
                for(int j = 0; j < objectMap.GetLength(1); j++) {
                    objectMap[i, j] = new MapObject(content, true, "NoTexture", i, j);
                }
            }
            for (int i = 1; i < objectMap.GetLength(0)-1; i++) {
                for (int j = 1; j < objectMap.GetLength(1)-1; j++) {
                    objectMap[i, j] = null;
                }
            }
        }

        // constructor that reads form a file map.cs
        public Map(ContentManager content, string filename)
        {
            tileMap = new Texture2D[100, 100];
            objectMap = new MapObject[100, 100];

            BinaryReader input = new BinaryReader(File.OpenRead("../../../Content/" + filename));
            for (int i = 0; i < tileMap.GetLength(0); i++)
            {
                for (int j = 0; j < tileMap.GetLength(1); j++)
                {
                    string txtrString = input.ReadString();
                    Texture2D texture = content.Load<Texture2D>(txtrString);
                    tileMap[i, j] = texture;
                }
            }
        }

        public string CheckArea(Entity e) {
            string sides = "";
            for (int i = (int)e.Loc.X - 2; i < (int)e.Loc.X + 2; i++) {
                for (int j = (int)e.Loc.Y - 2; j < (int)e.Loc.Y + 2; j++) {
                    if (i > -1 && j > -1 && i < objectMap.GetLength(0) && j < objectMap.GetLength(1)) {
                        if (objectMap[i, j] != null && !objectMap[i, j].CheckCollide(e).Equals("none")) {
                            if (e is Projectile) {
                                return "hit";
                            } else {
                                sides += objectMap[i, j].CheckCollide(e) + ",";
                            }
                        }
                    }
                }
            }

            return sides;
        }

        //tileMap property
        public Texture2D[,] TileMap {
            get {
                return tileMap;
            }
        }
        //tilesize property
        public int TileSize{
            get{
                return tileSize;
            }
        }
        //objectMap property
        public MapObject[,] ObjectMap
        {
            get {
                return objectMap;
            }
        }
    }
}
