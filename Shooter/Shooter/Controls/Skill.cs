using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shooter.Controls {
    class Skill {
        protected Texture2D texture;
        protected string skillName;
        protected double useRate;
        protected double rechargeRate;
        protected bool active;

        public Texture2D Texture {
            get { return texture; }
            set { texture = value; }
        }
        public string SkillName {
            get { return skillName; }
            set { skillName = value; }
        }
        public double UseRate {
            get { return useRate; }
            set { useRate = value; }
        }
        public double RechargeRate {
            get { return rechargeRate; }
            set { rechargeRate = value; }
        }
        public bool Active {
            get { return active; }
            set { active = value; }
        }

        public Skill(ContentManager content, string text, string name, double rRate, double uRate) {
            texture = content.Load<Texture2D>(text);
            skillName = name;
            useRate = uRate;
            rechargeRate = rRate;
        }

        public virtual void ActivateSkill() { }

        public virtual void DeactivateSkill() { }
    }
}
