using TableGame;

namespace Chess
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
            pos.SetValues(Position.Row - 2, Position.Column + 1);
            if (Tab.IsValidPosition(pos) && CanMove(pos))
            {
                moveList[pos.Row, pos.Column] = true;
            }
            //up+left
            pos.SetValues(Position.Row - 2, Position.Column - 1);
            if (Tab.IsValidPosition(pos) && CanMove(pos))
            {
                moveList[pos.Row, pos.Column] = true;
            }
            //down+right
            pos.SetValues(Position.Row + 2, Position.Column + 1);
            if (Tab.IsValidPosition(pos) && CanMove(pos))
            {
                moveList[pos.Row, pos.Column] = true;
            }
            //down+left
            pos.SetValues(Position.Row + 2, Position.Column - 1);
            if (Tab.IsValidPosition(pos) && CanMove(pos))
            {
                moveList[pos.Row, pos.Column] = true;
            }
            //right+up
            pos.SetValues(Position.Row - 1, Position.Column + 2);
            if (Tab.IsValidPosition(pos) && CanMove(pos))
            {
                moveList[pos.Row, pos.Column] = true;
            }
            //right+down
            pos.SetValues(Position.Row + 1, Position.Column + 2);
            if (Tab.IsValidPosition(pos) && CanMove(pos))
            {
                moveList[pos.Row, pos.Column] = true;
            }
            //left+up
            pos.SetValues(Position.Row - 1, Position.Column - 2);
            if (Tab.IsValidPosition(pos) && CanMove(pos))
            {
                moveList[pos.Row, pos.Column] = true;
            }
            //left+down
            pos.SetValues(Position.Row + 1, Position.Column - 2);
            if (Tab.IsValidPosition(pos) && CanMove(pos))
            {
                moveList[pos.Row, pos.Column] = true;
            }


            return moveList;
        }

    }
}