using Microsoft.Xna.Framework.Content;
using Shooter.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shooter.Controls {
    class OverCharged : Skill {
        protected Character player;
        protected int prevHp;
        public OverCharged(ContentManager content, Character p) : base(content, "NoTexture", "Overcharged", 6000, 3000) {
            player = p;
        }

        public override void ActivateSkill() {
            prevHp = player.Health;
            player.Health = 1;
            player.Weapon.Damage *= 2;
            active = true;
        }

        public override void DeactivateSkill() {
            player.Health = prevHp;
            player.Weapon.Damage /= 2;
            active = false;
        }
    }
}
