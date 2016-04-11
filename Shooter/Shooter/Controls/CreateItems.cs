using Microsoft.Xna.Framework.Content;
using Shooter.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shooter.Controls {
    static class CreateItems {
        //Creates a healthkit at a location
        public static void CreateHealthKit(ContentManager Content, List<PickUpItem> Items, double x, double y) {
            Items.Add(new PickUpItem(Content, "Health_Kit", "health", x, y));
        }

        //Creates a pistol ammo kit at a location
        public static void CreatePistolAmmo(ContentManager Content, List<PickUpItem> Items, double x, double y) {
            Items.Add(new PickUpItem(Content, "Pistol_Ammo", "pistolammo", x, y));
        }

        //Creates a smg ammo kit at a location
        public static void CreateSMGAmmo(ContentManager Content, List<PickUpItem> Items, double x, double y) {
            Items.Add(new PickUpItem(Content, "Submachine_Gun_Ammo", "smgammo", x, y));
        }

        //Checks through collsions with pick up items
        public static void CheckItemCollisions(List<PickUpItem> Items, Character player) {
            for (int f = 0; f < Items.Count; f++) {
                if (Items[f].CheckCollide(player)) {
                    if (Items[f].ItemType.Equals("health")) {
                        player.Health++;
                    } else if (Items[f].ItemType.Equals("smgammo")) {
                        Shooting.weapons[2].Ammo.Add(15);
                    } else if (Items[f].ItemType.Equals("pistolammo")) {
                        Shooting.weapons[1].Ammo.Add(9);
                    }
                    Items.RemoveAt(f);
                }
            }
        }

    }
}
