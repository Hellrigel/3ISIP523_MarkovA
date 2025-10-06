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
    private void Analyze()
    {
        string[] words = SplitWords(OriginalText);
        WordCount = words.Length;

        if (WordCount > 0)
        {
            ShortestWord = words[0];
            LongestWord = words[0];
            for (int i = 1; i < words.Length; i++)
            {
                if (words[i].Length < ShortestWord.Length)
                    ShortestWord = words[i];
                if (words[i].Length > LongestWord.Length)
                    LongestWord = words[i];
            }
        }

        SentenceCount = CountSentences(OriginalText);
        CountLetters(OriginalText);
        CountLetterFrequency(OriginalText);
    }

    private string[] SplitWords(string text)
    {
        List<string> words = new List<string>();
        StringBuilder currentWord = new StringBuilder();

        foreach (char c in text)
        {
            if (char.IsLetter(c))
            {
                currentWord.Append(c);
            }
            else
            {
                if (currentWord.Length > 0)
                {
                    words.Add(currentWord.ToString());
                    currentWord.Clear();
                }
            }
        }

        if (currentWord.Length > 0)
            words.Add(currentWord.ToString());

        return words.ToArray();
    }

    private int CountSentences(string text)
    {
        int count = 0;
        foreach (char c in text)
        {
            if (c == '.' || c == '!' || c == '?')

                count++;
        }
        return count;
    }

    private void CountLetters(string text)
    {
        string vowels = "аеёиоуыэюяaeiouy";
        VowelCount = 0;
        ConsonantCount = 0;

        foreach (char c in text.ToLower())
        {
            if (char.IsLetter(c))
            {
                if (vowels.IndexOf(c) >= 0)
                    VowelCount++;
                else
                    ConsonantCount++;
            }
        }
    }

    private void CountLetterFrequency(string text)
    {
        LetterFrequency = new Dictionary<char, int>();
        foreach (char c in text.ToLower())
        {
            if (char.IsLetter(c))
            {
                if (!LetterFrequency.ContainsKey(c))
                    LetterFrequency[c] = 0;
                LetterFrequency[c]++;
            }
        }
    }

    public void PrintStatistics()
    {
        Console.WriteLine("Статистика текста:");
        Console.WriteLine($"Общее количество слов: {WordCount}");
        Console.WriteLine($"Самое короткое слово: {ShortestWord}");
        Console.WriteLine($"Самое длинное слово: {LongestWord}");
        Console.WriteLine($"Количество предложений: {SentenceCount}");
        Console.WriteLine($"Количество гласных: {VowelCount}");
        Console.WriteLine($"Количество согласных: {ConsonantCount}");
        Console.WriteLine("Частота букв:");

        foreach (var kvp in LetterFrequency)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }
    }
}


