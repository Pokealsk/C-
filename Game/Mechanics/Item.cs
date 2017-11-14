using GameMaker.Libraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMaker.Game.Mechanics
{   /// <summary>
/// Superclass for all items, items should be made for their respective subtypes so don't use this class unless the item is useless
/// </summary>
   public class Item
    {
        public Item(string itemName, string itemDescription)
        {
            this.itemName = itemName;
            this.itemDescription = itemDescription;
        }
        public string itemName { get; set; }
        public string itemDescription { get; set; }
        
        public static Item getRandomItem()
        {
            Random rnd = new Random();
            int index = rnd.Next(1, Library.ItemLists.masterItemDic().Count);
            return Library.ItemLists.masterItemDic()[index];
        }               
    }
    public class AttackItem : Item
    {
        public AttackItem(string itemName, string itemDescription, int attackMod) : base(itemName, itemDescription)
        {
            this.attackMod = attackMod;
        }
        public int attackMod { get; set; }
    }
    public class DefenseItem : Item
    {
        public DefenseItem(string itemName, string itemDescription, int defense) : base(itemName, itemDescription)
        {
            this.defenseMod = defenseMod;
        }
        public int defenseMod { get; set; }
    }
    public class ExpItem : Item
    {
        public ExpItem(string itemName, string itemDescription, int exp) : base(itemName, itemDescription)
        {
            this.expMod = expMod;
        }
        public int expMod { get; set; }
    }
    public class HealthItem : Item
    {
        public HealthItem(string itemName, string itemDescription, int healthMod) : base(itemName, itemDescription)
        {
            this.healthMod = healthMod;
        }
        public int healthMod { get; set; }
    }
    public class LuckItem : Item
    {
        public LuckItem(string itemName, string itemDescription, int luckMod) : base(itemName, itemDescription)
        {
            this.luckMod = luckMod;
        }
        public int luckMod { get; set; }
    }
    public class MagicAttackItem : Item
    {
        public MagicAttackItem(string itemName, string itemDescription, int magicAttackMod) : base(itemName, itemDescription)
        {
            this.magicAttackMod = magicAttackMod;
        }
        public int magicAttackMod { get; set; }
    }
    public class MagicDefenseItem : Item
    {
        public MagicDefenseItem(string itemName, string itemDescription, int magicDefenseMod) : base(itemName, itemDescription)
        {
            this.magicDefenseMod = magicDefenseMod;
        }
        public int magicDefenseMod { get; set; }
    }
    public class SpeedItem : Item
    {
        public SpeedItem(string itemName, string itemDescription, int speedMod) : base(itemName, itemDescription)
        {
            this.speedMod = speedMod;
        }
        public int speedMod { get; set; }
    }
    public class CritModItem : Item
    {
        public CritModItem(string itemName, string itemDescription, int critMod) : base(itemName, itemDescription)
        {
            this.critMod = critMod;
        }
        public int critMod { get; set; }
    }
    /// <summary>
    /// Allows an item to increase both defense and magical defense
    /// </summary>
    public class FullDefenseItem : Item
    {
        public FullDefenseItem(string itemName, string itemDescription, int defenseMod, int magicDefenseMod) : base(itemName, itemDescription)
        {
            this.defenseMod = defenseMod;
            this.magicDefenseMod = magicDefenseMod;
        }
        public int defenseMod { get; set; }
        public int magicDefenseMod { get; set; }
    }
    /// <summary>
    /// Allows an item to increase both attack and magical attack
    /// </summary>
    public class FullAttackItem : Item
    {
        public FullAttackItem(string itemName, string itemDescription, int attackMod, int magicAttackMod) : base(itemName, itemDescription)
        {
            this.attackMod = attackMod;
            this.magicAttackMod = magicAttackMod;
        }
        public int attackMod { get; set; }
        public int magicAttackMod { get; set; }
    }
}
