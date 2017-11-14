using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameMaker.Game.Characters;
using GameMaker.Libraries;

namespace GameMaker.Game.Calculations
{
    public class Area
    {
        //use modifier readonly since this should never change
        public readonly static Area FOREST = new Area("Forest", 10);
        public readonly static Area CAVE = new Area("Cave", 20);
        public readonly static Area DESERT = new Area("Desert", 30);
        //public readonly static Area VILLAGE = new Area("Village", 30);
        public readonly static Area HARBOR = new Area("Harbor", 40);
        public readonly static Area GOBARRALS = new Area("Gobarrals", 50);
        public readonly static Area HAUNTEDGRAVEYARD = new Area("Haunted Graveyard", 60);
        public readonly static Area TOWN = new Area("Town", 70);
        public readonly static Area CASTLE = new Area("Castle", 80);
        public readonly static Area MOUNTAIN = new Area("Mountain", 90);
        public readonly static Area INNERVOLCANO = new Area("Inner Volcano", 100);

        /// <summary>
        /// Creates a new area
        /// </summary>
        /// <param name="areaName"> The name of the area</param>
        /// <param name="areaMaxLevel"> The maximum level of enemies that belong in this area</param>
        public Area(String areaName,int areaMaxLevel)
        {
            this.areaName = areaName;
            this.areaMaxLevel = areaMaxLevel;
            
        }
        
        public string areaName { get; set; }
        public int areaMaxLevel { get; set; }
        /// <summary>
        /// Makes and returns a list of every single area
        /// </summary>
        /// <returns>A list of all areaas in the game</returns>
        public static List<Area> allAreaList()
        {
            List<Area> areaList = new List<Area>();

            areaList.Add(FOREST);
            areaList.Add(CAVE);
            areaList.Add(DESERT);
            
            areaList.Add(HARBOR);
            areaList.Add(GOBARRALS);
            areaList.Add(HAUNTEDGRAVEYARD);
            areaList.Add(TOWN);
            areaList.Add(CASTLE);
            areaList.Add(MOUNTAIN);
            areaList.Add(INNERVOLCANO);
            return areaList;
        }
        
    } 
    
    
}
