using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shooter.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shooter.Controls {
    static class HUD {
        //BACKUP CODE TO PASTE BACK IN MAIN GAME CLASS IN CASE THIS CODE RUNS INTO ERRORS
        /*//Draws HUD___________________________________________________________________________________________
        //weapon indicator at bottom right
        spriteBatch.Draw(player.Weapon.Texture, new Rectangle(screenWidth - player.Weapon.Texture.Width * 2/3,
                                                              screenHeight - player.Weapon.Texture.Height * 2/3, 
                                                              player.Weapon.Texture.Width / 3, 
                                                              player.Weapon.Texture.Height / 3), Color.White);
        //Weapon name
        spriteBatch.DrawString(arial, player.Weapon.Name , new Vector2(screenWidth - player.Weapon.Texture.Width * 1 / 3, 
                                                                       screenHeight - player.Weapon.Texture.Height * 1 / 3 - arial.MeasureString(player.Weapon.Name).Y), Color.Red);
        //health bar background
        spriteBatch.Draw(health, new Rectangle(screenWidth / 20 + (screenWidth / 100), 
                                               screenHeight / 20 + (int)(screenHeight / 52), 
                                               screenWidth / 4 - (screenWidth / 50), 
                                               screenHeight / 15 - (int)(screenHeight / 30)), Color.Black);
        //health
        spriteBatch.Draw(health, new Rectangle(screenWidth / 20 + (screenWidth/100), 
                                               screenHeight / 20 + (int)(screenHeight/52), 
                                               (screenWidth / 4 - (screenWidth/50)) * (player.Health/player.MaxHealth), 
                                               screenHeight / 15 - (int)(screenHeight / 30)), Color.Magenta);
        //health bar
        spriteBatch.Draw(healthBar, new Rectangle(screenWidth/20, screenHeight/20, screenWidth / 4, screenHeight / 15),Color.White);

        //ammo
        for(int i = 0; i < player.Weapon.Ammo.Count; i++) {
            spriteBatch.Draw(health, new Rectangle(screenWidth * 6 / 7 - 2, 
                                                  (screenHeight / 18) + (i * 35) - 2,
                                                  player.Weapon.maxAmmo * 19 + 2,
                                                  31), Color.DarkGray);
            for(int j = 0; j < player.Weapon.Ammo[i]; j++) {
                spriteBatch.Draw(player.Weapon.AmmoTexture, 
                                new Rectangle(screenWidth * 6 / 7 + (j * 19),
                                             (screenHeight / 18) + (i * 35) - 2,
                                              19, 31), Color.White);
            }
        }
        spriteBatch.DrawString(arial, "Ammo Left: " + player.Weapon.TotalAmmo(), new Vector2(screenWidth * 6 / 7 - 2,
                                                                                            (screenHeight / 18) - arial.MeasureString(" ").Y - 10) , Color.Red);
        //___________________________________________________________________________________________*/
        public static void DrawHUD(Character player, ref SpriteBatch spriteBatch, int screenHeight, int screenWidth, SpriteFont arial, Texture2D health, Texture2D healthBar) {
            //Draws HUD___________________________________________________________________________________________
            //weapon indicator at bottom right
            spriteBatch.Draw(player.Weapon.Texture, new Rectangle(screenWidth - player.Weapon.Texture.Width * 2 / 3,
                                                                    screenHeight - player.Weapon.Texture.Height * 2 / 3,
                                                                    player.Weapon.Texture.Width / 3,
                                                                    player.Weapon.Texture.Height / 3), Color.White);
            //Weapon name
            spriteBatch.DrawString(arial, player.Weapon.Name, new Vector2(screenWidth - player.Weapon.Texture.Width * 1 / 2,
                                                                            screenHeight - player.Weapon.Texture.Height * 1 / 3 - arial.MeasureString(player.Weapon.Name).Y), Color.Red);
            //health bar background
            spriteBatch.Draw(health, new Rectangle(screenWidth / 20 + (screenWidth / 100),
                                                    screenHeight / 20 + (int)(screenHeight / 52),
                                                    screenWidth / 4 - (screenWidth / 50),
                                                    screenHeight / 15 - (int)(screenHeight / 30)), Color.Black);
            //health
            spriteBatch.Draw(health, new Rectangle(screenWidth / 20 + (screenWidth / 100),
                                                    screenHeight / 20 + (int)(screenHeight / 52),
                                                    (screenWidth / 4 - (screenWidth / 50)) * (player.Health / player.MaxHealth),
                                                    screenHeight / 15 - (int)(screenHeight / 30)), Color.Magenta);
            //health bar
            spriteBatch.Draw(healthBar, new Rectangle(screenWidth / 20, screenHeight / 20, screenWidth / 4, screenHeight / 15), Color.White);

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
            spriteBatch.DrawString(arial, "Ammo Left: " + player.Weapon.TotalAmmo() + " / " + player.Weapon.Ammo.Count, new Vector2(screenWidth * 6 / 7 - 2,
                                                                                                (screenHeight / 18) - arial.MeasureString(" ").Y - 10), Color.Red);
        }
    }
}
