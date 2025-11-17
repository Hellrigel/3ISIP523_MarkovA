using System;
using System.Collections.Generic;

namespace AutoService
{
    class Program
    {
        static AutoServiceApp app;
        static void Main(string[] args)
        {
            app = new AutoServiceApp();
            app.Run();
        }
    }

    class AutoServiceApp
    {
        List<Client> clients = new List<Client>();
        List<Car> cars = new List<Car>();
        List<Service> services = new List<Service>();
        List<Transaction> transactions = new List<Transaction>();

        public void Run()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("=== АВТОСЕРВИС ===");
                Console.WriteLine("1. Клиенты");
                Console.WriteLine("2. Машины");
                Console.WriteLine("3. Услуги");
                Console.WriteLine("4. Транзакции");
                Console.WriteLine("5. Выход");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": ClientsMenu(); break;
                    case "2": CarsMenu(); break;
                    case "3": ServicesMenu(); break;
                    case "4": TransactionsMenu(); break;
                    case "5": running = false; break;
                }
            }
        }

        void ClientsMenu()
        {
            bool open = true;
            while (open)
            {
                Console.Clear();
                Console.WriteLine("=== КЛИЕНТЫ ===");
                Console.WriteLine("1. Список");
                Console.WriteLine("2. Добавить");
                Console.WriteLine("3. Назад");
                string c = Console.ReadLine();
                if (c == "1")
                {
                    Console.Clear();
                    foreach (var x in clients) Console.WriteLine($"{x.Id}. {x.Name}");
                    Console.ReadKey();
                }
                else if (c == "2")
                {
                    Console.Clear();
                    Console.WriteLine("Имя:");
                    string name = Console.ReadLine();
                    clients.Add(new Client { Id = clients.Count + 1, Name = name });
                }
                else open = false;
            }
        }

        void CarsMenu()
        {
            bool open = true;
            while (open)
            {
                Console.Clear();
                Console.WriteLine("=== МАШИНЫ ===");
                Console.WriteLine("1. Список");
                Console.WriteLine("2. Добавить");
                Console.WriteLine("3. Назад");
                string c = Console.ReadLine();
                if (c == "1")
                {
                    Console.Clear();
                    foreach (var x in cars) Console.WriteLine($"{x.Id}. {x.Model} (клиент {x.ClientId})");
                    Console.ReadKey();
                }
                else if (c == "2")
                {
                    Console.Clear();
                    Console.WriteLine("Модель:");
                    string model = Console.ReadLine();
                    Console.WriteLine("ID клиента:");
                    int cid = int.Parse(Console.ReadLine());
                    cars.Add(new Car { Id = cars.Count + 1, Model = model, ClientId = cid });
                }
                else open = false;
            }
        }

        void ServicesMenu()
        {
            bool open = true;
            while (open)
            {
                Console.Clear();
                Console.WriteLine("=== УСЛУГИ ===");
                Console.WriteLine("1. Список");
                Console.WriteLine("2. Добавить");
                Console.WriteLine("3. Назад");
                string c = Console.ReadLine();
                if (c == "1")
                {
                    Console.Clear();
                    foreach (var x in services) Console.WriteLine($"{x.Id}. {x.Title} - {x.Price}р");
                    Console.ReadKey();
                }
                else if (c == "2")
                {
                    Console.Clear();
                    Console.WriteLine("Название:");
                    string t = Console.ReadLine();
                    Console.WriteLine("Цена:");
                    decimal p = decimal.Parse(Console.ReadLine());
                    services.Add(new Service { Id = services.Count + 1, Title = t, Price = p });
                }
                else open = false;
            }
        }

        void TransactionsMenu()
        {
            bool open = true;
            while (open)
            {
                Console.Clear();
                Console.WriteLine("=== ТРАНЗАКЦИИ ===");
                Console.WriteLine("1. Список");
                Console.WriteLine("2. Добавить");
                Console.WriteLine("3. Назад");
                string c = Console.ReadLine();
                if (c == "1")
                {
                    Console.Clear();
                    foreach (var x in transactions) Console.WriteLine($"{x.Id}. Машина {x.CarId}, Услуга {x.ServiceId}, Сумма {x.Amount}");
                    Console.ReadKey();
                }
                else if (c == "2")
                {
                    Console.Clear();
                    Console.WriteLine("ID машины:");
                    int car = int.Parse(Console.ReadLine());
                    Console.WriteLine("ID услуги:");
                    int serv = int.Parse(Console.ReadLine());
                    var s = services.Find(z => z.Id == serv);
                    transactions.Add(new Transaction { Id = transactions.Count + 1, CarId = car, ServiceId = serv, Amount = s.Price });
                }
                else open = false;
            }
        }
    }

    class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    class Car
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public int ClientId { get; set; }
    }

    class Service
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
    }

    class Transaction
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int ServiceId { get; set; }
        public decimal Amount { get; set; }
    }
}
