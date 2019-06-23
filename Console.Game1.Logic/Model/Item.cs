using ConsoleGame1.Logic.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame1.Logic.Model
{
    [Serializable]
    public class Item
    {
        /// <summary>
        /// кол-во ячеек информации в содержащих информацию об объекте + 3 ячейки (1-пустая, 1-buy, 1-sell).
        /// </summary>
        public static int Number = 8;
        /// <summary>
        /// ID предмета.
        /// </summary>
        public int ID { get; }
        /// <summary>
        /// Название предмета.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Цена предмета.
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// Описание предмета.
        /// </summary>
        public string Describe { get; set; }

        /// <summary>
        /// Инициализация нового предмета.
        /// </summary>
        /// <param name="Name"> Название предмета.</param>
        /// <param name="Price"> Цена предмета.</param>
        /// <param name="Describe"> Описание предмета.</param>
        /// <param name="ID"> ID предмета.(не задается пользователем, а возвращается програмно)</param>
        public Item(string Name, double Price, string Describe,int ID)
        {
            this.Name = Name;
            this.Price = Price;
            this.Describe = Describe;
            this.ID = ID;
        }

        /// <summary>
        /// Возвращает строку по номеру появления в объявлении переменных.
        /// </summary>
        /// <param name="address"> Номер. </param>
        /// <returns> Описание(название элемента).</returns>
        public string this[int address]
        {
            get
            {
                switch (address)
                {
                    case 0:
                        return UserController.ReturnCurentUser(UserController.CurentUserName).Count[ID - 1].ToString();
                    case 1:
                        return ID.ToString();
                    case 2:
                        return Name;
                    case 3:
                        return Price.ToString();
                    case 4:
                        return Describe;
                    case 5:
                        return " ";
                    case 6:
                        return "Buy";
                    case 7:
                        return "Sell";
                    default:
                        throw new ArgumentOutOfRangeException("Ошибка в диапазоне значение параметра", nameof(address));
                }
                
            }
        }

    }
}
