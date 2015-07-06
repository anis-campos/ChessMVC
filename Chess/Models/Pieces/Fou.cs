using System;

namespace Chess.Models.Pieces
{
    public class Fou : Pieces
    {
        public Fou(string name, Couleur couleur, Coord coord)
            : base(name, couleur, coord)
        {
        }

        public override bool IsMoveOk(int xFinal, int yFinal)
        {
            return Math.Abs(xFinal - GetX()) == Math.Abs(yFinal - GetY());
        }

        public override object Clone()
        {
            return new Fou(Name, Couleur, (Coord)Coord.Clone())
            {
                Captured = Captured,
                Depart = Depart
            };

        }
    }
}