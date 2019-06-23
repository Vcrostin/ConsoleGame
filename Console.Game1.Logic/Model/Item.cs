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
        public static int Number = 4;
        private static int NextID = 0;
        public int ID { get; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Describe { get; set; }
        public Item(string Name, double Price, string Describe)
        {
            this.Name = Name;
            this.Price = Price;
            this.Describe = Describe;
            ID = ++NextID;
        }

        /// <summary>
        /// Возвращает строку по номеру появления в объявлении переменных.
        /// </summary>
        /// <param name="address"> Номер. </param>
        /// <returns></returns>
        public string this[int address]
        {
            get
            {
                switch (address)
                {
                    case 0:
                        return ID.ToString();
                    case 1:
                        return Name;
                    case 2:
                        return Price.ToString();
                    case 3:
                        return Describe;
                    default:
                        throw new ArgumentOutOfRangeException("Ошибка в диапазоне значение параметра", nameof(address));
                }
                
            }
        }

    }
}
