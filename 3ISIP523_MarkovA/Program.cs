using System;
using System.Threading;

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
            Console.WriteLine("3 - ВЫЙТИ ИЗ ИГРЫ");

            Console.Write("Введите цифру: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": StartGame(); break;
                case "2": ShowCredits(); break;
                case "3":
                    Console.WriteLine("ХОРОШЕГО ПУТИ, СТРАННИК");
                    isRunning = false;
                    break;
                default:
                    Console.WriteLine("Неверный ввод!!!!");
                    break;
            }
        }
    }

    static void StartGame()
    {
        Console.Write("Введите имя вашего персонажа: ");
        string playerName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(playerName)) playerName = "ПУТЕШЕСТВЕННИК";

        Weapon startWeapon = new Weapon("МЕЧ УЧЕНИКА", 12);
        Armor startArmor = new Armor("КУРТКА ИЗ КОЖИ", 10);
        player = new Character(playerName.ToUpper(), 100, startWeapon, startArmor);

        Console.WriteLine($"\nДобро пожаловать, {player.Name}!");
        CampMenu();
    }

    static void CampMenu()
    {
        bool inCamp = true;

        while (inCamp && player.Hp > 0)
        {
            Console.WriteLine($"\n=== ЛАГЕРЬ ===");
            Console.WriteLine($"Твое HP: {player.Hp}");
            Console.WriteLine("1 - Инвентарь");
            Console.WriteLine("2 - В путь");
            Console.WriteLine("3 - Выход в меню");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1": ShowInventory(); break;
                case "2": Adventure(); break;
                case "3": inCamp = false; break;
                default: Console.WriteLine("Неверный ввод."); break;
            }
        }
    }

    static void ShowInventory()
    {
        Console.WriteLine($"\nИмя: {player.Name}");
        Console.WriteLine($"HP: {player.Hp}");
        Console.WriteLine($"Оружие: {player.CurrentWeapon.Name} (+{player.CurrentWeapon.Damage} урона)");
        Console.WriteLine($"Броня: {player.CurrentArmor.Name} (+{player.CurrentArmor.Defense} защиты)");
        Console.WriteLine("Нажмите любую клавишу...");
        Console.ReadKey();
    }

    static void Adventure()
    {
        turnCount++;
        Console.WriteLine($"\n=== ХОД {turnCount} ===");

        
        bool isBoss = turnCount % 10 == 0;

        if (random.Next(2) == 0)
            ChestEvent();
        else
            BattleEvent(isBoss);

        if (player.Hp <= 0)
        {
            Console.WriteLine("\nВы пали... Игра окончена.");
            Console.ReadKey();
        }
    }

    static void ChestEvent()
    {
        Console.WriteLine("\nВы нашли СУНДУК!");
        int drop = random.Next(3);

        switch (drop)
        {
            case 0:
                Console.WriteLine("Вы нашли лечебное зелье! Ваши HP полностью восстановлены.");
                player.Hp = 100;
                break;
            case 1:
                Weapon newWeapon = new Weapon("МЕЧ ВЕТЕРАНА", random.Next(15, 26));
                Console.WriteLine($"\nНайдено оружие: {newWeapon.Name} (+{newWeapon.Damage} урона)");
                Console.WriteLine($"Текущее оружие: {player.CurrentWeapon.Name} (+{player.CurrentWeapon.Damage})");
                Console.Write("Взять новое оружие? (y/n): ");
                if (Console.ReadLine().ToLower() == "y") player.CurrentWeapon = newWeapon;
                break;
            case 2:
                Armor newArmor = new Armor("ЖЕЛЕЗНЫЙ ДОСПЕХ", random.Next(15, 26));
                Console.WriteLine($"\nНайдена броня: {newArmor.Name} (+{newArmor.Defense} защиты)");
                Console.WriteLine($"Текущая броня: {player.CurrentArmor.Name} (+{player.CurrentArmor.Defense})");
                Console.Write("Взять новую броню? (y/n): ");
                if (Console.ReadLine().ToLower() == "y") player.CurrentArmor = newArmor;
                break;
        }

        Console.WriteLine("Нажмите любую клавишу для продолжения...");
        Console.ReadKey();
    }

    static void BattleEvent(bool boss = false)
    {
        Enemy enemy = EnemyGenerator(boss);
        Console.WriteLine($"\nВас атакует {(boss ? "БОСС" : "враг")} — {enemy.Name}!");

        bool frozen = false;

        while (player.Hp > 0 && enemy.Hp > 0)
        {
            Console.WriteLine($"\nВаши HP: {player.Hp} | HP врага: {enemy.Hp}");
            Console.WriteLine("1 - Атаковать");
            Console.WriteLine("2 - Защищаться");
            Console.Write("Ваш выбор: ");
            string choice = Console.ReadLine();

            bool defend = false;
            double blockPower = 0;

            if (!frozen)
            {
                switch (choice)
                {
                    case "1":
                        int damage = player.CurrentWeapon.Damage;
                        enemy.TakeDamage(damage);
                        Console.WriteLine($"Вы нанесли {damage} урона врагу!");
                        break;

                    case "2":
                        defend = true;
                        blockPower = random.Next(70, 101);
                        Console.WriteLine($"Вы приняли защитную стойку! Шанс уклонения 40%, блок {blockPower}%");
                        break;

                    default:
                        Console.WriteLine("Вы растерялись и пропустили ход!");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Вы заморожены и пропускаете ход!");
                frozen = false;
            }

            // Враггг
            if (enemy.Hp > 0)
            {
                bool dodge = defend && random.Next(100) < 40;
                if (dodge)
                {
                    Console.WriteLine("Вы уклонились от атаки!");
                    continue;
                }

                int enemyDamage = enemy.CalculateAttackDamage(player.CurrentArmor.Defense);
                if (defend)
                {
                    enemyDamage = (int)(enemyDamage * (1 - blockPower / 100.0));
                }

                player.Hp -= enemyDamage;
                Console.WriteLine($"{enemy.Name} атакует! Вы получили {enemyDamage} урона.");

                if (enemy.CanFreeze && random.Next(100) < enemy.FreezeChance)
                {
                    Console.WriteLine("Вы заморожены магией! Следующий ход пропущен!");
                    frozen = true;
                }
            }
        }

        if (enemy.Hp <= 0)
        {
            Console.WriteLine($"\nВы победили {enemy.Name}!");
        }
        else
        {
            Console.WriteLine("\nВы были повержены...");
        }

        Console.WriteLine("Нажмите любую клавишу для продолжения...");
        Console.ReadKey();
    }

    static Enemy EnemyGenerator(bool boss)
    {
        if (boss)
        {
            int roll = random.Next(4);
            switch (roll)
            {
                case 0: return new Enemy("ВВГ", 120, 18, 12, critChance: 30);
                case 1: return new Enemy("КОВАЛЬСКИЙ", 150, 16, 14, ignoreArmor: true);
                case 2: return new Enemy("АРХИМАГ C++", 90, 20, 11, canFreeze: true, freezeChance: 25);
                default: return new Enemy("ПЕСТОВ С--", 130, 22, 6, ignoreArmor: true, canFreeze: true, freezeChance: 30);
            }
        }
        else
        {
            int roll = random.Next(3);
            switch (roll)
            {
                case 0: return new Enemy("Гоблин", 40, 10, 5, critChance: 20);
                case 1: return new Enemy("Скелет", 45, 9, 6, ignoreArmor: true);
                default: return new Enemy("Маг", 35, 8, 4, canFreeze: true, freezeChance: 15);
            }
        }
    }

    static void ShowCredits()
    {
        Console.WriteLine("\n=== СОЗДАТЕЛИ ===");
        Console.WriteLine("Разработчик: Hellrigel");
        Console.WriteLine("Дизайнер: Hellrigel");
        Console.WriteLine("Тестировщик: Hellrigel");
        Console.WriteLine("Нажмите любую клавишу...");
        Console.ReadKey();
    }
}

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

