using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MapEditor
{// class for the editor to save / open map files
    class FileHandling
    {
        // attributes
        Stream str;

        // file writer
        public void writer(string n, string[] m) // takes a string argument for the name of the map and the map's 2D array
        {
            try // exception handler
            {
                // turning the map array into just strings
                string map = "";

                foreach (string item in m)
                {
                    map = map + item;
                }

                // create a .dat and write to it
                str = File.OpenWrite(n + ".dat");
                BinaryWriter output = new BinaryWriter(str);
                output.Write(map);
            }
            catch (IOException ioe) // catch exceptions
            {
                Console.WriteLine("Error saving file!" + ioe.Message);
            }
            finally
            {
                str.Close(); // close the file
            }
        }

        // reading in an existing map
        public string[] reader(string n) // string argument for filename
        {
            // array to hold map
            string[] mapAry = new string[];

            try
            {
                // access the file
                str = File.OpenRead(n + ".dat");
                BinaryReader input = new BinaryReader(str);

                // read in the data
                string line = "";
                string map = "";
                while((line = input.ReadString()) != null)
                {
                    map = map + line;
                }

                // put the strings back into an array here               
                
            }
            catch (IOException ioe) // catch exceptions
            {
                Console.WriteLine("Error reading file!" + ioe.Message);
            }
            finally
            {
                str.Close(); // close the file
            }

            // return map array
            return mapAry;
        }
    }
}
