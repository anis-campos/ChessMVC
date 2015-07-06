using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Models.Pieces;

namespace Chess.Models
{
    public class Jeu : ICloneable
    {

        private readonly Couleur _couleur;

        private readonly List<Pieces.Pieces> _pieces;

        public Jeu(Couleur couleur)
            : this(couleur, couleur == Couleur.Blanc ? GetJeuBlanc() : GetJeuNoir())
        {

        }

        public Jeu(Couleur couleur, List<Pieces.Pieces> pieces)
        {
            _couleur = couleur;
            _pieces = pieces;
        }

        public Couleur Couleur
        {
            get { return _couleur; }
        }

        public static List<Pieces.Pieces> GetJeuBlanc()
        {
            var list = new List<Pieces.Pieces>
            {
                new Tour("B_Tr1", Couleur.Blanc, new Coord(0, 7)),
                new Cavalier("B_Ca1", Couleur.Blanc, new Coord(1, 7)),
                new Fou("B_Fo1", Couleur.Blanc, new Coord(2, 7)),
                new Reine("B_Re1", Couleur.Blanc, new Coord(3, 7)),
                new Roi("B_Ro1", Couleur.Blanc, new Coord(4, 7)),
                new Fou("B_Fo2", Couleur.Blanc, new Coord(5, 7)),
                new Cavalier("B_Ca2", Couleur.Blanc, new Coord(6, 7)),
                new Tour("B_Tr2", Couleur.Blanc, new Coord(7, 7))
            };
            for (var i = 0; i < 8; i++)
                list.Add(new Pion("B_Pi" + (i + 1), Couleur.Blanc, new Coord(i, 6)));
            return list;
        }

        public static List<Pieces.Pieces> GetJeuNoir()
        {
            var list = new List<Pieces.Pieces>
            {
                new Tour("N_Tr1", Couleur.Noir, new Coord(0, 0)),
                new Cavalier("N_Ca1", Couleur.Noir, new Coord(1, 0)),
                new Fou("N_Fo1", Couleur.Noir, new Coord(2, 0)),
                new Reine("N_Re1", Couleur.Noir, new Coord(3, 0)),
                new Roi("N_Ro1", Couleur.Noir, new Coord(4, 0)),
                new Fou("N_Fo2", Couleur.Noir, new Coord(5, 0)),
                new Cavalier("N_Ca2", Couleur.Noir, new Coord(6, 0)),
                new Tour("N_Tr2", Couleur.Noir, new Coord(7, 0))
            };
            for (var i = 0; i < 8; i++)
                list.Add(new Pion("N_Pi" + (i + 1), Couleur.Noir, new Coord(i, 1)));
            return list;

        }

        public override string ToString()
        {
            return string.Join("\n", _pieces.Select(item => item.ToString()).ToArray());
        }

        public object Clone()
        {
            return new Jeu(_couleur, _pieces.Select(item => (Pieces.Pieces)item.Clone()).ToList());
        }

        private Pieces.Pieces GetPiece(Coord c)
        {
            Pieces.Pieces rep;
            return rep = _pieces.FirstOrDefault(piece => piece.IsHere(c));
        }

        public bool IsPieceHere(Coord init)
        {
            bool rep;
            return rep = GetPiece(init) != null;
        }

        public bool IsMoveOk(Coord init, Coord final)
        {
            var piece = GetPiece(init);
            return piece != null && piece.IsMoveOk(final);
        }

        public bool IsPion(Coord init)
        {
            var piece = GetPiece(init);
            return piece is Pion;
        }

        public bool IsCavalier(Coord init)
        {
            var piece = GetPiece(init);
            return piece is Cavalier;
        }

        public bool Capture(Coord final)
        {
            var piece = GetPiece(final);
            return piece != null && piece.Capture();
        }

        public bool Move(Coord init, Coord final)
        {
            var piece = GetPiece(init);
            return piece != null && piece.Move(final);
        }

        public string GetPieceName(Coord init)
        {
            var piece = GetPiece(init);
            return piece != null ? piece.GetName() : "";
        }

        public List<Coord> GetPath(Coord init, Coord final)
        {
            var piece = GetPiece(init);
            return piece != null ? piece.GetPath(final) : new List<Coord>();
        }

        public List<Coord> GetPiecesCoords()
        {
            return _pieces.Select(p => new Coord(p.GetX(), p.GetY())).ToList();
        }

        public Coord GetKingCoord()
        {
            var roi = _pieces.First(elem => elem is Roi);
            return new Coord(roi.GetX(), roi.GetY());
        }
    }



}