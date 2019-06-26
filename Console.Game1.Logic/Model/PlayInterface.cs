using ConsoleGame1.Logic.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame1.Logic.Model
{
    public class PlayInterface
    {
        /// <summary>
        /// x коорд.
        /// </summary>
        public int XBySpace { get; set; }
        /// <summary>
        /// y коорд.
        /// </summary>
        public int YBySpace { get; set; }
        public int XPosByCreatedStuff { get; set; } = 100;
        public int YPosByCreatedStuff { get; set; } = 50;
        public ConsoleColor ColorOfStuff { get; set; } = ConsoleColor.White;
        public static int WhitePrice { get; } = 10;
        public static int GreenPrice { get; } = 50;
        public static int BluePrice { get; } = 200;
        public static int RedPrice { get; } = 1000;
        public static ConsoleColor ShipColor { get; set; } = ConsoleColor.White;

        /// <summary>
        /// Установка центральной точки фигуры и ее перерисовка.
        /// </summary>
        /// <param name="x"> x коорд. </param>
        /// <param name="y"> y коорд. </param>
        public void PositionSet(int x,int y)
        {
            XBySpace = x;
            YBySpace = y;
            Draw();
        }

        /// <summary>
        /// Создание границ поля.
        /// </summary>
        public void CreateBorder()
        {
            Console.Clear();
            Console.WindowHeight = Console.LargestWindowHeight;
            Console.WindowWidth = Console.LargestWindowWidth;
            Console.CursorVisible = false;
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
            for(int i = 1; i < Console.WindowHeight; i++)
            {
                Console.SetCursorPosition(1, i);
                Console.Write("*");
                Console.SetCursorPosition(Console.WindowWidth-2, i);
                Console.Write("*");
            }
            for(int i = 1; i < Console.WindowWidth-1; i++)
            {
                Console.SetCursorPosition(i, 1);
                Console.Write("*");
                Console.SetCursorPosition(i, Console.WindowHeight - 1);
                Console.Write("*");
            }
        }

        /// <summary>
        /// Рисовка фигуры.
        /// </summary>
        public void Draw()
        {
            Console.ForegroundColor = ShipColor;
            Console.SetCursorPosition(XBySpace - 1, YBySpace);
            Console.Write("|X|");
            ComparePosStuffAndSpace();
            Console.ForegroundColor = ColorOfStuff;
            Console.SetCursorPosition(XPosByCreatedStuff, YPosByCreatedStuff);
            Console.Write("0");
            Console.ResetColor();
        }

        /// <summary>
        /// Метод, сравнивающий значение x и y у корабля и мусора. Изменяет след цвет на рандомный.
        /// </summary>
        private void ComparePosStuffAndSpace()
        {
            if ((XPosByCreatedStuff == XBySpace) && (YPosByCreatedStuff == YBySpace))
            {
                Random random = new Random();
                XPosByCreatedStuff = random.Next(Console.WindowWidth - 7) + 3;
                YPosByCreatedStuff = random.Next(Console.WindowHeight - 6) + 3;
                switch (ColorOfStuff)
                {
                    case ConsoleColor.White:
                        UserController.ReturnCurentUser(UserController.CurentUserName).CurrentStatic.WhiteStuff++;
                        UserController.ReturnCurentUser(UserController.CurentUserName).Balance += WhitePrice;
                        UserController.SaveData();
                        break;
                    case ConsoleColor.Green:
                        UserController.ReturnCurentUser(UserController.CurentUserName).CurrentStatic.GreenStuff++;
                        UserController.ReturnCurentUser(UserController.CurentUserName).Balance += GreenPrice;
                        UserController.SaveData();
                        break;
                    case ConsoleColor.Blue:
                        UserController.ReturnCurentUser(UserController.CurentUserName).CurrentStatic.BlueStuff++;
                        UserController.ReturnCurentUser(UserController.CurentUserName).Balance += BluePrice;
                        UserController.SaveData();
                        break;
                    case ConsoleColor.Red:
                        UserController.ReturnCurentUser(UserController.CurentUserName).CurrentStatic.RedStuff++;
                        UserController.ReturnCurentUser(UserController.CurentUserName).Balance += RedPrice;
                        UserController.SaveData();
                        break;
                }
                int color = random.Next(10);
                switch (color)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                        ColorOfStuff = ConsoleColor.White;
                        break;
                    case 4:
                    case 5:
                    case 6:
                        ColorOfStuff = ConsoleColor.Green;
                        break;
                    case 7:
                    case 8:
                        ColorOfStuff = ConsoleColor.Blue;
                        break;
                    case 9:
                        ColorOfStuff = ConsoleColor.Red;
                        break;
                }
            }
        }

        /// <summary>
        /// Очистка пред фигуры.
        /// </summary>
        public void Clear()
        {
            Console.SetCursorPosition(XBySpace - 1, YBySpace);
            Console.Write("   ");
        }
    }
}
