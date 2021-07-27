using System.Collections.Generic;
using TableGame;

namespace Chess
{
    class ChessGame
    {
        public Table Table {get; private set; }
        public int Turn {get; private set; }
        public Color ActivePlayer {get; private set; }
        public bool IsEnded {get; private set; }
        public bool IsCheck {get; private set; }
        public Piece EnPassantPawn {get; private set; }
        private HashSet<Piece> inGamePieces;
        private HashSet<Piece> capturedPieces;

        public ChessGame() 
        {
            Table = new Table(8, 8);
            Turn = 1;
            ActivePlayer = Color.Red;
            IsEnded = false;
            IsCheck = false;
            EnPassantPawn = null;
            inGamePieces = new HashSet<Piece>();
            capturedPieces = new HashSet<Piece>();
            PlacePieces();
        }

        public string GetPlayerByColor(Color c) 
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

        public Piece DoMove(Position currentPosition, Position nextPosition)
        {
            Piece p = Table.RemovePiece(currentPosition);
            p.IncreaseMovesCount();
            Piece capturedPiece = Table.RemovePiece(nextPosition);
            Table.PlacePiece(p, nextPosition);
            if(capturedPiece != null) 
            {
                capturedPieces.Add(capturedPiece);
            }
            //Special move minor castling
            if (p is King && nextPosition.Column == currentPosition.Column + 2)
            {
                Position currentPosR = new Position(currentPosition.Row, currentPosition.Column + 3);
                Position nextPosR = new Position(currentPosition.Row, currentPosition.Column + 1);
                Piece R = Table.RemovePiece(currentPosR);
                R.IncreaseMovesCount();
                Table.PlacePiece(R, nextPosR);
            }
            //Special move major castling
            if (p is King && nextPosition.Column == currentPosition.Column - 2)
            {
                Position currentPosR = new Position(currentPosition.Row, currentPosition.Column - 4);
                Position nextPosR = new Position(currentPosition.Row, currentPosition.Column - 1);
                Piece R = Table.RemovePiece(currentPosR);
                R.IncreaseMovesCount();
                Table.PlacePiece(R, nextPosR);
            }

            //Special move en passant
            if (p is Pawn)
            {
                if (currentPosition.Column != nextPosition.Column && capturedPiece == null)
                {
                    Position posP;
                    if (p.Color == Color.Red)
                    {
                        posP = new Position(nextPosition.Row + 1, nextPosition.Column);

                    }
                    else
                    {
                        posP = new Position(nextPosition.Row - 1, nextPosition.Column);
                    }
                    capturedPiece = Table.RemovePiece(posP);
                    capturedPieces.Add(capturedPiece);
                }
            }

        return capturedPiece;
        }

        public void UndoMove(Position currentPosition, Position nextPosition, Piece capturedPiece)
        {
            Piece p = Table.RemovePiece(nextPosition);
            p.DecreaseMovesCount();
            if (capturedPiece != null)
            {
                Table.PlacePiece(capturedPiece, nextPosition);
                capturedPieces.Remove(capturedPiece);
            }
            Table.PlacePiece(p, currentPosition);

            //Special move minor castling
            if (p is King && nextPosition.Column == currentPosition.Column + 2)
            {
                Position currentPosR = new Position(currentPosition.Row, currentPosition.Column + 3);
                Position nextPosR = new Position(currentPosition.Row, currentPosition.Column + 1);
                Piece R = Table.RemovePiece(nextPosR);
                R.DecreaseMovesCount();
                Table.PlacePiece(R, currentPosR);
            }
            //Special move major castling
            if (p is King && nextPosition.Column == currentPosition.Column - 2)
            {
                Position currentPosR = new Position(currentPosition.Row, currentPosition.Column - 4);
                Position nextPosR = new Position(currentPosition.Row, currentPosition.Column - 1);
                Piece R = Table.RemovePiece(nextPosR);
                R.DecreaseMovesCount();
                Table.PlacePiece(R, currentPosR);
            }
            //Special move en passant
            if (p is Pawn)
            {
                if (currentPosition.Column != nextPosition.Column && capturedPiece == EnPassantPawn)
                {
                    Piece pawn = Table.RemovePiece(nextPosition);
                    Position posP;
                    if (p.Color == Color.Red)
                    {
                        posP = new Position(3, nextPosition.Column);
                    }
                    else
                    {
                        posP = new Position(4, nextPosition.Column);
                    }
                    Table.PlacePiece(pawn, posP);
                }
            }
        }

