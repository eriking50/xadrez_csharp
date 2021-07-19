
namespace table
{
    class Piece
    {
        public Position position { get; set; }
        public Table tab { get; protected set; }
        public Color color { get; protected set;  }
        public int movement { get; protected set; }

        public Piece(Position position, Table tab, Color color)
        {
            this.position = position;
            this.tab = tab;
            this.color = color;
            this.movement = 0;
        }
    }
}
