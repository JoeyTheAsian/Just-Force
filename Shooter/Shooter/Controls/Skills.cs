using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Shooter.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shooter.Controls {
    static class Skills {
        //Static array for the skills in the game
        public static Skill[] skills = new Skill[2];
        public static double timer = 0;
        public static double reTimer = 100;
        //Creates the wepaons that are int the game
        public static void CreateSkills(ContentManager Content, Character p) {
            //Adds the Overcharged skill
            skills[0] = new OverCharged(Content, p);
            //Adds the Perseverance skill
            skills[1] = new Perseverance(Content, p);
        }

        //Uses the player's skill
        public static void UseSkill(Character player, KeyboardState state, KeyboardState oldState, int time) {
            //Checks to see if the key is pressed
            if(state.IsKeyDown(Keys.X) && oldState.IsKeyUp(Keys.X) && !player.Skill.Active && reTimer >= 100) {
                //Activate the skill and sets the timers
                player.Skill.ActivateSkill();
                player.Skill.Active = true;
                timer = 100;
                reTimer = 0;
            }
            //if the timer is greater than zero than decrement it by the skill's usage rate and time
            if (timer > 0) {
                timer -= time / player.Skill.UseRate;
            //If the skills was active and the timer has run out then deactivates the skills
            } else if(timer <= 0 && player.Skill.Active){
                player.Skill.DeactivateSkill();
            //If the skill is not active then recharges it by the recharge rate and time
            } else if(!player.Skill.Active && reTimer < 100) {
                reTimer += time / player.Skill.RechargeRate;
            }
            
        }

        //Switches the player's skill
        public static void SwitchSkills(Character player, KeyboardState state, KeyboardState oldState) {
            //Press Q to switch the player's skill
            if (state.IsKeyDown(Keys.Q) && oldState.IsKeyUp(Keys.Q)) {
                //Gets the index of the player's current skill
                int index = 0;
                for (int i = 0; i < skills.Length; i++) {
                    if (skills[i].SkillName.Equals(player.Skill.SkillName)) {
                        //Sets the new index to the old one plus one and breaks the loop
                        index = i + 1;
                        break;
                    }
                }
                
                //Makes sure there is a skill in the next slot or sets it to the first slot
                if (index == skills.Length) {
                    index = 0;
                }
                //Changes the skill
                player.Skill = skills[index];
            }
        }
     }
}
