using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP523_MarkovA.Models
{
    public class Character
    {
        public string Name { get; set; }
        public int Hp { get; set; }
        public Weapon CurrentWeapon { get; set; }
        public Armor CurrentArmor { get; set; }

        public Character(string name, int hp, Weapon weapon, Armor armor)
        {
            Name = name;
            Hp = hp;
            CurrentWeapon = weapon;
            CurrentArmor = armor;
        }
    }
}

