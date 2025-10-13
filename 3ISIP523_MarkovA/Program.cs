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

                