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
        public bool isCheck {get; private set; }
        private HashSet<Piece> inGamePieces;
        private HashSet<Piece> capturedPieces;

        public ChessGame() 
        {
            table = new Table(8, 8);
            turn = 1;
            activePlayer = Color.Red;
            isEnded = false;
            isCheck = false;
            inGamePieces = new HashSet<Piece>();
            capturedPieces = new HashSet<Piece>();
            placePieces();
        }

        public string getPlayerByColor(Color c) 
        {
            switch (c)    
            {
                case Color.Blue: 
                {
                    return "Azul";
                }
                case Color.Red: 
                {
                    return "Vermelho";
                }
                case Color.Green: 
                {
                    return "Verde";
                }
                case Color.Yellow: 
                {
                    return "Amarelo";
                }
                default:
                    return "Inválido";
            }
        }

        public Piece doMove(Position currentPosition, Position nextPosition)
        {
            Piece p = table.removePiece(currentPosition);
            p.increaseMovesCount();
            Piece capturedPiece = table.removePiece(nextPosition);
            table.placePiece(p, nextPosition);
            if(capturedPiece != null) 
            {
                capturedPieces.Add(capturedPiece);
            }
            return capturedPiece;
        }

        public void undoMove(Position currentPosition, Position nextPosition, Piece capturedPiece)
        {
            Piece p = table.removePiece(nextPosition);
            p.decreaseMovesCount();
            if (capturedPiece != null)
            {
                table.placePiece(capturedPiece, nextPosition);
                capturedPieces.Remove(capturedPiece);
            }

            table.placePiece(p, currentPosition);
        }

        public void doTurn(Position currentPosition, Position nextPosition)
        {
            Piece capturedPiece = doMove(currentPosition, nextPosition);
            
            if (isKingInCheck(activePlayer))
            {
                undoMove(currentPosition, nextPosition, capturedPiece);
                throw new TableException("Você não pode se colocar em xeque!");
            }

            if (isKingInCheck(enemyPlayer(activePlayer)))
            {
                isCheck = true;
            }
            else
            {
                isCheck = false;
            }

            if (isCheckmate(enemyPlayer(activePlayer)))
            {
                isEnded = true;
            }
            else
            {
                turn++;
                changePlayer();
            }
            
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

        public Color enemyPlayer(Color c)
        {
            if (c == Color.Red)
            {
                return Color.Yellow;
            }
            else
            {
                return Color.Red;
            }
        }

        private Piece kingPiece(Color c)
        {
            foreach (Piece p in piecesInGameByColor(c))
            { 
                if (p is King)
                {
                    return p;
                }
            }
            return null;
        }

        public bool isKingInCheck(Color c)
        {
            Piece R = kingPiece(c);

            if (R == null)
            {
                throw new TableException($"Não tem rei da cor {getPlayerByColor(c)} no tabuleiro");
            }

            foreach (Piece p in piecesInGameByColor(enemyPlayer(c)))
            {
                bool[,] moves = p.possibleMoves();
                if (moves[R.position.row, R.position.column])
                {
                    return true;
                }
            }
            return false;
        }

        public bool isCheckmate(Color c)
        {
            if (!isKingInCheck(c))
            {
                return false;
            }
            else
            {
                foreach (Piece p in piecesInGameByColor(c))
                {
                    bool[,] moves = p.possibleMoves();
                    for (int i = 0; i < table.rows; i++)
                    {
                        for (int j = 0; j < table.columns; j++)
                        {
                            if (moves[i, j])
                            {
                                Position currentPosition = p.position;
                                Position nextPosition = new Position(i, j); 
                                Piece capturedPiece = doMove(currentPosition, nextPosition);
                                bool testCheck = isKingInCheck(c);
                                undoMove(currentPosition, nextPosition, capturedPiece);
                                if (!testCheck)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
                return true;
            }
        }

        public void placeNewPiece(char col, int row, Piece p) 
        {
            table.placePiece(p, new ChessPosition(col, row).toPosition());
            inGamePieces.Add(p);
        }

        private void placePieces() 
        {
            // placeNewPiece('a', 8, new Tower(table, Color.Yellow));
            placeNewPiece('b', 8, new Tower(table, Color.Yellow));
            placeNewPiece('a', 8, new King(table, Color.Yellow));

            placeNewPiece('h', 7, new Tower(table, Color.Red));
            placeNewPiece('c', 1, new Tower(table, Color.Red));
            placeNewPiece('d', 1, new King(table, Color.Red));

        }
    }
}
