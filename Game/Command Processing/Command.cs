using GameMaker.Game.Characters;
using GameMaker.Game.Mechanics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2.Game.Command_Processing
{
   public class Command
    {
        /// <summary>
        /// Based on the given phase will tell the user appropriate commands that they can enter
        /// </summary>
        /// <param name="phase"> The phase of the game the user is in</param>
        /// <param name="user"> The character that represents the user's player</param>
        /// <returns>A string with each command separated by newLines</returns>
        public static string getMainCommands(string phase, Character user)
        {
            string returnValue = "Current Command Options: " + "\r\n";            
            switch (phase)
            {
                case "nameInput":
                    returnValue += "Please input any name" + "\r\n";

                    break;
                case "classInput":
                    returnValue += "Please choose one of the given classes" + "\r\n";

                    break;
                case "mainGame":
                    returnValue += "\"Move\" - Moves the user forward. Does not count towards moving to a new area" + "\r\n";
                    returnValue += "\"Advance\" - Moves the user forward. Every 10 advances enters a new area" + "\r\n";

                    break;
                case "combat":
                    returnValue += "\"Attack\" - User attacks the enemy" + "\r\n";
                    returnValue += "\"Cast\" - Moves into spell selection" + "\r\n";
                    returnValue += "\"Run\" - Possibly flees the current enemy (If you successfully flee this will advance you one spot)" + "\r\n";

                    break;
                case "spellCasting":
                    returnValue += "Please choose a spell from your spellbook, to get more info about a spell type \"info:\" <spellname>, your spell options are: " + "\r\n";
                    int subLength = 0;
                    foreach (Spell spell in user.peripherals.spellBook)
                    {
                        if (subLength >= 150)
                        {
                            subLength = 0;
                            returnValue += "\r\n";
                        }
                        subLength += spell.name.Length;
                        returnValue += spell.name + "\t";
                    }

                    break;
                case "boss":

                    break;
                case "lost":
                    returnValue += "\"New Game\" - Starts a new game" + "\r\n";
                    returnValue += "\"End\" - Closes the game" + "\r\n";

                    break;
                default:

                    break;
            }
            return returnValue;
        }
       
        /// <summary>
        /// Just has a small list of good names for the user
        /// </summary>
        /// <returns> One of five random strings representing a name suggestion</returns>
        public static string suggester()
        {
            Random rnd = new Random();
            string[] nameOptions = new string[5];

            nameOptions[0] = "Saitama" + "\r\n";
            nameOptions[1] = "Armstrong" + "\r\n";
            nameOptions[2] = "Ravaging Ravager" + "\r\n";
            nameOptions[3] = "Illustrious Industrious Employee" + "\r\n";
            nameOptions[4] = "Lonely Fisherman" + "\r\n";


            return nameOptions[rnd.Next(nameOptions.Length)];

        }
    }
}

