using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryApp
{
    internal class Program
    {
        static List<Book> books = new List<Book>();
        static int nextId = 1;

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;


            books.Add(new Book(nextId++, "1984", "Джордж Оруэлл", "Фантастика", 1949, 500));
            books.Add(new Book(nextId++, "Война и мир", "Лев Толстой", "Исторический", 1869, 700));
            books.Add(new Book(nextId++, "Преступление и наказание", "Фёдор Достоевский", "Роман", 1866, 650));
            books.Add(new Book(nextId++, "Шерлок Холмс", "Артур Конан Дойл", "Детектив", 1892, 400));
            books.Add(new Book(nextId++, "Краткая история времени", "Стивен Хокинг", "Научная", 1988, 800));

            while (true)
            {
                Console.WriteLine("\n=== МЕНЮ ===");
                Console.WriteLine("1 - Добавить книгу");
                Console.WriteLine("2 - Удалить книгу по ID");
                Console.WriteLine("3 - Найти книгу");
                Console.WriteLine("4 - Отсортировать книги");
                Console.WriteLine("5 - Самая дорогая и дешёвая книга");
                Console.WriteLine("6 - Группировать книги по авторам");
                Console.WriteLine("7 - Показать все книги");
                Console.WriteLine("0 - Выход");

                Console.Write("Введите команду: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddBook(); break;
                    case "2": RemoveBook(); break;
                    case "3": FindBook(); break;
                    case "4": SortBooks(); break;
                    case "5": ShowMinMax(); break;
                    case "6": GroupByAuthor(); break;
                    case "7": ShowAll(); break;
                    case "0": return;
                    default: Console.WriteLine("Неверный ввод!"); break;
                }
            }
        }

        static void AddBook()
        {
            Console.Write("Введите название книги: ");
            string title = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Ошибка: название не может быть пустым!");
                return;
            }

            Console.Write("Введите автора: ");
            string author = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(author))
            {
                Console.WriteLine("Ошибка: автор не может быть пустым!");
                return;
            }

            Console.WriteLine("Выберите жанр (введите номер):");
            Console.WriteLine("1 - Фантастика");
            Console.WriteLine("2 - Детектив");
            Console.WriteLine("3 - Роман");
            Console.WriteLine("4 - Исторический");
            Console.WriteLine("5 - Научная");
            string genre = "";
            string g = Console.ReadLine();
            switch (g)
            {
                case "1": genre = "Фантастика"; break;
                case "2": genre = "Детектив"; break;
                case "3": genre = "Роман"; break;
                case "4": genre = "Исторический"; break;
                case "5": genre = "Научная"; break;
                default:
                    Console.WriteLine("Ошибка: неверный жанр!");
                    return;
            }

            Console.Write("Введите год издания: ");
            if (!int.TryParse(Console.ReadLine(), out int year))
            {
                Console.WriteLine("Ошибка: неверный год!");
                return;
            }

            Console.Write("Введите цену: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                Console.WriteLine("Ошибка: цена должна быть положительной!");
                return;
            }
            books.Add(new Book(nextId++, title, author, genre, year, price));
            Console.WriteLine("Книга успешно добавлена!");
        }

        static void RemoveBook()
        {
            Console.Write("Введите ID книги для удаления: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var book = books.FirstOrDefault(b => b.Id == id);
                if (book != null)
                {
                    books.Remove(book);
                    Console.WriteLine("Книга удалена!");
                }
                else
                    Console.WriteLine("Книга с таким ID не найдена.");
            }
            else
            {
                Console.WriteLine("Ошибка: некорректный ID!");
            }
        }

        static void FindBook()
        {
            Console.WriteLine("Найти по:");
            Console.WriteLine("1 - Названию");
            Console.WriteLine("2 - Автору");
            Console.WriteLine("3 - Жанру");
            Console.Write("Выберите пункт: ");
            string choice = Console.ReadLine();

            IEnumerable<Book> found = new List<Book>();

            switch (choice)
            {
                case "1":
                    Console.Write("Введите название: ");
                    string title = Console.ReadLine();
                    found = books.Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
                    break;
                case "2":
                    Console.Write("Введите автора: ");
                    string author = Console.ReadLine();
                    found = books.Where(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase));
                    break;
                case "3":
                    Console.Write("Введите жанр: ");
                    string genre = Console.ReadLine();
                    found = books.Where(b => b.Genre.Contains(genre, StringComparison.OrdinalIgnoreCase));
                    break;
                default:
                    Console.WriteLine("Неверный выбор!");
                    return;
            }

            if (found.Any())
                foreach (var b in found) b.Show();
            else
                Console.WriteLine("Ничего не найдено.");
        }

        static void SortBooks()
        {
            Console.WriteLine("1 - По названию");
            Console.WriteLine("2 - По году");
            Console.Write("Выберите способ сортировки: ");
            string choice = Console.ReadLine();

            List<Book> sorted;
            if (choice == "1")
                sorted = books.OrderBy(b => b.Title).ToList();
            else if (choice == "2")
                sorted = books.OrderBy(b => b.Year).ToList();
            else
            {
                Console.WriteLine("Ошибка: неверный выбор!");
                return;
            }

            foreach (var b in sorted) b.Show();
        }

        static void ShowMinMax()
        {
            if (books.Count == 0)
            {
                Console.WriteLine("Список пуст!");
                return;
            }

            var min = books.OrderBy(b => b.Price).First();
            var max = books.OrderByDescending(b => b.Price).First();

            Console.WriteLine("\nСамая дешёвая книга:");
            min.Show();

            Console.WriteLine("Самая дорогая книга:");
            max.Show();
        }

        static void GroupByAuthor()
        {
            var groups = books.GroupBy(b => b.Author)
                              .Select(g => new { Автор = g.Key, Количество = g.Count() });

            Console.WriteLine("\nКоличество книг по авторам:");
            foreach (var g in groups)
            {
                Console.WriteLine($"{g.Автор}: {g.Количество}");
            }
        }

        static void ShowAll()
        {
            if (books.Count == 0)
                Console.WriteLine("Список книг пуст!");
            else
                foreach (var b in books) b.Show();
        }
    }
    class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }

        public Book(int id, string title, string author, string genre, int year, decimal price)
        {
            Id = id;
            Title = title;
            Author = author;
            Genre = genre;
            Year = year;
            Price = price;
        }

        public void Show()
        {
            Console.WriteLine($"\nID: {Id}");
            Console.WriteLine($"Название: {Title}");
            Console.WriteLine($"Автор: {Author}");
            Console.WriteLine($"Жанр: {Genre}");
            Console.WriteLine($"Год: {Year}");
            Console.WriteLine($"Цена: {Price} руб.");
        }
    }
}
