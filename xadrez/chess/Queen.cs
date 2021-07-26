using table;

namespace chess
{
    class Queen : Piece
    {
        public Queen(Table tab, Color color) : base(tab, color)
        {
        }

        public override string ToString()
        {
            return "D";
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

        //up
            pos.setValues(position.row - 1, position.column); 
            {
                while (tab.isValidPosition(pos) && canMove(pos)) 
                {
                    moveList[pos.row, pos.column] = true;

                    if (tab.piece(pos) != null && tab.piece(pos).color != color)
                    {
                        break;
                    }

                    pos.row = pos.row - 1;
                }
            }
            //right
            pos.setValues(position.row, position.column + 1); 
            {
                while (tab.isValidPosition(pos) && canMove(pos)) 
                {
                    moveList[pos.row, pos.column] = true;

                    if (tab.piece(pos) != null && tab.piece(pos).color != color)
                    {
                        break;
                    }

                    pos.column = pos.column + 1;
                }
            }
            //down
            pos.setValues(position.row + 1, position.column); 
            {
                while (tab.isValidPosition(pos) && canMove(pos)) 
                {
                    moveList[pos.row, pos.column] = true;

                    if (tab.piece(pos) != null && tab.piece(pos).color != color)
                    {
                        break;
                    }

                    pos.row = pos.row + 1;
                }
            }
            //left
            pos.setValues(position.row, position.column - 1); 
            {
                while (tab.isValidPosition(pos) && canMove(pos)) 
                {
                    moveList[pos.row, pos.column] = true;

                    if (tab.piece(pos) != null && tab.piece(pos).color != color)
                    {
                        break;
                    }

                    pos.column = pos.column - 1;
                }
            }
            //up+right
            pos.setValues(position.row - 1, position.column + 1); 
            {
                while (tab.isValidPosition(pos) && canMove(pos)) 
                {
                    moveList[pos.row, pos.column] = true;

                    if (tab.piece(pos) != null && tab.piece(pos).color != color)
                    {
                        break;
                    }

                    pos.row = pos.row - 1;
                    pos.column = pos.column + 1;
                }
            }
            //down+right
            pos.setValues(position.row + 1, position.column + 1); 
            {
                while (tab.isValidPosition(pos) && canMove(pos)) 
                {
                    moveList[pos.row, pos.column] = true;

                    if (tab.piece(pos) != null && tab.piece(pos).color != color)
                    {
                        break;
                    }

                    pos.row = pos.row + 1;
                    pos.column = pos.column + 1;
                }
            }
            //down+left
            pos.setValues(position.row + 1, position.column - 1); 
            {
                while (tab.isValidPosition(pos) && canMove(pos)) 
                {
                    moveList[pos.row, pos.column] = true;

                    if (tab.piece(pos) != null && tab.piece(pos).color != color)
                    {
                        break;
                    }

                    pos.row = pos.row + 1;
                    pos.column = pos.column - 1;
                }
            }
            //up+left
            pos.setValues(position.row - 1, position.column - 1); 
            {
                while (tab.isValidPosition(pos) && canMove(pos)) 
                {
                    moveList[pos.row, pos.column] = true;

                    if (tab.piece(pos) != null && tab.piece(pos).color != color)
                    {
                        break;
                    }

                    pos.row = pos.row - 1;
                    pos.column = pos.column - 1;
                }
            }

            return moveList;
        }

    }
}