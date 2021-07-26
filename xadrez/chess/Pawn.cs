using table;

namespace chess
{
    class Pawn : Piece
    {
        public Pawn(Table tab, Color color) : base(tab, color)
        {
        }

        public override string ToString()
        {
            return "P";
        }

        private bool hasEnemy(Position pos)
        {
            Piece p = tab.piece(pos);
            return p != null && p.color != color;
        }

        private bool isFree(Position pos)
        {
            return tab.piece(pos) == null;
        }


        public override bool[,] possibleMoves()
        {
            bool[,] moveList = new bool[tab.rows,tab.columns];

            Position pos = new Position(0, 0);

        if (color == Color.Red)
        {
            //normal move
            pos.setValues(position.row - 1, position.column);
            if (tab.isValidPosition(pos) && isFree(pos))
            {
                moveList[pos.row, pos.column] = true;
            }
            //first move
            pos.setValues(position.row - 2, position.column);
            if (tab.isValidPosition(pos) && isFree(pos) && movesCount == 0)
            {
                moveList[pos.row, pos.column] = true;
            }
            //attack left
            pos.setValues(position.row - 1, position.column - 1);
            if (tab.isValidPosition(pos) && hasEnemy(pos))
            {
                moveList[pos.row, pos.column] = true;
            }
            //attack right
            pos.setValues(position.row - 1, position.column + 1);
            if (tab.isValidPosition(pos) && hasEnemy(pos))
            {
                moveList[pos.row, pos.column] = true;
            }
        }
        else
        {
            //normal move
            pos.setValues(position.row + 1, position.column);
            if (tab.isValidPosition(pos) && isFree(pos))
            {
                moveList[pos.row, pos.column] = true;
            }
            //first move
            pos.setValues(position.row + 2, position.column);
            if (tab.isValidPosition(pos) && isFree(pos) && movesCount == 0)
            {
                moveList[pos.row, pos.column] = true;
            }
            //attack left
            pos.setValues(position.row + 1, position.column - 1);
            if (tab.isValidPosition(pos) && hasEnemy(pos))
            {
                moveList[pos.row, pos.column] = true;
            }
            //attack right
            pos.setValues(position.row + 1, position.column + 1);
            if (tab.isValidPosition(pos) && hasEnemy(pos))
            {
                moveList[pos.row, pos.column] = true;
            }
        }

            return moveList;
        }

    }
}