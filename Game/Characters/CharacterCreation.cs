using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameMaker.Game.Characters;
using GameMaker.Game.Mechanics;
using GameMaker.Game.Calculations;
using GameMaker.Libraries;

namespace GameMaker.Game.Characters 
{
    public class Character
    {
        
        public Area area { get; set; }
        public Stats stats { get; set; }
        public Modifiers modifiers { get; set; }
        public Peripheral peripherals { get; set; }
        private int areaCounter { get; set; }
        public int position { get; set; }
        //Allow for an empty character to be created to handle early phase
        public Character()
        {            
            this.peripherals = new Peripheral();
            this.modifiers = new Modifiers();
            this.stats = new Stats();
            this.area = Area.FOREST;
            this.areaCounter = 0;
            this.position = 1;
            this.stats.critMod = 1;
        }
        public Character(String className, String userName)
        {            
            this.peripherals = new Peripheral();
            this.modifiers = new Modifiers();
            this.stats = new Stats();
            this.area = Area.FOREST;
            this.areaCounter = 0;
            this.position = 0;

            StatAssignment statAssignment = new StatAssignment();
            statAssignment.setInitialStats(className,this);
            this.stats.playerName = userName;
        }
        /// <summary>
        /// advances to the next area
        /// </summary>

        public bool nextArea()
        {
            areaCounter++;
            if (areaCounter >= Area.allAreaList().Count)
            {
                return false;
            }
            else
            {
                this.area = Area.allAreaList()[areaCounter];
                this.position = 0;
                return true;
            }
            
             
        }
        /// <summary>
        /// Adds experience to the current object
        /// </summary>
        /// <param name="exp">However much exp you want to add</param>
        /// <returns>An int representing how many times the user leveled up</returns>
        public int addExp(int exp)
        {
            int itemExpMod = 1;
            foreach (Item userItem in this.peripherals.inventory)
            {
                if (userItem is ExpItem)
                {
                    itemExpMod += ((ExpItem)userItem).expMod;
                }
            }
            this.stats.exp += exp * itemExpMod;
            int counter = 0;
            while (this.stats.exp >= this.stats.levelUpExp)
            {
                  
                this.stats.levelUp();
                this.stats.exp -=this.stats.levelUpExp;
                if (this.stats.level < 50)
                {
                    this.stats.levelUpExp += 50;
                }
                else
                {
                    this.stats.levelUpExp += 1000;
                }
                this.stats.setToFullHealth();
                counter++;
            }
            return counter;
        }
        /// <summary>
        /// Gets attack based on all of the items and the users current attack
        /// </summary>
        /// <param name="userCharacter"></param>
        /// <returns></returns>
        public int getAttack()
        {
            int attack = this.stats.attack;
            foreach (Item userItem in this.peripherals.inventory)
            {
                if (userItem is AttackItem )
                {
                    attack += ((AttackItem)userItem).attackMod;
                }
                else if (userItem is FullAttackItem)
                {
                    attack += ((FullAttackItem)userItem).attackMod;
                }
            }

            return attack;
        }
        /// <summary>
        /// Gets defense while including all of the users items
        /// </summary>
        /// <returns>The users defence when buffed by items</returns>
        public int getDefense()
        {
            int defense = this.stats.defense;
            foreach (Item userItem in this.peripherals.inventory)
            {
                if (userItem is MagicDefenseItem)
                {
                    defense += ((MagicDefenseItem)userItem).magicDefenseMod;
                }
                else if (userItem is FullDefenseItem)
                {
                    defense += ((FullDefenseItem)userItem).defenseMod;
                }
            }
            return defense;
        }
        /// <summary>
        /// Gets max health while including all of the users items
        /// </summary>
        /// <returns>The users max health when buffed by items</returns>
        public int getMaxHealth()
        {
            int maxHealth = this.stats.maxHealth;
            foreach (Item userItem in this.peripherals.inventory)
            {
                if (userItem is HealthItem)
                {
                    maxHealth += ((HealthItem)userItem).healthMod;
                }
            }
            return maxHealth;
        }
        /// <summary>
        /// Gets critical modifier while including all of the users items
        /// </summary>
        /// <returns>The users critical modifier when buffed by items</returns>
        public int getCritMod()
        {
            int critMod = this.stats.critMod;
            foreach (Item userItem in this.peripherals.inventory)
            {
                if (userItem is CritModItem)
                {
                    critMod += ((CritModItem)userItem).critMod;
                }
            }
            return critMod;
        }
        /// <summary>
        /// Gets magic attack while including all of the users items
        /// </summary>
        /// <returns>The magic attack when buffed by items</returns>
        public int getMagicAttack()
        {
            int magicAttack = this.stats.magicAttack;
            foreach (Item userItem in this.peripherals.inventory)
            {
                if (userItem is MagicAttackItem)
                {
                    magicAttack += ((MagicAttackItem)userItem).magicAttackMod;
                }
                else if (userItem is FullAttackItem)
                {
                    magicAttack += ((FullAttackItem)userItem).magicAttackMod;
                }
            }
            return magicAttack;

        }
        /// <summary>
        /// Gets magic defense while including all of the users items
        /// </summary>
        /// <returns>The users magic defense when buffed by items</returns>
        public int getMagicDefense()
        {
            int magicDefense = this.stats.magicDefense;
            foreach (Item userItem in this.peripherals.inventory)
            {
                if (userItem is MagicDefenseItem)
                {
                    magicDefense += ((MagicDefenseItem)userItem).magicDefenseMod;
                }
                else if (userItem is FullDefenseItem)
                {
                    magicDefense += ((FullDefenseItem)userItem).magicDefenseMod;
                }
            }
            return magicDefense;
        }
        /// <summary>
        /// Gets luck while including all of the users items
        /// </summary>
        /// <returns>The users luck when buffed by items</returns>
        public int getLuck()
        {
            int luck = this.stats.luck;
            foreach (Item userItem in this.peripherals.inventory)
            {
                if (userItem is LuckItem)
                {
                    luck += ((LuckItem)userItem).luckMod;
                }
            }
            return luck;
        }
        /// <summary>
        /// Gets speed while including all of the users items
        /// </summary>
        /// <returns>The users speed when buffed by items</returns>
        public int getSpeed()
        {
            int speed = this.stats.speed;
            foreach (Item userItem in this.peripherals.inventory)
            {
                if (userItem is SpeedItem)
                {
                    speed += ((SpeedItem)userItem).speedMod;
                }
            }
            return speed;
        }
        /// <summary>
        /// Allows you to add an item to the players inventory
        /// </summary>
        /// <param name="newItem"></param>
        public void addItem(Item newItem)
        {
            if (newItem is HealthItem)
            {
                this.stats.currentHealth += ((HealthItem)newItem).healthMod;
            }
            this.peripherals.inventory.Add(newItem);
        }
        /// <summary>
        /// Checks if the user knows a spell
        /// </summary>
        /// <param name="spellName">The name of the spell you are looking for</param>
        /// <returns>A boolean, true if they know the spell, false if they don't</returns>
        public bool hasSpell(string spellName)
        {
            
            foreach (Spell spell in this.peripherals.spellBook)
            {
                if (spell.name.ToLower().Equals(spellName.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Adds a given spell to the user's spellbook
        /// </summary>
        /// <param name="newSpell">The spell to be added</param>
        public void addSpell(Spell newSpell)
        {
            this.peripherals.spellBook.Add(newSpell);
        }
}
    public class Stats
    {
        public string playerName { get; set; }
        public string roleName { get; set; }
        public int level { get; set; }        
        public int exp { get; set; }
        public int levelUpExp { get; set; }
        public int maxHealth { get; set; }
        public int currentHealth { get; set; }
        public int attack { get; set; }
        public int critMod { get; set; }
        public int defense { get; set; }
        public int magicAttack { get; set; }
        public int magicDefense { get; set; }
        public int luck { get; set; }
        public int speed { get; set; }

        public Stats()
        {
            playerName = "";
            roleName = "";
            level = 1;            
            exp = 0;
            levelUpExp = 50;
            maxHealth = 0;
            currentHealth = 0;
            attack = 0;
            critMod = 1;
            defense = 0;
            magicAttack = 0;
            magicDefense = 0;
            luck = 0;
            speed = 0;
        }

        public void levelUp()
        {
            this.level++;
            //sets increased stats
            switch (this.roleName.ToLower())
            {
                case "funeral director":
                    Random rnd = new Random();
                    
                    this.maxHealth += rnd.Next(5, 13);
                    this.currentHealth = maxHealth;
                    this.attack += rnd.Next(1, 5);
                    this.defense += rnd.Next(1, 3);
                    this.magicAttack += rnd.Next(0, 4);
                    this.magicDefense += rnd.Next(0, 2);
                    this.luck += rnd.Next(1, 5);
                    this.speed += rnd.Next(1, 3);
                    
                    break;
                case "knight":
                    this.maxHealth += 10;
                    this.attack += 3;
                    this.defense += 2;
                    this.magicAttack += 0;
                    this.magicDefense += 1;
                    this.luck += 1;
                    this.speed += 1;
                    
                    break;
                case "warrior":
                    this.maxHealth += 12;
                    this.attack += 3;
                    this.defense += 1;
                    this.magicAttack += 0;
                    this.magicDefense += 1;
                    this.luck += 1;
                    this.speed += 1;
                              
                    break;
                case "thief":
                    this.maxHealth += 7;
                    this.attack += 4;
                    this.defense += 1;
                    this.magicAttack += 1;
                    this.magicDefense += 1;
                    this.luck += 2;
                    this.speed += 3;
           
                    break;
                case "mage":
                    this.maxHealth += 8;
                    this.attack += 1;
                    this.defense += 1;
                    this.magicAttack += 3;
                    this.magicDefense += 2;
                    this.luck += 1;
                    this.speed += 1;

                    break;
                case "cleric":
                    this.maxHealth += 11;
                    this.attack += 2;
                    this.defense += 1;
                    this.magicAttack += 2;
                    this.magicDefense += 3;
                    this.luck += 2;
                    this.speed += 1;
        
                
                    break;
                case "jester":
                    this.maxHealth += 7;
                    this.attack += 2;
                    this.defense += 1;
                    this.magicAttack += 1;
                    this.magicDefense += 1;
                    this.luck += 4;
                    this.speed += 5;
                    this.critMod += 1;
                
                    break;
                default:
                    Console.WriteLine("This shouldn't happen");
                    break;
            }
        }
        /// <summary>
        /// Allows you to increment the exp of the current target
        /// </summary>
        /// <param name="amount">The amount of experience you want to add</param>
        public void addExp(int amount)
        {
            this.exp += amount;
        }
        /// <summary>
        /// Sets the current targets health to full
        /// </summary>
        public void setToFullHealth()
        {
            this.currentHealth = this.maxHealth;
        }

    }
    public class Modifiers
    {
        public Modifiers()
        {
            isBurned = false;
            isConfused = false;
            isAsleep = false;
            isParalyzed = false;
            turnsLeft = 0;
        }
        public bool isBurned { get; set; }
        public bool isConfused { get; set; }
        public bool isAsleep { get; set; }
        public bool isParalyzed { get; set; }
        public int turnsLeft { get; set; }
        /// <summary>
        /// Checks if the current thing is afflicted with some status condition
        /// </summary>
        /// <returns></returns>
        public bool currentlyAfflicted()
        {
            if (isAsleep || isBurned || isConfused || isParalyzed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Removes any current condition afflicting the current thing
        /// </summary>
        public void removeCondition()
        {
            isBurned = false;
            isConfused = false;
            isAsleep = false;
            isParalyzed = false;
            turnsLeft = 0;
        }
}
public class Peripheral
    {
        public Peripheral()
        {
            inventory = new List<Item>();
            spellBook = new List<Spell>();          
        }
        // need to change to item once items are implemented
        public List<Item> inventory { get; set; }
        // need to change to spell once spells are implemented
        public List<Spell> spellBook { get; set; }
    }

    public class StatAssignment
    {
      
      
        /// <summary>
        ///This Method sets the initial stats based on the class (inputClass) the user picks 
        /// </summary>
        
        public void setInitialStats(string inputRole, Character character)
        {
            /*
             	        maxHealth	Attack	Defense	MagicAttack	MagicDefense	Luck	Speed
                knight 	100 	30	    20	    0	        15	            5	    5
                warrior	120 	30	    10	    0	        10	            5   	5
                thief  	70  	40	    5	    5	        5	            10  	15
                mage   	80	    5	    5	    25	        20	            5	    10
                cleric 	110	    20	    10	    20	        25          	10	    5
                jester 	70	    10	    5	    10	        10	            20	    20
            */
            
            //set name of class within switch
            /*
            polymorphism - behavior change as object changes
            base class called character
            method called get attack and pass in class to get initial
            use to have multipliers and dividers
            eight copies of getAttack based on input class
            */
            

            
            do
            {
                //sets initial stats
                switch (inputRole.ToLower())
                {
                    case "funeral director":
                        Random rnd = new Random();
                        character.stats.maxHealth = rnd.Next(50, 130);
                        character.stats.attack = rnd.Next(5, 50);
                        character.stats.defense = rnd.Next(0, 25);
                        character.stats.magicAttack = rnd.Next(0, 35);
                        character.stats.magicDefense = rnd.Next(0, 25);
                        character.stats.luck = rnd.Next(4, 20);
                        character.stats.speed = rnd.Next(1, 25);
                        character.stats.roleName = "Funeral Director";
                        //Adds trocar
                        character.addItem(Library.ItemLists.masterItemDic()[12]);
                        //Adds trench coat
                        character.addItem(Library.ItemLists.masterItemDic()[29]);
                        //Adds bowler hat
                        character.addItem(Library.ItemLists.masterItemDic()[14]);

                        break;

                    case "knight":
                        character.stats.maxHealth = 100;
                        character.stats.attack = 30;
                        character.stats.defense = 20;
                        character.stats.magicAttack = 0;
                        character.stats.magicDefense = 15;
                        character.stats.luck = 4;
                        character.stats.speed = 5;
                        character.stats.roleName = "Knight";
                        //Longsword
                        character.addItem(Library.ItemLists.masterItemDic()[7]);
                        //Knight's Armor
                        character.addItem(Library.ItemLists.masterItemDic()[24]);
                        //Knight's Guantlets
                        character.addItem(Library.ItemLists.masterItemDic()[25]);
                        //Knight's Helmet
                        character.addItem(Library.ItemLists.masterItemDic()[26]);                       
                        //Knight's Leggings
                        character.addItem(Library.ItemLists.masterItemDic()[27]);

                        break;

                    case "warrior":
                        character.stats.maxHealth = 120;
                        character.stats.attack = 30;
                        character.stats.defense = 10;
                        character.stats.magicAttack = 0;
                        character.stats.magicDefense = 10;
                        character.stats.luck = 4;
                        character.stats.speed = 5;
                        character.stats.roleName = "Warrior";
                        //Broadsword
                        character.addItem(Library.ItemLists.masterItemDic()[3]);
                        //Brass Chestplate
                        character.addItem(Library.ItemLists.masterItemDic()[15]);
                        //Tattered Pants
                        character.addItem(Library.ItemLists.masterItemDic()[28]);
                        break;

                    case "thief":
                        character.stats.maxHealth = 70;
                        character.stats.attack = 40;
                        character.stats.defense = 5;
                        character.stats.magicAttack = 5;
                        character.stats.magicDefense = 5;
                        character.stats.luck = 8;
                        character.stats.speed = 15;
                        character.stats.roleName = "Thief";
                        //Dagger
                        character.addItem(Library.ItemLists.masterItemDic()[37]);
                        //Hood
                        character.addItem(Library.ItemLists.masterItemDic()[19]);
                        //Black Cloak
                        character.addItem(Library.ItemLists.masterItemDic()[13]);
                        //Gloves
                        character.addItem(Library.ItemLists.masterItemDic()[16]);

                        break;
                    case "mage":
                        character.stats.maxHealth = 80;
                        character.stats.attack = 5;
                        character.stats.defense = 5;
                        character.stats.magicAttack = 25;
                        character.stats.magicDefense = 20;
                        character.stats.luck = 4;
                        character.stats.speed = 10;
                        character.stats.roleName = "Mage";
                        //Staff
                        character.addItem(Library.ItemLists.masterItemDic()[31]);
                        //Mage Cloak
                        character.addItem(Library.ItemLists.masterItemDic()[33]);
                        //Dull Dagger
                        character.addItem(Library.ItemLists.masterItemDic()[5]);
                        //character.peripherals.spellBook.Add("Magic Spear
                        character.addSpell(Library.SpellList.getSpell("Fireball"));
                        character.addSpell(Library.SpellList.getSpell("Lightning"));

                        break;
                    case "cleric":
                        character.stats.maxHealth = 110;
                        character.stats.attack = 20;
                        character.stats.defense = 10;
                        character.stats.magicAttack = 20;
                        character.stats.magicDefense = 25;
                        character.stats.luck = 8;
                        character.stats.speed = 5;
                        character.stats.roleName = "Cleric";
                        //Mace
                        character.addItem(Library.ItemLists.masterItemDic()[8]);
                        //Long Robe
                        character.addItem(Library.ItemLists.masterItemDic()[32]);
                        //Talisman
                        character.addItem(Library.ItemLists.masterItemDic()[38]);
                        //character.peripherals.spellBook.Add("Heal
                        character.addSpell(Library.SpellList.getSpell("Heal"));
                        break;

                    case "jester":
                        character.stats.maxHealth = 70;
                        character.stats.attack = 10;
                        character.stats.defense = 5;
                        character.stats.magicAttack = 10;
                        character.stats.magicDefense = 10;
                        character.stats.luck = 16;
                        character.stats.speed = 20;
                        character.stats.critMod = 4;
                        character.stats.roleName = "Jester";
                        //Brass Knuckles
                        character.addItem(Library.ItemLists.masterItemDic()[2]);                        
                        //Jester Cap");
                        character.addItem(Library.ItemLists.masterItemDic()[20]);
                        //Jester Gloves");
                        character.addItem(Library.ItemLists.masterItemDic()[21]);
                        //Jester Outfit");
                        character.addItem(Library.ItemLists.masterItemDic()[22]);
                        //Jester Shoes
                        character.addItem(Library.ItemLists.masterItemDic()[23]);
                        

                        break;
                    default:
                        Console.WriteLine("This shouldn't happen");
                        break;
                }
            } while (character.stats.maxHealth == 0); //Had this for the loop condition since no character should start with 0 maxHealth
            character.stats.currentHealth = character.stats.maxHealth;
        }
    }
}



