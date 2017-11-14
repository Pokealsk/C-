using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameMaker.Libraries;
using GameMaker.Game.Characters;

namespace GameMaker.Game.Command_Processing
{
    class InputProcessor
    {
        public void processMainInput()
        {
            Console.WriteLine("Please choose an option: ");
            String input = Console.ReadLine();
            Boolean validInput = false;
            while (!validInput)
            {
                switch (input.ToLower())
                {
                    case "help":
                        Library.GameHelp.printMainHelp();
                        validInput = true;
                        break;
                    case "explore":
                        Library.GameHelp.printExplorationHelp();
                        validInput = true;
                        break;

                    default:
                        Console.WriteLine("Please enter a valid command or -1 to leave");
                        break;

                }
            }
        }
        public void processCombatInput(Character character, Monster monster)
        {
            Console.WriteLine("Please choose an option: ");
            String input = Console.ReadLine();
            Boolean validInput = false;
            while (!validInput)
            {
                switch (input.ToLower())
                {
                    case "Fight":
                        Library.GameHelp.printMainHelp();
                        validInput = true;

                        break;
                    case "explore":
                        Library.GameHelp.printExplorationHelp();
                        validInput = true;
                        break;

                    default:
                        Console.WriteLine("Please enter a valid command or -1 to leave");
                        break;

                }
            }
        }
    }
}
