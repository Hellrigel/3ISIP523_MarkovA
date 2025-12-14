using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP523_MarkovA.Enemies
{
    public class Slime : Enemy
    {
        public Slime()
            : base("СЛИЗЕНЬ", 50, 7, 2)
        {
        }

        public override void TakeDamage(int damage)
        {
            int reduced = damage - 2;
            if (reduced < 0) reduced = 0;

            int actual = Math.Max(0, reduced - Defense);
            Hp -= actual;
        }
    }

}
