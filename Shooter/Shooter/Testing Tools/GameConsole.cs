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
            commands.Add("Exit", "exit");
            commands.Add("Update Fps", "UpdateFPS");
            commands.Add("Create Normal Enemy", "CreateNormalEnemy");
            commands.Add("Create Riot Enemy", "CreateRiotEnemy");
            commands.Add("Fill player ammo", "player.weapon");
            commands.Add("Super Health", "player.Health");
            commands.Add("Print", "");
            commands.Add("Print Enemies", "printenemies");
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
                string line = Console.ReadLine();

                //Checks to see if the command exists
                if (!commands.ContainsKey(line))
                {
                    Console.WriteLine("Invalid Command");
                    continue;
                }
                //Prints the commands out
                else if (line.Equals("Print"))
                {
                    foreach (string command in commands.Keys)
                    {
                        Console.WriteLine(command);
                    }
                }
                //Checks if the command wants an enemy then gets the necessary location for the enemy
                else if (line.Equals("Create Normal Enemy") || line.Equals("Create Riot Enemy"))
                {
                    Console.WriteLine("Enter X:");
                    int x = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter Y:");
                    int y = int.Parse(Console.ReadLine());
                    if (line.Equals("Create Normal Enemy"))
                    {
                        return commands["Create Normal Enemy"] + "/" + x + "/" + y;
                    }
                    else {
                        return commands["Create Riot Enemy"] + "/" + x + "/" + y;
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
