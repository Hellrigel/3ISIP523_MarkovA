using System;
using System.Collections.Generic;
using System.Text;

class TextStatistics
{
    public string OriginalText { get; private set; }
    public int WordCount { get; private set; }
    public string ShortestWord { get; private set; }
    public string LongestWord { get; private set; }
    public int SentenceCount { get; private set; }
    public int VowelCount { get; private set; }
    public int ConsonantCount { get; private set; }
    public Dictionary<char, int> LetterFrequency { get; private set; }

    public TextStatistics(string text)
    {
        OriginalText = text;
        Analyze();
    }
    class Program
    {
        static void Main()
        {
            List<TextStatistics> history = new List<TextStatistics>();
            bool running = true;

            while (running)
            {
                Console.WriteLine("Введите текст (минимум 100 символов):");
                string input = Console.ReadLine();
                if (input.Length < 100)
                {
                    Console.WriteLine("Текст слишком короткий! Повторите ввод.");
                    continue;
                }

                TextStatistics stats = new TextStatistics(input);
                history.Add(stats);
                stats.PrintStatistics();

                Console.WriteLine("\nХотите ввести новый текст? (y)");
                string answer = Console.ReadLine().ToLower();

                if (answer != "y")
                    running = false;
            }

            Console.WriteLine("\nИстория статистики:");
            int index = 1;
            foreach (var stat in history)
            {
                Console.WriteLine($"\nТекст {index}:");
                stat.PrintStatistics();
                index++;
            }
        }
    }
    


