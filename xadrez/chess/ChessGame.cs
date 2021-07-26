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
            Piece K = kingPiece(c);

            if (K == null)
            {
                throw new TableException($"Não tem rei da cor {getPlayerByColor(c)} no tabuleiro");
            }

            foreach (Piece p in piecesInGameByColor(enemyPlayer(c)))
            {
                bool[,] moves = p.possibleMoves();
                if (moves[K.position.row, K.position.column])
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
            placeNewPiece('a', 7, new Pawn(table, Color.Yellow));
            placeNewPiece('b', 7, new Pawn(table, Color.Yellow));
            placeNewPiece('c', 7, new Pawn(table, Color.Yellow));
            placeNewPiece('d', 7, new Pawn(table, Color.Yellow));
            placeNewPiece('e', 7, new Pawn(table, Color.Yellow));
            placeNewPiece('f', 7, new Pawn(table, Color.Yellow));
            placeNewPiece('g', 7, new Pawn(table, Color.Yellow));
            placeNewPiece('h', 7, new Pawn(table, Color.Yellow));
            placeNewPiece('a', 8, new Rook(table, Color.Yellow));
            placeNewPiece('h', 8, new Rook(table, Color.Yellow));
            placeNewPiece('c', 8, new Bishop(table, Color.Yellow));
            placeNewPiece('f', 8, new Bishop(table, Color.Yellow));
            placeNewPiece('b', 8, new Knight(table, Color.Yellow));
            placeNewPiece('g', 8, new Knight(table, Color.Yellow));
            placeNewPiece('d', 8, new Queen(table, Color.Yellow));
            placeNewPiece('e', 8, new King(table, Color.Yellow));

            placeNewPiece('a', 2, new Pawn(table, Color.Red));
            placeNewPiece('b', 2, new Pawn(table, Color.Red));
            placeNewPiece('c', 2, new Pawn(table, Color.Red));
            placeNewPiece('d', 2, new Pawn(table, Color.Red));
            placeNewPiece('e', 2, new Pawn(table, Color.Red));
            placeNewPiece('f', 2, new Pawn(table, Color.Red));
            placeNewPiece('g', 2, new Pawn(table, Color.Red));
            placeNewPiece('h', 2, new Pawn(table, Color.Red));
            placeNewPiece('a', 1, new Rook(table, Color.Red));
            placeNewPiece('h', 1, new Rook(table, Color.Red));
            placeNewPiece('c', 1, new Bishop(table, Color.Red));
            placeNewPiece('f', 1, new Bishop(table, Color.Red));
            placeNewPiece('b', 1, new Knight(table, Color.Red));
            placeNewPiece('g', 1, new Knight(table, Color.Red));
            placeNewPiece('d', 1, new Queen(table, Color.Red));
            placeNewPiece('e', 1, new King(table, Color.Red));

        }
    }
}
