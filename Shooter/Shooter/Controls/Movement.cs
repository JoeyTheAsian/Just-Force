using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using System.Text;
using Shooter.Entities;

namespace Shooter.Controls {
    class Movement {
        //mutatable instantaneous velocity of character
        public double YVelocity;
        public double XVelocity;
        public double sprintVelocity;
        public double normVelocity;
        //immutable maximum velocity
        private double maxVelocity;
        private double acceleration;
        public Movement(double mv, double accel, int tileSize) {
            maxVelocity = mv;
            acceleration = accel;
            sprintVelocity = maxVelocity + 5.0 / tileSize;
            normVelocity = maxVelocity;
        }
        public void UpdateSprint(KeyboardState state, KeyboardState oldState, int tileSize, Character player) {
            if (state.IsKeyDown(Keys.LeftShift)) {
                if (player.CheckStamina()) {
                    maxVelocity = sprintVelocity;
                    player.IsSprinting = true;
                } else {
                    maxVelocity = normVelocity;
                }
            } else if (state.IsKeyUp(Keys.LeftShift) && oldState.IsKeyDown(Keys.LeftShift)) {
                maxVelocity = normVelocity;
                player.IsSprinting = false;
            }
        }
        public double UpdateY(double yVelocity, double timeElapsed, KeyboardState state) {
            if (((state.IsKeyDown(Keys.W)) && yVelocity > maxVelocity)) {
                yVelocity -= timeElapsed * acceleration;
                if (yVelocity < maxVelocity) {
                    yVelocity = maxVelocity;
                }
            } else if ((state.IsKeyDown(Keys.S)) && yVelocity < -1 * maxVelocity) {
                yVelocity += timeElapsed * acceleration;
                if (yVelocity > -1 * maxVelocity) {
                    yVelocity = -1 * maxVelocity;
                }
            }

            //WASD movement controls
            //______________________COMBOS_____________________________
            if (state.IsKeyDown(Keys.W) && (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.A))) {
                //if less than or equal to, increase by acceleration
                if (yVelocity < maxVelocity) {
                    yVelocity += (timeElapsed * Math.Sqrt((acceleration * acceleration / 2)));
                    //if above max velocity, set it to max velocity
                    if (yVelocity > maxVelocity) {
                        yVelocity = maxVelocity;
                    }
                }
            } else if (state.IsKeyDown(Keys.S) && (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.A))) {
                //if less than or equal to, increase by acceleration
                if (yVelocity > -1 * maxVelocity) {
                    yVelocity -= (timeElapsed * Math.Sqrt((acceleration * acceleration / 2)));
                    //if above max velocity, set it to max velocity
                    if (yVelocity > maxVelocity) {
                        yVelocity = maxVelocity;
                    }
                }
            }
             //______________________W KEY_____________________________
             else if (state.IsKeyDown(Keys.W)) {
                //if less than or equal to, increase by acceleration
                if (yVelocity < maxVelocity) {
                    yVelocity += (timeElapsed * acceleration);
                    //if above max velocity, set it to max velocity
                    if (yVelocity > maxVelocity) {
                        yVelocity = maxVelocity;
                    }
                }
            }
             //__________________________________S KEY_____________________________________
             else if (state.IsKeyDown(Keys.S)) {
                //if less than or equal to, increase by acceleration
                if (yVelocity > -1 * maxVelocity) {
                    yVelocity -= (timeElapsed * acceleration);
                    //if more, set it to max
                    if (yVelocity < -1 * maxVelocity) {
                        yVelocity = -1 * maxVelocity;
                    }
                }
            }
             //decelerate y axis________________________________________________________
             else {
                if ((state.IsKeyUp(Keys.W)) && yVelocity > 0) {
                    yVelocity -= timeElapsed * acceleration;
                    if (yVelocity < 0) {
                        yVelocity = 0;
                    }
                }
                if ((state.IsKeyUp(Keys.S)) && yVelocity < 0) {
                    yVelocity += timeElapsed * acceleration;
                    if (yVelocity > 0) {
                        yVelocity = 0;
                    }
                }
            }
            return yVelocity;
        }
        public double UpdateX(double xVelocity, double timeElapsed, KeyboardState state) {
            if (((state.IsKeyDown(Keys.A)) && xVelocity > maxVelocity)) {
                xVelocity -= timeElapsed * acceleration;
                if (xVelocity < maxVelocity) {
                    xVelocity = maxVelocity;
                }
            } else if ((state.IsKeyDown(Keys.D)) && xVelocity < -1 * maxVelocity) {
                xVelocity += timeElapsed * acceleration;
                if (xVelocity > -1 * maxVelocity) {
                    xVelocity = -1 * maxVelocity;
                }
            }
            //______________________COMBOS_____________________________
            if (state.IsKeyDown(Keys.A) && (state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.S))) {
                //if less than or equal to, increase by acceleration
                if (xVelocity < maxVelocity) {
                    xVelocity += (timeElapsed * Math.Sqrt((acceleration * acceleration / 2)));
                    //if above max velocity, set it to max velocity
                    if (xVelocity > maxVelocity) {
                        xVelocity = maxVelocity;
                    }
                }
            } else if (state.IsKeyDown(Keys.D) && (state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.S))) {
                //if less than or equal to, increase by acceleration
                if (xVelocity > -1 * maxVelocity) {
                    xVelocity -= (timeElapsed * Math.Sqrt((acceleration * acceleration / 2)));
                    //if above max velocity, set it to max velocity
                    if (xVelocity > maxVelocity) {
                        xVelocity = maxVelocity;
                    }
                }
            }
            //__________________________________A KEY____________________________________
            else if (state.IsKeyDown(Keys.A)) {
                //if less than or equal to, increase by acceleration
                if (xVelocity < maxVelocity) {
                    xVelocity += (timeElapsed * acceleration);
                    //if above max velocity, set it to max velocity
                    if (xVelocity > maxVelocity) {
                        xVelocity = maxVelocity;
                    }
                }
            }
            //__________________________________D KEY_____________________________________
            else if (state.IsKeyDown(Keys.D)) {
                //if less than or equal to, increase by acceleration
                if (xVelocity > -1 * maxVelocity) {
                    xVelocity -= (timeElapsed * acceleration);
                    //if more, set it to max
                    if (xVelocity < -1 * maxVelocity) {
                        xVelocity = -1 * maxVelocity;
                    }
                }
            } else {
                if (((state.IsKeyUp(Keys.W)) && xVelocity > 0)) {
                    xVelocity -= timeElapsed * acceleration;
                    if (xVelocity < 0) {
                        xVelocity = 0;
                    }
                }
                if ((state.IsKeyUp(Keys.S)) && xVelocity < 0) {
                    xVelocity += timeElapsed * acceleration;
                    if (xVelocity > 0) {
                        xVelocity = 0;
                    }
                }

            }
            return xVelocity;
        }

    }

}
