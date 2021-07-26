using System.Collections.Generic;
using table;

namespace chess
{
    class ChessGame
    {
        public Table table {get; private set; }
        public int turn {get; private set; }
        public Color activePlayer {get; private set; }
        public bool isEnded {get; private set; }
        private HashSet<Piece> inGamePieces;
        private HashSet<Piece> capturedPieces;

        public ChessGame() 
        {
            table = new Table(8, 8);
            turn = 1;
            activePlayer = Color.Red;
            isEnded = false;
            inGamePieces = new HashSet<Piece>();
            capturedPieces = new HashSet<Piece>();
            placePieces();
        }

        public void doMove(Position currentPosition, Position nextPosition)
        {
            Piece p = table.removePiece(currentPosition);
            p.increaseMovesCount();
            Piece capturedPiece = table.removePiece(nextPosition);
            table.placePiece(p, nextPosition);
            if(capturedPiece != null) 
            {
                capturedPieces.Add(capturedPiece);
            }
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

        public HashSet<Piece> capturedByColor(Color c)
        {
            HashSet<Piece> captured = new HashSet<Piece>();
            foreach ( Piece p in capturedPieces)
            {
                if (p.color == c)
                {
                    captured.Add(p);
                }
            }
            return captured;
        }

        public HashSet<Piece> piecesInGameByColor(Color c) 
        {
            HashSet<Piece> inGame = new HashSet<Piece>();
            foreach ( Piece p in inGamePieces)
            {
                if (p.color == c)
                {
                    inGame.Add(p);
                }
            }
            inGame.ExceptWith(capturedByColor(c));
            return inGame;
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

        public void placeNewPiece(char col, int row, Piece p) 
        {
            table.placePiece(p, new ChessPosition(col, row).toPosition());
            inGamePieces.Add(p);
        }

        private void placePieces() 
        {
            placeNewPiece('a', 8, new Tower(table, Color.Yellow));
            placeNewPiece('h', 8, new Tower(table, Color.Yellow));
            placeNewPiece('d', 8, new King(table, Color.Yellow));

            placeNewPiece('a', 1, new Tower(table, Color.Red));
            placeNewPiece('h', 1, new Tower(table, Color.Red));
            placeNewPiece('e', 1, new King(table, Color.Red));

        }
    }
}
