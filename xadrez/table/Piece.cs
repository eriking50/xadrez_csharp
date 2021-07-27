
namespace TableGame
{
    abstract class Piece
    {
        public Position Position { get; set; }
        public Table Tab { get; protected set; }
        public Color Color { get; protected set;  }
        public int MovesCount { get; protected set; }

        public Piece(Table tab, Color color)
        {
            this.Position = null;
            this.Tab = tab;
            this.Color = color;
            this.MovesCount = 0;
        }

        public void IncreaseMovesCount() 
        {
            MovesCount++;
        }

        public void DecreaseMovesCount()
        {
            MovesCount--;
        }

        public bool HasPossibleMoves()
        {
            bool[,] moves = PossibleMoves();

            for (int i = 0; i < Tab.Rows; i++)
            {
                for (int j = 0; j < Tab.Columns; j++)
                {
                    if (moves[i,j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CanMoveTo(Position pos) 
        {
            return PossibleMoves()[pos.Row, pos.Column];
        }

        public abstract bool[,] PossibleMoves();
    }
}
