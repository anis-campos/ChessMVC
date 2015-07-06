var tdSource;

function getCoord(square) {
    var y = square.parent().data("y");
    var x = square.data("x");

    return { x: x, y: y };
}


function initChessBoard() {

    var map = getInitMap();

    $("td").each(function () {

        var coord = getCoord($(this));

        var couleur = map[coord.y][coord.x].match(/.*-Blanc/) ? "BLANC" : "NOIR";
        $(this).children().addClass(map[coord.y][coord.x]);
        $(this).children().attr("data-Couleur", couleur);

        $(this).children().draggable({
            snap: ".case",
            create: function () { $(this).data("position", $(this).position()); },
            cursor: "pointer",
            start: function () { tdSource = $(this).parent(); $(this).stop(true, true); },
            containment: "#Echiquier"
        });

        $(this).droppable({
            drop: function (event, ui) {
                if (Move($(this)))
                    centrerPiece(ui.draggable, $(this));
                else
                    centrerPiece(ui.draggable, tdSource);
            }
        });
    });


}

function Move(target) {
    var rep;
    var init = getCoord(tdSource);
    var final = getCoord(target);
    var coords = {
        xInit: init.x,
        yInit: init.y,
        xFinal: final.x,
        yFinal: final.y
    };
    $.ajax({
        url: "/ChessGame/Move",
        type: "post",
        async: false,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(coords),
        success: function (data) {
            rep = data.Rep;
            showMessage(data.Message);
        }
    });
    return rep;
}

function showMessage(message) {
    $("#messages").append(message);
}

function getInitMap() {

    var map = new Array(8);
    for (var y = 0; y < 8; y++) {
        map[y] = new Array(8);
        for (var x = 0; x < 8; x++) {

            map[y][x] = "";
        }
    }

    map[0][0] = "Tour-Noir";
    map[0][1] = "Cavalier-Noir";
    map[0][2] = "Fou-Noir";
    map[0][3] = "Roi-Noir";
    map[0][4] = "Reine-Noir";
    map[0][5] = "Fou-Noir";
    map[0][6] = "Cavalier-Noir";
    map[0][7] = "Tour-Noir";

    map[1] = ["Pion-Noir", "Pion-Noir", "Pion-Noir", "Pion-Noir", "Pion-Noir", "Pion-Noir", "Pion-Noir", "Pion-Noir"];

    map[6] = ["Pion-Blanc", "Pion-Blanc", "Pion-Blanc", "Pion-Blanc", "Pion-Blanc", "Pion-Blanc", "Pion-Blanc", "Pion-Blanc"];

    map[7][0] = "Tour-Blanc";
    map[7][1] = "Cavalier-Blanc";
    map[7][2] = "Fou-Blanc";
    map[7][3] = "Reine-Blanc";
    map[7][4] = "Roi-Blanc";
    map[7][5] = "Fou-Blanc";
    map[7][6] = "Cavalier-Blanc";
    map[7][7] = "Tour-Blanc";

    return map;
}


function disableJeu(jeu, bool) {
    $("div[data-Couleur='" + jeu + "']").draggable(bool);
}

function centrerPiece(piece, target) {
    var topMove = target.position().top - piece.data("position").top + (target.outerHeight(true) - piece.outerHeight(true)) / 2;
    var leftMove = target.position().left - piece.data("position").left + (target.outerWidth(true) - piece.outerWidth(true)) / 2;
    piece.animate({ top: topMove, left: leftMove }, { duration: 600, easing: "easeOutBack" });
}