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
        private List<string> commands;

        //Fps object to handle fps
        private FPSHandling fpsHandler;


        public GameConsole(FPSHandling fps) {
            //Stores the fps parameter in the class
            fpsHandler = fps;

            //Initializes the list
            commands = new List<string>();

            //Adds the commands
            commands.Add("exit");
            commands.Add("UpdateFPS");
        }

        /// <summary>
        /// Opens input for the console
        /// </summary>
        public void OpenInput()
        {
            //Runs until the exit command is typed
            while (true)
            {
                //Stores the value of the command
                string line = Console.ReadLine();

                //Checks to see if the command exists
                if (!Check(line)){
                    Console.WriteLine("Invalid Command");
                }
                //Checks to exit the console
                else if (line.Equals(commands[0]))
                {
                    //breaks the loop and exits
                    break;
                }
                //Updates the fps
                else if (line.Equals(commands[1]))
                {
                    fpsHandler.UpdateFPS();
                    Console.WriteLine("Updated");
                }
            }
        }

        /// <summary>
        /// Method to check if the command exists in the list
        /// </summary>
        /// <param name="line">line to check</param>
        /// <returns>Returns if the object is in the list or not</returns>
        public bool Check(string line)
        {
            //Searches through the list
            foreach(string com in commands)
            {
                //Returns true and stops searching
                if (com.Equals(line))
                {
                    return true;
                }
            }

            //Returns false if never found
            return false;
        }
    }

}
