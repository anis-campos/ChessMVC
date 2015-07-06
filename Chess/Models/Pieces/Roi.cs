using System;

namespace Chess.Models.Pieces
{
    public class Roi : Pieces
    {
        public Roi(string name, Couleur couleur, Coord coord)
            : base(name, couleur, coord)
        {
        }

        public override bool IsMoveOk(int xFinal, int yFinal)
        {
            return Math.Abs(GetX() - xFinal) <= 1 && Math.Abs(yFinal - GetY()) <= 1;
        }

        public override object Clone()
        {
            return new Roi(Name, Couleur, (Coord)Coord.Clone())
            {
                Captured = Captured,
                Depart = Depart
            };
        }
    }
}