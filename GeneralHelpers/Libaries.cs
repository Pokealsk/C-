using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameMaker.Game.Characters;
using GameMaker.Game.Calculations;
using GameMaker.GeneralHelpers;
using GameMaker.Game.Mechanics;

namespace GameMaker.Libraries
{
    public class Library
    {
        /// <summary>
        ///Gets a list of all areas in the game (Each area has a name and maxlevel)
        /// </summary>
        /// <returns>A list of all areas in the game</returns>
        public static List<Area> areaList()
        {
            return Area.allAreaList();
        }
        /// <summary>
        /// Returns a list of all roles the user can choose
        /// </summary>
        /// <returns>A list containing all possible roles</returns>
        public static List<string> roleList()
        {
            List<string> roleList = new List<string>();
            roleList.Add("knight");
            roleList.Add("warrior");
            roleList.Add("thief");
            roleList.Add("mage");
            roleList.Add("cleric");
            roleList.Add("jester");
            roleList.Add("funeral director");


            return roleList;
        }
        /// <summary>
        /// Returns a print friendly version of the user's stats
        /// </summary>
        /// <param name="character"> The stats that you want to print out</param>
        /// <returns>A list of strings with each line being a different item of interest</returns>
        public static List<string> statList(Character character)
        {
            List<string> statList = new List<string>();

            statList.Add("Class: " + character.stats.roleName + "\n");
            statList.Add("Name: " + character.stats.playerName + "\n");
            statList.Add("Level: " + character.stats.level + "\n");
            int expNeeded = character.stats.levelUpExp - character.stats.exp;
            statList.Add("Experience until next level: " + expNeeded + "\n");
            statList.Add("Max Health: " + character.getMaxHealth() + "\n");
            statList.Add("Current Health: " + character.stats.currentHealth + "\n");
            statList.Add("Attack: " + character.getAttack() + "\n");
            statList.Add("Defense: " + character.getDefense() + "\n");
            statList.Add("Magic Attack: " + character.getMagicAttack() + "\n");
            statList.Add("Magic Defense: " + character.getMagicDefense() + "\n");
            statList.Add("Luck: " + character.getLuck() + "\n");
            statList.Add("Speed: " + character.getSpeed() + "\n");
         

            return statList;
        }
        /// <summary>
        /// Returns user friendly strings of the info of the enemy
        /// </summary>
        /// <param name="enemy"></param>
        /// <returns></returns>
        public static List<string> monsterStatList(Monster enemy)
        {
            List<string> monsterStats = new List<string>();
            monsterStats.Add("Name: " + enemy.name);     
            monsterStats.Add("Level: " + enemy.stats.level);
            monsterStats.Add("Health: " + enemy.stats.maxHealth);
            monsterStats.Add("Current health: " + enemy.stats.currentHealth);
            monsterStats.Add("Attack: " + enemy.stats.attack);
            monsterStats.Add("Defense: " + enemy.stats.defense);
            monsterStats.Add("Magical Attack: " + enemy.stats.magicAttack);
            monsterStats.Add("Magical Defense: " + enemy.stats.magicDefense);
            monsterStats.Add("Luck: " + enemy.stats.luck);
            monsterStats.Add("Speed: " + enemy.stats.speed);
            monsterStats.Add("Experience Value: " + enemy.stats.exp);
            return monsterStats;
        }
        public class GameHelp
        {
           
            public static string suggester()
            {
                Random rnd = new Random();
                string[] nameOptions = new string[5];

                nameOptions[0] = "Saitama" + "\n";
                nameOptions[1] = "Armstrong" + "\n";
                nameOptions[2] = "Ravaging" + "\n";
                nameOptions[3] = "Illustrious" + "\n";
                nameOptions[4] = "Infamous" + "\n";


                return nameOptions[rnd.Next(nameOptions.Length)];

            }
          
        }
        public class ItemLists
        {
            /// <summary>
            /// Gets a list of every single item in a dictionary
            /// </summary>
            /// <returns>A dictionary that contains all items indexed</returns>
            public static Dictionary<int, Item> masterItemDic()
            {
                Dictionary<int, Item> masterDic = new Dictionary<int, Item>();
                List<Item> masterList = new List<Item>();
                
                masterList.AddRange(itemAttackList());
                masterList.AddRange(itemDefenseList());
                masterList.AddRange(itemExpList());
                masterList.AddRange(itemMagicAttackList());
                masterList.AddRange(itemMagicDefenseList());
                masterList.AddRange(itemLuckList());
                masterList.AddRange(itemSpeedList());
                masterList.AddRange(itemHealthList());
                masterList.AddRange(itemCritModList());                
                for (int i = 0; i < masterList.Count(); i++)
                {
                    masterDic.Add((i + 1), masterList[i]);
                }
                return masterDic;

            }

