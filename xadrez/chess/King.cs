using TableGame;

namespace Chess
{
    class King : Piece
    {
        private ChessGame game;
        public King(Table tab, Color color, ChessGame game) : base(tab, color)
        {
            this.game = game;
        }

        public override string ToString()
        {
            return "R";
        }

        public bool CanMove(Position pos) 
        {
            Piece p = Tab.Piece(pos);
            return p == null || p.Color != Color;
        }

        public bool TestCastling(Position pos)
        {
            Piece p = Tab.Piece(pos);
            return p != null && p is Rook && p.Color == Color && p.MovesCount == 0;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] moveList = new bool[Tab.Rows,Tab.Columns];

            Position pos = new Position(0, 0);

            //up
            pos.SetValues(Position.Row - 1, Position.Column);
            if (Tab.IsValidPosition(pos) && CanMove(pos))
            {
                moveList[pos.Row, pos.Column] = true;
            }
            //up + right
            pos.SetValues(Position.Row - 1, Position.Column + 1);
            if (Tab.IsValidPosition(pos) && CanMove(pos))
            {
                moveList[pos.Row, pos.Column] = true;
            }
            //right
            pos.SetValues(Position.Row, Position.Column + 1);
            if (Tab.IsValidPosition(pos) && CanMove(pos))
            {
                moveList[pos.Row, pos.Column] = true;
            }
            //down + right
            pos.SetValues(Position.Row + 1, Position.Column + 1);
            if (Tab.IsValidPosition(pos) && CanMove(pos))
            {
                moveList[pos.Row, pos.Column] = true;
            }                                              
            //down
            pos.SetValues(Position.Row + 1, Position.Column);
            if (Tab.IsValidPosition(pos) && CanMove(pos))
            {
                moveList[pos.Row, pos.Column] = true;
            }            
            //down + left
            pos.SetValues(Position.Row + 1, Position.Column - 1);
            if (Tab.IsValidPosition(pos) && CanMove(pos))
            {
                moveList[pos.Row, pos.Column] = true;
            }
            //left
            pos.SetValues(Position.Row, Position.Column - 1);
            if (Tab.IsValidPosition(pos) && CanMove(pos))
            {
                moveList[pos.Row, pos.Column] = true;
            }            
            //up + left
            pos.SetValues(Position.Row - 1, Position.Column - 1);
            if (Tab.IsValidPosition(pos) && CanMove(pos))
            {
                moveList[pos.Row, pos.Column] = true;
            }

            //Special move = Castling
            if (MovesCount == 0 & !game.IsCheck)
            {
                //minor castling
                Position posR1 = new Position(Position.Row, Position.Column + 3);
                if (TestCastling(posR1))
                {
                    Position p1 = new Position(Position.Row, Position.Column + 1);
                    Position p2 = new Position(Position.Row, Position.Column + 2);
                    if (Tab.Piece(p1) == null && Tab.Piece(p2) == null)
                    {
                        moveList[Position.Row, Position.Column + 2] = true;
                    }
                }
                //major castling
                Position posR2 = new Position(Position.Row, Position.Column - 4);
                if (TestCastling(posR2))
                {
                    Position p1 = new Position(Position.Row, Position.Column - 1);
                    Position p2 = new Position(Position.Row, Position.Column - 2);
                    Position p3 = new Position(Position.Row, Position.Column - 3);
                    if (Tab.Piece(p1) == null && Tab.Piece(p2) == null && Tab.Piece(p3) == null)
                    {
                        moveList[Position.Row, Position.Column - 2] = true;
                    }
                }
            }

            return moveList;
        }
    }
}
