using table;

namespace chess
{
    class Knight : Piece
    {
        public Knight(Table tab, Color color) : base(tab, color)
        {
        }

        public override string ToString()
        {
            return "C";
        }

        public bool canMove(Position pos)
        {
            Piece p = tab.piece(pos);
            return p == null || p.color != color;
        }

        public override bool[,] possibleMoves()
        {
            bool[,] moveList = new bool[tab.rows,tab.columns];

            Position pos = new Position(0, 0);

            //up+right
            pos.setValues(position.row - 2, position.column + 1);
            if (tab.isValidPosition(pos) && canMove(pos))
            {
                moveList[pos.row, pos.column] = true;
            }
            //up+left
            pos.setValues(position.row - 2, position.column - 1);
            if (tab.isValidPosition(pos) && canMove(pos))
            {
                moveList[pos.row, pos.column] = true;
            }
            //down+right
            pos.setValues(position.row + 2, position.column + 1);
            if (tab.isValidPosition(pos) && canMove(pos))
            {
                moveList[pos.row, pos.column] = true;
            }
            //down+left
            pos.setValues(position.row + 2, position.column - 1);
            if (tab.isValidPosition(pos) && canMove(pos))
            {
                moveList[pos.row, pos.column] = true;
            }
            //right+up
            pos.setValues(position.row - 1, position.column + 2);
            if (tab.isValidPosition(pos) && canMove(pos))
            {
                moveList[pos.row, pos.column] = true;
            }
            //right+down
            pos.setValues(position.row + 1, position.column + 2);
            if (tab.isValidPosition(pos) && canMove(pos))
            {
                moveList[pos.row, pos.column] = true;
            }
            //left+up
            pos.setValues(position.row - 1, position.column - 2);
            if (tab.isValidPosition(pos) && canMove(pos))
            {
                moveList[pos.row, pos.column] = true;
            }
            //left+down
            pos.setValues(position.row + 1, position.column - 2);
            if (tab.isValidPosition(pos) && canMove(pos))
            {
                moveList[pos.row, pos.column] = true;
            }


            return moveList;
        }

    }
}