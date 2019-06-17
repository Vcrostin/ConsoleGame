using ConsoleGame1.Logic.Controller;
using System;
using ConsoleGame1.Logic.Model;

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

        }
    }
}
