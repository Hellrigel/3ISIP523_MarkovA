using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP523_MarkovA.Models
{
    public class Armor
    {
        public string Name { get; set; }
        public double Defense { get; set; }

        public Armor(string name, double defense)
        {
            Name = name;
            Defense = defense;
        }
    }

}
