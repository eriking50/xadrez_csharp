
namespace table
{
    class Table
    {
        public int rows { get; set; }
        public int columns { get; set; }
        private Piece[,] pieces;

        public Table(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
            pieces = new Piece[rows, columns];
        }

        public Piece piece(int row, int col)
        {
            return pieces[row, col];
        }

        public Piece piece(Position pos) {
            return pieces[pos.row, pos.column];
        }

        public bool hasPiece(Position pos) {
            validatePosition(pos);
            return piece(pos) != null;
        }

        public void placePiece(Piece p, Position pos)
        {
            if (hasPiece(pos))
            {
                throw new TableException("Já existe uma peça na posição escolhida");
            }
            pieces[pos.row, pos.column] = p;
            p.position = pos;
        }

        public bool isValidPosition(Position pos) {
            if (pos.row < 0 || pos.row >= rows || pos.column < 0 || pos.column >= columns) {
                return false;
            }
            return true;
        }

        public void validatePosition(Position pos) {
            if (!isValidPosition(pos))
            {
                throw new TableException("Posição Inválida");
            }
        }
    }
}
