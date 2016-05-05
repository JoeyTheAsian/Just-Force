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

        //Creates a smg ammo kit at a location
        public static void CreateShotgunAmmo(ContentManager Content, List<PickUpItem> Items, double x, double y) {
            Items.Add(new PickUpItem(Content, "Shotgun_Ammo", "shotgunammo", x, y));
        }

        //Creates a smg ammo kit at a location
        public static void CreateRifleAmmo(ContentManager Content, List<PickUpItem> Items, double x, double y) {
            Items.Add(new PickUpItem(Content, "Rifle_Ammo", "rifleammo", x, y));
        }

        //Checks through collsions with pick up items
        public static void CheckItemCollisions(List<PickUpItem> Items, Character player) {
            for (int f = 0; f < Items.Count; f++) {
                if (Items[f].CheckCollide(player)) {
                    if (Items[f].ItemType.Equals("health")) {
                        if (player.Health < player.MaxHealth && !SkillSystem.skills[0].Active) {
                            player.Health++;
                        }
                    } else if (Items[f].ItemType.Equals("pistolammo")) {
                        Shooting.weapons[1].Ammo.Add(9);
                    } else if (Items[f].ItemType.Equals("smgammo")) {
                        Shooting.weapons[2].Ammo.Add(15);
                    } else if (Items[f].ItemType.Equals("shotgunammo")) {
                        Shooting.weapons[3].Ammo.Add(6);
                    } else if (Items[f].ItemType.Equals("rifleammo")) {
                        Shooting.weapons[4].Ammo.Add(4);
                    }
                    Items.RemoveAt(f);
                }
            }
        }
        //Creates a random item at a specified position
        public static void CreateRandomItem(Random rng, ContentManager Content, List<PickUpItem> Items, double x, double y, Character player) {
            //A one in four chance of enemies dropping some sort of item
            int dropRate = rng.Next(0, 6);

            //If the value equals zero then drops a random item
            if (dropRate == 0) {
                int drop = rng.Next(0, 101);
                //Twenty percent chance for health
                if (player.Health < player.MaxHealth && !SkillSystem.skills[0].Active) {
                    if (drop > 80) {
                        CreateHealthKit(Content, Items, x, y);
                    }
                }
                //Thirty percent chance for pistol ammo
                if (drop > 50) {
                    CreatePistolAmmo(Content, Items, x, y);
                    //Fifteen percent chance for SMG ammo
                } else if (drop > 35) {
                    CreateSMGAmmo(Content, Items, x, y);
                    //Twenty percent chance for shotgun ammo
                } else if (drop > 15) {
                    CreateShotgunAmmo(Content, Items, x, y);
                    //Fifteen percent chance for rifle ammo
                } else {
                    CreateRifleAmmo(Content, Items, x, y);
                }
            }
        }

    }
}
