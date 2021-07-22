namespace table
{
    class Position
    {
        public int row { get; set; }
        public int column { get; set; }

        public Position(int row, int column)
        {
            this.row = row;
            this.column = column;
        }

        public void setValues(int row, int col) 
        {
            this.row = row;
            this.column = col;            
        }

        public override string ToString()
        {
            return $"Posição: {row},{column} ";
            
        }
    }
}
