using ConsoleGame1.Logic.Controller;
using System;
using ConsoleGame1.Logic.Model;
using System.Threading;

namespace ConsoleGame1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Первая игра";
            string name = UserController.Authorization();
            User CurentUser = UserController.ReturnCurentUser(name);
            Console.Clear();
            Console.WriteLine("Авторизация прошла успешно!");
            Console.WriteLine($"Вы вошли под логином {CurentUser.Login}");
            Console.WriteLine("Для продолжения нажмите любую клавишу...");
            Console.ReadKey();
            Console.Clear();
            while (true)
            {
                const string PlayButton = "Играть.";
                const string StatisticsButton = "Статистика.";
                const string OptionButton = "Настройки.";
                const string ExitButton = "Выход.";
                Element[,] elements = new Element[,]
                {
                    {
                        new Element(PlayButton)
                    },
                    {
                        new Element(StatisticsButton)
                    },
                    {
                        new Element(OptionButton)
                    },
                    {
                        new Element(ExitButton)
                    }
                };
                ChoseMenu MainMenu = new ChoseMenu(elements);
                MainMenu.MenuButtonSet();
                switch (MainMenu.ChosenElement.Text)
                {
                    case PlayButton:
                        PlayInterface A = new PlayInterface();
                        A.CreateBorder();
                        A.PositionSet(50, 50);
                        GameController.Movement(A);
                        //TODO: добавить действия..
                        break;
                    case StatisticsButton:
                        //TODO: добавить действия..
                        break;
                    case OptionButton:
                        //TODO: добавить действия..
                        break;
                    case ExitButton:
                        Thread.CurrentThread.Abort();
                        Thread.Sleep(1000);
                        break;
                }
                Console.Clear();
            }
        }
    }
}
