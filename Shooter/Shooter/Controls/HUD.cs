using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shooter.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shooter.Controls {
    static class HUD {
        public static void DrawHUD(Character player, ref SpriteBatch spriteBatch, int screenHeight, int screenWidth, SpriteFont arial, Texture2D health, Texture2D healthBar, Texture2D stamina, Texture2D[] skillIcon) {
            //Draws HUD___________________________________________________________________________________________
            //weapon indicator at bottom right
            spriteBatch.Draw(player.Weapon.Texture, new Rectangle(screenWidth - player.Weapon.Texture.Width * 1 / 2,
                                                                    screenHeight - player.Weapon.Texture.Height * 1 / 2,
                                                                    player.Weapon.Texture.Width / 3,
                                                                    player.Weapon.Texture.Height / 3), Color.White);
            //Weapon name
            spriteBatch.DrawString(arial, player.Weapon.Name, new Vector2(screenWidth - player.Weapon.Texture.Width * 1 / 3,
                                                                            screenHeight - player.Weapon.Texture.Height * 1 / 2 - arial.MeasureString(player.Weapon.Name).Y), Color.Red);
            //health bar background
            spriteBatch.Draw(health, new Rectangle(screenWidth / 20 + (screenWidth / 100),
                                                    screenHeight / 20 + (int)(screenHeight / 52),
                                                    screenWidth / 4 - (screenWidth / 50),
                                                    screenHeight / 15 - (int)(screenHeight / 30)), Color.Black);
            //health
            if (SkillSystem.skills[1].Active) {
                spriteBatch.Draw(health, new Rectangle(screenWidth / 20 + (screenWidth / 100),
                                                        screenHeight / 20 + (int)(screenHeight / 52),
                                                        (int)((screenWidth / 4 - (screenWidth / 50)) * (player.Health / (player.MaxHealth * 2 + 0.0))),
                                                        screenHeight / 15 - (int)(screenHeight / 30)), Color.RoyalBlue);
            } else {
                spriteBatch.Draw(health, new Rectangle(screenWidth / 20 + (screenWidth / 100),
                                                        screenHeight / 20 + (int)(screenHeight / 52),
                                                        (int)((screenWidth / 4 - (screenWidth / 50)) * (player.Health / (player.MaxHealth + 0.0))),
                                                        screenHeight / 15 - (int)(screenHeight / 30)), Color.Magenta);
            }
            //Draws background for stamina
            spriteBatch.Draw(health, new Rectangle(screenWidth / 20 + (screenWidth / 100),
                                                    screenHeight / 10 + (int)(screenHeight / 52),
                                                    screenWidth / 4 - (screenWidth / 50),
                                                    screenHeight / 15 - (int)(screenHeight / 30)), Color.Black);
            //Draws stamina bar
            spriteBatch.Draw(stamina, new Rectangle(screenWidth / 20 + (screenWidth / 100),
                                                        screenHeight / 10 + (int)(screenHeight / 52),
                                                        (int)((screenWidth / 4 - (screenWidth / 50)) * (player.Stamina / (100.0))),
                                                        screenHeight / 15 - (int)(screenHeight / 30)), Color.White);
            //stamina bar
            spriteBatch.Draw(healthBar, new Rectangle(screenWidth / 20, (int)(screenHeight / 8.5), screenWidth / 4, screenHeight / 25), Color.White);
            //health bar
            spriteBatch.Draw(healthBar, new Rectangle(screenWidth / 20, screenHeight / 20, screenWidth / 4, screenHeight / 15), Color.White);

            //Skill Icons
            spriteBatch.Draw(health, new Rectangle(screenWidth / 20 + (screenWidth / 100),
                                                    (int)(screenHeight / 6.3),
                                                    (screenWidth / 28) * 3,
                                                    (int)(screenHeight / 14.5) - (int)(screenHeight / 30)), Color.SlateGray);
            //If no skills are active then draw them gray
            if (!SkillSystem.CheckActiveSkills()) {
                spriteBatch.Draw(skillIcon[0], new Rectangle((screenWidth / 20 + (screenWidth / 100)) + 3, (int)(screenHeight / 6.3), screenWidth / 30, screenHeight / 30), Color.White);
                spriteBatch.Draw(skillIcon[1], new Rectangle((screenWidth / 20 + (screenWidth / 100) + (screenWidth / 30)) + 4, (int)(screenHeight / 6.3), screenWidth / 30, screenHeight / 30), Color.White);
                spriteBatch.Draw(skillIcon[2], new Rectangle((screenWidth / 20 + (screenWidth / 100) + (screenWidth / 30) * 2) + 5, (int)(screenHeight / 6.3), screenWidth / 30, screenHeight / 30), Color.White);
            } else {
                //If the first one is active then draw it green and the others red
                if (SkillSystem.skills[0].Active) {
                    spriteBatch.Draw(skillIcon[0], new Rectangle((screenWidth / 20 + (screenWidth / 100)) + 3, (int)(screenHeight / 6.3), screenWidth / 30, screenHeight / 30), Color.Green);
                    spriteBatch.Draw(skillIcon[1], new Rectangle((screenWidth / 20 + (screenWidth / 100) + (screenWidth / 30)) + 4, (int)(screenHeight / 6.3), screenWidth / 30, screenHeight / 30), Color.Red);
                    spriteBatch.Draw(skillIcon[2], new Rectangle((screenWidth / 20 + (screenWidth / 100) + (screenWidth / 30) * 2) + 5, (int)(screenHeight / 6.3), screenWidth / 30, screenHeight / 30), Color.Red);
                    //If the second one is active then draw it green and the others red
                } else if (SkillSystem.skills[1].Active) {
                    spriteBatch.Draw(skillIcon[0], new Rectangle((screenWidth / 20 + (screenWidth / 100)) + 3, (int)(screenHeight / 6.3), screenWidth / 30, screenHeight / 30), Color.Red);
                    spriteBatch.Draw(skillIcon[1], new Rectangle((screenWidth / 20 + (screenWidth / 100) + (screenWidth / 30)) + 4, (int)(screenHeight / 6.3), screenWidth / 30, screenHeight / 30), Color.Green);
                    spriteBatch.Draw(skillIcon[2], new Rectangle((screenWidth / 20 + (screenWidth / 100) + (screenWidth / 30) * 2) + 5, (int)(screenHeight / 6.3), screenWidth / 30, screenHeight / 30), Color.Red);
                    //If the third one is active then draw it green and the others red
                } else if (SkillSystem.skills[2].Active) {
                    spriteBatch.Draw(skillIcon[0], new Rectangle((screenWidth / 20 + (screenWidth / 100)) + 3, (int)(screenHeight / 6.3), screenWidth / 30, screenHeight / 30), Color.Red);
                    spriteBatch.Draw(skillIcon[1], new Rectangle((screenWidth / 20 + (screenWidth / 100) + (screenWidth / 30)) + 4, (int)(screenHeight / 6.3), screenWidth / 30, screenHeight / 30), Color.Red);
                    spriteBatch.Draw(skillIcon[2], new Rectangle((screenWidth / 20 + (screenWidth / 100) + (screenWidth / 30) * 2) + 5, (int)(screenHeight / 6.3), screenWidth / 30, screenHeight / 30), Color.Green);
                    //If none are able to be used then draw them red
                } else {
                    spriteBatch.Draw(skillIcon[0], new Rectangle((screenWidth / 20 + (screenWidth / 100)) + 3, (int)(screenHeight / 6.3), screenWidth / 30, screenHeight / 30), Color.Red);
                    spriteBatch.Draw(skillIcon[1], new Rectangle((screenWidth / 20 + (screenWidth / 100) + (screenWidth / 30)) + 4, (int)(screenHeight / 6.3), screenWidth / 30, screenHeight / 30), Color.Red);
                    spriteBatch.Draw(skillIcon[2], new Rectangle((screenWidth / 20 + (screenWidth / 100) + (screenWidth / 30) * 2) + 5, (int)(screenHeight / 6.3), screenWidth / 30, screenHeight / 30), Color.Red);
                }
            }


            //ammo
            for (int i = 0; i < player.Weapon.Ammo.Count && i < 8; i++) {
                spriteBatch.Draw(health, new Rectangle(screenWidth * 6 / 7 - 2,
                                                        (screenHeight / 18) + (i * 35) - 2,
                                                        player.Weapon.maxAmmo * 18 + 2,
                                                        31), Color.DarkGray);
                for (int j = 0; j < player.Weapon.Ammo[i]; j++) {
                    spriteBatch.Draw(player.Weapon.AmmoTexture,
                                    new Rectangle(screenWidth * 6 / 7 + (j * 17),
                                                    (screenHeight / 18) + (i * 35) - 2,
                                                    19, 31), Color.White);
                }
            }
            spriteBatch.DrawString(arial, "Ammo Left: " + player.Weapon.TotalAmmo() + " / " + player.Weapon.Ammo.Count, new Vector2((int)(screenWidth * 5.3 / 7 - 2),
                                                                                                (screenHeight / 18) - arial.MeasureString(" ").Y - 10), Color.Red);
        }
    }
}
