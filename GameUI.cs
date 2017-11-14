using ConsoleApplication2.Game.Command_Processing;
using GameMaker.Game.Calculations;
using GameMaker.Game.Characters;

using GameMaker.Game.Mechanics;

using GameMaker.Libraries;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameUI
{
    public partial class GameUI : Form
    {
        //enemyInfo = right text box
        //userInfo = left text box
        //inputBox = bottom text box
        //action = middle text box
        private Character user { get; set; }
        
        private string phase { get; set; }
        private string inputName { get; set; }
        private Monster enemy { get; set; }
        

        private int something;
        public int getSomething()
        { 
            return something;
        }
        public void setSomething(int something)
        {
            this.something = something;
        }
            

        public GameUI()
        { 
            InitializeComponent();
            //Don't want to let the user edit anything other than InputBox
            enemyInfo.ReadOnly = true;
            userInfo.ReadOnly = true;
            outputBox.ReadOnly = true;
            commandBox.ReadOnly = true;
            //Initialize user
            user = new Character();
            phase = "nameInput";

            updateEnemyInfo();
            updateUserInfo();
            updateCommands(); updateCommands();
            //Set initial text to ask for input
            outputBox.Text = "What is your name? (If you need a suggestion type the word \"Suggestion\")" + "\n";
            outputBox.ScrollBars = ScrollBars.Both;            
        }

        private void InputBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            
         
            //reference keys library and then cast it so that their types match
            if (e.KeyChar == (char)Keys.Return)
            {
                updateEnemyInfo();
                updateUserInfo();
                String input = inputBox.Text;
                inputBox.Text = "";
                Combat combat = new Combat();
                switch (phase)
                {                    
                    case "nameInput":
                        if (!input.ToLower().Equals("suggestion"))
                        {
                            this.inputName = input;
                            outputBox.AppendText("Your name is " + inputName + "\n");                            
                            System.Threading.Thread.Sleep(1000);
                            outputBox.AppendText("Please choose a class from these options:" + "\n");
                            System.Threading.Thread.Sleep(1000);
                            foreach (string str in Library.roleList())
                            {
                                outputBox.AppendText(str + "\n");
                            }
                            
                            phase = "classInput";
                            break;
                        }
                        else
                        {
                            outputBox.AppendText(Library.GameHelp.suggester());
                        }
                        break;
                    case "classInput":
                        input = input.ToLower();
                        if (!Library.roleList().Contains(input))
                        {
                            outputBox.AppendText("Please choose a valid class" + "\n");                            
                        }
                        else
                        {
                            this.user = new Character(input, this.inputName);
                            outputBox.AppendText("You are now " + this.inputName + " the " + input + "\n");
                            outputBox.AppendText("Let the games begin " + "\n");
                            phase = "mainGame";
                        }
                        break;
                    case "mainGame":
                        Random rnd = new Random();
                        input = input.ToLower();
                        if (input.Equals("move") || input.Equals("advance"))
                        {
                            if (input.Equals("advance"))
                            {
                                user.position += 1;
                            }
                            
                            //logic for if they encounter an enemy
                            if (rnd.Next(2) == 0)
                            {
                                if (input.Equals("advance"))
                                {
                                    outputBox.AppendText("You have advanced,  there are no enemies here. " + (10 - user.position) + " advances until next area." + "\n");
                                    //If this is true they found an item or spell
                                    int somethingFound = rnd.Next(4);
                                    if (somethingFound <= 1)
                                    {
                                        if (something == 1)
                                        {
                                            Item foundItem = Item.getRandomItem();
                                            outputBox.AppendText("You have found an item called " + foundItem.itemName + "!" + "\r\n");
                                            user.addItem(foundItem);
                                        }
                                        else
                                        {
                                            Spell foundSpell = Spell.getRandomSpell();
                                            if (!user.hasSpell(foundSpell.name))
                                            {
                                                outputBox.AppendText("You have found the spell " + foundSpell.name + "!" + "\r\n");
                                                user.addSpell(foundSpell);
                                            }
                                            
                                        }
                                    }
                                }
                                else
                                {
                                    outputBox.AppendText("You have moved ahead,  there are no enemies here. " + (10 - user.position) + " advances until next area." + "\n");
                                }
                                if (user.position >= 10)
                                {
                                    if (user.nextArea())
                                    {
                                        outputBox.AppendText("You have entered the " + user.area.areaName + "\r\n");
                                    }
                                    else
                                    {
                                        outputBox.AppendText("Conglaturations, you are winner!" + "\r\n");
                                        phase = "lost";
                                    }
                                }
                            }
                            else
                            {
                                phase = "combat";
                                
                                this.enemy = new Monster(user.area);
                                outputBox.AppendText("A " + enemy.name + " has appeared, will you attack, cast a spell, or run?" + "\n");
                            }
                            
                        }
                      //  this.enemy = processor.processMainInput();
                        
                        break;
                    case "combat":
                        
                        input = input.ToLower();
                        if (input.Equals("attack")){
                            int result = combat.combat(user, enemy, outputBox);
                            if (result == 1)
                            {
                                outputBox.AppendText("You have defeated the " + enemy.name + "! " + "\r\n");
                                outputBox.AppendText("You have gained " + enemy.getMonsterExp() + " exp points!" + "\r\n");
                                int timesLeveledUp =user.addExp(enemy.getMonsterExp());
                                for (int i = 0; i < timesLeveledUp; i++ )
                                {
                                    outputBox.AppendText("You have leveled up to level " + user.stats.level + "!" + "\r\n");
                                }
                                enemy = null;
                                phase = "mainGame";
                                if (user.position >= 10)
                                {
                                    if (user.nextArea())
                                    {
                                        outputBox.AppendText("You have entered the " + user.area.areaName + "\r\n");
                                    }
                                    else
                                    {
                                        outputBox.AppendText("Conglaturations, you are winner!" + "\r\n");
                                        phase = "lost";
                                    }
                                    
                                }
                                else
                                {
                                    outputBox.AppendText((10 - user.position) + " advances until next area." + "\r\n");
                                }
                            }
                            else if (result == 2)
                            {
                                outputBox.AppendText("You have died!" + "\n");
                                phase = "lost";
                            }
                        }
                        else if (input.Equals("spell") || input.Equals("cast") || input.Equals("cast a spell"))
                        {
                            outputBox.AppendText("Please choose a spell from your spellbook: " + "\n");
                            if (user.peripherals.spellBook.Count == 0)
                            {
                                outputBox.AppendText("Oh wait it's empty, you can't cast a spell " + "\n");
                            }
                            else
                            {
                                phase = "spellCasting";
                            }                            
                        }                        
                        else if (input.Equals("run"))
                        {
                            //Generates a random number between 0 and 1 and decides if they escaped
                            Random random = new Random();
                            int escaped = random.Next(2);
                            if (escaped == 0)
                            {
                                outputBox.AppendText("You were unable to escape, the enemy attacks!"+"\n");

                                
                                int freeResult = combat.freeEnemyAttack(user, enemy, outputBox);
                                if(freeResult == 2)
                                {
                                    outputBox.AppendText("You have died!" + "\n");
                                    phase = "lost";
                                }
                            }
                            else
                            {
                                user.position += 1;
                                outputBox.AppendText("You successfully escaped! "+ (10 - user.position) + " advances until next area." + "\n");                                
                                enemy = null;
                                phase = "mainGame";
                            }                        
                        }                        

                        break;
                    case "spellCasting":
                        
                        input = input.ToLower();
                        bool knowsSpell = user.hasSpell(input);
                        if (input.Length > 5 && input.Substring(0, 5).Equals("more:")){
                            bool found = false;
                            string[] inputArray = input.Split(':');
                            if (inputArray.Length == 2)
                            {
                                string inputSpell = inputArray[1];
                                inputSpell = inputSpell.Trim();
                                if (user.hasSpell(inputSpell))
                                {
                                    found = true;
                                    Spell informationSpell = Library.SpellList.getSpell(inputSpell);
                                    outputBox.AppendText(informationSpell.description() + "\r\n");
                                }
                            }
                            if (!found)
                            {
                                outputBox.AppendText("Spell not found!" + "\r\n");
                            }                                                            
                        }
                        
                        else if (knowsSpell)
                        {
                            phase = "combat";

                            int magicResult = combat.combat(user, enemy, outputBox, input);                            
                            if (magicResult == 1)
                            {
                                outputBox.AppendText("You have defeated the " + enemy.name + "! " + "\r\n");
                                outputBox.AppendText("You have gained " + enemy.getMonsterExp() + " exp points!" + "\r\n");
                                int timesLeveledUp = user.addExp(enemy.getMonsterExp());
                                for (int i = 0; i < timesLeveledUp; i++)
                                {
                                    outputBox.AppendText("You have leveled up to level " + user.stats.level + "!" + "\r\n");
                                }
                                enemy = null;
                                phase = "mainGame";
                                if (user.position >= 10)
                                {
                                   if(user.nextArea())
                                    {
                                        outputBox.AppendText("You have entered the " + user.area.areaName + "\r\n");
                                    }
                                    else
                                    {
                                        outputBox.AppendText("Conglaturations, you are winner!" + "\r\n");
                                        phase = "lost";
                                    }
                                }
                                else
                                {
                                    outputBox.AppendText((10 - user.position) + " advances until next area." + "\r\n");
                                }
                            }
                            else if (magicResult == 2)
                            {
                                outputBox.AppendText("You have died!" + "\n");
                                phase = "lost";
                            }
                            
                        }
                        else
                        {
                            outputBox.AppendText("Please choose a valid spell" + "\r\n");
                        }
                        break;
                    case "boss":
                        input = input.ToLower();
                        break;
                    case "lost":
                        input = input.ToLower();
                        if (input.Equals("new game"))
                        {
                            outputBox.AppendText("You have started a new game" + "\n");
                            phase = "nameInput";
                        }
                        else if (input.Equals("end"))
                        {
                            outputBox.AppendText("Thank you for playing" + "\n");
                            Application.Exit();
                        }
                        break;
                    default:
                        outputBox.AppendText("Please input a valid command" + "\n");
                        break;                     
                }
                updateEnemyInfo();
                updateUserInfo();
                updateCommands();
            }
            else
            {
                
            }
            commandBox.SelectionStart = commandBox.Text.Length;
            commandBox.ScrollToCaret();
        }
        /// <summary>
        /// Gets the correct command list for whatever phase the user is in and displays it on the screen
        /// </summary>
        private void updateCommands()
        {
            commandBox.Text = Command.getMainCommands(phase, user);
        }
        /// <summary>
        /// Refreshes the GUI for the user's health, items, etc
        /// </summary>
        private void updateUserInfo()
        {
            userInfo.Text = "Health: " +"\r\n";
            userInfo.AppendText("\r\n");
            if (user.stats.currentHealth > 0)
            {
                userHealthBar.Value = (int)Math.Round((double)user.stats.currentHealth / (double)user.getMaxHealth()* 100, MidpointRounding.ToEven);
            }
            else
            {
                userHealthBar.Value = 0;
            }
            
            foreach (string stat in Library.statList(user))
            {
                userInfo.AppendText(stat);
            }
            userInfo.AppendText("\n");
            userInfo.AppendText("Current Inventory: " + "\n");
            foreach (Item item in user.peripherals.inventory)
            {
                userInfo.AppendText(item.itemName + "\n");
            }
            userInfo.AppendText("\n");
            userInfo.AppendText("Current Spells: " + "\n");
            foreach (Spell spell in user.peripherals.spellBook)
            {
                userInfo.AppendText(spell.name + "\n");
            }


        }
        /// <summary>
        /// Updates the GUI for the enemies health
        /// </summary>
        private void updateEnemyInfo()
        {
            enemyInfo.Text = "Health: ";
            
            enemyInfo.AppendText("\n");            
            enemyInfo.AppendText("\n");            
            enemyInfo.AppendText("Current Enemy: ");
            //Enemy health bar
            if (this.enemy == null || this.enemy.stats.currentHealth<=0)
            {                
                enemyHealthBar.Value = 0;
            }
            else
            {
                enemyHealthBar.Value = (int)Math.Round((double)enemy.stats.currentHealth / (double)enemy.stats.maxHealth * 100, MidpointRounding.ToEven);
            }
            if (this.enemy == null)
            {
                enemyInfo.AppendText("None!");
            }
            //Actual text of panel
            else
            {
                enemyInfo.AppendText("\n");
                enemyInfo.AppendText("\n");
                foreach (string stat in Library.monsterStatList(enemy))
                {
                    enemyInfo.AppendText(stat + "\n");
                }
            }

        }

        
        private void userHealthBar_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            
        //    //draw the grid  in the background
        //    System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.Black);
        //    System.Drawing.Graphics gridBackground;
        //    gridBackground = this.CreateGraphics();
        //    //distance the gridlines are separated by
        //    int incrementer = 100;
                  
        //    int horizontalPainter = 0;
        //    int verticalPainter = 0;
        //    while (horizontalPainter <= 600)
        //    {
        //       // e.Graphics.DrawLine(pen, new Point(0, horizontalPainter), new Point(pictureBox1.Right, horizontalPainter));
        //        Rectangle rectangle = new Rectangle(new Point(0, horizontalPainter), new Size(100, 100));
        //        e.Graphics.DrawRectangle(pen, rectangle);
        //        horizontalPainter += incrementer;
                
        //    }
        //    while (verticalPainter <= 1000)
        //    {
        //        //e.Graphics.DrawLine(pen, new Point(verticalPainter, 0), new Point(verticalPainter, pictureBox1.Bottom));
        //        verticalPainter += incrementer;
        //    }
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void GameUI_Load(object sender, EventArgs e)
        {

        }

        private void enemyHealthBar_Click(object sender, EventArgs e)
        {

        }

        private void enemyInfo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