        public void DoTurn(Position currentPosition, Position nextPosition)
        {
            Piece capturedPiece = DoMove(currentPosition, nextPosition);
            
            if (IsKingInCheck(ActivePlayer))
            {
                UndoMove(currentPosition, nextPosition, capturedPiece);
                throw new TableException("Você não pode se colocar em xeque!");
            }

            Piece p = Table.Piece(nextPosition);

            //Special move Promotion

            if (p is Pawn)
            {
                if ((p.Color == Color.Red && nextPosition.Row == 0) || (p.Color == Color.Yellow && nextPosition.Row == 7))
                {
                    p = Table.RemovePiece(nextPosition);
                    inGamePieces.Remove(p);
                    Piece queen = new Queen(Table, p.Color);
                    Table.PlacePiece(queen, nextPosition);
                    inGamePieces.Add(queen);
                    
                }
            }

            if (IsKingInCheck(EnemyPlayer(ActivePlayer)))
            {
                IsCheck = true;
            }
            else
            {
                IsCheck = false;
            }

            if (IsCheckmate(EnemyPlayer(ActivePlayer)))
            {
                IsEnded = true;
            }
            else
            {
                Turn++;
                ChangePlayer();
            }


            //Special move En Passant
            if (p is Pawn && (nextPosition.Row == currentPosition.Row + 2 || nextPosition.Row == currentPosition.Row - 2))
            {
                EnPassantPawn = p;
            }
            else
            {
                EnPassantPawn = null;
            }
        }

        public void ValidateCurrentPosition(Position pos) 
        {
            if (Table.Piece(pos) == null)
            {
                throw new TableException("Não existe nenhuma peça na posição escolhida");
            }
            if(ActivePlayer != Table.Piece(pos).Color)
            {
                throw new TableException("Esta peça não é da sua equipe");
            }
            if(!Table.Piece(pos).HasPossibleMoves())
            {
                throw new TableException("Não há movimentos possíveis para essa peça");
            }
        }

        public void ValidateNextPosition(Position current, Position next)
        {
            if (!Table.Piece(current).CanMoveTo(next))
            {
                throw new TableException("Posição escolhida inválida");
            }
        }

        public HashSet<Piece> CapturedByColor(Color c)
        {
            HashSet<Piece> captured = new HashSet<Piece>();
            foreach ( Piece p in capturedPieces)
            {
                if (p.Color == c)
                {
                    captured.Add(p);
                }
            }
            return captured;
        }

        public HashSet<Piece> PiecesInGameByColor(Color c) 
        {
            HashSet<Piece> inGame = new HashSet<Piece>();
            foreach ( Piece p in inGamePieces)
            {
                if (p.Color == c)
                {
                    inGame.Add(p);
                }
            }
            inGame.ExceptWith(CapturedByColor(c));
            return inGame;
        }

        public void ChangePlayer()
        {
            if (ActivePlayer == Color.Red)
            {
                ActivePlayer = Color.Yellow;
            }
            else
            {
                ActivePlayer = Color.Red;
            }
        }

        public Color EnemyPlayer(Color c)
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

        private Piece KingPiece(Color c)
        {
            foreach (Piece p in PiecesInGameByColor(c))
            { 
                if (p is King)
                {
                    return p;
                }
            }
            return null;
        }

