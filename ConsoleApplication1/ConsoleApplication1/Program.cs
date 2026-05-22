using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameInventoryProject
{
    public struct Item
    {
        public string Name { get; set; }
        public int Weight { get; set; }
        public int Cost { get; set; }

        public Item(string name, int weight, int cost)
        {
            Name = name;
            Weight = weight;
            Cost = cost;
        }
    }
    class Program
    {
        const int MaxValue = 100;
        static Item[] IdItem = new Item[MaxValue];
        static int Count = 0;
        

        static void ShowsMenu()
        {
            Console.WriteLine("======== Система инвентаря ========");
            Console.WriteLine("1. Просмотр всех предметов");
            Console.WriteLine("2. Добавить предмет");
            Console.WriteLine("3. Выход");
            Console.WriteLine("Выберите действия");
        }

        static void Main(string[] args)
        {
            Console.Title = "Управление инвентарём";
            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                ShowsMenu();
                string choise = Console.ReadLine();

                switch (choise)
                {
                    case "1":
                        ViewItems();
                        Console.ReadKey(); 
                        break;
                    case "2":
                        AddItem();
                        Console.ReadKey();
                        break;
                    case "3":
                        isRunning = false;
                        Console.Write("Программа завершена");
                        break;
                    default:
                        Console.WriteLine("Вы ввели неправильную команду!");
                        break;
                }

            }

            

        }

        static void ViewItems()
        {
            Console.WriteLine("Список предметов");

            if (Count == 0)
            {
                Console.WriteLine("Список предметов пуст");
                return;
            }

            Console.WriteLine("{0,-10} {1, -10} {2, -10}", "Приедмет", "Вес(г)", "Стоимость");
            Console.WriteLine(new string('-', 45));

            for (int i = 0; i < Count; i++)
            {
                Console.WriteLine("{0,-10} {1, -10} {2, -10}",
                    IdItem[i].Name, IdItem[i].Weight, IdItem[i].Cost);
            }

            Console.WriteLine(new string('-', 45));
            Console.WriteLine($"Количество записей {Count}");
        }

        static void AddItem()
        {
            Console.WriteLine("Добавить предмет");

            if (Count >= MaxValue)
            {
                Console.WriteLine("Ошибка добавленияб, нет места");
                return;
            }


            string newItemName = string.Empty;
            while (true)
            {
                Console.WriteLine("Введите название предмета");
                newItemName = Console.ReadLine();
                if (newItemName != string.Empty)
                {
                    bool isDuplicate = false;
                    for (int i = 0; i < Count; i++)
                    {
                        if (IdItem[i].Name == newItemName)
                        {
                            isDuplicate = true;
                            break;
                        }

                    }

                    if (isDuplicate)
                    {
                        Console.WriteLine("Данный предмет уже существует");
                    }
                    else
                    {
                        break;
                    }
                } Console.WriteLine("Неккоректное значение");
            }

            int weightItem = 0;
            while (true)
            {
                Console.WriteLine("Введите вес(г)");

                if (int.TryParse(Console.ReadLine(), out weightItem))
                {
                    if (weightItem > 0) { break;}
                }
                
                Console.WriteLine("Неверно введенные данные");
                
            }

            int costItem = 0;
            while (true)
            {
                Console.WriteLine("Введите стоимость");

                if (int.TryParse(Console.ReadLine(), out costItem))
                {
                    if (costItem > 0) { break;}
                }
                Console.WriteLine("Неверно введенные данные");

            }
            IdItem[Count] = new Item(newItemName, weightItem, costItem);
            Count++;
            Console.WriteLine("Запись добавлена");
        }
        
    }
}
