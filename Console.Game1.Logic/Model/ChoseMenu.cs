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
        public Element ChosenElement => Elements[Index];
        /// <summary>
        /// Текущий выбранный элемент.
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// Все элементы выбора.    
        /// </summary>
        public Element[] Elements { get; set; }
        /// <summary>
        /// Создание класса выделения.
        /// </summary>
        /// <param name="a"> Массив из выделяемых элементов. </param>
        public ChoseMenu(Element[] a)
        {
            Elements = a;
            Elements[0].IsSelected = true;
        }

        /// <summary>
        /// Выбор следующего элемента.
        /// </summary>
        public void SelectNext()
        {
            if (Index != Elements.Count() - 1)
            {
                Elements[Index].IsSelected = false;
                Elements[++Index].IsSelected = true;
            }
        }

        /// <summary>
        /// Выбор предыдущего элемента.
        /// </summary>
        public void SelectPrev()
        {
            if (Index != 0)
            {
                Elements[Index].IsSelected = false;
                Elements[--Index].IsSelected = true;
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
                Console.SetCursorPosition(0, Console.CursorTop - Elements.Count());
            }
            foreach(var element in Elements)
            {
                element.Print();
            }
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
            Console.WriteLine(this.Text);
            Console.ResetColor();
        }
    }
    
}
