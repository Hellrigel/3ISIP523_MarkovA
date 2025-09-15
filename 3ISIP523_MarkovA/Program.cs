class Program
{
    static void Main()
    {
        System.Console.Write("Количество операций (2-40): ");
        int count = int.Parse(System.Console.ReadLine());

        string[] names = new string[count];
        decimal[] amounts = new decimal[count];

        for (int i = 0; i < count; i++)
        {
            System.Console.Write($"Операция {i + 1}: ");
            string input = System.Console.ReadLine();
            int separator = input.IndexOf(';');
            names[i] = input.Substring(0, separator).Trim();
            amounts[i] = decimal.Parse(input.Substring(separator + 1).Trim());
        }

        while (true)
        {
            System.Console.WriteLine("\n1. Вывод\n2. Статистика\n3. Сортировка\n4. Конвертация\n5. Поиск\n0. Выход");
            System.Console.Write("Выбор: ");
            string choice = System.Console.ReadLine();

            if (choice == "0") break;
            if (choice == "1") ShowData(names, amounts);
            if (choice == "2") ShowStats(amounts);
            if (choice == "3") BubbleSort(names, amounts);
            if (choice == "4") ConvertCurrency(names, amounts);
            if (choice == "5") SearchByName(names, amounts);
        }
    }

    static void ShowData(string[] names, decimal[] amounts)
    {
        for (int i = 0; i < names.Length; i++)
            System.Console.WriteLine($"{names[i]} - {amounts[i]} руб");
    }

    static void ShowStats(decimal[] amounts)
    {
        decimal sum = 0, max = amounts[0], min = amounts[0];
        for (int i = 0; i < amounts.Length; i++)
        {
            sum += amounts[i];
            if (amounts[i] > max) max = amounts[i];
            if (amounts[i] < min) min = amounts[i];
        }
        decimal avg = sum / amounts.Length;

        System.Console.WriteLine($"Сумма: {sum} руб");
        System.Console.WriteLine($"Среднее: {avg} руб");
        System.Console.WriteLine($"Макс: {max} руб");
        System.Console.WriteLine($"Мин: {min} руб");
    }

    static void BubbleSort(string[] names, decimal[] amounts)
    {
        for (int i = 0; i < amounts.Length - 1; i++)
        {
            for (int j = 0; j < amounts.Length - i - 1; j++)
            {
                if (amounts[j] > amounts[j + 1])
                {
                    decimal tempAmount = amounts[j];
                    amounts[j] = amounts[j + 1];
                    amounts[j + 1] = tempAmount;

                    string tempName = names[j];
                    names[j] = names[j + 1];
                    names[j + 1] = tempName;
                }
            }
        }
        System.Console.WriteLine("Отсортировано!");
    }

    static void ConvertCurrency(string[] names, decimal[] amounts)
    {
        System.Console.Write("Курс (рубль к валюте): ");
        decimal rate = decimal.Parse(System.Console.ReadLine());
        System.Console.Write("Символ валюты: ");
        string symbol = System.Console.ReadLine();

        for (int i = 0; i < names.Length; i++)
            System.Console.WriteLine($"{names[i]} - {amounts[i] * rate} {symbol}");
    }

    static void SearchByName(string[] names, decimal[] amounts)
    {
        System.Console.Write("Поиск: ");
        string term = System.Console.ReadLine().ToLower();

        bool found = false;
        for (int i = 0; i < names.Length; i++)
        {
            if (names[i].ToLower().Contains(term))
            {
                System.Console.WriteLine($"{names[i]} - {amounts[i]} руб");
                found = true;
            }
        }
        if (!found) System.Console.WriteLine("Не найдено");
    }
}