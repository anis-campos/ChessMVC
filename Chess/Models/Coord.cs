using System;

namespace Chess.Models
{
    public class Coord : ICloneable,IEquatable<Coord>
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coord(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(Coord other)
        {
            return other.X == X && other.Y == Y;
        }

        public override string ToString()
        {
            return "[x=" + X + ", y=" + Y + "]";
        }

        public object Clone()
        {
            return new Coord(X,Y);
        }

        public static bool IsDiagonal(Coord init, Coord final)
        {
            return Math.Abs(final.X - init.X) == Math.Abs(final.Y - init.Y);
        }

        public static bool coordonnees_valides(Coord target)
        {
            return coordonnees_valides(target.X,target.Y);
        }

        public static bool coordonnees_valides(int x, int y)
        {
            return ((x <= 7) && (x >= 0) && (y <= 7) && (y >= 0));
        }
    }
}