public class Weapon
{
    public string Name { get; set; }
    public int Damage { get; set; }
    public Weapon(string name, int damage)
    {
        Name = name;
        Damage = damage;
    }
}

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

public class Enemy
{
    public string Name { get; set; }
    public int Hp { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public bool IgnoreArmor { get; set; }
    public bool CanFreeze { get; set; }
    public int FreezeChance { get; set; }
    public int CritChance { get; set; }

    public Enemy(string name, int hp, int attack, int defense, bool ignoreArmor = false, bool canFreeze = false, int freezeChance = 0, int critChance = 0)
    {
        Name = name;
        Hp = hp;
        Attack = attack;
        Defense = defense;
        IgnoreArmor = ignoreArmor;
        CanFreeze = canFreeze;
        FreezeChance = freezeChance;
        CritChance = critChance;
    }

    public void TakeDamage(int damage)
    {
        int actual = Math.Max(0, damage - Defense);
        Hp -= actual;
    }

    public int CalculateAttackDamage(double playerArmor)
    {
        int baseDamage = Attack;
        if (CritChance > 0 && new Random().Next(100) < CritChance)
        {
            Console.WriteLine("КРИТИЧЕСКИЙ УДАР!");
            baseDamage = (int)(baseDamage * 1.5);
        }
        if (IgnoreArmor) return baseDamage;
        return Math.Max(1, baseDamage - (int)(playerArmor / 3));
    }
}
