using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Shooter.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shooter.Controls
{
    static class CreateItems
    {
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
        public static void CheckItemCollisions(List<PickUpItem> Items, Character player, ContentManager Content, Queue<SoundEffect> curSounds) {
            for (int f = 0; f < Items.Count; f++) {
                if (Items[f].CheckCollide(player)) {
                    if (Items[f].ItemType.Equals("health")) {
                        if (player.Health < player.MaxHealth && !SkillSystem.skills[0].Active) {
                            player.Health += 2;
                            //curSounds.Enqueue(Content.Load<SoundEffect>("healthSound"));
                        } else {
                            return;
                        }
                    } else if (Items[f].ItemType.Equals("pistolammo")) {
                        Shooting.weapons[1].Ammo.Add(9);
                        //curSounds.Enqueue(Content.Load<SoundEffect>("ammoSound"));
                    } else if (Items[f].ItemType.Equals("smgammo")) {
                        Shooting.weapons[2].Ammo.Add(15);
                        //curSounds.Enqueue(Content.Load<SoundEffect>("ammoSound"));
                    } else if (Items[f].ItemType.Equals("shotgunammo")) {
                        Shooting.weapons[3].Ammo.Add(6);
                        //curSounds.Enqueue(Content.Load<SoundEffect>("ammoSound"));
                    } else if (Items[f].ItemType.Equals("rifleammo")) {
                        Shooting.weapons[4].Ammo.Add(4);
                        //curSounds.Enqueue(Content.Load<SoundEffect>("ammoSound"));
                    }
                    Items.RemoveAt(f);
                }
            }
        }
        //Creates a random item at a specified position
        public static void CreateRandomItem(Random rng, ContentManager Content, List<PickUpItem> Items, double x, double y, Character player) {
            //A one in four chance of enemies dropping some sort of item
            int dropRate = rng.Next(0, 5);

            //If the value equals zero then drops a random item
            if (dropRate == 0 || dropRate == 1) {
                int drop = rng.Next(0, 101);
                // health
                if (drop > 90 && Shooting.weapons[4].IsAcquired) {
                    CreateRifleAmmo(Content, Items, x, y);               
                }
                //Thirty percent chance for rifle ammo
                else if (drop > 70 && Shooting.weapons[3].IsAcquired) {
                    CreateShotgunAmmo(Content, Items, x, y);
                    //Fifteen percent chance for SMG ammo
                } else if (drop > 55 && Shooting.weapons[2].IsAcquired) {
                    CreateSMGAmmo(Content, Items, x, y);
                    //Twenty percent chance for shotgun ammo
                } else if (drop > 30) {
                    CreatePistolAmmo(Content, Items, x, y);
                    //Fifteen percent chance for rifle ammo
                } else {
                    CreateHealthKit(Content, Items, x, y);
                }
            }
        }

    }
}
