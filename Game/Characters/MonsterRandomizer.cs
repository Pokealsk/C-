using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameMaker.Game.Calculations;
using GameMaker.Libraries;
namespace GameMaker.Game.Characters
{
   
    public class Monster
    {
       /// <summary>
       /// Given an area makes a random monster, relies on the MonsterRandomizer class for the actual stat population
       /// </summary>
       /// <param name="area">The area the monster belongs to</param>
        public Monster(Area area)
        {
            stats = new Stats();
            peripherals = new Peripheral();
            modifiers = new Modifiers();
            MonsterRandomizer.createMonster(area,this);
        }
        public string name { get; set; }
        public string type { get; set; }
        public string prefix { get; set; }
        public Stats stats { get; set; }
        public Peripheral peripherals { get; set; }
        public Modifiers modifiers { get; set; }
        /// <summary>
        /// Handles experience through adding every stat up and then dividing by 2
        /// </summary>
        /// <returns>The monsters experience value</returns>
        public int getMonsterExp()
        {
            int exp = getSumOfStats() / 2;
            return exp;

        }
        /// <summary>
        /// Adds up all the stats of the current monster
        /// </summary>
        /// <returns>The sum of every single stat in the monster</returns>
        public int getSumOfStats()
        {
            int sum = 0;
            sum += this.stats.attack;
            sum += this.stats.critMod;
            sum += this.stats.defense;
            sum += this.stats.maxHealth;
            sum += this.stats.luck;
            sum += this.stats.magicAttack;
            sum += this.stats.magicDefense;
            sum += this.stats.speed;
            return sum;
        }
       
    }
    public class MonsterRandomizer
    {
        /// <summary>
        /// Goes through the randomization process based on an area and a monster
        /// </summary>
        /// <param name="area">The area the monster belongs in</param>
        /// <param name="enemy">An empty monster object to be filled</param>
        /// <returns>A fully created monster</returns>
        public static Monster createMonster(Area area, Monster enemy)
        {
            
            Random rnd = new Random();

            enemy.name = getRandomMonster(area);
            enemy.stats.level = area.areaMaxLevel - rnd.Next(10);
            getMonsterBaseStats(enemy);
            enemy.type = getMonsterType();
            getMonsterStatsFromType(enemy);
            enemy.prefix = getMonsterPrefix();
            getNewStatsFromPrefix(enemy);
            enemy.name = enemy.prefix + " " + enemy.type + " " + enemy.name;
            
            return enemy;
        }
        /// <summary>
        /// Gets a random type, such as Lucky or Magic for the monster
        /// </summary>
        /// <returns>Returns a string representing the type the monster is</returns>
        public static string getMonsterType()
        {
            Random rnd = new Random();
            
            string[] types = new[] { "Lucky", "Magic", "Strong", "Speedy", "Gluttonous" };
           return types[rnd.Next(types.Length)];
            

            
        }        
        /// <summary>
        /// Adjusts monsters stats based on the enemies type, such as magic, melee, etc
        /// </summary>
        /// <param name="enemy"></param>
        /// <returns></returns>
        public static void getMonsterStatsFromType(Monster enemy)
        {
            switch (enemy.type.ToLower())
            {
                case "magic":
                    enemy.stats.attack /= 2;
                    enemy.stats.defense = (int)Math.Ceiling((double)enemy.stats.defense / 2);
                    enemy.stats.magicAttack = (enemy.stats.magicAttack * 3) / 2;
                    enemy.stats.magicDefense = (enemy.stats.magicDefense * 3) / 2;
                    break;
                case "melee":
                    enemy.stats.attack = (int)Math.Ceiling((double)enemy.stats.attack * 3 / 2);
                    enemy.stats.defense = (int)Math.Ceiling((double)enemy.stats.defense * 3 / 2);
                    enemy.stats.magicAttack /= 2;
                    enemy.stats.magicDefense /= 2;
                    break;
                case "gluttonous":
                    enemy.stats.maxHealth = enemy.stats.currentHealth *= 2;
                    enemy.stats.defense = (int)Math.Ceiling((double)enemy.stats.defense /2);
                    enemy.stats.magicDefense = (int)Math.Ceiling((double)enemy.stats.magicDefense / 2);
                    break;
                case "speedy":
                    enemy.stats.speed *= 2;
                    break;
                case "lucky":
                    enemy.stats.luck *= 2;
                    break;

                    
                    default:
                    break;

            }
            
        }
        /// <summary>
        /// Gets a random prefix based on weights, weak is 10%, 
        /// </summary>
        /// <returns></returns>
        public static string getMonsterPrefix()
        {
            Random rnd = new Random();
            string[] prefixes = { "Weak", "Standard", "Elite", "Villainous", "EPIC" };
            int randomNumber = rnd.Next(101);
            int prefixNumber = 1;
            if (randomNumber <= 10)
            {
                prefixNumber = 0;
            }
            else if (randomNumber <= 80)
            {
                prefixNumber = 1;
            }
            else if (randomNumber <= 90)
            {
                prefixNumber = 2;
            }
            else if (randomNumber <= 99)
            {
                prefixNumber = 3;
            }
            else if (randomNumber == 100)
            {
                prefixNumber = 4;
            }
            string prefix = prefixes[prefixNumber];
            
            
            return prefix;
        }
        /// <summary>
        /// Gets a random monster name based on the area that you want
        /// </summary>
        /// <param name="area">The current area we want to generate a monster for</param>
        /// <returns>A string representing that monsters name</returns>
        public static string getRandomMonster(Area area)
        {
            Random randomID = new Random();
            Dictionary<int, string> monsterIDDic = new Dictionary<int, string>();
            int monsterID = 0;
            string monsterName = "Bad Eggs";

            area.areaName.ToLower();
            switch (area.areaName.ToLower())
            {
                case "forest":
                    monsterIDDic = Library.MonsterList.monsterListForest();
                    break;
                case "cave":
                    monsterIDDic = Library.MonsterList.monsterListCave();
                    break;
                case "desert":
                    monsterIDDic = Library.MonsterList.monsterListDesert();
                    break;
                case "village":
                    monsterIDDic = Library.MonsterList.monsterListVillage();
                    break;
                case "harbor":
                    monsterIDDic = Library.MonsterList.monsterListHarbor();
                    break;
                case "gobarrals":
                    monsterIDDic = Library.MonsterList.monsterListGobarrals();
                    break;
                case "haunted graveyard":
                    monsterIDDic = Library.MonsterList.monsterListHauntedGraveyard();
                    break;
                case "town":
                    monsterIDDic = Library.MonsterList.monsterListTown();
                    break;
                case "castle":
                    monsterIDDic = Library.MonsterList.monsterListCastle();
                    break;
                case "mountain":
                    monsterIDDic = Library.MonsterList.monsterListMountain();
                    break;
                case "inner volcano":
                    monsterIDDic = Library.MonsterList.monsterListInnerVolcano();
                    break;
                default:
                    monsterIDDic = Library.MonsterList.monsterMasterList();
                    break;


            }
            monsterID = randomID.Next(0, monsterIDDic.Count);
            foreach (var monster in monsterIDDic)
            {
                if (monsterID == monster.Key)
                {
                    monsterName = monster.Value;
                }
            }
            return monsterName;
        }
        public static void getNewStatsFromPrefix(Monster enemy)
        {
           
            switch (enemy.prefix.ToLower())
            {
                case "weak":
                    getSpecificModifiedStats(enemy,.7);
                    break;
                case "standard":
                    break;
                case "elite":
                    getSpecificModifiedStats(enemy, 1.1);
                    break;
                case "villainous":
                    getSpecificModifiedStats(enemy, 1.2);
                        break;
                case "epic":
                    getSpecificModifiedStats(enemy, 1.5);
                    break;
                        default:
                    break;

            }            

        }


