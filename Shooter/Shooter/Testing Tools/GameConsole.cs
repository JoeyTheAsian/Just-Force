using System;
using Shooter.Testing_Tools;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shooter.Testing_Tools
{
    class GameConsole
    {

        //A list to hold commands
        private Dictionary<string, string> commands;

        public GameConsole()
        {


            //Initializes the list
            commands = new Dictionary<string, string>();

            //Adds the commands
            commands.Add("EXIT", "exit");
            commands.Add("UPDATEFPS", "UpdateFps");
            commands.Add("CREATENORMALENEMY", "CreateNormalEnemy");
            commands.Add("CREATERIOTENEMY", "CreateRiotEnemy");
            commands.Add("FILLPLAYERAMMO", "player.weapon");
            commands.Add("SUPERHEALTH", "player.Health");
            commands.Add("PRINT", "");
            commands.Add("PRINTENEMIES", "printenemies");
        }

        /// <summary>
        /// Opens input for the console
        /// </summary>
        public string OpenInput()
        {
            //Runs until the exit command is typed
            while (true)
            {
                //Stores the value of the command
                string line = Console.ReadLine().ToUpper();

                //Checks to see if the command exists
                if (!commands.ContainsKey(line))
                {
                    Console.WriteLine("Invalid Command");
                    continue;
                }
                //Prints the commands out
                else if (line.Equals("PRINT"))
                {
                    foreach (string command in commands.Keys)
                    {
                        Console.WriteLine(command);
                    }
                }
                //Checks if the command wants an enemy then gets the necessary location for the enemy
                else if (line.Equals("CREATENORMALENEMY") || line.Equals("CREATERIOTENEMY"))
                {
                    Console.WriteLine("Enter X:");
                    int x = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter Y:");
                    int y = int.Parse(Console.ReadLine());
                    if (line.Equals("CREATENORMALENEMY"))
                    {
                        return commands["CREATENORMALENEMY"] + "/" + x + "/" + y;
                    }
                    else {
                        return commands["CREATERIOTENEMY"] + "/" + x + "/" + y;
                    }
                }
                //Else returns the command to check
                else {
                    return commands[line];
                }

            }
        }

    }
}
