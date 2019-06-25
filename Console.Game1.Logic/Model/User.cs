using ConsoleGame1.Logic.Model;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleGame1.Logic.Model
{
    [Serializable]
    public class User
    {
        /// <summary>
        /// Логин.
        /// </summary>
        public string Login { get; }
        /// <summary>
        /// id след. нового пользователя
        /// </summary>
        private static int NextID { get; set; } = 0;
        /// <summary>
        /// Кол-во предметов в инвентаре каждого типа.
        /// </summary>
        public int[] Count { get; set; }
        /// <summary>
        /// ID текущего пользователя.
        /// </summary>
        private int ID { get; }
        /// <summary>
        /// Пароль.
        /// </summary>
        private string Password { get; }
        /// <summary>
        /// Соль для пароля.
        /// </summary>
        string Sold { get; }
        /// <summary>
        /// Дата рождения.
        /// </summary>
        public DateTime DateOfBirth { get; set; }
        /// <summary>
        /// Дата регистрации в приложении.
        /// </summary>
        DateTime DateOfRegistration { get; }
        /// <summary>
        /// Возраст.
        /// </summary>
        public int Age
        {
            get
            {
                if (DateTime.Now >= DateOfBirth.AddYears(DateTime.Now.Year - DateOfBirth.Year))
                {
                    return DateTime.Now.Year - DateOfBirth.Year;
                }
                else
                {
                    return DateTime.Now.Year - DateOfBirth.Year - 1;
                    
                }
            }
        }
        /// <summary>
        /// Баланс счета.
        /// </summary>s
        public int Balance { get; set; } = 0;
        public Statics CurrentStatic;

        /// <summary>
        /// Создание пользователя только с логином для проверки на уникальность/существование.
        /// </summary>
        /// <param name="Name"> Имя пользователя </param>
        public User(string Name)
        {
            Login = Name;
        }

        /// <summary>
        /// Создание нового пользователя.
        /// </summary>
        /// <param name="Name"> Логин. </param>
        /// <param name="Password"> Пароль. </param>
        /// <param name="BirthDay"> Дата рождения. </param>
        public User(string Name,string Password,DateTime BirthDay)
        {
            CurrentStatic = new Statics();
            DateOfRegistration = DateTime.Now;
            ID = ++NextID;
            Login = Name;
            Sold = Guid.NewGuid().ToString();
            this.Password = GetHashCode(Sold + Password);
            DateOfBirth = BirthDay;
        }

        /// <summary>
        /// Получение MD5 кода строки.
        /// </summary>
        /// <param name="Password"> Строка. </param>
        /// <returns></returns>
        static public string GetHashCode(string Password)
        {
            byte[] hash = Encoding.ASCII.GetBytes(Password);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] hashenc = md5.ComputeHash(hash);
            string result = "";
            foreach (var b in hashenc)
            {
                result += b.ToString("x2");
            }
            return result;
        }

        /// <summary>
        /// Статический метод поиска пользователей в архиве.
        /// </summary>
        /// <param name="Name"> Логин искомого пользователя. </param>
        /// <param name="UsersData"> Ссылка на коллекцию пользователей. </param>
        /// <returns> Возвращает ссылку на пользователя с логином Name</returns>
        static public User ReturnChosenUser(string Name,List<User> UsersData)
        {
            foreach(var u in UsersData)
            {
                if (u.Login == Name)
                {
                    return u;
                }
            }
            Console.WriteLine("ОШИБКА");
            throw new Exception("ПРОИЗОШЛА ХЕРНЯ!");
        }

        /// <summary>
        /// Статический метод проверки паролей.
        /// </summary>
        /// <param name="checkpass"> Введенный пароль. </param>
        /// <param name="Name"> Имя юзера. </param>
        /// <param name="UsersData"> Коллекция данных пользователей. </param>
        /// <returns> Возвращает bool значение. </returns>
        static public bool CheckPass(string checkpass,string Name,List<User>UsersData)
        {
            string MBpass = GetHashCode(ReturnChosenUser(Name, UsersData).Sold + checkpass);
            return MBpass != ReturnChosenUser(Name, UsersData).Password;
        }


    }
}
