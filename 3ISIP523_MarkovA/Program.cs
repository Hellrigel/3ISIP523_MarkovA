using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopInventory
{
    enum Category
    {
        Electronics = 1,
        Food,
        Clothes
    }

    class Product
    {
        private static int _lastId = 1000;
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Category Category { get; set; }
        public bool InStock => Quantity > 0;

        public Product(string name, decimal price, int quantity, Category category)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Название товара не может быть пустым");
            if (price <= 0)
                throw new ArgumentException("Цена должна быть больше нуля");
            if (quantity < 0)
                throw new ArgumentException("Количество не может быть отрицательным");

            Code = GenerateCode();
            Name = name;
            Price = price;
            Quantity = quantity;
            Category = category;
        }

        private static string GenerateCode()
        {
            _lastId++;
            return "1" + _lastId.ToString();
        }

        public override string ToString()
        {
            return $"Код: {Code}\nНазвание: {Name}\nЦена: {Price} руб.\n" +
                   $"Количество: {Quantity}\nОстаток на складе: {(InStock ? "Да" : "Нет")}\n" +
                   $"Категория: {Category}\n";
        }
    }

    class Program
    {
        static List<Product> products = new List<Product>();

        static void Main()
        {

            products.Add(new Product("Хлеб", 50, 20, Category.Food));
            products.Add(new Product("Молоко", 70, 15, Category.Food));
            products.Add(new Product("Футболка", 1200, 5, Category.Clothes));
            products.Add(new Product("Смартфон", 25000, 2, Category.Electronics));
            products.Add(new Product("Ноутбук", 65000, 1, Category.Electronics));

            while (true)
            {
                Console.WriteLine("\n МЕНЮ :)");
                Console.WriteLine("1. Добавить товар +");
                Console.WriteLine("2. Удалить товар -");
                Console.WriteLine("3. Заказать поставку ===");
                Console.WriteLine("4. Продать товар $$$");
                Console.WriteLine("5. Поиск товара 8");
                Console.WriteLine("6. Показать все товары ]]]");
                Console.WriteLine("0. Выход ---");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddProduct(); break;
                    case "2": RemoveProduct(); break;
                    case "3": OrderSupply(); break;
                    case "4": SellProduct(); break;
                    case "5": SearchProduct(); break;
                    case "6": ShowAllProducts(); break;
                    case "0": return;
                    default: Console.WriteLine("Неверный выбор."); break;
                }
            }
        }

        static void AddProduct()
        {
            try
            {
                Console.Write("Введите название: ");
                string name = Console.ReadLine();

                Console.Write("Введите цену: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal price) || price <= 0)
                {
                    Console.WriteLine("Ошибка: цена должна быть положительным числом.");
                    return;
                }

                Console.Write("Введите количество: ");
                if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity < 0)
                {
                    Console.WriteLine("Ошибка: количество должно быть неотрицательным.");
                    return;
                }

                Console.WriteLine("Выберите категорию: 1 - Электроника, 2 - Еда, 3 - Одежда");
                if (!int.TryParse(Console.ReadLine(), out int cat) || !Enum.IsDefined(typeof(Category), cat))
                {
                    Console.WriteLine("Ошибка: неверная категория.");
                    return;
                }

                products.Add(new Product(name, price, quantity, (Category)cat));
                Console.WriteLine("Товар успешно добавлен.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }

        static void RemoveProduct()
        {
            Console.Write("Введите код товара для удаления: ");
            string code = Console.ReadLine();
            var product = products.FirstOrDefault(p => p.Code == code);

            if (product == null)
                Console.WriteLine("Товар не найден.");
            else
            {
                products.Remove(product);
                Console.WriteLine("Товар удален.");
            }
        }

        