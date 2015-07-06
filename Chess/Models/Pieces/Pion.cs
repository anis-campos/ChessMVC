using System;

namespace Chess.Models.Pieces
{
    public class Pion : Pieces
    {
        public Pion(string name, Couleur couleur, Coord coord) : base(name, couleur, coord)
        {
        }

        public bool IsMoveDiagOk(int xFinal, int yFinal)
        {

            if (Math.Abs(GetX() - xFinal) != 1)
            {
                return false;
            }
            switch (Couleur)
            {
                case Couleur.Blanc:
                    return (GetY() - yFinal == 1);

                case Couleur.Noir:
                    return (yFinal - GetY() == 1);
                default:
                    return false;
            }
        }

        public override bool IsMoveOk(int xFinal, int yFinal)
        {
            if (xFinal != GetX())
            {
                return false;
            }

            var deltaY = yFinal - GetY();

            switch (Couleur)
            {
                case Couleur.Blanc:
                    return deltaY < 0 && deltaY >= (Depart ? -2 : -1);

                case Couleur.Noir:
                    return deltaY > 0 && deltaY <= (Depart ? 2 : 1);
                default:
                    return false;
            }
        }

        public override object Clone()
        {
            return new Pion(Name, Couleur, (Coord)Coord.Clone())
            {
                Captured = Captured,
                Depart = Depart
            };
        }
    }
}