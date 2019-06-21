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
        public int X { get; set; }
        /// <summary>
        /// y коорд.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Установка центральной точки фигуры и ее перерисовка.
        /// </summary>
        /// <param name="x"> x коорд. </param>
        /// <param name="y"> y коорд. </param>
        public void PositionSet(int x,int y)
        {
            X = x;
            Y = y;
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
            Console.SetCursorPosition(X - 1, Y);
            Console.Write("|X|");
        }

        /// <summary>
        /// Очистка пред фигуры.
        /// </summary>
        public void Clear()
        {
            Console.SetCursorPosition(X - 1, Y);
            Console.Write("   ");
        }
    }
}
