using System;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Models
{
    public class Echiquier : ICloneable
    {

        private readonly Dictionary<Couleur, Jeu> _jeux;

        private bool _isAClone;

        private Couleur _jeuCourant;
        private string _message;

        public Echiquier()
            : this(Couleur.Blanc)
        {

        }

        public Echiquier(Couleur jeuCourant)
            : this(jeuCourant, new Jeu(Couleur.Blanc), new Jeu(Couleur.Noir))
        {

        }

        private Echiquier(Couleur jeuCourant, Jeu jeuBlanc, Jeu jeuNoir)
        {
            _jeuCourant = jeuCourant;

            _isAClone = false;

            _jeux = new Dictionary<Couleur, Jeu>
            {
                {Couleur.Blanc, jeuBlanc},
                {Couleur.Noir, jeuNoir}
            };

        }

        public string Message
        {
            get { return _message; }
        }

        public Couleur GetCurrentPlayer
        {
            get { return _jeuCourant; }
        }

        public void SwitchJoueur()
        {
            _jeuCourant = Advairsaire().Couleur;
        }

        public bool Move(Coord init, Coord final)
        {
            bool rep = false;
            if (Coord.coordonnees_valides(final)
                && !init.Equals(final)
                && JeuCourant().IsPieceHere(init)
                && !JeuCourant().IsPieceHere(final)
                && JeuCourant().IsMoveOk(init, final)
                //&& !ColissionInPath(init, final)
                )
            {
                /*if (!_isAClone && MetRoiCourantEnDanger(init, final))
                {
                    _message = "Ce mouvement met votre roi en danger !!!";
                    return false;
                }*/

                if (Advairsaire().IsPieceHere(final))
                {

                    if (JeuCourant().IsPion(init) && !Coord.IsDiagonal(init, final))
                    {
                        _message = "Le pion ne mange qu'en diagonale avant !";
                        return false;
                    }
                    Advairsaire().Capture(final);
                    rep = JeuCourant().Move(init, final);
                }
                else
                {
                    if (JeuCourant().IsPion(init) && Coord.IsDiagonal(init, final))
                    {
                        _message = "Le pion se deplace en avant !";
                        return false;
                    }

                    rep = JeuCourant().Move(init, final);
                }
            }
            else
            {
                _message = "Il y a une/plusieurs erreur : ";
                if (!Coord.coordonnees_valides(final))
                {
                    _message += "\n\t-> Coordonnées hors échiquier";
                }
                if (!JeuCourant().IsPieceHere(init))
                {
                    _message += "\n\t-> Ce n'est pas une pièce de votre équipe";
                }
                if (JeuCourant().IsPieceHere(final))
                {
                    _message += "\n\t-> Vous n'avez pas le droit de manger une pièce de votre équipe";
                }
                if (init.Equals(final))
                {
                    _message += "\n\t-> Déplacement sur la même case";
                }
                if (!JeuCourant().IsMoveOk(init, final))
                {
                    _message += "\n\t-> Déplacement interdit pour cette pièce : " + JeuCourant().GetPieceName(init);
                }
                if (ColissionInPath(init, final))
                {
                    _message += "\n\t-> Il y a une pièce sur la trajectoire";
                }
            }
            return rep;
        }

        private bool MetRoiCourantEnDanger(Coord init, Coord final)
        {
            Echiquier mondeParallele = (Echiquier)Clone();
            Coord roi = mondeParallele.JeuCourant().GetKingCoord();

            mondeParallele.Move(init, final);
            return mondeParallele.RoiEnDanger(roi);
        }

        private bool RoiEnDanger(Coord roi)
        {
            return JeuCourant().GetPiecesCoords().Any(c => JeuCourant().IsMoveOk(c,roi) && !ColissionInPath(c, roi));
        }

        private Jeu JeuCourant()
        {
            return _jeux[_jeuCourant];
        }

        private Jeu Advairsaire()
        {
            return _jeuCourant == Couleur.Blanc ? _jeux[Couleur.Noir] : _jeux[Couleur.Blanc];
        }

        private bool ColissionInPath(Coord init, Coord final)
        {
            var list = JeuCourant().GetPath(init, final);
            if (!list.Any()) return false;
            var rep2 = list.Any(coord => Advairsaire().IsPieceHere(coord) || JeuCourant().IsPieceHere(coord));
            return rep2;
        }

        public override string ToString()
        {
            return String.Join("\n--------------\n", _jeux.Values.Select(item => item.ToString()));
        }

        public object Clone()
        {
            return new Echiquier(_jeuCourant, (Jeu)_jeux[Couleur.Blanc].Clone(), (Jeu)_jeux[Couleur.Noir].Clone()) { _isAClone = true };
        }
    }
}