        public bool IsKingInCheck(Color c)
        {
            Piece K = KingPiece(c);

            if (K == null)
            {
                throw new TableException($"Não tem rei da cor {GetPlayerByColor(c)} no tabuleiro");
            }

            foreach (Piece p in PiecesInGameByColor(EnemyPlayer(c)))
            {
                bool[,] moves = p.PossibleMoves();
                if (moves[K.Position.Row, K.Position.Column])
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsCheckmate(Color c)
        {
            if (!IsKingInCheck(c))
            {
                return false;
            }
            else
            {
                foreach (Piece p in PiecesInGameByColor(c))
                {
                    bool[,] moves = p.PossibleMoves();
                    for (int i = 0; i < Table.Rows; i++)
                    {
                        for (int j = 0; j < Table.Columns; j++)
                        {
                            if (moves[i, j])
                            {
                                Position currentPosition = p.Position;
                                Position nextPosition = new Position(i, j); 
                                Piece capturedPiece = DoMove(currentPosition, nextPosition);
                                bool testCheck = IsKingInCheck(c);
                                UndoMove(currentPosition, nextPosition, capturedPiece);
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

        public void PlaceNewPiece(char col, int row, Piece p) 
        {
            Table.PlacePiece(p, new ChessPosition(col, row).ToPosition());
            inGamePieces.Add(p);
        }

        private void PlacePieces() 
        {
            PlaceNewPiece('a', 7, new Pawn(Table, Color.Yellow, this));
            PlaceNewPiece('b', 7, new Pawn(Table, Color.Yellow, this));
            PlaceNewPiece('c', 7, new Pawn(Table, Color.Yellow, this));
            PlaceNewPiece('d', 7, new Pawn(Table, Color.Yellow, this));
            PlaceNewPiece('e', 7, new Pawn(Table, Color.Yellow, this));
            PlaceNewPiece('f', 7, new Pawn(Table, Color.Yellow, this));
            PlaceNewPiece('g', 7, new Pawn(Table, Color.Yellow, this));
            PlaceNewPiece('h', 7, new Pawn(Table, Color.Yellow, this));
            PlaceNewPiece('a', 8, new Rook(Table, Color.Yellow));
            PlaceNewPiece('h', 8, new Rook(Table, Color.Yellow));
            PlaceNewPiece('c', 8, new Bishop(Table, Color.Yellow));
            PlaceNewPiece('f', 8, new Bishop(Table, Color.Yellow));
            PlaceNewPiece('b', 8, new Knight(Table, Color.Yellow));
            PlaceNewPiece('g', 8, new Knight(Table, Color.Yellow));
            PlaceNewPiece('d', 8, new Queen(Table, Color.Yellow));
            PlaceNewPiece('e', 8, new King(Table, Color.Yellow, this));

            PlaceNewPiece('a', 2, new Pawn(Table, Color.Red, this));
            PlaceNewPiece('b', 2, new Pawn(Table, Color.Red, this));
            PlaceNewPiece('c', 2, new Pawn(Table, Color.Red, this));
            PlaceNewPiece('d', 2, new Pawn(Table, Color.Red, this));
            PlaceNewPiece('e', 2, new Pawn(Table, Color.Red, this));
            PlaceNewPiece('f', 2, new Pawn(Table, Color.Red, this));
            PlaceNewPiece('g', 2, new Pawn(Table, Color.Red, this));
            PlaceNewPiece('h', 2, new Pawn(Table, Color.Red, this));
            PlaceNewPiece('a', 1, new Rook(Table, Color.Red));
            PlaceNewPiece('h', 1, new Rook(Table, Color.Red));
            PlaceNewPiece('c', 1, new Bishop(Table, Color.Red));
            PlaceNewPiece('f', 1, new Bishop(Table, Color.Red));
            PlaceNewPiece('b', 1, new Knight(Table, Color.Red));
            PlaceNewPiece('g', 1, new Knight(Table, Color.Red));
            PlaceNewPiece('d', 1, new Queen(Table, Color.Red));
            PlaceNewPiece('e', 1, new King(Table, Color.Red, this));

        }
    }
}
