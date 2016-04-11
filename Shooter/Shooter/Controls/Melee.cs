using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shooter.Entities;
using Microsoft.Xna.Framework;

namespace Shooter.Controls {
    class Melee : Weapon{
        //Construcotr that creates a melee weapon object
        public Melee(ContentManager content, bool au, double spr, int fr, string t, int d, string ammoT, string n, int maxAm, int ammoC, double rlTime) : base(content, au, spr, fr, t, d, ammoT, n, maxAm, ammoC, rlTime) {}

        //Overrrides the shoot method to create a more accurate short range projectile
        public override Projectile Shoot(ContentManager content, Character p, Camera c, int tileSize) {
            return new Projectile(content, p.Loc.X, p.Loc.Y, p.Direction + GetSpread() * (Math.PI / 180.0), 5, "Blade_Temp", true,
                                                new Rectangle((int)((c.camPos.X + p.Loc.X) * tileSize), (int)((c.camPos.Y + p.Loc.Y) * tileSize), tileSize, tileSize), 1);
        }
    }
}
