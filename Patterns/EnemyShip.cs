using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Patterns
{
 
    public abstract class GameObject
    {
        public Grid Grid;
        
        public IMoveStrategy MoveStrategy;
       
        public bool isAlive;

        public abstract void Update();

        
    }


    //public class Bullet : GameObject
    //{
    //    public Bullet()
    //    {

    //    }
    //    public override void Update()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    #region MyRegion
    ///// <summary>Оснащение корабля пушкой </summary>
    //public interface IFireGun
    //{
    //    /// <summary>Выстрел из пушки</summary>
    //    Image FireGun();
    //}

    ///// <summary>Оснащение корабля ракетами</summary>
    //public interface IFireMissile
    //{
    //    /// <summary>Запуск ракеты</summary>
    //    Image FireMissile();
    //}

    ///// <summary>Оснащение корабля минами</summary>
    //public interface IFireBomb
    //{
    //    /// <summary>Запуск ракеты</summary>
    //    Image FireBomb();
    //}

    ///// <summary>Стрельба зелеными снарядами</summary>
    //public class GunGreen : IFireGun
    //{
    //    public Image FireGun()
    //    {
    //        //в первую строку ложим картинку корабля
    //        Uri U = new Uri(@"Img/Fire/FireGreen.png", UriKind.Relative);
    //        BitmapImage Bi = new BitmapImage(U);
    //        Image Img = new Image();
    //        Img.Source = Bi;
    //        Img.Stretch = Stretch.Fill;
    //        return Img;
    //    }
    //} 
    #endregion


    public class EnemyShipMk1 : GameObject
    {
        
        public EnemyShipMk1()
        {
            isAlive = true;

            Grid=new Grid();
            Grid.Width = 62;
            Grid.Height = 51;
            
            Grid.Margin = new Thickness(Rnd.Next(500), -700, 0, 0);

            MoveStrategy = new UpdateDropDownMoveZigZag();

            Uri U = new Uri(@"Img/ShipEnemy/enemy1.png", UriKind.Relative);
            BitmapImage Bi = new BitmapImage(U);
            Image Img = new Image();
            Img.Source = Bi;
            Img.Stretch = Stretch.Fill;
            Grid.Children.Add(Img);
        }

        public override void Update()
        {
            Grid.Margin = MoveStrategy.Update(Grid.Margin);
            
            if (Grid.Margin.Top > 900) isAlive = false;
           
        }
    }
}
