﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shooter.Entities;
using Shooter.Controls;

namespace Shooter.MapClasses {
    class MapObject {
        protected Texture2D objTexture;
        public bool collision;
        protected Coord loc;

        public Texture2D ObjTexture {
            get { return objTexture; }
        }
        public MapObject(ContentManager content, int x, int y) {
            objTexture = content.Load<Texture2D>("EmptyTile");
            collision = false;
            loc = new Coord(x, y);
        }
        public MapObject(ContentManager content, bool c, string texture, int x, int y) {
            try {
                objTexture = content.Load<Texture2D>(texture);
            } catch (ContentLoadException e) {
                Console.WriteLine(e.ToString());
                Console.WriteLine("File Not Found: " + texture + ", Defaulting to NoTexture");
                objTexture = content.Load<Texture2D>("NoTexture");
            }
            collision = c;
            loc = new Coord(x, y);
        }

        /// <summary>
        /// Checks to see if there is collision
        /// </summary>
        /// <param name="e"></param>
        /// <param name="tileSize"></param>
        /// <returns></returns>
        public string CheckCollide(Entity e, Camera c, bool left, bool right, bool top, bool bot) {
            //First checks to see if the object has collision before testing for collision
            if (!collision) {
                //Returns none if there is no collision
                return "none";
            }
            //Checks to see if the entity is a Projectile
            if (e is Projectile) {
                //Then checks for collision
                if (this.loc.X - 0.5 <= e.Loc.X && this.loc.X + 0.5 >= e.Loc.X && this.loc.Y - 0.5 <= e.Loc.Y && this.loc.Y + 0.5 >= e.Loc.Y) {
                    //Returns hit if the object does collide
                    return "hit";
                }
            }
            //Checks to see if the entity is a character and if it is the player
            else if (e is Character) {

                //Gets the boolean values of the the corresponding four corners of the player
                bool topLeftCorner = (this.loc.X - 0.5 <= e.Loc.X - 0.5 && this.loc.X + 0.5 >= e.Loc.X - 0.5 && this.loc.Y - 0.5 <= e.Loc.Y - 0.5 && this.loc.Y + 0.5 >= e.Loc.Y - 0.5);
                bool topRightCorner = (this.loc.X - 0.5 <= e.Loc.X + 0.5 && this.loc.X + 0.5 >= e.Loc.X + 0.5 && this.loc.Y - 0.5 <= e.Loc.Y - 0.5 && this.loc.Y + 0.5 >= e.Loc.Y - 0.5);
                bool botLeftCorner = (this.loc.X - 0.5 <= e.Loc.X - 0.5 && this.loc.X + 0.5 >= e.Loc.X - 0.5 && this.loc.Y - 0.5 <= e.Loc.Y + 0.5 && this.loc.Y + 0.5 >= e.Loc.Y + 0.5);
                bool botRightCorner = (this.loc.X - 0.5 <= e.Loc.X + 0.5 && this.loc.X + 0.5 >= e.Loc.X + 0.5 && this.loc.Y - 0.5 <= e.Loc.Y + 0.5 && this.loc.Y + 0.5 >= e.Loc.Y + 0.5);

                //Checks if any corners collide
                if (topLeftCorner || topRightCorner || botLeftCorner || botRightCorner) {
                    //Creates variables to store the corners being checked
                    double corner1X, corner2X, corner1Y, corner2Y;

                    //Checks the top left corner to determine the side of collision
                    if (topLeftCorner) {
                        //Gets the points for the two corners to check
                        corner1X = Math.Abs((this.loc.X + 0.5) - (e.Loc.X - 0.5));
                        corner1Y = Math.Abs((this.loc.Y - 0.5) - (e.Loc.Y - 0.5));
                        corner2X = Math.Abs((this.loc.X - 0.5) - (e.Loc.X - 0.5));
                        corner2Y = Math.Abs((this.loc.Y + 0.5) - (e.Loc.Y - 0.5));
                        //Uses the distance formula to see which side it is closer to and returns the closer side
                        if (Math.Sqrt(Math.Pow(corner1X, 2) + Math.Pow(corner1Y, 2)) <= Math.Sqrt(Math.Pow(corner2X, 2) + Math.Pow(corner2Y, 2)) && !right) {
                            e.Loc.X += corner1X;
                            if (e.IsPlayer) {
                                c.camPos.X -= corner1X;
                            }
                            //Console.WriteLine("Right");
                        } else if (Math.Sqrt(Math.Pow(corner1X, 2) + Math.Pow(corner1Y, 2)) > Math.Sqrt(Math.Pow(corner2X, 2) + Math.Pow(corner2Y, 2)) && !bot)  {
                            e.Loc.Y += corner2Y;
                            if (e.IsPlayer) {
                                c.camPos.Y -= corner2Y;
                            }
                            //Console.WriteLine("Bottom");
                        }
                    } if (topRightCorner) {
                        //Gets the points for the two corners to check
                        corner1X = Math.Abs((this.loc.X - 0.5) - (e.Loc.X + 0.5));
                        corner1Y = Math.Abs((this.loc.Y - 0.5) - (e.Loc.Y - 0.5));
                        corner2X = Math.Abs((this.loc.X + 0.5) - (e.Loc.X + 0.5));
                        corner2Y = Math.Abs((this.loc.Y + 0.5) - (e.Loc.Y - 0.5));
                        //Uses the distance formula to see which side it is closer to and returns the closer side
                        if (Math.Sqrt(Math.Pow(corner1X, 2) + Math.Pow(corner1Y, 2)) <= Math.Sqrt(Math.Pow(corner2X, 2) + Math.Pow(corner2Y, 2)) && !left) {
                            e.Loc.X -= corner1X;
                            if (e.IsPlayer) {
                                c.camPos.X += corner1X;
                            }
                            //Console.WriteLine("Left");
                        } else if (Math.Sqrt(Math.Pow(corner1X, 2) + Math.Pow(corner1Y, 2)) > Math.Sqrt(Math.Pow(corner2X, 2) + Math.Pow(corner2Y, 2)) && !bot) {
                            e.Loc.Y += corner2Y;
                            if (e.IsPlayer) {
                                c.camPos.Y -= corner2Y;
                            }
                            //Console.WriteLine("Bottom");
                        }
                    } if (botLeftCorner) {
                        //Gets the points for the two corners to check
                        corner1X = Math.Abs((this.loc.X - 0.5) - (e.Loc.X - 0.5));
                        corner1Y = Math.Abs((this.loc.Y - 0.5) - (e.Loc.Y + 0.5));
                        corner2X = Math.Abs((this.loc.X + 0.5) - (e.Loc.X - 0.5));
                        corner2Y = Math.Abs((this.loc.Y + 0.5) - (e.Loc.Y + 0.5));
                        //Uses the distance formula to see which side it is closer to and returns the closer side
                        if (Math.Sqrt(Math.Pow(corner1X, 2) + Math.Pow(corner1Y, 2)) <= Math.Sqrt(Math.Pow(corner2X, 2) + Math.Pow(corner2Y, 2)) && !top) {
                            e.Loc.Y -= corner1Y;
                            if (e.IsPlayer) {
                                c.camPos.Y += corner1Y;
                            }
                            //Console.WriteLine("Top");
                        } else if (Math.Sqrt(Math.Pow(corner1X, 2) + Math.Pow(corner1Y, 2)) > Math.Sqrt(Math.Pow(corner2X, 2) + Math.Pow(corner2Y, 2)) && !right) {
                            e.Loc.X += corner2X;
                            if (e.IsPlayer) {
                                c.camPos.X -= corner2X;
                            }
                            //Console.WriteLine("Right");
                        }


                    } if (botRightCorner) {
                        //Gets the points for the two corners to check
                        corner1X = Math.Abs((this.loc.X + 0.5) - (e.Loc.X + 0.5));
                        corner1Y = Math.Abs((this.loc.Y - 0.5) - (e.Loc.Y + 0.5));
                        corner2X = Math.Abs((this.loc.X - 0.5) - (e.Loc.X + 0.5));
                        corner2Y = Math.Abs((this.loc.Y + 0.5) - (e.Loc.Y + 0.5));
                        //Uses the distance formula to see which side it is closer to and returns the closer side
                        if (Math.Sqrt(Math.Pow(corner1X, 2) + Math.Pow(corner1Y, 2)) <= Math.Sqrt(Math.Pow(corner2X, 2) + Math.Pow(corner2Y, 2)) && !top) {
                            e.Loc.Y -= corner1Y;
                            if (e.IsPlayer) {
                                c.camPos.Y += corner1Y;
                            }
                            //Console.WriteLine("Top");
                        } else if (Math.Sqrt(Math.Pow(corner1X, 2) + Math.Pow(corner1Y, 2)) > Math.Sqrt(Math.Pow(corner2X, 2) + Math.Pow(corner2Y, 2)) && !left) {
                            e.Loc.X -= corner2X;
                            if (e.IsPlayer) {
                                c.camPos.X += corner2X;
                            }
                            //Console.WriteLine("Left");
                        }
                    }
                }
            } 
            //Returns none if there is no collision
            return "none";
        }
    }
}
