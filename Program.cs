using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameMaker;
using GameMaker.Game.Characters;
using GameMaker.Libraries;
using GameMaker.Game;
using GameMaker.Game.Calculations;

using System.Windows.Forms;
using GameUI;

namespace GameMaker
{
    public class Program
    {
        public static void Main(string[] args)
        {            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GameUI.GameUI());
            
            //Average experience for Forest: 80
            //Average for cave: 290
            //Average for desert: 600
            //Average for harbor:790
            //Average for gobarrals: 3000
            //Average for hauntedgraveyard: 6500
            //Average for town: 12000
            //Average for castle: 7000
            //Average for mountain: 8500
            //Average for innervolcano:20000
        }       
    }
}
