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


        private List<GameObject> GameObjectList=new List<GameObject>();

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

            EnemyShipMk1 en = new EnemyShipMk1(new StrategyMoveZigZag());
            GameObjectList.Add(en);
            MainGrid.Children.Add(en.Grid);

            GameObject obj=en.FireBullets();
            GameObjectList.Add(obj);
            MainGrid.Children.Add(obj.Grid);

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

                GameObjectUpdate();
            });

            
        }

        /// <summary>Запускаем Update на всех обьектах. если они мертвы то удаляем их</summary>
        private void GameObjectUpdate()
        {
            for (int i = 0; i < GameObjectList.Count; i++)
            {
                GameObjectList[i].Update();

                //если объект мертв удаляем его
                if (!GameObjectList[i].isAlive)
                {
                    MainGrid.Children.Remove(GameObjectList[i].Grid);
                    GameObjectList.RemoveAt(i);
                }
                
            }

        }

        /// <summary>Прокрутка заднего фона </summary>
        private void BkgUpdate()
        {
            ImageThickness.Top += 4;

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

                    case Key.Space:
                        //если с прошлого выстрела прошло больше n времени значит стреляем еще
                        if ((DateTime.Now - PlayerShip.FireBulleTime).Milliseconds > 300)
                        {
                            PlayerShip.FireBulleTime = DateTime.Now;
                            GameObject obj = PlayerShip.FireBullets();
                            GameObjectList.Add(obj);
                            MainGrid.Children.Add(obj.Grid);
                        }
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

