
namespace table
{
    abstract class Piece
    {
        public Position position { get; set; }
        public Table tab { get; protected set; }
        public Color color { get; protected set;  }
        public int movesCount { get; protected set; }

        public Piece(Table tab, Color color)
        {
            this.position = null;
            this.tab = tab;
            this.color = color;
            this.movesCount = 0;
        }

        public void increaseMovesCount() 
        {
            movesCount++;
        }

        public bool hasPossibleMoves()
        {
            bool[,] aux = possibleMoves();

            for (int i = 0; i < tab.rows; i++)
            {
                for (int j = 0; j < tab.columns; j++)
                {
                    return true;
                }
            }
            return false;
        }

        public bool canMoveTo(Position pos) 
        {
            return possibleMoves()[pos.row, pos.column];
        }

        public abstract bool[,] possibleMoves();

        public abstract bool canMove(Position pos);
    }
}
