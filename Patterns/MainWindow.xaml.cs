using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Patterns
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>Корабль игрока </summary>
        private PlayerShip PlayerShip;

        /// <summary>Множество нажатых клавиш </summary>
        private HashSet<Key> KeyboardState = new HashSet<Key>();

        public Timer Timer = new Timer(50);

        //управление скролингом фона
        private Thickness ImageThickness; 

        //Конструктор
        public MainWindow()
        {
            InitializeComponent();

            ImageThickness = BkgImage.Margin;

            //нельзя напрямую создать обьект применен синглтон
            //PlayerShip = new PlayerShip();

            PlayerShip = PlayerShip.GetShip();

            MainGrid.Children.Add(PlayerShip.Grid);

            Timer.Elapsed += GameUpdate;
            Timer.Start();
        }

        /// <summary>Основной цикл программы </summary>
        private void GameUpdate(object sender, ElapsedEventArgs e)
        {   
           Dispatcher.Invoke(() =>
            {
                //обработка данных клавиатуры
                KeyboardUpdate();
                //Прокрутка заднего фона
                BkgUpdate();
            });

            
        }

        /// <summary>Прокрутка заднего фона </summary>
        private void BkgUpdate()
        {
            ImageThickness.Top += 3;

            if (ImageThickness.Top >= 0)
            {
                ImageThickness.Top = -2400;
            }
            BkgImage.Margin = ImageThickness;
            
        }

        /// <summary>Обработка нажатия нескольких клавиш </summary>
        private void KeyboardUpdate()
        {
            foreach (Key K in KeyboardState)
            {
                switch (K)
                {
                    case Key.Left:
                        PlayerShip.MoveLeft();
                        break;

                    case Key.Right:
                        PlayerShip.MoveRight();
                        break;

                    case Key.Up:
                        PlayerShip.MoveUp();
                        break;

                    case Key.Down:
                        PlayerShip.MoveDown();
                        break;
                }
            }
        }

        /// <summary>Занести нажатую клавишу во множество </summary>
        private void MainGrid_KeyDown(object sender, KeyEventArgs e)
        {
            KeyboardState.Add(e.Key);
        }

        /// <summary>Удалить нажатую клавишу из множества </summary>
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            KeyboardState.Remove(e.Key);
        }
    }
}

