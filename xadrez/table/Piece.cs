
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

        public abstract bool[,] possibleMoves();

        public abstract bool canMove(Position pos);
    }
}
