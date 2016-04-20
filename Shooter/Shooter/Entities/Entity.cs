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
        //STORES c.camPos LOCATION "loc" = location
        protected Coord loc;
        //direction the entity is pointed, 0.0 degrees is facing upwards
        protected double direction;
        //Collision boolean, determines if you can walk through entity
        protected bool collision;
        //Rectangle object, to check for collision
        protected Rectangle entityRectangle;
        //stores entity's texture
        protected Texture2D entTexture;
        //Boolean value for the entity being the player
        protected bool isPlayer;
        //default constructor
        public Entity(ContentManager content) {
            loc = new Coord(0,0);
            entTexture = content.Load<Texture2D>("NoTexture");
            collision = false;
            direction = 0.0;
            entityRectangle = new Rectangle(0, 0, 0, 0);
            isPlayer = false;
        }

        //parameters: pass in game content manager, x coord, y coord, texture file name
        public Entity(ContentManager content, double x, double y, string t, Rectangle r) {
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
            isPlayer = false;
            direction = 0.0;
            entityRectangle = r;
        }
        public Entity(ContentManager content, double x, double y, double dir, string t, bool c, Rectangle r) {
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
            entityRectangle = r;
            isPlayer = false;
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
        //Rectangle property
        public Rectangle rectangle
        {
            get { return entityRectangle; }
            set { entityRectangle = value; }
        }
        //Property for the isPlayer attribute
        public bool IsPlayer
        {
            get { return isPlayer; }
            set { isPlayer = value; }
        }
    }
}
