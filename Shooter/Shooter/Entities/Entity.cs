using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Shooter.MapClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Shooter.Entities {
    class Entity {
        //STORES GLOBAL LOCATION "loc" = location
        protected Coord loc;
        //direction the entity is pointed, 0.0 degrees is facing upwards
        protected double direction;
        //Collision boolean, determines if you can walk through entity
        protected bool collision;

        //stores entity's texture
        protected Texture2D entTexture;

        //default constructor
        public Entity(ContentManager content) {
            loc = new Coord(0,0);
            entTexture = content.Load<Texture2D>("NoTexture");
            collision = false;
            direction = 0.0;
        }

        //parameters: pass in game content manager, x coord, y coord, texture file name
        public Entity(ContentManager content, double x, double y, string t) {
            //try to set texture to specified name
            try {
                entTexture = content.Load<Texture2D>(t);
            }catch(FileNotFoundException){
                entTexture = content.Load<Texture2D>("NoTexture");
                Console.WriteLine(t + "Not found. Using default texture.");
            }
            //set coordinates
            loc = new Coord();
            loc.X = x;
            loc.Y = y;
            
            direction = 0.0;
        }
        public Entity(ContentManager content, double x, double y, double dir, string t, bool c) {
            //try to set texture to specified name
            try {
                entTexture = content.Load<Texture2D>(t);
            } catch (FileNotFoundException) {
                entTexture = content.Load<Texture2D>("NoTexture");
                Console.WriteLine(t + "Not found. Using default texture.");
            }
            loc = new Coord();
            loc.X = x;
            loc.Y = y;
            collision = c;
            //direction can only be an angle from 0 - 360
            if (dir >= 4 || dir < -4) {
                direction = 0;
            } else {
                direction = dir;
            }
        }


        //Texture property
        public Texture2D EntTexture{
            get{
                return entTexture;
            }
            set {
                entTexture = value;
            }
        }
        public Coord Loc{
            get{
                return loc;
            }
            set{
                loc = value;
            }
        }
        public double Direction {
            get { return direction; }
            set { direction = value; }
        }
        //MOVEMENT
        //moves entity "x" by "y" units
        public void Move(double x, double y) {
            loc.X += x;
            loc.Y += y;
        }
        //moves entity one OR "p" units in specified direction
        public void MoveUp() {
            loc.Y -= 1;
        }
        public void MoveUp(double p) {
            loc.Y -= p;
        }
        //_________________________
        public void MoveDown() {
            loc.Y += 1;
        }
        public void MoveDown(double p) {
            loc.Y += p;
        }
        //_________________________
        public void MoveLeft() {
            loc.X -= 1;
        }
        public void MoveLeft(double p) {
            loc.X -= 1;
        }
        //_________________________
        public void MoveRight() {
            loc.X += 1;
        }
        public void MoveRight(double p) {
            loc.X += p;
        }
    }
}
