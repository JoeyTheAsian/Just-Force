using Microsoft.Xna.Framework.Content;
using Shooter.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shooter.Controls
{
    class Rhinoceros : Skill
    {
        //Constructor that takes the player object, and passes up static names for this skill
        public Rhinoceros(ContentManager content) : base(content, "NoTexture", "Rhinoceros", 6000, 3000) {}
        //Stores the player's previous health, doubles the player's damage, halves the player's health and sets the skill to active
        public override void ActivateSkill() {
            //Sets the skill to active and starts the two timers
            active = true;
            timer = 100;
            reTimer = 0;
        }
        //Restores the player's previous health, halves their weapon damage and sets the player's skill to deactive
        public override void DeactivateSkill() {
            //Sets the skill to deactive
            active = false;
        }
    }
}
