using System;
using Shooter.Testing_Tools;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shooter.Testing_Tools {
    class GameConsole {

        //A list to hold commands
        private Dictionary<string, string> commands;

        public GameConsole() {


            //Initializes the list
            commands = new Dictionary<string, string>();

            //Adds the commands
            commands.Add("EXIT", "exit");
            commands.Add("UPDATEFPS", "UpdateFps");
            commands.Add("CREATENORMALENEMY", "CREATENORMALENEMY");
            commands.Add("CREATERIOTENEMY", "CREATERIOTENEMY");
            commands.Add("FILLPLAYERAMMO", "player.weapon");
            commands.Add("SUPERHEALTH", "player.Health");
            commands.Add("PRINT", "");
            commands.Add("PRINTENEMIES", "printenemies");
            commands.Add("CREATEHEALTHKIT", "createhealthkit");
            commands.Add("CREATEAMMOKIT", "createammokit");
        }

        /// <summary>
        /// Opens input for the console
        /// </summary>
        public string OpenInput() {
            //Runs until the exit command is typed
            while (true) {
                //Stores the value of the command
                string line = Console.ReadLine().ToUpper();

                //Checks to see if the command exists
                if (!commands.ContainsKey(line)) {
                    Console.WriteLine("Invalid Command");
                    continue;
                }
                //Prints the commands out
                else if (line.Equals("PRINT")) {
                    foreach (string command in commands.Keys) {
                        Console.WriteLine(command);
                    }
                }
                //Checks if the command wants an enemy then gets the necessary location for the enemy
                else if (line.Equals("CREATENORMALENEMY") || line.Equals("CREATERIOTENEMY") || line.Equals("CREATEHEALTHKIT") || line.Equals("CREATEAMMOKIT")) {
                    double x, y;
                    while (true) {
                        Console.WriteLine("Enter X:");
                        if (double.TryParse(Console.ReadLine(), out x)) {
                            break;
                        }
                    }
                    while (true) {
                        Console.WriteLine("Enter Y:");
                        if (double.TryParse(Console.ReadLine(), out y)) {
                            break;
                        }
                    }
                    //Retruns type of object to create
                    if (line.Equals("CREATENORMALENEMY")) {
                        return commands["CREATENORMALENEMY"] + "/" + x + "/" + y;
                    } else if (line.Equals("CREATERIOTENEMY")) {
                        return commands["CREATERIOTENEMY"] + "/" + x + "/" + y;
                    } else if (line.Equals("CREATEHEALTHKIT")) {
                        return commands["CREATEHEALTHKIT"] + "/" + x + "/" + y;
                    }
                    string type = "";
                    //If it is ammo gets type of ammo
                    while (true) {
                        Console.WriteLine("Enter Type(pistol, rifle, shotgun, smg):");
                        type = Console.ReadLine().ToLower();
                        if (type.Equals("pistol") || type.Equals("rifle") || type.Equals("shotgun") || type.Equals("smg")) {
                            break;
                        }
                    }

                    return commands["CREATEAMMOKIT"] + "/" + x + "/" + y + "/" + type;
                }
                //Else returns the command to check
                else {
                    return commands[line];
                }

            }
        }

    }
}
