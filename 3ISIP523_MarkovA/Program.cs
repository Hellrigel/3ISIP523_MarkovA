using System;

class Program
{
    static Character player; // Вынес игрока в поле класса, чтобы был доступен во всех методах

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
                    Console.WriteLine("Неверный ввод!");
                    break;
            }
        }
    }

    static void StartGame()
    {
        Console.Write("Введите имя вашего персонажа: ");
        string playerName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(playerName))
        {
            playerName = "ПУТЕШЕСТВЕННИК";
            Console.WriteLine("Имя не может быть пустым. Установлено имя по умолчанию: ПУТЕШЕСТВЕННИК");
        }
        else
        {
            playerName = playerName.Trim().ToUpper();
        }

        Weapon longbow = new Weapon("ЛУК ДЛИННЫЙ", 17);
        Armor woodenArmor = new Armor("ДЕРЕВЯННАЯ БРОНЯ", 20);
        player = new Character(playerName, 100, longbow, woodenArmor);

        Console.WriteLine($"\n=== ДОБРО ПОЖАЛОВАТЬ, {player.Name}! ===");
        Console.WriteLine($"Здоровье: {player.Hp} HP");
        Console.WriteLine($"Оружие: {player.CurrentWeapon.Name} ({player.CurrentWeapon.Damage} урона)");
        Console.WriteLine($"Броня: {player.CurrentArmor.Name} ({player.CurrentArmor.Defense} защиты)");

        CampMenu();
    }

    static void CampMenu()
    {
        bool inCamp = true;

        while (inCamp && player.Hp > 0)
        {
            Console.WriteLine($"\n=== ДОБРО ПОЖАЛОВАТЬ В ЛАГЕРЬ, {player.Name}! ===");
            Console.WriteLine("1 - ПОСМОТРЕТЬ ИНВЕНТАРЬ");
            Console.WriteLine("2 - ПОСПАТЬ У КОСТРА");
            Console.WriteLine("3 - ОТПРАВИТЬСЯ В ПУТЬ");
            Console.WriteLine("4 - ВЕРНУТЬСЯ В ГЛАВНОЕ МЕНЮ");

            Console.Write("Введите цифру: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": ShowInventory(); break;
                case "2": Sleep(); break;
                case "3": GoAdventure(); break;
                case "4": inCamp = false; break;
                default: Console.WriteLine("Неверный ввод!"); break;
            }
        }
    }

    static void ShowInventory()
    {
        Console.WriteLine("\n=== ВАШ ИНВЕНТАРЬ ===");
        Console.WriteLine($"Здоровье: {player.Hp} HP");
        Console.WriteLine($"Оружие: {player.CurrentWeapon.Name} ({player.CurrentWeapon.Damage} урона)");
        Console.WriteLine($"Броня: {player.CurrentArmor.Name} ({player.CurrentArmor.Defense} защиты)");
        Console.WriteLine("\nНажмите любую клавишу для продолжения...");
        Console.ReadKey();
    }

    static void Sleep()
    {
        int healed = 30;
        player.Hp = Math.Min(100, player.Hp + healed); // Не даем здоровью превысить максимум
        Console.WriteLine($"\nВы хорошо выспались у костра и восстановили {healed} HP!");
        Console.WriteLine($"Теперь у вас {player.Hp} HP");
        Console.WriteLine("Нажмите любую клавишу для продолжения...");
        Console.ReadKey();
    }

    static void GoAdventure()
    {
        Console.WriteLine("\n=== ВЫ ОТПРАВЛЯЕТЕСЬ В ПУТЬ ===");
        Console.WriteLine("Вы покидаете безопасный лагерь и отправляетесь в темные пещеры...");

        // Здесь будет логика приключений
        Random random = new Random();
        string[] events = {
            "Вы встретили странного торговца...",
            "Впереди слышны странные звуки...",
            "Вы нашли древние письмена на стене...",
            "В темноте что-то шевелится..."
        };

        string randomEvent = events[random.Next(events.Length)];
        Console.WriteLine(randomEvent);

        Console.WriteLine("\nНажмите любую клавишу для продолжения...");
        Console.ReadKey();

        // Простая битва для демонстрации
        SimpleBattle();
    }

    static void SimpleBattle()
    {
        Console.WriteLine("\n=== ВНЕЗАПНАЯ АТАКА ===");
        Console.WriteLine("Из темноты выпрыгивает скелет!");

        int skeletonHp = 30;
        int skeletonDamage = 8;

        while (skeletonHp > 0 && player.Hp > 0)
        {
            Console.WriteLine($"\nВаше HP: {player.Hp} | HP скелета: {skeletonHp}");
            Console.WriteLine("1 - АТАКОВАТЬ");
            Console.WriteLine("2 - ПОПРОБОВАТЬ УБЕЖАТЬ");
            Console.Write("Выберите действие: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    int playerDamage = player.CurrentWeapon.Damage;
                    skeletonHp -= playerDamage;
                    Console.WriteLine($"Вы атаковали скелета и нанесли {playerDamage} урона!");
                    break;

                case "2":
                    Random random = new Random();
                    if (random.Next(2) == 0)
                    {
                        Console.WriteLine("Вам удалось сбежать обратно в лагерь!");
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Вам не удалось сбежать!");
                    }
                    break;

                default:
                    Console.WriteLine("Неверный ввод! Вы пропускаете ход.");
                    break;
            }

            // Ход скелета
            if (skeletonHp > 0)
            {
                int actualDamage = Math.Max(1, skeletonDamage - (int)player.CurrentArmor.Defense);
                player.Hp -= actualDamage;
                Console.WriteLine($"Скелет атаковал вас и нанес {actualDamage} урона!");
            }
        }

        if (skeletonHp <= 0)
        {
            Console.WriteLine("\n✅ Вы победили скелета!");
            Console.WriteLine("Вы возвращаетесь в лагерь для отдыха...");
        }
        else if (player.Hp <= 0)
        {
            Console.WriteLine("\n💀 Вы пали в бою...");
        }

        Console.WriteLine("Нажмите любую клавишу для продолжения...");
        Console.ReadKey();
    }

    static void ShowCredits()
    {
        Console.WriteLine("\n=== СОЗДАТЕЛИ ===");
        Console.WriteLine("Разработчик: Hellrigel");
        Console.WriteLine("Художник: Hellrigel");
        Console.WriteLine("Тестировщик: Hellrigel");
        Console.WriteLine("Нажмите любую клавишу для продолжения...");
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