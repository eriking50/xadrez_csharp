using System;
using table;

namespace chess
{
    class ChessGame
    {
        public Table table {get; private set; }
        public int turn {get; private set; }
        public Color activePlayer {get; private set; }
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

        public void doTurn(Position currentPosition, Position nextPosition)
        {
            doMove(currentPosition, nextPosition);
            turn++;
            changePlayer();
        }

        public void validateCurrentPosition(Position pos) 
        {
            if (table.piece(pos) == null)
            {
                throw new TableException("Não existe nenhuma peça na posição escolhida");
            }
            if(activePlayer != table.piece(pos).color)
            {
                throw new TableException("Esta peça não é da sua equipe");
            }
            if(!table.piece(pos).hasPossibleMoves())
            {
                throw new TableException("Não há movimentos possíveis para essa peça");
            }
        }

        public void validateNextPosition(Position current, Position next)
        {
            if (!table.piece(current).canMoveTo(next))
            {
                throw new TableException("Posição escolhida inválida");
            }
        }

        public void changePlayer()
        {
            if (activePlayer == Color.Red)
            {
                activePlayer = Color.Yellow;
            }
            else
            {
                activePlayer = Color.Red;
            }
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
