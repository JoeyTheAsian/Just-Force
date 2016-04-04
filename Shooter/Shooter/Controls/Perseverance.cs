using Microsoft.Xna.Framework.Content;
using Shooter.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shooter.Controls {
    class Perseverance : Skill {
        protected Character player;
        public Perseverance(ContentManager content, Character p) : base(content, "NoTexture", "Perseverance", 6000, 3000) {
            player = p;
        }

        public override void ActivateSkill() {
            player.Health *= 2;
            player.Weapon.Damage /= 2;
            active = true;
        }

        public override void DeactivateSkill() {
            player.Health /= 2;
            player.Weapon.Damage *= 2;
            active = false;
        }
    }
}
