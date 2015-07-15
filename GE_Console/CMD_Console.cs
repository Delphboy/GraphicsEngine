using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GE_Console
{
    public class CMD_Console
    {
        public static void Main(string[] args)
        {
            string cmd = "";

            Console.WriteLine("Please enter a command. Type 'HELP' for a list of commands");
            Console.Write("CMD: ");

            cmd = Console.ReadLine();

            checkCMD(cmd);
           
        }

        static void checkCMD(string cmd) {
            cmd = cmd.ToUpper();
            

            switch (cmd)
            {
                case "set-X":

                    break;
                case "set-Y":

                    break;

                case "HELP":
                    listCommands();
                    break;
                default:
                    listCommands();
                    break;
            }
        }

        static void listCommands() {
            Console.WriteLine("A List of commands is as follows");
        }
    }
}
