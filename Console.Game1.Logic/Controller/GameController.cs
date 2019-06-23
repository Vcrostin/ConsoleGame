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
                    ItemsCollections.Add(new Item(name, price, Describe));
                    SaveData();
                    Console.CursorVisible = false;
                    Console.WriteLine("Элемент успешно создан. Для продолжения нажмите любую клавишу.");
                    Console.ReadKey();
                    A.CreateBorder();
                    A.Draw();
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
            if (A.Y - 1 > 1)
            {
                A.Clear();
                A.PositionSet(A.X, A.Y - 1);
            }
        }
        /// <summary>
        /// Перемещение вниз.
        /// </summary>
        /// <param name="A"> Объект перемещения. </param>
        private static void MoveDown(PlayInterface A)
        {
            if (A.Y + 1 < Console.WindowHeight - 1)
            {
                A.Clear();
                A.PositionSet(A.X, A.Y + 1);
            }
        }
        /// <summary>
        /// Перемещение влево.
        /// </summary>
        /// <param name="A"> Объект перемещения. </param>
        private static void MoveLeft(PlayInterface A)
        {
            if (A.X - 1 > 2)
            {
                A.Clear();
                A.PositionSet(A.X - 1, A.Y);
            }
        }
        /// <summary>
        /// Перемещение вправо.
        /// </summary>
        /// <param name="A"> Объект перемещения. </param>
        private static void MoveRight(PlayInterface A)
        {
            if (A.X + 1 < Console.WindowWidth - 3)
            {
                A.Clear();
                A.PositionSet(A.X + 1, A.Y);
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
            Element[,] ListElement = null;
            foreach(var s in ItemsCollections)
            {
                if (ListElement == null)
                {
                    ListElement = new Element[0, 0];
                }

                ListElement = TheLastElement(ListElement, s);
            }
            ChoseMenu ListOfItems = new ChoseMenu(ListElement);
            ListOfItems.MenuButtonSet();

        }
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
            for(int j = 0; j < Item.Number; j++)
            {
                newElement[Listing.GetLength(0), j] = new Element(s[j]);
            }
            return newElement;
        }

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
        public static void SaveData()
        {
            var formatter = new BinaryFormatter();
            using (var fs = new FileStream("ItemsCollections.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, ItemsCollections);
            }
        }
    }
}
