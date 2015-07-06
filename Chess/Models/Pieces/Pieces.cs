using System;
using System.Collections.Generic;

namespace Chess.Models.Pieces
{
    public abstract class Pieces : ICloneable
    {
        protected string Name;
        protected readonly Couleur Couleur;
        protected readonly Coord Coord;
        protected bool Depart;
        protected bool Captured;

        protected Pieces(string name, Couleur couleur, Coord coord)
        {
            Name = name;
            Couleur = couleur;
            Coord = coord;
            Depart = true;
            Captured = false;
        }

        public bool IsHere(Coord c)
        {
            return c.Equals(Coord);
        }

        public bool Capture()
        {
            Captured = true;
            return Captured;
        }

        public Couleur GetCouleur()
        {
            return Couleur;
        }

        public string GetName()
        {
            return Name;
        }

        public int GetX()
        {
            return Coord.X;
        }

        public int GetY()
        {
            return Coord.Y;
        }

        public bool GetCapture()
        {
            return Captured;
        }

        public override string ToString()
        {
            return "[" + Name + " " + Couleur + "]" + " " + Coord;
        }

        public abstract bool IsMoveOk(int xFinal, int yFinal);

        public bool IsMoveOk(Coord target)
        {
            return IsMoveOk(target.X, target.Y);
        }

        public bool Move(int xFinal, int yFinal)
        {
            Coord.X = xFinal;
            Coord.Y = yFinal;
            Depart = false;
            return true;
        }

        public bool Move(Coord target)
        {
            return Move(target.X, target.Y);
        }

        public List<Coord> GetRange()
        {
            List<Coord> rep = new List<Coord>();
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (IsMoveOk(x, y))
                        rep.Add(new Coord(x, y));
                }
            }
            return rep;
        }

        public List<Coord> GetPath(Coord target)
        {
            var rep = new List<Coord>();

            if (this is Cavalier) return rep;

            var x = GetX() + Math.Sign(target.X - GetX());
            var y = GetY() + Math.Sign(target.Y - GetY());

            while (!(x == target.X && y == target.Y))
            {
                x += Math.Sign(target.X - x);
                y += Math.Sign(target.Y - y);
                rep.Add(new Coord(x, y));
            }

            return rep;

        }

        public abstract object Clone();
    }
}