using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameMaker.Game.Characters;
using System.Windows.Forms;
using GameMaker.Game.Mechanics;
using GameMaker.Libraries;

namespace GameMaker.Game.Calculations
{
    public class Combat
    {
        private TextBox outBox;
        private Character userCharacter { get; set; }
        private Monster enemy { get; set; }

        public int freeEnemyAttack(Character userCharacter, Monster enemy, TextBox outBox)
        {
            
            this.userCharacter = userCharacter;
            this.enemy = enemy;
            this.outBox = outBox;
            int enemyDamage = getEnemyDamage();
            userCharacter.stats.currentHealth -= enemyDamage;
            outBox.AppendText("The " + enemy.name + " dealt " + enemyDamage + " damage to you, you have " + userCharacter.stats.currentHealth + "  hit points left" + "\n");
            if (userCharacter.stats.currentHealth <= 0)
            {
                return 2;
            }              
            return 0;
        }
        /// <summary>
        /// Performs combat based on the given parameters, this overload allows the user to cast spells at enemies
        /// </summary>
        /// <param name="userCharacter">The user</param>
        /// <param name="enemy">The enemy</param>
        /// <param name="outBox">The textbox where the output text should go</param>
        /// <param name="userSpellName">The spell the user wants to cast</param>
        /// <returns></returns>
        public int combat(Character userCharacter, Monster enemy, TextBox outBox, string userSpellName)
        {
            Spell currentSpell = Library.SpellList.getSpell(userSpellName);
            this.userCharacter = userCharacter;
            this.enemy = enemy;
            this.outBox = outBox;
            bool attacking = true;
            bool selfAttack = false;
            bool healing = userSpellName.ToLower().Equals("heal");
            if (enemy.modifiers.isAsleep)
            {
                attacking = false;
                outBox.AppendText("The " + enemy.name + " is currently asleep.");
                enemy.modifiers.turnsLeft--;
                if (enemy.modifiers.turnsLeft == 0)
                {
                    outBox.AppendText("The " + enemy.name + " woke up and can now attack!");
                    enemy.modifiers.removeCondition();
                }
            }
            else if (enemy.modifiers.isParalyzed)
            {
                Random rnd = new Random();
                if (rnd.Next(2) == 0)
                {
                    attacking = false;
                    outBox.AppendText("The " + enemy.name + " is currently paralyzed.");                    
                }
                enemy.modifiers.turnsLeft--;
                if (enemy.modifiers.turnsLeft <= 0)
                {
                    outBox.AppendText("The " + enemy.name + " is no longer paralyzed!");
                    enemy.modifiers.removeCondition();
                }
            }
            else if (enemy.modifiers.isConfused)
            {
                Random rnd = new Random();
                
                outBox.AppendText("The " + enemy.name + " is currently confused.");
                enemy.modifiers.turnsLeft--;
                if (rnd.Next(2) == 0)
                {
                    selfAttack = true;
                    outBox.AppendText("The " + enemy.name + " punched itself directly in the face.");
                }
                if (enemy.modifiers.turnsLeft == 0)
                {
                    outBox.AppendText("The " + enemy.name + " is not confused anymore!");
                    enemy.modifiers.removeCondition();
                }
            }
            if (enemy.stats.speed > userCharacter.getSpeed())
            {
               
                if (attacking)
                {
                    int enemyDamage = getEnemyDamage();
                    if (selfAttack)
                    {
                        outBox.AppendText("The " + enemy.name + " is currently confused.");
                        enemy.stats.currentHealth -= enemyDamage;
                        outBox.AppendText("From the direct punch to the face, the monster took " + enemyDamage + " damage, and has " + enemy.stats.currentHealth + " hit points left " + "\n");
                        if (enemy.stats.currentHealth <= 0)
                        {
                            return 1;
                        }
                    }
                    else
                    {
                        userCharacter.stats.currentHealth -= enemyDamage;
                        outBox.AppendText("The " + enemy.name + " dealt " + enemyDamage + " damage to you, you have " + userCharacter.stats.currentHealth + "  hit points left" + "\n");
                        if (userCharacter.stats.currentHealth <= 0)
                        {
                            return 2;
                        }
                    }
                }
                if (healing)
                {
                    userHeal();
                }
                else
                {
                    int userDamage = getUserMagicDamage(currentSpell);
                    enemy.stats.currentHealth -= userDamage;
                    outBox.AppendText("You dealt " + userDamage + " damage to the monster, it has " + enemy.stats.currentHealth + "  hit points left" + "\n");
                    if (enemy.stats.currentHealth <= 0)
                    {
                        return 1;
                    }
                }

            }
            else
            {

                int userDamage = getUserMagicDamage(currentSpell);
                enemy.stats.currentHealth -= userDamage;
                outBox.AppendText("You dealt " + userDamage + " damage to the monster, it has " + enemy.stats.currentHealth + "  hit points left" + "\n");
                if (enemy.stats.currentHealth <= 0)
                {
                    return 1;
                }
                if (attacking)
                {
                    int enemyDamage = getEnemyDamage();
                    if (selfAttack)
                    {
                        outBox.AppendText("The " + enemy.name + " is currently confused.");
                        enemy.stats.currentHealth -= enemyDamage;
                        outBox.AppendText("From the direct punch to the face, the monster took " + enemyDamage + " damage, and has " + enemy.stats.currentHealth + " hit points left " + "\n");
                        if (enemy.stats.currentHealth <= 0)
                        {
                            return 1;
                        }
                    }
                    else
                    {
                        userCharacter.stats.currentHealth -= enemyDamage;
                        outBox.AppendText("The " + enemy.name + " dealt " + enemyDamage + " damage to you, you have " + userCharacter.stats.currentHealth + "  hit points left" + "\n");
                        if (userCharacter.stats.currentHealth <= 0)
                        {
                            return 2;
                        }
                    }
                }
            }
            if (enemy.modifiers.isBurned)
            {
                int burnDamage = (int)Math.Ceiling((double)enemy.stats.maxHealth / 10);
                enemy.stats.currentHealth -= burnDamage;
                outBox.AppendText("The burn dealt " + burnDamage + " damage to the monster, it has " + enemy.stats.currentHealth + "  hit points left" + "\n");
            }
            if (enemy.stats.currentHealth <= 0)
            {
                return 1;
            }
            return 0;
        }
        /// <summary>
        /// Performs combat based on the character and enemy
        /// returns either 0 if nothing happened, 1 if the enemy died, and 2 if the user died
        /// </summary>
        /// <param name="userCharacter">The user</param>
        /// <param name="enemy">The enemy</param>
        /// <param name="outBox">The place to print out the result to</param>
        /// <returns> </returns>
        public int combat(Character userCharacter, Monster enemy, TextBox outBox)
        {
            this.userCharacter = userCharacter;
            this.enemy = enemy;
            this.outBox = outBox;
            bool attacking = true;
            bool selfAttack = false;
            if (enemy.modifiers.isAsleep)
            {
                attacking = false;
                outBox.AppendText("The " + enemy.name + " is currently asleep." + "\r\n");
                enemy.modifiers.turnsLeft--;
                if (enemy.modifiers.turnsLeft == 0)
                {
                    outBox.AppendText("The " + enemy.name + " woke up and can now attack!" + "\r\n");
                    enemy.modifiers.removeCondition();
                }
            }
            else if (enemy.modifiers.isParalyzed)
            {
                Random rnd = new Random();
                if (rnd.Next(2) == 0)
                {
                    attacking = false;
                    outBox.AppendText("The " + enemy.name + " is currently paralyzed." + "\r\n");
                }
                enemy.modifiers.turnsLeft--;
                if (enemy.modifiers.turnsLeft <= 0)
                {
                    outBox.AppendText("The " + enemy.name + " is no longer paralyzed!" + "\r\n");
                    enemy.modifiers.removeCondition();
                }
            }
            else if (enemy.modifiers.isConfused)
            {
                Random rnd = new Random();

                outBox.AppendText("The " + enemy.name + " is currently confused." + "\r\n");
                enemy.modifiers.turnsLeft--;
                if (rnd.Next(2) == 0)
                {
                    selfAttack = true;
                    outBox.AppendText("The " + enemy.name + " punched itself directly in the face." + "\r\n");
                }
                if (enemy.modifiers.turnsLeft == 0)
                {
                    outBox.AppendText("The " + enemy.name + " is not confused anymore!" + "\r\n");
                    enemy.modifiers.removeCondition();
                }
            }
            if (enemy.stats.speed > userCharacter.getSpeed())
            {

                if (attacking)
                {
                    int enemyDamage = getEnemyDamage();
                    if (selfAttack)
                    {
                        outBox.AppendText("The " + enemy.name + " is currently confused." + "\r\n");
                        enemy.stats.currentHealth -= enemyDamage;
                        outBox.AppendText("From the direct punch to the face, the monster took " + enemyDamage + " damage, and has " + enemy.stats.currentHealth + " hit points left " + "\r\n");
                        if (enemy.stats.currentHealth <= 0)
                        {
                            return 1;
                        }
                    }
                    else
                    {
                        userCharacter.stats.currentHealth -= enemyDamage;
                        outBox.AppendText("The " + enemy.name + " dealt " + enemyDamage + " damage to you, you have " + userCharacter.stats.currentHealth + "  hit points left" + "\r\n");
                        if (userCharacter.stats.currentHealth <= 0)
                        {
                            return 2;
                        }
                    }
                }

                int userDamage = getUserMeleeDamage();
                enemy.stats.currentHealth -= userDamage;
                outBox.AppendText("You dealt " + userDamage + " damage to the monster, it has " + enemy.stats.currentHealth + "  hit points left" + "\n");
                if (enemy.stats.currentHealth <= 0)
                {
                    return 1;
                }

            }
            else
            {

                int userDamage = getUserMeleeDamage();
                enemy.stats.currentHealth -= userDamage;
                outBox.AppendText("You dealt " + userDamage + " damage to the monster, it has " + enemy.stats.currentHealth + "  hit points left" + "\n");
                if (enemy.stats.currentHealth <= 0)
                {
                    return 1;
                }

                if (attacking)
                {
                    int enemyDamage = getEnemyDamage();
                    if (selfAttack)
                    {
                        outBox.AppendText("The " + enemy.name + " is currently confused." + "\r\n");
                        enemy.stats.currentHealth -= enemyDamage;
                        outBox.AppendText("From the direct punch to the face, the monster took " + enemyDamage + " damage, and has " + enemy.stats.currentHealth + " hit points left " + "\n");
                        if (enemy.stats.currentHealth <= 0)
                        {
                            return 1;
                        }
                    }
                    else
                    {
                        userCharacter.stats.currentHealth -= enemyDamage;
                        outBox.AppendText("The " + enemy.name + " dealt " + enemyDamage + " damage to you, you have " + userCharacter.stats.currentHealth + "  hit points left" + "\n");
                        if (userCharacter.stats.currentHealth <= 0)
                        {
                            return 2;
                        }
                    }
                }
            }
            if (enemy.modifiers.isBurned)
            {
                int burnDamage = (int)Math.Ceiling((double)enemy.stats.maxHealth / 10);
                enemy.stats.currentHealth -= burnDamage;
                outBox.AppendText("The burn dealt " + burnDamage + " damage to the monster, it has " + enemy.stats.currentHealth + "  hit points left" + "\n");
            }
            if (enemy.stats.currentHealth <= 0)
            {
                return 1;
            }
            return 0;
        }
        /// <summary>
        /// Gets the damage of the enemy attacking the user
        /// </summary>
        /// <param name="enemy"></param>
        /// <param name="userCharacter"></param>
        /// <returns></returns>
        private int getEnemyDamage()
        {
            return getEnemyMeleeDamage();
        }
        private int getEnemyMeleeDamage()
        {
            Random rnd = new Random();
            double rndMod = ((double)rnd.Next(40, 50) / 50);
            double playerDefenseMod = (double)userCharacter.getDefense() / 5;
            if (getCriticalHit(enemy.stats.luck))
            {
                //defaults that enemies have no critmod, can be added in later if needed
                if (enemy.stats.attack * getCritDamage(enemy.stats.attack, 0) <= userCharacter.getDefense())
                {
                    return 0;
                }
                else
                {
                    outBox.AppendText("The enemy's dealt a crit!" + "\n");
                    return (int)Math.Ceiling(2 * enemy.stats.critMod * (enemy.stats.attack / playerDefenseMod) * rndMod);
                }
            }
            else
            {
                return (int)Math.Ceiling((enemy.stats.attack / playerDefenseMod) * rndMod);
            }

        }
        /// <summary>
        /// Calculates the melee damage of userCharacter attacking the enemy
        /// </summary>
        /// <returns>An integer representing that damage</returns>
        private int getUserMeleeDamage()
        {
            Random rnd = new Random();
            double rndMod = ((double)rnd.Next(40, 50) / 50);
            double defenseMod = (double)enemy.stats.defense / 2;
            if (getCriticalHit(userCharacter.getLuck()))
            {
                if (userCharacter.getAttack() * getCritDamage(userCharacter.getAttack(), userCharacter.getCritMod()) <= enemy.stats.defense)
                {
                    return 0;
                }
                else
                {
                    outBox.AppendText("You've dealt a crit!" + "\n");
                    return (int)Math.Ceiling(2 * userCharacter.getCritMod() * (enemy.stats.attack / defenseMod) * rndMod);
                }
            }
            else
            {
                return (int)Math.Ceiling((userCharacter.getAttack() / defenseMod) * rndMod);

            }
        }
        /// <summary>
        /// Deals the enemy damage and applies status effects based on the given spell
        /// </summary>
        /// <param name="spell">The spell attacking the user</param>
        /// <returns>An int representing the damage dealt</returns>
        private int getUserMagicDamage(Spell spell)
        {
            Random rnd = new Random();
            double rndMod = ((double)rnd.Next(40, 50) / 50);
            double magicDefenseMod = (double)enemy.stats.magicDefense / 2;
            double magicPower = (double)spell.power / 5;
            //check to see which status effect we are going to apply
            if (rnd.Next(3) == 0)
            {
                if (!enemy.modifiers.currentlyAfflicted())
                {
                    if (spell.canBurn)
                    {
                        outBox.AppendText("The enemy is now burnt" + "\r\n");
                        enemy.modifiers.isBurned = true;
                        enemy.modifiers.turnsLeft = 4;
                        
                    }
                    else if (spell.canConfuse)
                    {
                        outBox.AppendText("The enemy is now confused" + "\r\n");
                        enemy.modifiers.isConfused = true;
                        enemy.modifiers.turnsLeft = 3;
                    }
                    else if (spell.canParalyze)
                    {
                        outBox.AppendText("The enemy is now paralyzed" + "\r\n");
                        enemy.modifiers.isParalyzed = true;
                        enemy.modifiers.turnsLeft = 4;
                    }
                    else if (spell.canSleep)
                    {
                        outBox.AppendText("The enemy is now asleep" + "\r\n");
                        enemy.modifiers.isAsleep = true;
                        enemy.modifiers.turnsLeft = 2;
                    }
                }
            }
            return (int)Math.Ceiling((userCharacter.getMagicAttack() / magicDefenseMod) * rndMod);

        }
        /// <summary>
        /// Heals the current thing
        /// </summary>
        private void userHeal()
        {
            int healAmount = (int)Math.Ceiling((double)userCharacter.stats.maxHealth / 20);
            userCharacter.stats.currentHealth += healAmount;
            outBox.AppendText("You healed for " + healAmount + " hit points, you now have " + userCharacter.stats.currentHealth + " health.:" + "\r\n");
        }
        

        
        /// <summary>
        /// Gets the critical damage given luck and a critical modifier
        /// </summary>
        /// <param name="luck">The critical hit user's luck stat</param>
        /// <param name="critmod">The critical hit user's critical modifier</param>
        /// <returns>An integer representing the damage that they will deal</returns>
        private int getCritDamage(int luck, int critmod)
        {
            int critMultiplier = 2 * userCharacter.getCritMod();

            return critMultiplier;
        }
        /// <summary>
        /// Figures out if the current luck will cause a critical hit
        /// </summary>
        /// <param name="luck">The luck of whoever is trying to crit</param>
        /// <returns>A boolean, true if they crit, false if they didn't</returns>
        private bool getCriticalHit(int luck)
        {
            bool criticalHit = false;
            Random rnd = new Random();
            int crit = rnd.Next(1, 15);
            if (crit >= 1 && crit <= luck / 4)
            {
                criticalHit = true;
            }
            return criticalHit;
        }
    }
}
