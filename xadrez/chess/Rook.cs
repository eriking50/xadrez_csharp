using TableGame;

namespace Chess
{
    class Rook : Piece
    {
        public Rook(Table tab, Color color) : base(tab, color)
        {
        }

        public override string ToString()
        {
            return "T";
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

            return moveList;
        }
    }
    
}
