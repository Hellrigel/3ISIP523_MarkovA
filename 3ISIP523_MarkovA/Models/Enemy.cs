using System;

public class Enemy
{
    public string Name { get; protected set; }
    public int Hp { get; protected set; }
    public int Attack { get; protected set; }
    public int Defense { get; protected set; }

    public Enemy(string name, int hp, int attack, int defense)
    {
        Name = name;
        Hp = hp;
        Attack = attack;
        Defense = defense;
    }

    public virtual void TakeDamage(int damage)
    {
        int actual = Math.Max(0, damage - Defense);
        Hp -= actual;
    }

    public int CalculateAttackDamage(double playerArmor)
    {
        return Math.Max(1, Attack - (int)(playerArmor / 3));
    }
}