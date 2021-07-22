using System;
using table;

namespace chess
{
    class ChessGame
    {
        public Table table {get; private set; }
        private int turn;
        private Color activePlayer;
        public bool isEnded {get; private set; }

        public ChessGame() 
        {
            table = new Table(8, 8);
            turn = 1;
            activePlayer = Color.Red;
            isEnded = false;
            placePieces();
        }

        public void doMove(Position currentPosition, Position nextPosition)
        {
            Piece p = table.removePiece(currentPosition);
            p.increaseMovesCount();
            Piece capturedPiece = table.removePiece(nextPosition);
            table.placePiece(p, nextPosition);
        }

        private void placePieces() 
        {
                table.placePiece(new Tower(table, Color.Yellow), new ChessPosition('a', 8).toPosition());
				table.placePiece(new Tower(table, Color.Yellow), new ChessPosition('h', 8).toPosition());
				table.placePiece(new King(table, Color.Yellow), new ChessPosition('d', 8).toPosition());

				table.placePiece(new Tower(table, Color.Red), new ChessPosition('h', 1).toPosition());
				table.placePiece(new Tower(table, Color.Red), new ChessPosition('a', 1).toPosition());
				table.placePiece(new King(table, Color.Red), new ChessPosition('e', 1).toPosition());

        }
    }
}
