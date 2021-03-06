using TableGame;

namespace Chess
{
    class ChessPosition
    {
        public char Column { get; set; }
        public int Row {get; set; }

        public ChessPosition(char col, int row) 
        {
            this.Column = col;
            this.Row = row;
        }

        public Position ToPosition() 
        {
            return new Position(8 - Row, Column - 'a');
        }

        public override string ToString() 
        {
            return $"{Column}{Row}";
        }
    }
}
