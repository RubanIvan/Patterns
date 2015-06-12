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
        public Grid Grid=new Grid();
        
        public IMoveStrategy MoveStrategy;

        public IFireBullet FireBullet;
       
        public bool isAlive=true;

        public virtual void Update()
        {
            Grid.Margin = MoveStrategy.Update(Grid.Margin);

            //если вылетели за экран маркеруем объект как мертвый
            if (Grid.Margin.Top > 900 || Grid.Margin.Top < -900) isAlive = false;
        }


        public GameObject(IMoveStrategy moveStrategy)
        {
            MoveStrategy = moveStrategy;
        }


        
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

        public EnemyShipMk1(IMoveStrategy moveStrategy):base(moveStrategy)
        {
            Grid.Width = 62;
            Grid.Height = 51;
            
            Grid.Margin = new Thickness(Rnd.Next(500), -700, 0, 0);
            
            Uri U = new Uri(@"Img/ShipEnemy/enemy1.png", UriKind.Relative);
            BitmapImage Bi = new BitmapImage(U);
            Image Img = new Image();
            Img.Source = Bi;
            Img.Stretch = Stretch.Fill;
            Grid.Children.Add(Img);

            FireBullet = new FireBulletsRed();


        }


        public GameObject FireBullets()
        {
            return FireBullet.GetFireBullet(Grid.Margin);
        }
    }


   

}
