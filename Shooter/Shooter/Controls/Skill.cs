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
        //Constructor to set up the values for everything
        public Skill(ContentManager content, string text, string name, double rRate, double uRate) {
            texture = content.Load<Texture2D>(text);
            skillName = name;
            useRate = uRate;
            rechargeRate = rRate;
        }
        //Method to be overridden by child classes
        public virtual void ActivateSkill() { }
        //Method to be overridden by child classes
        public virtual void DeactivateSkill() { }
    }
}
