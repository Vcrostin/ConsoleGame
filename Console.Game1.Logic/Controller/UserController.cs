using ConsoleGame1.Logic.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace ConsoleGame1.Logic.Controller
{
    public class UserController
    {
        /// <summary>
        /// Список пользователей.
        /// </summary>
        public static List<User> UsersData { get; set; }

        /// <summary>
        ///  Логин текущего пользователя.
        /// </summary>
        public string CurentUserName { get; set; }
        /// <summary>
        /// Текущий юзер.
        /// </summary>
        User CurentUser { get; set; }

        /// <summary>
        /// Создание "пробного" пользователя
        /// </summary>
        /// <param name="UserName"> Имя пользователя. </param>
        public UserController(string UserName)
        {
            UsersData = UploadData();

            CurentUser = UsersData.SingleOrDefault(u => u.Login == UserName);

            if (CurentUser == null)
            {
                Console.WriteLine("Пользователя с таким именем ещё нет. Создать нового пользователя?");
                CreateYesNoMenu();
            }
            else
            {
                string checkpassword;
                CurentUserName = CurentUser.Login;
                int Count = 0;
                do
                {
                    Console.WriteLine("Введите пароль");
                    checkpassword = Console.ReadLine();
                    Count++;
                    if (Count >= 3)
                    {
                        throw new TimeoutException("Вы исчерпали лимит попыток на пароль");
                    }
                } while (User.CheckPass(checkpassword, UserName,UsersData));
            }
        }

        /// <summary>
        /// Сохранение данных пользователей.
        /// </summary>
        public void SaveData()
        {
            var formatter = new BinaryFormatter();
            using (var fs = new FileStream("DataCollections.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, UsersData);
            }
        }

        /// <summary>
        /// Загрузка данных пользователей.
        /// </summary>
        /// <returns></returns>
        private List<User> UploadData()
        {
            var formatter = new BinaryFormatter();
            using (var fs=new FileStream("DataCollections.dat", FileMode.OpenOrCreate))
            {
                if(fs.Length>0 && formatter.Deserialize(fs) is List<User> users)
                {
                    return users;
                }
                else
                {
                    return new List<User>();
                }
            }
        }

        /// <summary>
        /// Авторизация.
        /// </summary>
        public static string Authorization()
        {
            Console.WriteLine("Введите логин");
            string name = Console.ReadLine();
            UserController user = new UserController(name);
            return name;
        }

        /// <summary>
        /// Регистрация нового пользователя.
        /// </summary>
        public void Registration()
        {
            Console.CursorVisible = true;
            Console.WriteLine("Введите логин:");
            string name = Console.ReadLine();
            string password;
            string check = "";
            do
            {
                Console.WriteLine($"Придумайте{check} пароль:");
                password = Console.ReadLine();
                check = " более сложный";
            } while (password.Length < 5);
            DateTime DateBirth;
            do
            {
                Console.WriteLine("Введите дату рождения");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime DateOfBirth) && DateOfBirth > new DateTime(1500, 01, 01))
                {
                    DateBirth = DateOfBirth;
                    break;
                }
                else
                {
                    Console.WriteLine("Вы ввели неверный формат даты");
                }
            } while (true);
            User user = new User(name, password, DateBirth);
            UsersData.Add(user);
            SaveData();
        }

        /// <summary>
        /// Создание меню выбора с кнопками да и нет.
        /// </summary>
        public void CreateYesNoMenu()
        {
            ConsoleKeyInfo cki;
            var elements = new[]
            {
                        new Element("Yes"),
                        new Element("No")
            };
            ChoseMenu Menu = new ChoseMenu(elements);
            Console.CursorVisible = false;
            do
            {
                Menu.Draw();
                cki = Console.ReadKey(true);
                switch (cki.Key)
                {
                    case ConsoleKey.UpArrow:
                        Menu.SelectPrev();
                        break;
                    case ConsoleKey.DownArrow:
                        Menu.SelectNext();
                        break;
                }
            } while (cki.Key!=ConsoleKey.Enter);
            switch (Menu.ChosenElement.Text)
            {
                case "Yes":
                    Registration();
                    break;
                case "No":
                    break;
            }
            Console.Clear();
            Authorization();
        }

        /// <summary>
        ///  Возвращает ссылку на пользователя.
        /// </summary>
        /// <param name="name"> Логин пользователя. </param>
        /// <returns></returns>
        public static User ReturnCurentUser(string name)
        {
            foreach(var s in UsersData)
            {
                if (s.Login == name)
                {
                    return s;
                }
            }
            return null;
        }

    }
}
