using Microsoft.Xna.Framework.Content;
using Shooter.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shooter.Controls {
    class OverCharged : Skill {
        //Object to store a player and their previous hp
        protected Character player;
        protected int prevHp;

        //Constructor that takes the player object, and passes up static names for this skill
        public OverCharged(ContentManager content, Character p) : base(content, "NoTexture", "Overcharged", 6000, 3300) {
            player = p;
        }
        //Stores the player's previous health, doubles the player's damage, halves the player's health and sets the skill to active
        public override void ActivateSkill() {
            prevHp = player.Health;
            player.Health = 1;
            Shooting.weapons[1].Damage *= 2;
            Shooting.weapons[2].Damage *= 2;
            Shooting.weapons[3].Damage *= 2;
            Shooting.weapons[4].Damage *= 2;
            //Sets the skill to active and starts the two timers
            active = true;
            timer = 100;
            reTimer = 0;
        }
        //Restores the player's previous health, halves their weapon damage and sets the player's skill to deactive
        public override void DeactivateSkill() {
            player.Health = prevHp;
            Shooting.weapons[1].Damage /= 2;
            Shooting.weapons[2].Damage /= 2;
            Shooting.weapons[3].Damage /= 2;
            Shooting.weapons[4].Damage /= 2;
            //Sets the skill to deactive
            active = false;
        }
    }
}
