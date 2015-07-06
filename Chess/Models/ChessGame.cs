namespace Chess.Models
{
    public class ChessGame
    {

        private static ChessGame _instance;

        private Echiquier _echiquier;

        private ChessGame(Echiquier echiquier)
        {
            _echiquier = echiquier;
        }

        private ChessGame():this(new Echiquier()) { }

        public static ChessGame Instance
        {
            get { return _instance ?? (_instance = new ChessGame()); }
        }

        public bool Move(Coord init, Coord final)
        {
            var rep = false;
            rep = _echiquier.Move(init, final);
            if(rep)
                _echiquier.SwitchJoueur();
            return rep;
        }

        public string GetMessage()
        {
            return _echiquier.Message;
        }


        public Couleur GetCurrentPlayer()
        {
            return _echiquier.GetCurrentPlayer;
        }
    }
}