            private static List<Item> itemHealthList()
            {
                List<Item> itemHealthList = new List < Item > ();
                itemHealthList.Add(new HealthItem("Plump Roast", "You feel like you gained 5 pounds eating this. You did.", 30));

                return itemHealthList;
            }
            private static List<Item> itemAttackList()
            {
                List<Item> itemAttackList = new List<Item>();
                itemAttackList.Add(new AttackItem("Bow", "A lean intrument of death enemies would fear from many yards away", 5));
                itemAttackList.Add(new AttackItem("Brass Knuckles", "A blunt, short range weapon, brass fits around the knuckles, looks like knuckles, and gives people the chuckles", 4));
                itemAttackList.Add(new AttackItem("Broadsword", "A sharp sword which is broad", 6));
                itemAttackList.Add(new AttackItem("Dagger", "A short-ranged punishing blade, most dangerous in the hands of skilled thieves", 2));
                itemAttackList.Add(new AttackItem("Dull dagger", "I guess it's better than a spoon", 1));
                itemAttackList.Add(new AttackItem("Katana", "A long, sleek sword, used by ninjas worldwide(but mainly in asia)", 5));
                itemAttackList.Add(new AttackItem("Longsword", "A sharp sword which is long", 4));
                itemAttackList.Add(new AttackItem("Mace", "Lets your foes realize how crushingly short the future is", 4));
                itemAttackList.Add(new AttackItem("Shortsword", "A sharp sword which is short", 3));
                itemAttackList.Add(new AttackItem("Spear", "A Long-ranged staff with a sharp point on the end", 4));
                itemAttackList.Add(new AttackItem("Steroids", "A strength-enhancing drug", 3));
                itemAttackList.Add(new AttackItem("Trocar", "A hollow metal sword that succs", 5));
               




                return itemAttackList;
            }
            private static List<Item> itemDefenseList()
            {
                List<Item> itemDefenseList = new List<Item>();
                itemDefenseList.Add(new DefenseItem("Black Cloak", "Specifically used for a nice flourish", 2));
                itemDefenseList.Add(new DefenseItem("Bowler Hat", "A roundabout way to cover your head", 1));
                itemDefenseList.Add(new DefenseItem("Brass Chestplate", "A chestplate made of a nice brass alloy", 4));
                itemDefenseList.Add(new DefenseItem("Gloves", "A pair of cloth gloves", 1));
                itemDefenseList.Add(new DefenseItem("Greaves", "Ye olde shin guards", 2));
                itemDefenseList.Add(new DefenseItem("Helmet", "A cast of metal used to protect the head", 3));
                itemDefenseList.Add(new DefenseItem("Hood", "This \"helmet\" will help you remain unseen", 1));
                itemDefenseList.Add(new DefenseItem("Jester Cap", "A piece of the jester set with some nice bells hanging out", 2));
                itemDefenseList.Add(new DefenseItem("Jester Gloves", "A piece of the jester set that covers your dainty little fingers", 2));
                itemDefenseList.Add(new DefenseItem("Jester Outfit", "A piece of the jester set  that is incredibly flamboyant", 2));
                itemDefenseList.Add(new DefenseItem("Jester Shoes", "A piece of the jester set that is oversized", 2));
                itemDefenseList.Add(new DefenseItem("Knight's Armor", "A piece of the knight set offering a fair amount of chest protection", 3));
                itemDefenseList.Add(new DefenseItem("Knight's Gauntlets", "A piece of the knight set offering a fair amount of leg protection", 3));
                itemDefenseList.Add(new DefenseItem("Knight's Helmet", "A piece of the knight set offering a fair amount of facial protection", 3));
                itemDefenseList.Add(new DefenseItem("Knight's Leggings", "A piece of the knight set offering a fair amount of leg protection", 3));
                itemDefenseList.Add(new DefenseItem("Tattered Pants", "These pants have seen better days", 1));
                itemDefenseList.Add(new DefenseItem("Trench Coat", "Your style will really flash with this coat", 2));
                
                return itemDefenseList;
            }
            private static List<Item> itemMagicAttackList()
            {
                List<Item> itemMagicAttackList = new List<Item>();
                itemMagicAttackList.Add(new MagicAttackItem("Staff", "A magical weapon that helps a user channel magic", 2));
                itemMagicAttackList.Add(new MagicAttackItem("Talisman", "A magical talisman that assists with channeling magic", 1));

                return itemMagicAttackList;
            }
            private static List<Item> itemMagicDefenseList()
            {
                List<Item> itemMagicDefenseList = new List<Item>();

                itemMagicDefenseList.Add(new MagicDefenseItem("Long Robe", "Specifically used for a nice miraculous flourish", 5));
                itemMagicDefenseList.Add(new MagicDefenseItem("Mage Cloak", "Specifically used for a nice magical flourish", 5));
                return itemMagicDefenseList;
            }
            private static List<Item> itemLuckList()
            {
                List<Item> itemLuckList = new List<Item>();
                itemLuckList.Add(new LuckItem("Rabbits Paw", "Poor little guy, at least you know 3 other paws are probably lying around too", 2));
                return itemLuckList;
            }
            private static List<Item> itemSpeedList()
            {
                List<Item> itemSpeedList = new List<Item>();
                itemSpeedList.Add(new SpeedItem("Heelies", "The pinnacle of fashion", 3));

                return itemSpeedList;
            }
            private static List<Item> itemExpList()
            {

                List<Item> itemExpList = new List<Item>();
                itemExpList.Add(new ExpItem("Lucky egg", "Seems like it has a life  in it, allowing you to experience double", 2));
                return itemExpList;
            }
            private static List<Item> itemCritModList()
            {

                List<Item> itemCritModList = new List<Item>();
                itemCritModList.Add(new CritModItem("Curved Dagger", "You feel like this dagger will have your enemies back.", 2));
                return itemCritModList;
            }
        }
        public class SpellList
        {
            ///<summary>
            ///gives all spells an id of 0 up to spells.length 
            /// </summary>
            public static Dictionary<int, Spell> masterSpellDic()
            {
                Dictionary<int, Spell> masterSpellListAndID = new Dictionary<int, Spell>();
                int id = 1;
                foreach (Spell spell in getSimpleSpellList())
                {
                    masterSpellListAndID.Add(id, spell);
                    id++;
                }
                return masterSpellListAndID;
            }
            ///<summary>
            ///Creates all spells and adds them to the list
            /// </summary>
            public static List<Spell> getSimpleSpellList()
            {
                List<Spell> spellList = new List<Spell>();
                spellList.Add(SpellAssist.spellMaker("Fireball", 50, false, true));
                spellList.Add(SpellAssist.spellMaker("Lightning", 50, false, false, false, true));
                spellList.Add(SpellAssist.spellMaker("Magical Fist", 50, false, false, false, true));
                spellList.Add(SpellAssist.spellMaker("Heal", 30, true));
                spellList.Add(SpellAssist.spellMaker("Sword Rain", 100));
                spellList.Add(SpellAssist.spellMaker("Meteor Rain", 70, false, true, true));
                spellList.Add(SpellAssist.spellMaker("Flame Shower", 60, false, true));
                spellList.Add(SpellAssist.spellMaker("Tree Slam", 70));
                return spellList;
            }
            /// <summary>
            /// Given the name of a spell it will give you the proper spell object that the name represents
            /// </summary>
            /// <param name="spellName">The name of the spell you want to look up</param>
            /// <returns>A spell object if the current spell is found, null if the spell isn't found</returns>
            public static Spell getSpell(string spellName)
            {
                foreach (KeyValuePair<int, Spell> currentEntry in masterSpellDic())
                {
                    if (currentEntry.Value.name.ToLower().Equals(spellName.ToLower()))
                    {
                        return currentEntry.Value;
                    }
                }
                return null;
            }

        }
        public class MonsterList
        {
            /// <summary>
            /// Gets a dictionary containing all monsters with a name and an ID
            /// </summary>
            /// <returns>A dictionary containing an id followed by a name</returns>
            public static Dictionary<int, string> monsterMasterList()
            {
                Dictionary<int, string> masterDic = new Dictionary<int, string>();
                CodeAssist.combineDictionaryWithIds(monsterListForest(), monsterMasterList());
                CodeAssist.combineDictionaryWithIds(monsterListCave(), monsterMasterList());
                CodeAssist.combineDictionaryWithIds(monsterListDesert(), monsterMasterList());
                CodeAssist.combineDictionaryWithIds(monsterListVillage(), monsterMasterList());
                CodeAssist.combineDictionaryWithIds(monsterListHarbor(), monsterMasterList());
                CodeAssist.combineDictionaryWithIds(monsterListGobarrals(), monsterMasterList());
                CodeAssist.combineDictionaryWithIds(monsterListHauntedGraveyard(), monsterMasterList());
                CodeAssist.combineDictionaryWithIds(monsterListTown(), monsterMasterList());
                CodeAssist.combineDictionaryWithIds(monsterListCastle(), monsterMasterList());
                CodeAssist.combineDictionaryWithIds(monsterListMountain(), monsterMasterList());
                CodeAssist.combineDictionaryWithIds(monsterListInnerVolcano(), monsterMasterList());
                return masterDic;
            } 
            public static Dictionary<int, string> monsterListForest()
            {
                Dictionary<int, string> monsterDic = new Dictionary<int, string>();
                monsterDic.Add(0, "Bark Beetle");
                monsterDic.Add(1, "Deer");
                monsterDic.Add(2, "Elk");
                monsterDic.Add(3, "Wolf");
                monsterDic.Add(4, "Slime");

                return monsterDic;
            }
            public static Dictionary<int, string> monsterListCave()
            {
                Dictionary<int, string> monsterDic = new Dictionary<int, string>();
                monsterDic.Add(0, "Dank Beetle");
                monsterDic.Add(1, "Bat");
                monsterDic.Add(2, "Rat");
                monsterDic.Add(3, "Spider");
                monsterDic.Add(4, "Caterpillar");
                monsterDic.Add(5, "ButterFly");
                return monsterDic;
            }
            public static Dictionary<int, string> monsterListDesert()
            {
                Dictionary<int, string> monsterDic = new Dictionary<int, string>();
                monsterDic.Add(0, "Desert Beetle");
                monsterDic.Add(1, "Pig");
                monsterDic.Add(2, "Boar");
                monsterDic.Add(3, "Scorpion");
                monsterDic.Add(4, "Cobra");
                return monsterDic;
            }
            public static Dictionary<int, string> monsterListVillage()
            {
                Dictionary<int, string> monsterDic = new Dictionary<int, string>();
                monsterDic.Add(0, "Scavenger Beetle");
                monsterDic.Add(1, "Farmer");
                monsterDic.Add(2, "Elder");
                return monsterDic;
            }
            public static Dictionary<int, string> monsterListHarbor()
            {
                Dictionary<int, string> monsterDic = new Dictionary<int, string>();
                monsterDic.Add(0, "Wet Beetle");
                monsterDic.Add(1, "Carp");
                monsterDic.Add(2, "Crab");
                monsterDic.Add(3, "Lobster");
                return monsterDic;
            }
            public static Dictionary<int, string> monsterListGobarrals()
            {
                Dictionary<int, string> monsterDic = new Dictionary<int, string>();
                monsterDic.Add(0, "Spoopy Beetle");
                monsterDic.Add(1, "Spider Swarm");
                monsterDic.Add(2, "Spider");
                monsterDic.Add(3, "Daddy Longlegs");
                return monsterDic;
            }
            public static Dictionary<int, string> monsterListHauntedGraveyard()
            {
                Dictionary<int, string> monsterDic = new Dictionary<int, string>();
                monsterDic.Add(0, "Ghost Beetle");
                monsterDic.Add(1, "Zombie");
                monsterDic.Add(2, "Skeleton");
                monsterDic.Add(3, "Ghost");
                monsterDic.Add(4, "Necromancer");
                return monsterDic;
            }
            public static Dictionary<int, string> monsterListTown()
            {
                Dictionary<int, string> monsterDic = new Dictionary<int, string>();
                monsterDic.Add(0, "Domestic Beetle");
                monsterDic.Add(1, "Mimic");
                monsterDic.Add(2, "Sparring Partner");
                monsterDic.Add(3, "Sparring Master");
                monsterDic.Add(4, "Wizard's Apprentice");
                monsterDic.Add(5, "Sage");
                monsterDic.Add(6, "Town Drunk");
                monsterDic.Add(7, "Clown");
                return monsterDic;
            }
            public static Dictionary<int, string> monsterListCastle()
            {
                Dictionary<int, string> monsterDic = new Dictionary<int, string>();
                monsterDic.Add(0, "Royal Beetle");
                monsterDic.Add(1, "Warrior");
                monsterDic.Add(2, "Knight");
                monsterDic.Add(3, "Thief");
                monsterDic.Add(4, "Mage");
                monsterDic.Add(5, "Cleric");
                monsterDic.Add(6, "Jester");
                monsterDic.Add(7, "Funeral Director");

                return monsterDic;
            }
            public static Dictionary<int, string> monsterListMountain()
            {
                Dictionary<int, string> monsterDic = new Dictionary<int, string>();
                monsterDic.Add(0, "Big Beetle");
                monsterDic.Add(1, "Flame Salamander");
                monsterDic.Add(2, "Cephalopod");
                monsterDic.Add(3, "DireWolf");
                monsterDic.Add(4, "Billy Goat");
                return monsterDic;
            }
            public static Dictionary<int, string> monsterListInnerVolcano()
            {
                Dictionary<int, string> monsterDic = new Dictionary<int, string>();
                monsterDic.Add(0, "Flame Beetle");
                monsterDic.Add(1, "Basilisk");
                monsterDic.Add(2, "Wyvern");
                monsterDic.Add(3, "Drake");
                return monsterDic;
            }
        }
        
    }
}
