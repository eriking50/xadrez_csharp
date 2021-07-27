using TableGame;

namespace Chess
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

        public bool CanMove(Position pos)
        {
            Piece p = Tab.Piece(pos);
            return p == null || p.Color != Color;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] moveList = new bool[Tab.Rows,Tab.Columns];

            Position pos = new Position(0, 0);

        //up
            pos.SetValues(Position.Row - 1, Position.Column); 
            {
                while (Tab.IsValidPosition(pos) && CanMove(pos)) 
                {
                    moveList[pos.Row, pos.Column] = true;

                    if (Tab.Piece(pos) != null && Tab.Piece(pos).Color != Color)
                    {
                        break;
                    }

                    pos.Row = pos.Row - 1;
                }
            }
            //right
            pos.SetValues(Position.Row, Position.Column + 1); 
            {
                while (Tab.IsValidPosition(pos) && CanMove(pos)) 
                {
                    moveList[pos.Row, pos.Column] = true;

                    if (Tab.Piece(pos) != null && Tab.Piece(pos).Color != Color)
                    {
                        break;
                    }

                    pos.Column = pos.Column + 1;
                }
            }
            //down
            pos.SetValues(Position.Row + 1, Position.Column); 
            {
                while (Tab.IsValidPosition(pos) && CanMove(pos)) 
                {
                    moveList[pos.Row, pos.Column] = true;

                    if (Tab.Piece(pos) != null && Tab.Piece(pos).Color != Color)
                    {
                        break;
                    }

                    pos.Row = pos.Row + 1;
                }
            }
            //left
            pos.SetValues(Position.Row, Position.Column - 1); 
            {
                while (Tab.IsValidPosition(pos) && CanMove(pos)) 
                {
                    moveList[pos.Row, pos.Column] = true;

                    if (Tab.Piece(pos) != null && Tab.Piece(pos).Color != Color)
                    {
                        break;
                    }

                    pos.Column = pos.Column - 1;
                }
            }
            //up+right
            pos.SetValues(Position.Row - 1, Position.Column + 1); 
            {
                while (Tab.IsValidPosition(pos) && CanMove(pos)) 
                {
                    moveList[pos.Row, pos.Column] = true;

                    if (Tab.Piece(pos) != null && Tab.Piece(pos).Color != Color)
                    {
                        break;
                    }

                    pos.Row = pos.Row - 1;
                    pos.Column = pos.Column + 1;
                }
            }
            //down+right
            pos.SetValues(Position.Row + 1, Position.Column + 1); 
            {
                while (Tab.IsValidPosition(pos) && CanMove(pos)) 
                {
                    moveList[pos.Row, pos.Column] = true;

                    if (Tab.Piece(pos) != null && Tab.Piece(pos).Color != Color)
                    {
                        break;
                    }

                    pos.Row = pos.Row + 1;
                    pos.Column = pos.Column + 1;
                }
            }
            //down+left
            pos.SetValues(Position.Row + 1, Position.Column - 1); 
            {
                while (Tab.IsValidPosition(pos) && CanMove(pos)) 
                {
                    moveList[pos.Row, pos.Column] = true;

                    if (Tab.Piece(pos) != null && Tab.Piece(pos).Color != Color)
                    {
                        break;
                    }

                    pos.Row = pos.Row + 1;
                    pos.Column = pos.Column - 1;
                }
            }
            //up+left
            pos.SetValues(Position.Row - 1, Position.Column - 1); 
            {
                while (Tab.IsValidPosition(pos) && CanMove(pos)) 
                {
                    moveList[pos.Row, pos.Column] = true;

                    if (Tab.Piece(pos) != null && Tab.Piece(pos).Color != Color)
                    {
                        break;
                    }

                    pos.Row = pos.Row - 1;
                    pos.Column = pos.Column - 1;
                }
            }

            return moveList;
        }

    }
}