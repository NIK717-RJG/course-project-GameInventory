using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameInventoryProject
{
    class Program
    {
        const int MaxValue = 100;
        static string[] NameItem = new string[MaxValue];
        static int[] WeightItem = new int[MaxValue];
        static int[] CostItem = new int[MaxValue];
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
                    NameItem[i], WeightItem[i], CostItem[i]);
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
                
                bool isDuplicate = false;
                for (int i = 0; i < Count; i++)
                {
                    if (NameItem[i] == newItemName)
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
            
            

            NameItem[Count] = newItemName;
            WeightItem[Count] = weightItem;
            CostItem[Count] = costItem;
            Count++;
            Console.WriteLine("Запись добавлена");
        }
        
    }
}
