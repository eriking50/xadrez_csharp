using table;

namespace chess
{
    class ChessPosition
    {
        public char column { get; set; }
        public int row {get; set; }

        public ChessPosition(char col, int row) 
        {
            this.column = col;
            this.row = row;
        }

        public Position toPosition() 
        {
            return new Position(8 - row, column - 'a');
        }

        public override string ToString() 
        {
            return $"{column}{row}";
        }
    }
}
