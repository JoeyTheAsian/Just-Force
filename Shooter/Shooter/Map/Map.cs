using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shooter.GameMap {
    class Map {
        protected  string[,] tileMap;

        //default map constructor, makes a concrete bitmap with nothing on it
        public Map() {
            tileMap = new string[20,35];
            for(int i = 0; i < tileMap.GetLength(0); i++) {
                for(int j = 0; j < tileMap.GetLength(1); j++) {
                    tileMap[i,j] = "ConcreteTexture";
                }
            }
        }
        public string[,] TileMap {
            get {
                return tileMap;
            }
        }
    }
}
