using GameMaker.Libraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMaker.Game.Mechanics
{
    public class Spell
    {
        public static Spell getRandomSpell()
        {
            Random rnd = new Random();
            int index = rnd.Next(1, Library.SpellList.masterSpellDic().Count);
            return Library.SpellList.masterSpellDic()[index];
        }
        public string name { get; set; }
        public int power { get; set; }        
        public bool selfTarget { get; set; }
        public bool canBurn { get; set; }
        public bool canConfuse { get; set; }
        public bool canParalyze { get; set; }
        public bool canSleep { get; set; }
        /// <summary>
        /// Gets a print friendly version of this spell
        /// </summary>
        /// <returns>A string containing the print friendly version of the spell</returns>
        public string description()
        {
            string returnValue = "";
            if (this.name.Equals("heal"))
            {
                returnValue = "This spell heals you for 5% of your health";
            }
            else
            {
                returnValue = "" + this.name + " has a power of " + this.power + " and " + statusDescription();
            }
            return returnValue;
        }
        public string statusDescription()
        {
            if (canBurn)
            {
                return "can burn.";
            }
            else if (canConfuse)
            {
                return "can coonfuse.";
            }
            else if (canParalyze)
            {
                return "can paralyze.";
            }
            else if (canSleep)
            {
                return "can cause sleep.";
            }
            else
            {
                return " does not cause status effects.";
            }
        }
        
    }
    public class SpellAssist
    {
        public static Spell spellMaker(string name, int power)
        {
            Spell createSpell = new Spell();
            createSpell.name = name;
            createSpell.power = power;
            return createSpell;
        }
        public static Spell spellMaker(string name, int power, bool selfTarget)
        {
            Spell createSpell = new Spell();
            createSpell.name = name;
            createSpell.power = power;
            createSpell.selfTarget = selfTarget;
            return createSpell;
        }
        public static Spell spellMaker(string name, int power, bool selfTarget, bool burn)
        {
            Spell createSpell = new Spell();
            createSpell.name = name;
            createSpell.power = power;
            createSpell.selfTarget = selfTarget;
            createSpell.canBurn = burn;
            return createSpell;
        }
        public static Spell spellMaker(string name, int power, bool selfTarget, bool burn, bool confuse)
        {
            Spell createSpell = new Spell();
            createSpell.name = name;
            createSpell.power = power;
            createSpell.selfTarget = selfTarget;
            createSpell.canBurn = burn;
            createSpell.canConfuse = confuse;
            return createSpell;
        }
        public static Spell spellMaker(string name, int power,bool selfTarget, bool burn, bool confuse, bool paralyze)
        {
            Spell createSpell = new Spell();
            createSpell.name = name;
            createSpell.power = power;
            createSpell.canBurn = burn;
            createSpell.selfTarget = selfTarget;
            createSpell.canConfuse = confuse;
            createSpell.canParalyze = paralyze;
            
            return createSpell;
        }
        public static Spell spellMaker(string name, int power, bool selfTarget, bool burn, bool confuse, bool paralyze, bool sleep)
        {
            Spell createSpell = new Spell();
            createSpell.name = name;
            createSpell.power = power;
            createSpell.selfTarget = selfTarget;
            createSpell.canBurn = burn;
            createSpell.canConfuse = confuse;
            createSpell.canParalyze = paralyze;
            createSpell.canSleep = sleep;
            return createSpell;
        }
    }
    
   }
