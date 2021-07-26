using table;

namespace chess
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

            //Special move en passant
            if (position.row == 3)
            {
                Position posL = new Position(position.row, position.column - 1);
                if (tab.isValidPosition(posL) && hasEnemy(posL) && tab.piece(posL) == game.enPassantPawn)
                {
                    moveList[pos.row, pos.column] = true;
                }

                Position posR = new Position(position.row, position.column + 1);
                if (tab.isValidPosition(posR) && hasEnemy(posR) && tab.piece(posR) == game.enPassantPawn)
                {
                    moveList[pos.row, pos.column] = true;
                }
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

            //Special move en passant
            if (position.row == 4)
            {
                Position posL = new Position(position.row, position.column - 1);
                if (tab.isValidPosition(posL) && hasEnemy(posL) && tab.piece(posL) == game.enPassantPawn)
                {
                    moveList[pos.row, pos.column] = true;
                }
                
                Position posR = new Position(position.row, position.column + 1);
                if (tab.isValidPosition(posR) && hasEnemy(posR) && tab.piece(posR) == game.enPassantPawn)
                {
                    moveList[pos.row, pos.column] = true;
                }
            }
        }

            return moveList;
        }

    }
}