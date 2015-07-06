using System.Web.Mvc;
using Chess.Models;

namespace Chess.Controllers
{
    public class ChessGameController : Controller
    {
        
        public ChessGameController()
        {
            var chessGame = ChessGame.Instance;
        }

        //
        // GET: /ChessGame/

        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Move(int xInit, int yInit, int xFinal, int yFinal)
        {
            return Json(new Reponse { Rep = ChessGame.Instance.Move(new Coord(xInit, yInit), new Coord(xFinal, yFinal)), Message = ChessGame.Instance.GetMessage()});
        }


        [HttpPost]
        public ActionResult GetCurrentPlayer()
        {
            return Json(ChessGame.Instance.GetCurrentPlayer());
        }

        class Reponse
        {
            public bool Rep;
            public string Message;
        }

    }
}
