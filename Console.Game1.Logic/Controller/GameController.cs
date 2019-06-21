using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGame1.Logic.Model;
using System.Threading;

namespace ConsoleGame1.Logic.Controller
{
    public class GameController
    {
        /// <summary>
        /// Перемещение объекта в 2D пространстве.
        /// </summary>
        /// <param name="A"> Объект. </param>
        public static void Movement(PlayInterface A)
        {
            ConsoleKeyInfo cki;
            do
            {
                cki = Console.ReadKey(true);
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
                }
            } while (cki.Key != ConsoleKey.Escape);
        }
        /// <summary>
        /// Перемещение вверх.
        /// </summary>
        /// <param name="A"> Объект перемещения. </param>
        public static void MoveUp(PlayInterface A)
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
        public static void MoveDown(PlayInterface A)
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
        public static void MoveLeft(PlayInterface A)
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
        public static void MoveRight(PlayInterface A)
        {
            if (A.X + 1 < Console.WindowWidth - 4)
            {
                A.Clear();
                A.PositionSet(A.X + 1, A.Y);
            }
        }
    }
}
