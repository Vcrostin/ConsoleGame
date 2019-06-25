using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGame1.Logic.Model;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace ConsoleGame1.Logic.Controller
{
    public class GameController
    {
        private static List<Item> ItemsCollections { get; set; }

        /// <summary>
        /// Назначение клавиш при движении объекта.
        /// </summary>
        /// <param name="A"> Объект. </param>
        public static void KeyAssignment(PlayInterface A)
        {
            ConsoleKeyInfo cki;
            do
            {
                cki = Console.ReadKey(true);
                if (((cki.Modifiers & ConsoleModifiers.Alt) != 0) && (cki.Key == ConsoleKey.E))
                {
                    PressAltAndE(A);
                }
                else
                {
                    switch (cki.Key)
                    {
                        case ConsoleKey.UpArrow:
                            MoveUp(A);
                            break;
                        case ConsoleKey.DownArrow:
                            MoveDown(A);
                            break;
                        case ConsoleKey.LeftArrow:
                            MoveLeft(A);
                            break;
                        case ConsoleKey.RightArrow:
                            MoveRight(A);
                            break;
                        case ConsoleKey.E:
                            CallInventory(A);
                            break;
                    }
                }
            } while (cki.Key != ConsoleKey.Escape);
        }

        /// <summary>
        /// Перемещение вверх.
        /// </summary>
        /// <param name="A"> Объект перемещения. </param>
        private static void MoveUp(PlayInterface A)
        {
            if (A.YBySpace - 1 > 1)
            {
                A.Clear();
                A.PositionSet(A.XBySpace, A.YBySpace - 1);
            }
        }

        /// <summary>
        /// Перемещение вниз.
        /// </summary>
        /// <param name="A"> Объект перемещения. </param>
        private static void MoveDown(PlayInterface A)
        {
            if (A.YBySpace + 1 < Console.WindowHeight - 1)
            {
                A.Clear();
                A.PositionSet(A.XBySpace, A.YBySpace + 1);
            }
        }

        /// <summary>
        /// Перемещение влево.
        /// </summary>
        /// <param name="A"> Объект перемещения. </param>
        private static void MoveLeft(PlayInterface A)
        {
            if (A.XBySpace - 1 > 2)
            {
                A.Clear();
                A.PositionSet(A.XBySpace - 1, A.YBySpace);
            }
        }

        /// <summary>
        /// Перемещение вправо.
        /// </summary>
        /// <param name="A"> Объект перемещения. </param>
        private static void MoveRight(PlayInterface A)
        {
            if (A.XBySpace + 1 < Console.WindowWidth - 3)
            {
                A.Clear();
                A.PositionSet(A.XBySpace + 1, A.YBySpace);
            }
        }

        /// <summary>
        /// Вызов инвентаря (по умолчанию кл E).
        /// </summary>
        /// <param name="A"> Объект вызова. </param>
        private static void CallInventory(PlayInterface A)
        {
            Console.Clear();
            ItemsCollections = LoadData();
            Element[,] ListElement = new Element[0, 0];
            foreach (var s in ItemsCollections)
            {
                ListElement = TheLastElement(ListElement, s);
            }
            Element[,] AddListElement = new Element[ListElement.GetLength(0) + 2, ListElement.GetLength(1)];
            for (int i = 0; i < ListElement.GetLength(0); i++)
            {
                for (int j = 0; j < ListElement.GetLength(1); j++)
                {
                    AddListElement[i, j] = ListElement[i, j];
                }
            }
            for (int i = 0; i < ListElement.GetLength(1) - 1; i++)
            {
                AddListElement[ListElement.GetLength(0), i] = new Element(" ");
            }
            AddListElement[ListElement.GetLength(0), ListElement.GetLength(1) - 1] = new Element("Выход");

            AddListElement[ListElement.GetLength(0) + 1,0] = new Element("Balance");
            AddListElement[ListElement.GetLength(0) + 1, 1] = new Element(UserController.ReturnCurentUser(UserController.CurentUserName).Balance.ToString());
            for(int i = 2; i < ListElement.GetLength(1); i++)
            {
                AddListElement[ListElement.GetLength(0) + 1, i] = new Element(" ");
            }
            ListElement = AddListElement;
            ChoseMenu ListOfItems = new ChoseMenu(ListElement)
            {
                IndexX = Item.Number - 1
            };
            ListOfItems.Elements[0, 0].IsSelected = false;
            ListOfItems.Elements[0, Item.Number - 1].IsSelected = true;
            ListOfItems.MenuButtonSet();
            while (ListOfItems.IndexY < ItemsCollections.Count)
            {
                if (ListOfItems.IndexX == Item.Number - 1)
                {
                    if (UserController.ReturnCurentUser(UserController.CurentUserName).Count[ListOfItems.IndexY] > 0)
                    {
                        UserController.ReturnCurentUser(UserController.CurentUserName).Count[ListOfItems.IndexY]--;
                        ListOfItems.Elements[ListOfItems.IndexY, 0].Text = (int.Parse(ListOfItems.Elements[ListOfItems.IndexY, 0].Text) - 1).ToString();
                        UserController.ReturnCurentUser(UserController.CurentUserName).Balance += int.Parse(ListOfItems.Elements[ListOfItems.IndexY, 3].Text);
                    }
                }
                else
                {
                    if (UserController.ReturnCurentUser(UserController.CurentUserName).Balance >= int.Parse(ListOfItems.Elements[ListOfItems.IndexY, 3].Text))
                    {
                        UserController.ReturnCurentUser(UserController.CurentUserName).Count[ListOfItems.IndexY]++;
                        ListOfItems.Elements[ListOfItems.IndexY, 0].Text = (int.Parse(ListOfItems.Elements[ListOfItems.IndexY, 0].Text) + 1).ToString();
                        UserController.ReturnCurentUser(UserController.CurentUserName).Balance -= int.Parse(ListOfItems.Elements[ListOfItems.IndexY, 3].Text);
                    }
                }
                UserController.SaveData();
                ListOfItems.Elements[ListElement.GetLength(0) - 1, 1] = new Element(UserController.ReturnCurentUser(UserController.CurentUserName).Balance.ToString());
                ListOfItems.MenuButtonSet();
            } 
            A.CreateBorder();
            A.Draw();
        }

        /// <summary>
        /// Доп метод для реализации инвентаря.
        /// </summary>
        /// <param name="Listing"> Массив куда нужно внести объекты (фактически создается новый массив)</param>
        /// <param name="s"> Элемент класса Item которого нужно разделить на составляющие чтобы добавить эти составляющие в массив по x увечив его по y </param>
        /// <returns> Возращает новый массив дополненный элементами из Item s</returns>
        private static Element[,] TheLastElement(Element[,] Listing,Item s)
        {
            Element[,] newElement = new Element[Listing.GetLength(0) + 1, Item.Number];
            for(int i = 0; i < Listing.GetLength(0); i++)
            {
                for(int j = 0; j < Listing.GetLength(1); j++)
                {
                    newElement[i, j] = Listing[i, j];
                }
            }
            for (int j = 0; j < Item.Number; j++)
            {
                newElement[Listing.GetLength(0), j] = new Element(s[j]);
            }
            return newElement;
        }

        /// <summary>
        /// Загрузка данных содержащих информацию о возможных объектах.
        /// </summary>
        /// <returns> Возращает ссылку на коллекцию.</returns>
        public static List<Item> LoadData()
        {
            var formatter = new BinaryFormatter();
            using (var fs = new FileStream("ItemsCollections.dat", FileMode.OpenOrCreate))
            {
                if (fs.Length > 0 && formatter.Deserialize(fs) is List<Item> items)
                {
                    return items;
                }
                else
                {
                    return new List<Item>();
                }
            }
        }

        /// <summary>
        /// Сохранение данных коллекции ItemsCollections.
        /// </summary>
        public static void SaveData()
        {
            var formatter = new BinaryFormatter();
            using (var fs = new FileStream("ItemsCollections.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, ItemsCollections);
            }
        }

        /// <summary>
        /// Команды выполняемые при удержании альт и е (т.е. добавление элементов в коллекцию вручную)
        /// </summary>
        /// <param name="A"> Ссылка на интерфейс объекта.ы</param>
        public static void PressAltAndE(PlayInterface A)
        {
            ItemsCollections = LoadData();
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Создание нового элемента инвентаря");
            Console.WriteLine("Введите имя:");
            Console.CursorVisible = true;
            string name = Console.ReadLine();
            Console.WriteLine("Введите цену:");
            double price = double.Parse(Console.ReadLine());
            Console.WriteLine("Введите описание:");
            string Describe = Console.ReadLine();
            int ID = ItemsCollections.Count + 1;
            ItemsCollections.Add(new Item(name, price, Describe, ID));
            if (UserController.ReturnCurentUser(UserController.CurentUserName).Count == null)
            {
                UserController.ReturnCurentUser(UserController.CurentUserName).Count = new int[1];
            }
            else
            {
                int[] a = new int[UserController.ReturnCurentUser(UserController.CurentUserName).Count.Length + 1];
                int i = 0;
                foreach (var s in UserController.ReturnCurentUser(UserController.CurentUserName).Count)
                {
                    a[i++] = s;
                }
                UserController.ReturnCurentUser(UserController.CurentUserName).Count = a;
            }
            SaveData();
            UserController.SaveData();
            Console.CursorVisible = false;
            Console.WriteLine("Элемент успешно создан. Для продолжения нажмите любую клавишу.");
            Console.ReadKey();
            A.CreateBorder();
            A.Draw();
        }
    }
}
