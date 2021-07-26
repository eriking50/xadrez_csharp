using table;

namespace chess
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

        public bool canMove(Position pos) 
        {
            Piece p = tab.piece(pos);
            return p == null || p.color != color;
        }

        public bool testCastling(Position pos)
        {
            Piece p = tab.piece(pos);
            return p != null && p is Rook && p.color == color && p.movesCount == 0;
        }

        public override bool[,] possibleMoves()
        {
            bool[,] moveList = new bool[tab.rows,tab.columns];

            Position pos = new Position(0, 0);

            //up
            pos.setValues(position.row - 1, position.column);
            if (tab.isValidPosition(pos) && canMove(pos))
            {
                moveList[pos.row, pos.column] = true;
            }
            //up + right
            pos.setValues(position.row - 1, position.column + 1);
            if (tab.isValidPosition(pos) && canMove(pos))
            {
                moveList[pos.row, pos.column] = true;
            }
            //right
            pos.setValues(position.row, position.column + 1);
            if (tab.isValidPosition(pos) && canMove(pos))
            {
                moveList[pos.row, pos.column] = true;
            }
            //down + right
            pos.setValues(position.row + 1, position.column + 1);
            if (tab.isValidPosition(pos) && canMove(pos))
            {
                moveList[pos.row, pos.column] = true;
            }                                              
            //down
            pos.setValues(position.row + 1, position.column);
            if (tab.isValidPosition(pos) && canMove(pos))
            {
                moveList[pos.row, pos.column] = true;
            }            
            //down + left
            pos.setValues(position.row + 1, position.column - 1);
            if (tab.isValidPosition(pos) && canMove(pos))
            {
                moveList[pos.row, pos.column] = true;
            }
            //left
            pos.setValues(position.row, position.column - 1);
            if (tab.isValidPosition(pos) && canMove(pos))
            {
                moveList[pos.row, pos.column] = true;
            }            
            //up + left
            pos.setValues(position.row - 1, position.column - 1);
            if (tab.isValidPosition(pos) && canMove(pos))
            {
                moveList[pos.row, pos.column] = true;
            }

            //Special move = Castling
            if (movesCount == 0 & !game.isCheck)
            {
                //minor castling
                Position posR1 = new Position(position.row, position.column + 3);
                if (testCastling(posR1))
                {
                    Position p1 = new Position(position.row, position.column + 1);
                    Position p2 = new Position(position.row, position.column + 2);
                    if (tab.piece(p1) == null && tab.piece(p2) == null)
                    {
                        moveList[position.row, position.column + 2] = true;
                    }
                }
            }

            return moveList;
        }
    }
}
