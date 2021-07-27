using TableGame;

namespace Chess
{
    class Bishop : Piece
    {
        public Bishop(Table tab, Color color) : base(tab, color)
        {
        }

        public override string ToString()
        {
            return "B";
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