        /// <summary>
        /// Modifies the stats based on a modifier passed in
        /// </summary>
        /// <param name="enemy"></param>
        /// <param name="modifier"></param>
        public static void getSpecificModifiedStats(Monster enemy, double modifier)
        {
            double modAttack = enemy.stats.attack * modifier;
            double modCritMod = enemy.stats.critMod * modifier;
            double modDefense = enemy.stats.defense * modifier;
            double modLuck = enemy.stats.luck * modifier;
            double modHealth = enemy.stats.maxHealth * modifier;
            double modMagicAttack = enemy.stats.magicAttack * modifier;
            double modMagicDefense = enemy.stats.magicDefense * modifier;
            double modSpeed = enemy.stats.speed * modifier;

            enemy.stats.attack = (int)modAttack;
            enemy.stats.critMod = (int)modCritMod;
            enemy.stats.defense = (int)modDefense;
            enemy.stats.luck = (int)modLuck;
            enemy.stats.maxHealth = enemy.stats.currentHealth = (int)modHealth;
            enemy.stats.magicAttack = (int)modMagicAttack;
            enemy.stats.magicDefense = (int)modMagicDefense;
            enemy.stats.speed = (int)(modSpeed);
        }
        /// <summary>
        /// Sets the monsters information based on what kind of monster it is
        /// </summary>
        /// <param name="enemy">The monster we want to get stats for</param>
        public static void getMonsterBaseStats(Monster enemy)
        {
            
            Random rnd = new Random();
            switch (enemy.name.ToLower())
            {
                case "bark beetle":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 1 * enemy.stats.level;
                    enemy.stats.attack = 1 * enemy.stats.level;
                    enemy.stats.defense = 1 * enemy.stats.level;
                    enemy.stats.magicAttack = 1 * enemy.stats.level;
                    enemy.stats.magicDefense = 1 * enemy.stats.level;
                    enemy.stats.luck = 1 * enemy.stats.level;
                    enemy.stats.speed = 1 * enemy.stats.level;
                    break;
                case "deer":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 4 * enemy.stats.level;
                    enemy.stats.attack = 2 * enemy.stats.level;
                    enemy.stats.defense = 2 * enemy.stats.level;
                    enemy.stats.magicAttack = 5 * enemy.stats.level;
                    enemy.stats.magicDefense = 3 * enemy.stats.level;
                    enemy.stats.luck = 1 * enemy.stats.level;
                    enemy.stats.speed = 4 * enemy.stats.level;
                    break;
                case "elk":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 5 * enemy.stats.level + 2;
                    enemy.stats.attack = 3 * enemy.stats.level;
                    enemy.stats.defense = 3 * enemy.stats.level;
                    enemy.stats.magicAttack = 0;
                    enemy.stats.magicDefense = 1 * enemy.stats.level;
                    enemy.stats.luck = 1 * enemy.stats.level;
                    enemy.stats.speed = 3 * enemy.stats.level;
                    break;
                case "wolf":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 4 * enemy.stats.level;
                    enemy.stats.attack = 4 * enemy.stats.level;
                    enemy.stats.defense = 1 * enemy.stats.level;
                    enemy.stats.magicAttack = 0;
                    enemy.stats.magicDefense = 2 * enemy.stats.level;
                    enemy.stats.luck = 1 * enemy.stats.level;
                    enemy.stats.speed = 4 * enemy.stats.level;
                    break;
                case "slime":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 6 * enemy.stats.level;
                    enemy.stats.attack = 2 * enemy.stats.level;
                    enemy.stats.defense = 1 * enemy.stats.level;
                    enemy.stats.magicAttack = 2 * enemy.stats.level;
                    enemy.stats.magicDefense = 5 * enemy.stats.level;
                    enemy.stats.luck = 1 * enemy.stats.level;
                    enemy.stats.speed = 2 * enemy.stats.level;
                    break;
                case "dank beetle":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 1 * enemy.stats.level;
                    enemy.stats.attack = 1 * enemy.stats.level;
                    enemy.stats.defense = 1 * enemy.stats.level;
                    enemy.stats.magicAttack = 1 * enemy.stats.level;
                    enemy.stats.magicDefense = 1 * enemy.stats.level;
                    enemy.stats.luck = 1 * enemy.stats.level;
                    enemy.stats.speed = 1 * enemy.stats.level;
                    break;
                case "bat":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 3 * enemy.stats.level;
                    enemy.stats.attack = 3 * enemy.stats.level;
                    enemy.stats.defense = 2 * enemy.stats.level;
                    enemy.stats.magicAttack = 0 * enemy.stats.level;
                    enemy.stats.magicDefense = 2 * enemy.stats.level;
                    enemy.stats.luck = 2 * enemy.stats.level;
                    enemy.stats.speed = 2 * enemy.stats.level;
                    break;
                case "rat":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 4 * enemy.stats.level;
                    enemy.stats.attack = 3 * enemy.stats.level;
                    enemy.stats.defense = 1 * enemy.stats.level;
                    enemy.stats.magicAttack = 2 * enemy.stats.level;
                    enemy.stats.magicDefense = 1 * enemy.stats.level;
                    enemy.stats.luck = 3 * enemy.stats.level;
                    enemy.stats.speed = 4 * enemy.stats.level;
                    break;
                case "spider":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 4 * enemy.stats.level;
                    enemy.stats.attack = 2 * enemy.stats.level;
                    enemy.stats.defense = 2 * enemy.stats.level;
                    enemy.stats.magicAttack = 1 * enemy.stats.level;
                    enemy.stats.magicDefense = 2 * enemy.stats.level;
                    enemy.stats.luck = 2 * enemy.stats.level;
                    enemy.stats.speed = 3 * enemy.stats.level;
                    break;
                case "caterpillar":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 3 * enemy.stats.level;
                    enemy.stats.attack = 1 * enemy.stats.level;
                    enemy.stats.defense = 1 * enemy.stats.level;
                    enemy.stats.magicAttack = 1 * enemy.stats.level;
                    enemy.stats.magicDefense = 4 * enemy.stats.level;
                    enemy.stats.luck = 1 * enemy.stats.level;
                    enemy.stats.speed = 2 * enemy.stats.level;
                    break;
                case "butterFly":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 5 * enemy.stats.level;
                    enemy.stats.attack = 3 * enemy.stats.level;
                    enemy.stats.defense = 2 * enemy.stats.level;
                    enemy.stats.magicAttack = 4 * enemy.stats.level;
                    enemy.stats.magicDefense = 5 * enemy.stats.level;
                    enemy.stats.luck = 3 * enemy.stats.level;
                    enemy.stats.speed = 3 * enemy.stats.level;
                    break;
                case "desert beetle":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 1 * enemy.stats.level;
                    enemy.stats.attack = 1 * enemy.stats.level;
                    enemy.stats.defense = 1 * enemy.stats.level;
                    enemy.stats.magicAttack = 1 * enemy.stats.level;
                    enemy.stats.magicDefense = 1 * enemy.stats.level;
                    enemy.stats.luck = 1 * enemy.stats.level;
                    enemy.stats.speed = 1 * enemy.stats.level;
                    break;

                case "pig":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 7 * enemy.stats.level;
                    enemy.stats.attack = 3 * enemy.stats.level;
                    enemy.stats.defense = 3 * enemy.stats.level;
                    enemy.stats.magicAttack = 3 * enemy.stats.level;
                    enemy.stats.magicDefense = 2 * enemy.stats.level;
                    enemy.stats.luck = 1 * enemy.stats.level;
                    enemy.stats.speed = 1 * enemy.stats.level;
                    break;

                case "boar":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 7 * enemy.stats.level;
                    enemy.stats.attack = 4 * enemy.stats.level;
                    enemy.stats.defense = 4 * enemy.stats.level;
                    enemy.stats.magicAttack = 0;
                    enemy.stats.magicDefense = 3 * enemy.stats.level;
                    enemy.stats.luck = 1 * enemy.stats.level;
                    enemy.stats.speed = 3 * enemy.stats.level;
                    break;

                case "scorpion":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 4 * enemy.stats.level;
                    enemy.stats.attack = 3 * enemy.stats.level;
                    enemy.stats.defense = 2 * enemy.stats.level;
                    enemy.stats.magicAttack = 0;
                    enemy.stats.magicDefense = 5 * enemy.stats.level;
                    enemy.stats.luck = 2 * enemy.stats.level;
                    enemy.stats.speed = 2 * enemy.stats.level;
                    break;

                case "cobra":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 4 * enemy.stats.level;
                    enemy.stats.attack = 3 * enemy.stats.level;
                    enemy.stats.defense = 4 * enemy.stats.level;
                    enemy.stats.magicAttack = 3 * enemy.stats.level;
                    enemy.stats.magicDefense = 4 * enemy.stats.level;
                    enemy.stats.luck = 3 * enemy.stats.level;
                    enemy.stats.speed = 3 * enemy.stats.level;
                    break;

                case "scavenger beetle":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 1 * enemy.stats.level;
                    enemy.stats.attack = 1 * enemy.stats.level;
                    enemy.stats.defense = 1 * enemy.stats.level;
                    enemy.stats.magicAttack = 1 * enemy.stats.level;
                    enemy.stats.magicDefense = 1 * enemy.stats.level;
                    enemy.stats.luck = 1 * enemy.stats.level;
                    enemy.stats.speed = 1 * enemy.stats.level;
                    break;

                case "farmer":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 5 * enemy.stats.level;
                    enemy.stats.attack = 3 * enemy.stats.level;
                    enemy.stats.defense = 3 * enemy.stats.level;
                    enemy.stats.magicAttack = 3 * enemy.stats.level;
                    enemy.stats.magicDefense = 3 * enemy.stats.level;
                    enemy.stats.luck = 2 * enemy.stats.level;
                    enemy.stats.speed = 2 * enemy.stats.level;
                    break;

                case "elder":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 4 * enemy.stats.level;
                    enemy.stats.attack = 2 * enemy.stats.level;
                    enemy.stats.defense = 2 * enemy.stats.level;
                    enemy.stats.magicAttack = 6 * enemy.stats.level;
                    enemy.stats.magicDefense = 6 * enemy.stats.level;
                    enemy.stats.luck = 1 * enemy.stats.level;
                    enemy.stats.speed = 1 * enemy.stats.level;
                    break;

                case "wet beetle":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 1 * enemy.stats.level;
                    enemy.stats.attack = 1 * enemy.stats.level;
                    enemy.stats.defense = 1 * enemy.stats.level;
                    enemy.stats.magicAttack = 1 * enemy.stats.level;
                    enemy.stats.magicDefense = 1 * enemy.stats.level;
                    enemy.stats.luck = 1 * enemy.stats.level;
                    enemy.stats.speed = 1 * enemy.stats.level;
                    break;

                case "carp":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 10 * enemy.stats.level;
                    enemy.stats.attack = 2 * enemy.stats.level;
                    enemy.stats.defense = 2 * enemy.stats.level;
                    enemy.stats.magicAttack = 2 * enemy.stats.level;
                    enemy.stats.magicDefense = 2 * enemy.stats.level;
                    enemy.stats.luck = 2 * enemy.stats.level;
                    enemy.stats.speed = 2 * enemy.stats.level;
                    break;

                case "crab":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 5 * enemy.stats.level;
                    enemy.stats.attack = 5 * enemy.stats.level;
                    enemy.stats.defense = 5 * enemy.stats.level;
                    enemy.stats.magicAttack = 3 * enemy.stats.level;
                    enemy.stats.magicDefense = 3 * enemy.stats.level;
                    enemy.stats.luck = 2 * enemy.stats.level;
                    enemy.stats.speed = 3 * enemy.stats.level;
                    break;

                case "lobster":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 6 * enemy.stats.level;
                    enemy.stats.attack = 4 * enemy.stats.level;
                    enemy.stats.defense = 4 * enemy.stats.level;
                    enemy.stats.magicAttack = 2 * enemy.stats.level;
                    enemy.stats.magicDefense = 5 * enemy.stats.level;
                    enemy.stats.luck = 2 * enemy.stats.level;
                    enemy.stats.speed = 4 * enemy.stats.level;
                    break;

                case "spoopy beetle":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 1 * enemy.stats.level;
                    enemy.stats.attack = 1 * enemy.stats.level;
                    enemy.stats.defense = 1 * enemy.stats.level;
                    enemy.stats.magicAttack = 1 * enemy.stats.level;
                    enemy.stats.magicDefense = 1 * enemy.stats.level;
                    enemy.stats.luck = 1 * enemy.stats.level;
                    enemy.stats.speed = 1 * enemy.stats.level;
                    break;

                case "spider swarm":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 7 * enemy.stats.level;
                    enemy.stats.attack = 4 * enemy.stats.level;
                    enemy.stats.defense = 2 * enemy.stats.level;
                    enemy.stats.magicAttack = 3 * enemy.stats.level;
                    enemy.stats.magicDefense = 5 * enemy.stats.level;
                    enemy.stats.luck = 3 * enemy.stats.level;
                    enemy.stats.speed = 4 * enemy.stats.level;
                    break;

                
                case "daddy longlegs":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 8 * enemy.stats.level;
                    enemy.stats.attack = 5 * enemy.stats.level;
                    enemy.stats.defense = 5 * enemy.stats.level;
                    enemy.stats.magicAttack = 4 * enemy.stats.level;
                    enemy.stats.magicDefense = 4 * enemy.stats.level;
                    enemy.stats.luck = 1 * enemy.stats.level;
                    enemy.stats.speed = 3 * enemy.stats.level;
                    break;

                case "ghost beetle":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 1 * enemy.stats.level;
                    enemy.stats.attack = 1 * enemy.stats.level;
                    enemy.stats.defense = 1 * enemy.stats.level;
                    enemy.stats.magicAttack = 1 * enemy.stats.level;
                    enemy.stats.magicDefense = 1 * enemy.stats.level;
                    enemy.stats.luck = 1 * enemy.stats.level;
                    enemy.stats.speed = 1 * enemy.stats.level;
                    break;

                case "zombie":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 5 * enemy.stats.level;
                    enemy.stats.attack = 6 * enemy.stats.level;
                    enemy.stats.defense = 3 * enemy.stats.level;
                    enemy.stats.magicAttack = 2 * enemy.stats.level;
                    enemy.stats.magicDefense = 4 * enemy.stats.level;
                    enemy.stats.luck = 2 * enemy.stats.level;
                    enemy.stats.speed = 2 * enemy.stats.level;
                    break;

                case "skeleton":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 6 * enemy.stats.level;
                    enemy.stats.attack = 4 * enemy.stats.level;
                    enemy.stats.defense = 8 * enemy.stats.level;
                    enemy.stats.magicAttack = 4 * enemy.stats.level;
                    enemy.stats.magicDefense = 1 * enemy.stats.level;
                    enemy.stats.luck = 1 * enemy.stats.level;
                    enemy.stats.speed = 2 * enemy.stats.level;
                    break;

                case "ghost":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 3 * enemy.stats.level;
                    enemy.stats.attack = 2 * enemy.stats.level;
                    enemy.stats.defense = 5 * enemy.stats.level;
                    enemy.stats.magicAttack = 6 * enemy.stats.level;
                    enemy.stats.magicDefense = 1 * enemy.stats.level;
                    enemy.stats.luck = 2 * enemy.stats.level;
                    enemy.stats.speed = 2 * enemy.stats.level;
                    break;

                case "necromancer":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 5 * enemy.stats.level;
                    enemy.stats.attack = 1 * enemy.stats.level;
                    enemy.stats.defense = 2 * enemy.stats.level;
                    enemy.stats.magicAttack = 6 * enemy.stats.level;
                    enemy.stats.magicDefense = 9 * enemy.stats.level;
                    enemy.stats.luck = 2 * enemy.stats.level;
                    enemy.stats.speed = 2 * enemy.stats.level;
                    break;

                case "domestic beetle":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 1 * enemy.stats.level;
                    enemy.stats.attack = 1 * enemy.stats.level;
                    enemy.stats.defense = 1 * enemy.stats.level;
                    enemy.stats.magicAttack = 1 * enemy.stats.level;
                    enemy.stats.magicDefense = 1 * enemy.stats.level;
                    enemy.stats.luck = 1 * enemy.stats.level;
                    enemy.stats.speed = 1 * enemy.stats.level;
                    break;

                case "mimic":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 4 * enemy.stats.level;
                    enemy.stats.attack = 4 * enemy.stats.level;
                    enemy.stats.defense = 4 * enemy.stats.level;
                    enemy.stats.magicAttack = 2 * enemy.stats.level;
                    enemy.stats.magicDefense = 2 * enemy.stats.level;
                    enemy.stats.luck = 2 * enemy.stats.level;
                    enemy.stats.speed = 2 * enemy.stats.level;
                    break;

                case "sparring partner":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 6 * enemy.stats.level;
                    enemy.stats.attack = 5 * enemy.stats.level;
                    enemy.stats.defense = 5 * enemy.stats.level;
                    enemy.stats.magicAttack = 0;
                    enemy.stats.magicDefense = 5 * enemy.stats.level;
                    enemy.stats.luck = 3 * enemy.stats.level;
                    enemy.stats.speed = 3 * enemy.stats.level;
                    break;

                case "sparring master":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 10 * enemy.stats.level;
                    enemy.stats.attack = 7 * enemy.stats.level;
                    enemy.stats.defense = 7 * enemy.stats.level;
                    enemy.stats.magicAttack = 0;
                    enemy.stats.magicDefense = 7 * enemy.stats.level;
                    enemy.stats.luck = 4 * enemy.stats.level;
                    enemy.stats.speed = 4 * enemy.stats.level;
                    break;

                case "wizard's apprentice":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 6 * enemy.stats.level;
                    enemy.stats.attack = 3 * enemy.stats.level;
                    enemy.stats.defense = 3 * enemy.stats.level;
                    enemy.stats.magicAttack = 5 * enemy.stats.level;
                    enemy.stats.magicDefense = 7 * enemy.stats.level;
                    enemy.stats.luck = 3 * enemy.stats.level;
                    enemy.stats.speed = 2 * enemy.stats.level;
                    break;

                case "sage":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 7 * enemy.stats.level;
                    enemy.stats.attack = 4 * enemy.stats.level;
                    enemy.stats.defense = 3 * enemy.stats.level;
                    enemy.stats.magicAttack = 7 * enemy.stats.level;
                    enemy.stats.magicDefense = 8 * enemy.stats.level;
                    enemy.stats.luck = 2 * enemy.stats.level;
                    enemy.stats.speed = 2 * enemy.stats.level;
                    break;

                case "town drunk":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 5 * enemy.stats.level;
                    enemy.stats.attack = 3 * enemy.stats.level;
                    enemy.stats.defense = 4 * enemy.stats.level;
                    enemy.stats.magicAttack = 3 * enemy.stats.level;
                    enemy.stats.magicDefense = 1 * enemy.stats.level;
                    enemy.stats.luck = 1 * enemy.stats.level;
                    enemy.stats.speed = 2 * enemy.stats.level;
                    break;

                case "clown":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 6 * enemy.stats.level;
                    enemy.stats.attack = 5 * enemy.stats.level;
                    enemy.stats.defense = 4 * enemy.stats.level;
                    enemy.stats.magicAttack = 3 * enemy.stats.level;
                    enemy.stats.magicDefense = 3 * enemy.stats.level;
                    enemy.stats.luck = 4 * enemy.stats.level;
                    enemy.stats.speed = 4 * enemy.stats.level;
                    break;

                case "royal beetle":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 1 * enemy.stats.level;
                    enemy.stats.attack = 1 * enemy.stats.level;
                    enemy.stats.defense = 1 * enemy.stats.level;
                    enemy.stats.magicAttack = 1 * enemy.stats.level;
                    enemy.stats.magicDefense = 1 * enemy.stats.level;
                    enemy.stats.luck = 1 * enemy.stats.level;
                    enemy.stats.speed = 1 * enemy.stats.level;
                    break;

                case "warrior":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 12 * enemy.stats.level;
                    enemy.stats.attack = 6 * enemy.stats.level;
                    enemy.stats.defense = 3 * enemy.stats.level;
                    enemy.stats.magicAttack = 0;
                    enemy.stats.magicDefense = 2 * enemy.stats.level;
                    enemy.stats.luck = 2 * enemy.stats.level;
                    enemy.stats.speed = 2 * enemy.stats.level;
                    break;

                case "knight":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 10 * enemy.stats.level;
                    enemy.stats.attack = 6 * enemy.stats.level;
                    enemy.stats.defense = 4 * enemy.stats.level;
                    enemy.stats.magicAttack = 0;
                    enemy.stats.magicDefense = 3 * enemy.stats.level;
                    enemy.stats.luck = 2 * enemy.stats.level;
                    enemy.stats.speed = 2 * enemy.stats.level;
                    break;

                case "thief":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 7 * enemy.stats.level;
                    enemy.stats.attack = 8 * enemy.stats.level;
                    enemy.stats.defense = 2 * enemy.stats.level;
                    enemy.stats.magicAttack = 1 * enemy.stats.level;
                    enemy.stats.magicDefense = 1 * enemy.stats.level;
                    enemy.stats.luck = 4 * enemy.stats.level;
                    enemy.stats.speed = 6 * enemy.stats.level;
                    break;

                case "mage":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 8 * enemy.stats.level;
                    enemy.stats.attack = 3 * enemy.stats.level;
                    enemy.stats.defense = 3 * enemy.stats.level;
                    enemy.stats.magicAttack = 5 * enemy.stats.level;
                    enemy.stats.magicDefense = 7 * enemy.stats.level;
                    enemy.stats.luck = 3 * enemy.stats.level;
                    enemy.stats.speed = 2 * enemy.stats.level;
                    break;

                case "cleric":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 11 * enemy.stats.level;
                    enemy.stats.attack = 4 * enemy.stats.level;
                    enemy.stats.defense = 2 * enemy.stats.level;
                    enemy.stats.magicAttack = 7 * enemy.stats.level;
                    enemy.stats.magicDefense = 5 * enemy.stats.level;
                    enemy.stats.luck = 2 * enemy.stats.level;
                    enemy.stats.speed = 1 * enemy.stats.level;
                    break;

                case "jester":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 7 * enemy.stats.level;
                    enemy.stats.attack = 4 * enemy.stats.level;
                    enemy.stats.defense = 2 * enemy.stats.level;
                    enemy.stats.magicAttack = 3 * enemy.stats.level;
                    enemy.stats.magicDefense = 3 * enemy.stats.level;
                    enemy.stats.luck = 4 * enemy.stats.level;
                    enemy.stats.speed = 5 * enemy.stats.level;
                    break;

                case "funeral director":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = rnd.Next(1, 15) * enemy.stats.level;
                    enemy.stats.attack = rnd.Next(1, 15) * enemy.stats.level;
                    enemy.stats.defense = rnd.Next(1, 15) * enemy.stats.level;
                    enemy.stats.magicAttack = rnd.Next(1, 15) * enemy.stats.level;
                    enemy.stats.magicDefense = rnd.Next(1, 15) * enemy.stats.level;
                    enemy.stats.luck = rnd.Next(1, 10) * enemy.stats.level;
                    enemy.stats.speed = rnd.Next(1, 10) * enemy.stats.level;
                    break;

                case "big beetle":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 1 * enemy.stats.level;
                    enemy.stats.attack = 1 * enemy.stats.level;
                    enemy.stats.defense = 1 * enemy.stats.level;
                    enemy.stats.magicAttack = 1 * enemy.stats.level;
                    enemy.stats.magicDefense = 1 * enemy.stats.level;
                    enemy.stats.luck = 1 * enemy.stats.level;
                    enemy.stats.speed = 1 * enemy.stats.level;
                    break;

                case "flame salamander":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 8 * enemy.stats.level;
                    enemy.stats.attack = 3 * enemy.stats.level;
                    enemy.stats.defense = 6 * enemy.stats.level;
                    enemy.stats.magicAttack = 5 * enemy.stats.level;
                    enemy.stats.magicDefense = 3 * enemy.stats.level;
                    enemy.stats.luck = 2 * enemy.stats.level;
                    enemy.stats.speed = 3 * enemy.stats.level;
                    break;

                case "cephalopod":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 10 * enemy.stats.level;
                    enemy.stats.attack = 5 * enemy.stats.level;
                    enemy.stats.defense = 4 * enemy.stats.level;
                    enemy.stats.magicAttack = 3 * enemy.stats.level;
                    enemy.stats.magicDefense = 5 * enemy.stats.level;
                    enemy.stats.luck = 2 * enemy.stats.level;
                    enemy.stats.speed = 2 * enemy.stats.level;
                    break;

                case "direwolf":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 8 * enemy.stats.level;
                    enemy.stats.attack = 6 * enemy.stats.level;
                    enemy.stats.defense = 3 * enemy.stats.level;
                    enemy.stats.magicAttack = 0;
                    enemy.stats.magicDefense = 3 * enemy.stats.level;
                    enemy.stats.luck = 4 * enemy.stats.level;
                    enemy.stats.speed = 4 * enemy.stats.level;
                    break;

                case "billy goat":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 7 * enemy.stats.level;
                    enemy.stats.attack = 7 * enemy.stats.level;
                    enemy.stats.defense = 6 * enemy.stats.level;
                    enemy.stats.magicAttack = 0;
                    enemy.stats.magicDefense = 1 * enemy.stats.level;
                    enemy.stats.luck = 3 * enemy.stats.level;
                    enemy.stats.speed = 5 * enemy.stats.level;
                    break;

                case "flame beetle":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 1 * enemy.stats.level;
                    enemy.stats.attack = 1 * enemy.stats.level;
                    enemy.stats.defense = 1 * enemy.stats.level;
                    enemy.stats.magicAttack = 1 * enemy.stats.level;
                    enemy.stats.magicDefense = 1 * enemy.stats.level;
                    enemy.stats.luck = 1 * enemy.stats.level;
                    enemy.stats.speed = 1 * enemy.stats.level;
                    break;

                case "basilisk":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 9 * enemy.stats.level;
                    enemy.stats.attack = 4 * enemy.stats.level;
                    enemy.stats.defense = 7 * enemy.stats.level;
                    enemy.stats.magicAttack = 4 * enemy.stats.level;
                    enemy.stats.magicDefense = 7 * enemy.stats.level;
                    enemy.stats.luck = 2 * enemy.stats.level;
                    enemy.stats.speed = 2 * enemy.stats.level;
                    break;

                case "wyvern":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 10 * enemy.stats.level;
                    enemy.stats.attack = 6 * enemy.stats.level;
                    enemy.stats.defense = 4 * enemy.stats.level;
                    enemy.stats.magicAttack = 8 * enemy.stats.level;
                    enemy.stats.magicDefense = 7 * enemy.stats.level;
                    enemy.stats.luck = 2 * enemy.stats.level;
                    enemy.stats.speed = 3 * enemy.stats.level;
                    break;

                case "drake":
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 11 * enemy.stats.level;
                    enemy.stats.attack = 9 * enemy.stats.level;
                    enemy.stats.defense = 8 * enemy.stats.level;
                    enemy.stats.magicAttack = 4 * enemy.stats.level;
                    enemy.stats.magicDefense = 4 * enemy.stats.level;
                    enemy.stats.luck = 3 * enemy.stats.level;
                    enemy.stats.speed = 3 * enemy.stats.level;
                    break;
                default:
                    enemy.stats.maxHealth = enemy.stats.currentHealth = 1;
                    enemy.stats.attack = 1;
                    enemy.stats.defense = 1;
                    enemy.stats.magicAttack = 1;
                    enemy.stats.magicDefense = 1;
                    enemy.stats.luck = 1;
                    enemy.stats.speed = 1;
                    break;

    

            }            
        }

        


    }

}

