using _3ISIP523_MarkovA.Models;
using System;

class Program
{
    static Character player;
    static Random random = new Random();
    static int turnCount = 0;

    static void Main()
    {
        bool isRunning = true;

        while (isRunning)
        {
            Console.WriteLine("\n=== TWO SKELETONS IN A BLACK CAVE ===");
            Console.WriteLine("1 - НАЧАТЬ ИГРУ");
            Console.WriteLine("2 - СОЗДАТЕЛИ");
            Console.WriteLine("3 - ВЫЙТИ");

            Console.Write("Введите цифру: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": StartGame(); break;
                case "2": ShowCredits(); break;
                case "3": isRunning = false; break;
                default: Console.WriteLine("Неверный ввод."); break;
            }
        }
    }

    static void StartGame()
    {
        Console.Write("Введите имя персонажа: ");
        string name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name))
            name = "ПУТЕШЕСТВЕННИК";

        player = new Character(
            name.ToUpper(),
            100,
            new Weapon("МЕЧ УЧЕНИКА", 12),
            new Armor("КОЖАНАЯ КУРТКА", 10)
        );

        CampMenu();
    }

    static void CampMenu()
    {
        bool inCamp = true;

        while (inCamp && player.Hp > 0)
        {
            Console.WriteLine("\n=== ЛАГЕРЬ ===");
            Console.WriteLine($"HP: {player.Hp}");
            Console.WriteLine("1 - Инвентарь");
            Console.WriteLine("2 - В путь");
            Console.WriteLine("3 - Выход");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": ShowInventory(); break;
                case "2": Adventure(); break;
                case "3": inCamp = false; break;
            }
        }
    }

    static void ShowInventory()
    {
        Console.WriteLine($"\nИмя: {player.Name}");
        Console.WriteLine($"HP: {player.Hp}");
        Console.WriteLine($"Оружие: {player.CurrentWeapon.Name} (+{player.CurrentWeapon.Damage})");
        Console.WriteLine($"Броня: {player.CurrentArmor.Name} (+{player.CurrentArmor.Defense})");
        Console.ReadKey();
    }

    static void Adventure()
    {
        turnCount++;
        bool isBoss = turnCount % 10 == 0;

        Enemy enemy = EnemyFactory.CreateEnemy(isBoss);
        Battle(enemy);
    }

    static void Battle(Enemy enemy)
    {
        Console.WriteLine($"\nВас атакует {enemy.Name}!");

        while (player.Hp > 0 && enemy.Hp > 0)
        {
            Console.WriteLine($"\nВаши HP: {player.Hp} | HP врага: {enemy.Hp}");
            Console.WriteLine("1 - Атаковать");
            Console.WriteLine("2 - Защищаться");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                enemy.TakeDamage(player.CurrentWeapon.Damage);
                Console.WriteLine("Вы атаковали!");
            }

            if (enemy.Hp > 0)
            {
                int damage = enemy.CalculateAttackDamage(player.CurrentArmor.Defense);
                player.Hp -= damage;
                Console.WriteLine($"{enemy.Name} наносит {damage} урона!");
            }
        }

        Console.WriteLine(enemy.Hp <= 0 ? "ПОБЕДА!" : "ПОРАЖЕНИЕ...");
        Console.ReadKey();
    }

    static void ShowCredits()
    {
        Console.WriteLine("\nРазработчик: Hellrigel");
        Console.ReadKey();
    }
}
