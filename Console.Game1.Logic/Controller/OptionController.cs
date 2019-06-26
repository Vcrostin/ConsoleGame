using System;
using ConsoleGame1.Logic.Model;

namespace ConsoleGame1.Logic.Controller
{
    public class OptionController
    {
        public static void OptionMenu()
        {
            Element[,] MenuButton = new Element[,]
            {
                {
                    new Element(" Цвет корабля"),
                    new Element(" "),
                    new Element(" Белый "),
                    new Element(" Зеленый "),
                    new Element(" Синий "),
                    new Element(" Красный ")
                }
            };
            ChoseMenu choseMenu = new ChoseMenu(MenuButton);
            choseMenu.Elements[0, 0].IsSelected = false;
            choseMenu.Elements[0, 2].IsSelected = true;
            choseMenu.IndexX = 2;
            choseMenu.MenuButtonSet();
            if (choseMenu.Elements[0, 2].IsSelected == true)
            {
                PlayInterface.ShipColor = ConsoleColor.White;
            }
            if (choseMenu.Elements[0, 3].IsSelected == true)
            {
                PlayInterface.ShipColor = ConsoleColor.Green;
            }
            if (choseMenu.Elements[0, 4].IsSelected == true)
            {
                PlayInterface.ShipColor = ConsoleColor.Blue;
            }
            if (choseMenu.Elements[0, 5].IsSelected == true)
            {
                PlayInterface.ShipColor = ConsoleColor.Red;
            }
        }
    }
}
