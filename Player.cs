using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    class Player
    {
        public float maxHealth;
        

        public int Level { get;  set; }
        public string Name { get;  set; }
        public string CharacterClass { get;  set; }
        public double Attack { get;  set; }
        public double Defense { get;  set; }
        public double Health { get;  set; }
        public int Gold { get; set; }
        public  List<Item> Inventory { get;  set; } = new List<Item>();
        public  List<Item> EquippedItems { get;  set; } = new List<Item>();

        public Player() 
        { }

        public Player(int level, string name, string characterClass, int attack, int defense, int health, int gold)
        {
            Level = level;
            Name = name;
            CharacterClass = characterClass;
            Attack = attack;
            Defense = defense;
            Health = health;
            Gold = gold;
            maxHealth = health;
        }

        public static void LevelUp(Player player)
        {
            player.Level += 1;
            player.Attack += 0.5;
            player.Defense += 1;
        }

        
    }
}
