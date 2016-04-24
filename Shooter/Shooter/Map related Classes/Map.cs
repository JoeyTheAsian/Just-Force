using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Shooter.Entities;
using Shooter.Controls;

namespace Shooter.MapClasses {
    class Map {
        protected Texture2D[,] tileMap;
        protected MapObject[,] objectMap;
        protected int[,] tileRot;
        protected int[,] objRot;
        //currently active sounds on the map, used for AI to detect sounds and move towards position
        public List<Coord> sounds = new List<Coord>();
        //tiles are 100px by 100px, use this variable for everything related to tile scaling
        int tileSize;

        //default map constructor, makes a concrete bitmap with nothing on it
        public Map(ContentManager content, int screenWidth, int screenHeight) {
            tileSize = screenWidth / 20;
            tileMap = new Texture2D[20, 100];
            objectMap = new MapObject[tileMap.GetLength(0), tileMap.GetLength(1)];
            tileRot = new int[tileMap.GetLength(0), tileMap.GetLength(1)];
            objRot = new int[tileMap.GetLength(0), tileMap.GetLength(1)];
            //loops through entire tileMap array and sets each value to concrete
            for (int i = 0; i < tileMap.GetLength(0); i++) {
                for (int j = 0; j < tileMap.GetLength(1); j++) {
                    tileMap[i, j] = content.Load<Texture2D>("ConcreteCorner");
                }
            }
            //loops through objectMap array and sets edges to noTexture
            for (int i = 0; i < objectMap.GetLength(0); i++) {
                for (int j = 0; j < objectMap.GetLength(1); j++) {
                    objectMap[i, j] = new MapObject(content, true, "NoTexture", i, j);
                }
            }
            for (int i = 1; i < objectMap.GetLength(0) - 1; i++) {
                for (int j = 1; j < objectMap.GetLength(1) - 1; j++) {
                    objectMap[i, j] = null;
                }
            }
            for (int i = 1; i < objectMap.GetLength(0); i += 6) {
                for (int j = 0; j < objectMap.GetLength(1) - 3; j++) {
                    objectMap[i, j] = new MapObject(content, true, "NoTexture", i, j);
                }
            }
        }

        // constructor that reads fro a file map.cs
        public Map(ContentManager content, string filename, Camera c, Character player, List<Enemy> enemies, int screenWidth) {
            Texture2D empTexture = content.Load<Texture2D>("EmptyTile");
            tileSize = screenWidth / 20;

            BinaryReader input = new BinaryReader(File.OpenRead("../../../Content/" + filename));
            int mapWidth = input.ReadInt32();
            int mapHeight = input.ReadInt32();

            tileMap = new Texture2D[mapWidth, mapHeight];
            objectMap = new MapObject[mapWidth, mapHeight];
            tileRot = new int[tileMap.GetLength(0), tileMap.GetLength(1)];
            objRot = new int[tileMap.GetLength(0), tileMap.GetLength(1)];


            for (int i = 0; i < tileMap.GetLength(0); i++) {
                for (int j = 0; j < tileMap.GetLength(1); j++) {
                    string txtrString = input.ReadString();
                    if (!txtrString.Equals("null")) {
                        Texture2D texture = content.Load<Texture2D>(txtrString);
                        tileMap[i, j] = texture;
                    } else {
                        tileMap[i, j] = empTexture;
                    }
                }
            }

            for (int i = 0; i < tileMap.GetLength(0); i++) {
                for (int j = 0; j < tileMap.GetLength(1); j++) {
                    tileRot[i, j] = input.ReadInt32();
                }
            }

            for (int i = 0; i < objectMap.GetLength(0); i++) {
                for (int j = 0; j < objectMap.GetLength(1); j++) {
                    string txtrString = input.ReadString();
                    if (!txtrString.Equals("null")) {
                        objectMap[i, j] = new MapObject(content, true, txtrString, i, j);
                    } else {
                        objectMap[i, j] = null;
                    }
                }
            }

            for (int i = 0; i < objectMap.GetLength(0); i++) {
                for (int j = 0; j < objectMap.GetLength(1); j++) {
                    objRot[i, j] = input.ReadInt32();
                }
            }

            int entWidth = input.ReadInt32();
            int entHieght = input.ReadInt32();

            for (int x = 0; x < entWidth; x++) {
                for (int y = 0; y < entHieght; y++) {
                    string txtrString = input.ReadString();
                    if (!txtrString.Equals("null")) {
                        continue;
                    } else if (txtrString.Equals("Enemy")) {
                        CreateEnemy.CreateNormalEnemy(ref enemies, content, c, this, x, y);
                    }
                }
            }

            string playerPos = input.ReadString();
            Console.WriteLine(playerPos);
            string[] playerParts = playerPos.Split(',');
            double distX = player.Loc.X - double.Parse(playerParts[1]);
            double distY = player.Loc.Y - double.Parse(playerParts[2]);
            /*
            player.Loc.X -= distX;
            player.Loc.Y -= distY;
            c.camPos.X += distX;
            c.camPos.Y += distY;
            */
            input.Close();
        }

        public string[] CheckArea(Entity e) {
            //Array to store the sides which the entity collides with
            string[] sides = new string[10];
            //Index in the arry
            int index = 0;
            //Loops thorugh nearby x values
            for (int i = (int)e.Loc.X - 2; i < (int)e.Loc.X + 2; i++) {
                //Loops through nearby y values
                for (int j = (int)e.Loc.Y - 2; j < (int)e.Loc.Y + 2; j++) {
                    //Checks if the spot is a valid spot to check
                    if (i > -1 && j > -1 && i < objectMap.GetLength(0) && j < objectMap.GetLength(1)) {
                        //Checks to see if the object is null or collides
                        if (objectMap[i, j] != null && !objectMap[i, j].CheckCollide(e).Equals("none")) {
                            //If the entity is a projectile then set the first index has a hit in it
                            if (e is Projectile) {
                                sides[0] = "hit";
                                //Else is a character and puts the side with the collsion in the array
                            } else {
                                sides[index] = objectMap[i, j].CheckCollide(e);
                            }
                            index++;
                        }
                    }
                }
            }
            //Return the sides
            return sides;
        }

        //tileMap property
        public Texture2D[,] TileMap {
            get {
                return tileMap;
            }
        }
        //tilesize property
        public int TileSize {
            get {
                return tileSize;
            }
        }
        //objectMap property
        public MapObject[,] ObjectMap {
            get {
                return objectMap;
            }
        }

        //objectMap rot property
        public int[,] ObjRot {
            get {
                return objRot;
            }
        }

        //tileMap rot property
        public int[,] TileRot {
            get {
                return tileRot;
            }
        }
    }
}
