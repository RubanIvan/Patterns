using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Patterns
{

    public class PlayerShip
    {
        /// <summary>Сылка на  </summary>
        public Grid Grid;

        /// <summary>спрайты кадров анимации пламени двигателя</summary>
        public List<BitmapImage> ShipEngImage = new List<BitmapImage>();

        /// <summary>количество кадров анимации пламени двигателя</summary>
        protected const int AnimShipEngMaxFrame = 4;

        
        /// <summary>Завис. свойство для анимации пламени двигателя</summary>
        public static readonly DependencyProperty CurFrameRotateProperty = DependencyProperty.Register(
            "CurFrameRotate", typeof(int), typeof(Gem), new PropertyMetadata(default(int), CurFrameShipEngChange));

        
        /// <summary>обработчик события срабатывает после смены каждого кадра</summary>
        private static void CurFrameShipEngChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //Устанавливает изображение равное кадру анимации
            ((Gem)(d)).Source = ((Gem)(d)).ImgList[((Gem)(d)).CurFrameRotate];
        }

        //Конструктор
        public PlayerShip()
        {
            Grid = new Grid();

            RowDefinitionCollection rdefs = Grid.RowDefinitions;
            rdefs.Add(new RowDefinition() { Height = new GridLength(53) });
            rdefs.Add(new RowDefinition() { Height = new GridLength(26) });

            ColumnDefinitionCollection cdefs = Grid.ColumnDefinitions;
            cdefs.Add(new ColumnDefinition(){Width = new GridLength(1,GridUnitType.Star)});
            cdefs.Add(new ColumnDefinition() { Width = new GridLength(16, GridUnitType.Pixel) });
            cdefs.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            Uri U = new Uri(@"Img/ShipPlayer/ship.png",UriKind.Relative);
            BitmapImage Bi=new BitmapImage(U);
            Image Img = new Image();
            Img.Source = Bi;
            Img.Stretch=Stretch.Fill;
            Grid.SetColumnSpan(Img,3);
            Grid.Children.Add(Img);

            Grid.Height = 74;
            Grid.Width = 53 + 26;
            Grid.Margin=new Thickness(0,0,0,0);

            //Grid.SetColumn(Img,1);
            //Grid.SetRow(Img, 1);

            U = new Uri(@"Img/ShipPlayer/ship_fire1.png", UriKind.Relative);
            ShipEngImage.Add(new BitmapImage(U));
            U = new Uri(@"Img/ShipPlayer/ship_fire2.png", UriKind.Relative);
            ShipEngImage.Add(new BitmapImage(U));
            U = new Uri(@"Img/ShipPlayer/ship_fire3.png", UriKind.Relative);
            ShipEngImage.Add(new BitmapImage(U));
            U = new Uri(@"Img/ShipPlayer/ship_fire2.png", UriKind.Relative);
            ShipEngImage.Add(new BitmapImage(U));


        }

        public void MoveRight()
        {
            Grid.Margin = new Thickness(Grid.Margin.Left + 10, Grid.Margin.Top, 0, 0);
        }

        public void MoveLeft()
        {
            Grid.Margin = new Thickness(Grid.Margin.Left - 10, Grid.Margin.Top, 0, 0);
        }


        public void MoveUp()
        {
            Grid.Margin = new Thickness(Grid.Margin.Left,Grid.Margin.Top - 10, 0, 0);
        }

        public void MoveDown()
        {
            Grid.Margin = new Thickness(Grid.Margin.Left, Grid.Margin.Top + 10, 0, 0);
        }
    }
}
