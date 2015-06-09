using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Patterns
{

    /// <summary>Координаты корабля</summary>
    public struct Point
    {
        public int x;
        public int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Point(Point p)
        {
            x = p.x;
            y = p.y;
        }

        public static bool operator ==(Point a, Point b)
        {
            if (a.x == b.x && a.y == b.y) return true;
            return false;
        }

        public static bool operator !=(Point a, Point b)
        {
            return !(a == b);
        }

        public static Point operator +(Point a, Point b)
        {
            return new Point(a.x + b.x, a.y + b.y);
        }

        public static Point operator *(Point a, int b)
        {
            return new Point(a.x * b, a.y * b);
        }

        public override string ToString()
        {
            return string.Format("x:={0},y:={1}", x, y);
        }
    }

    public static class Rnd
    {
        static Random R = new Random(DateTime.Now.Millisecond);

        static Rnd()
        {
            for (int i = 0; i < 500; i++)
            {
                R.Next();
            }
        }

        public static int Next(int Max)
        {
            return R.Next(Max);
        }

        public static int Next()
        {
            return R.Next();
        }

        public static int Next(int Min, int Max)
        {
            return R.Next(Min, Max);
        }
    }

}
