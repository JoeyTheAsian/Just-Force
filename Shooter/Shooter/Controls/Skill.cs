using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shooter.Controls {
    class Skill {
        //Variables for texture and name
        protected Texture2D texture;
        protected string skillName;
        //Value for how fast the skill is drained
        protected double useRate;
        //Value for how fast the skill is recharged
        protected double rechargeRate;
        //Bool for the if the skill is active
        protected bool active;
        //Timer for usage
        protected double timer;
        //Timer for recharge
        protected double reTimer;
        //Bool to see if the player has the skill
        protected bool obtained;

        //Property for the texture
        public Texture2D Texture {
            get { return texture; }
            set { texture = value; }
        }
        //Property for the skill's name
        public string SkillName {
            get { return skillName; }
            set { skillName = value; }
        }
        //Property for skill's usage rate
        public double UseRate {
            get { return useRate; }
            set { useRate = value; }
        }
        //Property for the skill's recharge rate
        public double RechargeRate {
            get { return rechargeRate; }
            set { rechargeRate = value; }
        }
        //Property for if the skill is active
        public bool Active {
            get { return active; }
            set { active = value; }
        }
        //Property for if the skill's use timer
        public double Timer {
            get { return timer; }
            set { timer = value; }
        }
        //Property for the skill's recharge timer
        public double ReTimer {
            get { return reTimer; }
            set { reTimer = value; }
        }
        //Property for the skill's recharge timer
        public bool Obtained {
            get { return obtained; }
            set { obtained = value; }
        }
        //Constructor to set up the values for everything
        public Skill(ContentManager content, string text, string name, double rRate, double uRate) {
            texture = content.Load<Texture2D>(text);
            skillName = name;
            useRate = uRate;
            rechargeRate = rRate;
            timer = 0;
            reTimer = 100;
            obtained = false;
        }
        //Method to be overridden by child classes
        public virtual void ActivateSkill() { }
        //Method to be overridden by child classes
        public virtual void DeactivateSkill() { }

        public virtual void Status(int time) {
            //Sets the timer values to the max or min
            if (timer > 100) {
                timer = 100;
            } else if (timer < 0) {
                timer = 0;
            }
            //Sets the timer values to the max or min
            if (ReTimer > 100) {
                ReTimer = 100;
            } else if (ReTimer < 0) {
                ReTimer = 0;
            }
            //if the timer is greater than zero than decrement it by the skill's usage rate and time
            if (timer > 0) {
                timer -= time / UseRate;
                //If the skills was active and the timer has run out then deactivates the skills
            } else if (timer <= 0 && Active) {
                DeactivateSkill();
                //If the skill is not active then recharges it by the recharge rate and time
            } else if (!Active && reTimer < 100) {
                reTimer += time / RechargeRate;
            }
        }
    }
}
