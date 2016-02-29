using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shooter.MapClasses {
    class Map {
        protected Texture2D[,] tileMap;
        protected MapObject[,] objectMap;
        //tiles are 100px by 100px, use this variable for everything related to tile scaling
        int tileSize = 100;

        //default map constructor, makes a concrete bitmap with nothing on it
        public Map(ContentManager content) {
            tileMap = new Texture2D[100, 100];
            objectMap = new MapObject[100, 100];
            //loops through entire tileMap array and sets each value to concrete
            for (int i = 0; i < tileMap.GetLength(0); i++) {
                for (int j = 0; j < tileMap.GetLength(1); j++) {
                    tileMap[i, j] = content.Load<Texture2D>("ConcreteCorner");
                }
            }
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
