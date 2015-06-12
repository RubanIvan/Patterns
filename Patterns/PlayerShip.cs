using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace Patterns
{

    public class PlayerShip : Grid
    {
        //Применение шаблона синглтон
        /// <summary>Поле хранит единственный экземпляр </summary>
        private static PlayerShip _instance;

        /// <summary>Статический метод возвращает эекзкмпляр</summary>
        public static PlayerShip GetShip()
        {
            if (_instance == null)
            {
                PlayerShip PlayerShip = new PlayerShip();
                _instance = PlayerShip;
                return PlayerShip;
            }
            return _instance;
        }

        /// <summary>Сылка на обрамляюшую сетку</summary>
        public Grid Grid;

        /// <summary>Изображение корабля </summary>
        public Image ShipImage;

        /// <summary>Изображение пламени двигателя </summary>
        public Image ShipEngineImage;

        /// <summary>спрайты кадров анимации пламени двигателя</summary>
        public List<BitmapImage> ShipEngImageList = new List<BitmapImage>();

        /// <summary>количество кадров анимации пламени двигателя</summary>
        protected const int AnimShipEngMaxFrame = 4;

        /// <summary>Завис. свойство для анимации пламени двигателя</summary>
        public static readonly DependencyProperty CurFrameRotateProperty = DependencyProperty.Register(
            "CurFrameRotate", typeof(int), typeof(PlayerShip), new PropertyMetadata(default(int), CurFrameShipEngChange));

        /// <summary>Текущий кадр анимации двигателя </summary>
        public int CurFrameEngImage
        {
            get { return (int)GetValue(CurFrameRotateProperty); }
            set { SetValue(CurFrameRotateProperty, value); }
        }

        /// <summary>обработчик события срабатывает после смены каждого кадра</summary>
        private static void CurFrameShipEngChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //Устанавливает изображение равное кадру анимации
            ((PlayerShip)(d)).ShipEngineImage.Source = ((PlayerShip)(d)).ShipEngImageList[((PlayerShip)(d)).CurFrameEngImage];
        }

        //создаем анимацию вращения
        public Int32Animation AnimEngFire = new Int32Animation(0, AnimShipEngMaxFrame - 1, TimeSpan.FromSeconds(0.4));

        /// <summary>интерфейс выстрела из пушки </summary>
        public IFireBullet FireBullet;

        //время последнего выстрела из пушки
        public DateTime FireBulleTime;

        //Конструктор зделан приватным для применения синглтона
        private PlayerShip()
        {
            //в коде создаем сетку делим ее на 2 строки 3 колонки
            Grid = new Grid();

            RowDefinitionCollection rdefs = Grid.RowDefinitions;
            rdefs.Add(new RowDefinition() { Height = new GridLength(53) });
            rdefs.Add(new RowDefinition() { Height = new GridLength(26) });

            ColumnDefinitionCollection cdefs = Grid.ColumnDefinitions;
            cdefs.Add(new ColumnDefinition(){Width = new GridLength(1,GridUnitType.Star)});
            cdefs.Add(new ColumnDefinition() { Width = new GridLength(16, GridUnitType.Pixel) });
            cdefs.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            //в первую строку ложим картинку корабля
            Uri U = new Uri(@"Img/ShipPlayer/ship.png",UriKind.Relative);
            BitmapImage Bi=new BitmapImage(U);
            Image Img = new Image();
            Img.Source = Bi;
            Img.Stretch=Stretch.Fill;
            Grid.SetColumnSpan(Img,3);
            ShipImage = Img;
            Grid.Children.Add(Img);

            Grid.Height = 74;
            Grid.Width = 53 + 26;
            Grid.Margin=new Thickness(0,0,0,0);

            //список кадров пламени двигателя
            U = new Uri(@"Img/ShipPlayer/ship_fire1.png", UriKind.Relative);
            ShipEngImageList.Add(new BitmapImage(U));
            U = new Uri(@"Img/ShipPlayer/ship_fire2.png", UriKind.Relative);
            ShipEngImageList.Add(new BitmapImage(U));
            U = new Uri(@"Img/ShipPlayer/ship_fire3.png", UriKind.Relative);
            ShipEngImageList.Add(new BitmapImage(U));
            U = new Uri(@"Img/ShipPlayer/ship_fire2.png", UriKind.Relative);
            ShipEngImageList.Add(new BitmapImage(U));

            //ложим пламя во вторую строку посередине
            Img = new Image();
            Img.Source = ShipEngImageList[0];
            Grid.SetColumn(Img, 1);
            Grid.SetRow(Img, 1);
            ShipEngineImage = Img;
            Grid.Children.Add(Img);


            //для анимации двигателя задаем повторятся вечно
            AnimEngFire.RepeatBehavior = RepeatBehavior.Forever;
            //Запускаем анимацию
            BeginAnimation(CurFrameRotateProperty, AnimEngFire);


            FireBullet=new PlayerFireBulletsGreen();
            FireBulleTime=DateTime.Now;
        }

        /// <summary>Выстрел из пушки</summary>
        public GameObject FireBullets()
        {
            return FireBullet.GetFireBullet(Grid.Margin);
        }

        #region Движение в стороны
        public void MoveRight()
        {
            Grid.Margin = new Thickness(Grid.Margin.Left + 20, Grid.Margin.Top, 0, 0);
        }

        public void MoveLeft()
        {
            Grid.Margin = new Thickness(Grid.Margin.Left - 20, Grid.Margin.Top, 0, 0);
        }

        public void MoveUp()
        {
            Grid.Margin = new Thickness(Grid.Margin.Left, Grid.Margin.Top - 20, 0, 0);
        }

        public void MoveDown()
        {
            Grid.Margin = new Thickness(Grid.Margin.Left, Grid.Margin.Top + 20, 0, 0);
        } 
        #endregion


    }
}
