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
            List<Item> items = new List<Item>(MaxValue); 
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
                        ViewItems(items);
                        Console.ReadKey(); 
                        break;
                    case "2":
                        AddItem(items);
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

        static void PrintTable(List<Item> items)
        {
            

            if (items.Count == 0)
            {
                Console.WriteLine("Список предметов пуст");
                return;
            }

            Console.WriteLine("{0,-10} {1, -10} {2, -10}", "Приедмет", "Вес(г)", "Стоимость");
            Console.WriteLine(new string('-', 45));

            foreach (var item in items)
            {
                Console.WriteLine("{0,-10} {1, -10} {2, -10}",
                    item.Name, item.Weight, item.Cost);
            }

            Console.WriteLine(new string('-', 45));
            Console.WriteLine($"Количество записей {items.Count}");
        }

        static void ViewItems(List<Item> items)
        {
            Console.Clear ();
            Console.WriteLine("Список предметов");

            if (items.Count == 0)
            {
                Console.WriteLine("Список предметов пуст");
                return;
            }

            PrintTable(items);

            ShowInventoryMenu();
            bool isRunning = true;
            while (isRunning)
            {
                string choise = Console.ReadLine();
                switch(choise)
                {
                    case "1":
                        ItemDelete(items);
                        isRunning = false;
                        break;
                    case "2":
                        EditItem(items);
                        isRunning=false; 
                        break;
                    case "3":
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Вы ввели неправильную команду!");
                        break;
                }
            }
        }

        static void AddItem(List<Item> newItem)
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
                    foreach (var item in newItem)
                    {
                        if (item.Name == newItemName)
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
            newItem.Add(new Item(newItemName, weightItem, costItem));
            
            Console.WriteLine("Запись добавлена");
        }

        static void ShowInventoryMenu()
        {
            Console.WriteLine("\n======== Управение инвентарем ========");
            Console.WriteLine("1. Удалить предмет");
            Console.WriteLine("2. Редактирование предмета");
            Console.WriteLine("3. Выход из инвентаря");
            Console.WriteLine("Выберите действия");
        }

        static void ItemDelete(List<Item> item)
        {
            Console.WriteLine("Введите название предмета:");
            string nameItem = Console.ReadLine();
            int index = item.FindIndex(i => i.Name == nameItem);
            if (index != -1)
            {
                item.RemoveAt(index);
                Console.WriteLine($"Предмет успешно удален");
                
            }
            else { Console.WriteLine("Предмет не найден"); }
        }

        static void EditItem(List<Item> item)
        {
            Console.WriteLine("Введите название предмета: ");
            string searchName = Console.ReadLine();
            int index = item.FindIndex(i =>i.Name == searchName);

            if (index == -1)
            {
                Console.WriteLine("Предмет не найден");
                return;
            }

            Item currentItem = item[index];
            Console.WriteLine($"Редактирование предмета: {currentItem.Name} {currentItem.Weight} {currentItem.Cost}");

            //Изменение названия
            string newName = string.Empty;
            while(true)
            {
                Console.WriteLine("Введите новое название (Enter - для пропуска)");
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    newName = currentItem.Name;
                    break;
                }
                if (input != currentItem.Name && item.Any(i => i.Name == input))
                {
                    Console.WriteLine("Предмет с таким названием существует");
                }
                else
                {
                    newName = input;
                    break;
                }
            }

            //Изменение веса
            int newWeight = 0;
            while (true)
            {
                Console.WriteLine("Введите новый вес(г) (Enter - для пропуска)");
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    newWeight = currentItem.Weight;
                    break;
                }
                if (int.TryParse(input, out newWeight) && newWeight > 0)
                {
                    break;
                }
                Console.WriteLine("Неккоректный ввод (Должно быть число и больше нуля)");
            }

            //Изменеине стоимости
            int newCost = 0;
            while (true)
            {
                Console.WriteLine("Введите новою стоимость (Enter - для пропуска)");
                string input = Console.ReadLine() ;

                if (string.IsNullOrEmpty (input))
                {
                    newCost = currentItem.Cost;
                    break;
                }
                if (int.TryParse(input, out newCost) && newCost > 0)
                {
                    break;
                }
                Console.WriteLine("Неккоректный ввод (Должно быть число и больше нуля)");
            }

            item[index] = new Item(newName, newWeight, newCost);
            Console.WriteLine("Запись успешна изменина");
        }
        
        
    }
}
