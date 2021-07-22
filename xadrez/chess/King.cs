using table;

namespace chess
{
    class King : Piece
    {
        public King(Table tab, Color color) : base(tab, color)
        {
        }

        public override string ToString()
        {
            return "R";
        }

        public override bool canMove(Position pos) {
            Piece p = tab.piece(pos);
            return p == null || p.color != color;
        }

        public override bool[,] possibleMoves()
        {
            bool[,] moveList = new bool[tab.rows,tab.columns];

            Position pos = new Position(0, 0);

            //up
            pos.setValues(position.row -1, position.column);
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

            return moveList;
        }
    }
}
