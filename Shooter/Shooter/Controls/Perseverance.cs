using Microsoft.Xna.Framework.Content;
using Shooter.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shooter.Controls {
    class Perseverance : Skill {
        //Object to store a player
        protected Character player;
        //Constructor that takes the player object, and passes up static names for this skill
        public Perseverance(ContentManager content, Character p) : base(content, "NoTexture", "Perseverance", 6000, 3000) {
            player = p;
        }
        //Doubles the player's health, halves the player's damage and sets the skill to active
        public override void ActivateSkill() {
            player.Health *= 2;
            player.Weapon.Damage /= 2;
            //Sets the skill to active and starts the two timers
            active = true;
            timer = 100;
            reTimer = 0;
        }
        //Doubles the player's damage, halves the player's health and sets the skill to deactive
        public override void DeactivateSkill() {
            player.Health /= 2;
            player.Weapon.Damage *= 2;
            //Sets the skill to deactive
            active = false;
        }
    }
}
