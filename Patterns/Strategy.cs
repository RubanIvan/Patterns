using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Patterns
{
    /// <summary>общий интерфейс для стратегии движения</summary>
    public interface IMoveStrategy
    {
        Thickness Update(Thickness Position);
    }

    /// <summary>Простая стратегия движения вниз</summary>
    public class UpdateDropDownMove : IMoveStrategy
    {
        public Thickness Update(Thickness Position)
        {
            return new Thickness(Position.Left, Position.Top + 15, Position.Right, Position.Bottom);
        }
    }

    /// <summary>Дерганое движение</summary>
    public class UpdateDropDownMoveRnd : IMoveStrategy
    {
        public Thickness Update(Thickness Position)
        {
            return new Thickness(Position.Left + Rnd.Next(-10, 10), Position.Top + 10 + Rnd.Next(20), Position.Right, Position.Bottom);
        }
    }

    /// <summary>Движение зиг загом</summary>
    public class UpdateDropDownMoveZigZag : IMoveStrategy
    {
        private int dx = 15;
        private int k = 1;

        public Thickness Update(Thickness Position)
        {
            

            if (Position.Left > 500 && k>0 ) k=-1;
            if (Position.Left < -500 && k<0 ) k = 1;

            Debug.WriteLine(dx);

            return new Thickness(Position.Left +(dx*k) , Position.Top + 8, Position.Right, Position.Bottom);
        }
    }
}
