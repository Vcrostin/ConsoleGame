using ConsoleGame1.Logic.Controller;
using System;

namespace ConsoleGame1.Logic.Model
{
    [Serializable]
    public class Statics
    {
        public int WhiteStuff { get; set; } = 0;
        public int GreenStuff { get; set; } = 0;
        public int BlueStuff { get; set; } = 0;
        public int RedStuff { get; set; } = 0;
        public int HaveMoney => 
            WhiteStuff * PlayInterface.WhitePrice + 
            GreenStuff * PlayInterface.GreenPrice + 
            BlueStuff * PlayInterface.BluePrice + 
            RedStuff * PlayInterface.RedPrice;

        public override string ToString()
        {
            string statics = 
                $"_____СТАТИСТИКА_____" +
                $" Кол-во белого мусора : {WhiteStuff} \n" +
                $" Кол-во зеленого мусора : {GreenStuff} \n" +
                $" Кол-во синего мусора : {BlueStuff} \n" +
                $" Кол-во красного мусора : {RedStuff} \n" +
                $" Всего заработано : {HaveMoney} \n" +
                $" Дата рождения : {UserController.ReturnCurentUser(UserController.CurentUserName).DateOfBirth} \n" +
                $" Возраст : {UserController.ReturnCurentUser(UserController.CurentUserName).Age} \n" +
                $" Текущий баланс счёта :{UserController.ReturnCurentUser(UserController.CurentUserName).Balance}";
            return statics;
        }
    }
}
