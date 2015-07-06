using System;

namespace Chess.Models.Pieces
{
    public class Tour
    : Pieces
    {
        public Tour(string name, Couleur couleur, Coord coord)
            : base(name, couleur, coord)
        {
        }

        public override bool IsMoveOk(int xFinal, int yFinal)
        {
            return (Math.Abs(xFinal - GetX()) == 0
                 && Math.Abs(GetY() - yFinal) > 0)
                 || (Math.Abs(xFinal - GetX()) > 0
                 && Math.Abs(GetY() - yFinal) == 0);
        }

        public override object Clone()
        {
            return new Tour(Name, Couleur, (Coord)Coord.Clone())
            {
                Captured = Captured,
                Depart = Depart
            };
        }
    }
}