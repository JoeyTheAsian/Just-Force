using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using System.Text;

namespace Shooter.Controls {
    class Movement {
        private double maxVelocity;
        private double acceleration;
        public Movement(double mv, double accel) {
            maxVelocity = mv;
            acceleration = accel;
        }
        public void UpdateSprint(KeyboardState state, KeyboardState oldState, int tileSize) {
            if (state.IsKeyDown(Keys.LeftShift) && oldState.IsKeyUp(Keys.LeftShift)) {
                maxVelocity += 5.0 / tileSize;
            }
            else if (state.IsKeyUp(Keys.LeftShift) && oldState.IsKeyDown(Keys.LeftShift)) {
                maxVelocity -= 5.0 / tileSize;
            }
        }
        public double UpdateY(double yVelocity, double timeElapsed, KeyboardState state) {
            //WASD movement controls
            //______________________W KEY_____________________________
            if (state.IsKeyDown(Keys.W)) {
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
            //__________________________________A KEY____________________________________
            if (state.IsKeyDown(Keys.A)) {
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
                if ((state.IsKeyUp(Keys.W)) && xVelocity > 0) {
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
