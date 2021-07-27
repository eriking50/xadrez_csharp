using TableGame;

namespace Chess
{
    class Pawn : Piece
    {
        private ChessGame game;
        public Pawn(Table tab, Color color, ChessGame game) : base(tab, color)
        {
            this.game = game;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool HasEnemy(Position pos)
        {
            Piece p = Tab.Piece(pos);
            return p != null && p.Color != Color;
        }

        private bool IsFree(Position pos)
        {
            return Tab.Piece(pos) == null;
        }


        public override bool[,] PossibleMoves()
        {
            bool[,] moveList = new bool[Tab.Rows,Tab.Columns];

            Position pos = new Position(0, 0);

        if (Color == Color.Red)
        {
            //normal move
            pos.SetValues(Position.Row - 1, Position.Column);
            if (Tab.IsValidPosition(pos) && IsFree(pos))
            {
                moveList[pos.Row, pos.Column] = true;
            }
            //first move
            pos.SetValues(Position.Row - 2, Position.Column);
            if (Tab.IsValidPosition(pos) && IsFree(pos) && MovesCount == 0)
            {
                moveList[pos.Row, pos.Column] = true;
            }
            //attack left
            pos.SetValues(Position.Row - 1, Position.Column - 1);
            if (Tab.IsValidPosition(pos) && HasEnemy(pos))
            {
                moveList[pos.Row, pos.Column] = true;
            }
            //attack right
            pos.SetValues(Position.Row - 1, Position.Column + 1);
            if (Tab.IsValidPosition(pos) && HasEnemy(pos))
            {
                moveList[pos.Row, pos.Column] = true;
            }

            //Special move en passant
            if (Position.Row == 3)
            {
                Position posL = new Position(Position.Row, Position.Column - 1);
                if (Tab.IsValidPosition(posL) && HasEnemy(posL) && Tab.Piece(posL) == game.EnPassantPawn)
                {
                    moveList[pos.Row, pos.Column] = true;
                }

                Position posR = new Position(Position.Row, Position.Column + 1);
                if (Tab.IsValidPosition(posR) && HasEnemy(posR) && Tab.Piece(posR) == game.EnPassantPawn)
                {
                    moveList[pos.Row, pos.Column] = true;
                }
            }
        }
        else
        {
            //normal move
            pos.SetValues(Position.Row + 1, Position.Column);
            if (Tab.IsValidPosition(pos) && IsFree(pos))
            {
                moveList[pos.Row, pos.Column] = true;
            }
            //first move
            pos.SetValues(Position.Row + 2, Position.Column);
            if (Tab.IsValidPosition(pos) && IsFree(pos) && MovesCount == 0)
            {
                moveList[pos.Row, pos.Column] = true;
            }
            //attack left
            pos.SetValues(Position.Row + 1, Position.Column - 1);
            if (Tab.IsValidPosition(pos) && HasEnemy(pos))
            {
                moveList[pos.Row, pos.Column] = true;
            }
            //attack right
            pos.SetValues(Position.Row + 1, Position.Column + 1);
            if (Tab.IsValidPosition(pos) && HasEnemy(pos))
            {
                moveList[pos.Row, pos.Column] = true;
            }

            //Special move en passant
            if (Position.Row == 4)
            {
                Position posL = new Position(Position.Row, Position.Column - 1);
                if (Tab.IsValidPosition(posL) && HasEnemy(posL) && Tab.Piece(posL) == game.EnPassantPawn)
                {
                    moveList[pos.Row, pos.Column] = true;
                }
                
                Position posR = new Position(Position.Row, Position.Column + 1);
                if (Tab.IsValidPosition(posR) && HasEnemy(posR) && Tab.Piece(posR) == game.EnPassantPawn)
                {
                    moveList[pos.Row, pos.Column] = true;
                }
            }
        }

            return moveList;
        }

    }
}