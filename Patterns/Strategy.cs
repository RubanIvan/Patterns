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
    /// <summary>общий интерфейс для стратегии движения</summary>
    public interface IMoveStrategy
    {
        Thickness Update(Thickness Position);
    }

    /// <summary>Простая стратегия движения вниз</summary>
    public class StrategyMoveDown15 : IMoveStrategy
    {
        public Thickness Update(Thickness Position)
        {
            return new Thickness(Position.Left, Position.Top + 15, Position.Right, Position.Bottom);
        }
    }


    public class StrategyMoveDown35 : IMoveStrategy
    {
        public Thickness Update(Thickness Position)
        {
            return new Thickness(Position.Left, Position.Top + 35, Position.Right, Position.Bottom);
        }
    }


    public class StrategyMoveUp35 : IMoveStrategy
    {
        public Thickness Update(Thickness Position)
        {
            return new Thickness(Position.Left, Position.Top - 35, Position.Right, Position.Bottom);
        }
    }

    /// <summary>Дерганое движение</summary>
    public class StrategyMoveRnd : IMoveStrategy
    {
        public Thickness Update(Thickness Position)
        {
            return new Thickness(Position.Left + Rnd.Next(-10, 10), Position.Top + 10 + Rnd.Next(20), Position.Right, Position.Bottom);
        }
    }

    /// <summary>Движение зиг загом</summary>
    public class StrategyMoveZigZag : IMoveStrategy
    {
        private int dx = 15;
        private int k = 1;

        public Thickness Update(Thickness Position)
        {
            if (Position.Left > 500 && k > 0) k = -1;
            if (Position.Left < -500 && k < 0) k = 1;

            return new Thickness(Position.Left + (dx * k), Position.Top + 8, Position.Right, Position.Bottom);
        }
    }


    /// <summary>Интурфейс выстрела из пушки</summary>
    public interface IFireBullet
    {
        GameObject GetFireBullet(Thickness Pos);
    }

    /// <summary>Пушка стреляющая красными выстрелами</summary>
    public class FireBulletsRed : IFireBullet
    {
        public GameObject GetFireBullet(Thickness Pos)
        {
            return new BulletRed(new StrategyMoveDown35(), Pos);
        }
    }

    /// <summary>Красные выстрелы</summary>
    public class BulletRed : GameObject
    {
        public BulletRed(IMoveStrategy moveStrategy, Thickness Pos)
            : base(moveStrategy)
        {

            Grid.Width = 25;
            Grid.Height = 25;

            Grid.Margin = Pos;

            Uri U = new Uri(@"Img/Fire/FireRed.png", UriKind.Relative);
            BitmapImage Bi = new BitmapImage(U);
            Image Img = new Image();
            Img.Source = Bi;
            Img.Stretch = Stretch.Fill;
            Grid.Children.Add(Img);
        }

    }


    /// <summary>Пушка стреляющая зелеными выстрелами</summary>
    public class FireBulletsGreen : IFireBullet
    {
        public GameObject GetFireBullet(Thickness Pos)
        {
            return new BulletGreen(new StrategyMoveDown35(), Pos);
        }
    }

    /// <summary>Зеленые выстрелы</summary>
    public class BulletGreen : GameObject
    {
        public BulletGreen(IMoveStrategy moveStrategy, Thickness Pos)
            : base(moveStrategy)
        {

            Grid.Width = 25;
            Grid.Height = 25;

            Grid.Margin = Pos;

            Uri U = new Uri(@"Img/Fire/FireGreen.png", UriKind.Relative);
            BitmapImage Bi = new BitmapImage(U);
            Image Img = new Image();
            Img.Source = Bi;
            Img.Stretch = Stretch.Fill;
            Grid.Children.Add(Img);
        }

    }
    
    /// <summary>Пушка игрока стреляющая зелеными выстрелами</summary>
    public class PlayerFireBulletsGreen : IFireBullet
    {
        public GameObject GetFireBullet(Thickness Pos)
        {
            Thickness pos = Pos;
            pos.Top+= -50;
            return new BulletGreen(new StrategyMoveUp35(), pos);
        }
    }

}
