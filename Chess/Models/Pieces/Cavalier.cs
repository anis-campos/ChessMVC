using System;

namespace Chess.Models.Pieces
{
    public class Cavalier : Pieces
    {
        public Cavalier(string name, Couleur couleur, Coord coord)
            : base(name, couleur, coord)
        {
        }

        public override bool IsMoveOk(int xFinal, int yFinal)
        {
            return (Math.Abs(xFinal - GetX()) == 2 && Math.Abs(yFinal - GetY()) == 1)
              || (Math.Abs(xFinal - GetX()) == 1 && Math.Abs(yFinal - GetY()) == 2);
        }

        public override object Clone()
        {
            return new Cavalier(Name, Couleur, (Coord)Coord.Clone())
            {
                Captured = Captured,
                Depart = Depart
            };
        }
    }
}