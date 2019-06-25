using System;
using ConsoleGame1.Logic.Model;

namespace ConsoleGame1.Logic.Controller
{
    public class StaticsController
    {
        public static void StaticList()
        {
            ConsoleKeyInfo cki;
            Console.WriteLine(UserController.ReturnCurentUser(UserController.CurentUserName).CurrentStatic);
            Console.WriteLine("Нажмите \"Esc\" для возврата в меню..");
            do
            {
                cki = Console.ReadKey(true);
            } while (cki.Key != ConsoleKey.Escape);
        }
    }
}
