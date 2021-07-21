
namespace table
{
    class Piece
    {
        public Position position { get; set; }
        public Table tab { get; protected set; }
        public Color color { get; protected set;  }
        public int movementCount { get; protected set; }

        public Piece(Table tab, Color color)
        {
            this.position = null;
            this.tab = tab;
            this.color = color;
            this.movementCount = 0;
        }

        public void increaseMovementCount() {
            movementCount++;
        }
    }
}
