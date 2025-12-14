using _3ISIP523_MarkovA.Enemies;
using System;

public static class EnemyFactory
{
    private static Random random = new Random();

    public static Enemy CreateEnemy(bool boss)
    {
        if (boss)
            return new Enemy("БОСС", 120, 18, 10);

        int roll = random.Next(4);

        return roll switch
        {
            0 => new Enemy("Гоблин", 40, 10, 5),
            1 => new Enemy("Скелет", 45, 9, 6),
            2 => new Enemy("Маг", 35, 8, 4),
            3 => new Slime()
        };
    }
}
