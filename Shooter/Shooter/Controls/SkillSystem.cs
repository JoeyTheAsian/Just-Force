using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Shooter.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shooter.Controls {
    static class SkillSystem {
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
            if(state.IsKeyDown(Keys.D1) && oldState.IsKeyUp(Keys.D1) && !CheckActiveSkills() && skills[0].Obtained) {
                //Activate the skill and sets the timers
                skills[0].ActivateSkill();
            } else if (state.IsKeyDown(Keys.D2) && oldState.IsKeyUp(Keys.D2) && !CheckActiveSkills() && skills[1].Obtained) {
                //Activate the skill and sets the timers
                skills[1].ActivateSkill();
            }
            foreach (Skill s in skills) {
                    s.Status(time);                
            }           
        }

        /*//Switches the player's skill
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
        }*/
        public static bool CheckActiveSkills() {
            foreach(Skill s in skills) {
                if (s.Active || s.ReTimer < 100) {
                    return true;
                }                
            }
            return false;
        }
     }
}
