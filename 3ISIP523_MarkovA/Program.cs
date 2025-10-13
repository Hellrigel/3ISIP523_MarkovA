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
                Console.Write("Введите циферку: ");
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
        