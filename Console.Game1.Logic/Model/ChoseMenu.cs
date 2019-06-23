using System;
using System.Linq;

namespace ConsoleGame1.Logic.Model
{
    /// <summary>
    /// Меню выбора.
    /// </summary>
    public class ChoseMenu
    {
        /// <summary>
        /// Выбранный элемент
        /// </summary>
        public Element ChosenElement => Elements[IndexY, IndexX];
        /// <summary>
        /// Текущий выбранный элемент.
        /// </summary>
        public int IndexX { get; set; }
        public int IndexY { get; set; }
        /// <summary>
        /// Все элементы выбора.    
        /// </summary>
        public Element[,] Elements { get; set; }
        /// <summary>
        /// Создание класса выделения.
        /// </summary>
        /// <param name="a"> Массив из выделяемых элементов. </param>
        public ChoseMenu(Element[,] a)
        {
            Elements = a;
            Elements[0, 0].IsSelected = true;
        }

        /// <summary>
        /// Выбор следующего элемента. По горизонтали.
        /// </summary>
        public void SelectNextY()
        {
            if ((IndexX != Elements.GetLength(1) - 1) && (Elements[IndexY, IndexX + 1].Text != ""))
            {
                Elements[IndexY, IndexX].IsSelected = false;
                Elements[IndexY, ++IndexX].IsSelected = true;
            }
        }

        /// <summary>
        /// Выбор предыдущего элемента. По горизонтали.
        /// </summary>
        public void SelectPrevY()
        {
            if ((IndexX != 0)&&(Elements[IndexY,IndexX-1].Text!=""))
            {
                Elements[IndexY, IndexX].IsSelected = false;
                Elements[IndexY, --IndexX].IsSelected = true;
            }
        }

        /// <summary>
        /// Выбор следующего элемента. По вертикали.
        /// </summary>
        public void SelectNextX()
        {
            if ((IndexY != Elements.GetLength(0) - 1) && (Elements[IndexY + 1, IndexX].Text != ""))
            {
                Elements[IndexY, IndexX].IsSelected = false;
                Elements[++IndexY, IndexX].IsSelected = true;
            }
        }

        /// <summary>
        ///  Выбор предыдущего элемента. По вертикали.
        /// </summary>
        public void SelectPrevX()
        {
            if ((IndexY != 0) && (Elements[IndexY - 1, IndexX].Text != ""))
            {
                Elements[IndexY, IndexX].IsSelected = false;
                Elements[--IndexY, IndexX].IsSelected = true;
            }
        }

        /// <summary>
        /// Проверка на создание объекта либо же его перерисовки
        /// </summary>
        private bool IsCreated { get; set; } = false;
        /// <summary>
        /// Рисовка интерфейса выбора.
        /// </summary>
        public void Draw()
        {
            Console.CursorVisible = false;
            if (IsCreated == false)
            {
                Console.SetCursorPosition(0, Console.CursorTop);
                IsCreated = true;
            }
            else
            {
                Console.SetCursorPosition(0, Console.CursorTop - Elements.GetLength(0));
            }
            for(int i = 0; i < Elements.GetLength(0); i++)
            {
                for(int j = 0; j < Elements.GetLength(1); j++)
                {
                    Elements[i, j].Print();
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Навигация в меню.
        /// </summary>
        public void MenuButtonSet()
        {
            ConsoleKeyInfo cki;
            do
            {
                Draw();
                Console.CursorVisible = false;
                cki = Console.ReadKey(true);
                switch (cki.Key)
                {
                    case ConsoleKey.LeftArrow:
                        SelectPrevY();
                        break;
                    case ConsoleKey.RightArrow:
                        SelectNextY();
                        break;
                    case ConsoleKey.UpArrow:
                        SelectPrevX();
                        break;
                    case ConsoleKey.DownArrow:
                        SelectNextX();
                        break;
                }
            } while (cki.Key != ConsoleKey.Enter);
        }
    }
    //Делегат который не пригодился.
    public delegate void OnClickEnter();

    /// <summary>
    /// Класс элементов выбора.
    /// </summary>
    public class Element
    {
        /// <summary>
        /// Имя первой строки выбора
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Fore цвет выбранного элемента.
        /// </summary>
        private static readonly ConsoleColor SelectedForeColor = ConsoleColor.Black;
        /// <summary>
        /// Фоновый цвет выбранного элемента.
        /// </summary>
        private static readonly ConsoleColor SelectedBackColor = ConsoleColor.Gray;
        /// <summary>
        /// Показывает выбран ли элемент.
        /// </summary>
        public bool IsSelected { get; set; } = false;
        /// <summary>
        /// Делегат элемента.
        /// </summary>
        public OnClickEnter Click { get; set; }

        /// <summary>
        /// Инициализация одного элемента выбора.
        /// </summary>
        /// <param name="Name"> Текст элемента выбора. </param>
        /// <param name="OnClick"> Действия выполняемые при клике. </param>
        //??TODO:
        public Element(string Name/*,OnClickEnter OnClick*/)
        {
            Text = Name;
        }

        /// <summary>
        /// Выделение конкретного элемента.
        /// </summary>
        public void Print()
        {
            if (IsSelected)
            {
                Console.BackgroundColor = SelectedBackColor;
                Console.ForegroundColor = SelectedForeColor;
            }
            Console.Write("{0}", this.Text);
            Console.ResetColor();
            Console.Write(" ");
        }
    }
    
}
