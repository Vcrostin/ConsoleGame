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
                ConsoleKeyInfo cki;
                ChoseMenu MainMenu = new ChoseMenu(elements);
                MainMenu.MenuButtonSet(out cki);
                switch (MainMenu.ChosenElement.Text)
                {
                    case PlayButton:
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
