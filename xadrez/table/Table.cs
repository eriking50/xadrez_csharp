
namespace TableGame
{
    class Table
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        private Piece[,] pieces;

        public Table(int rows, int columns)
        {
            this.Rows = rows;
            this.Columns = columns;
            pieces = new Piece[rows, columns];
        }

        public Piece Piece(int row, int col)
        {
            return pieces[row, col];
        }

        public Piece Piece(Position pos) 
        {
            return pieces[pos.Row, pos.Column];
        }

        public bool HasPiece(Position pos) 
        {
            ValidatePosition(pos);
            return Piece(pos) != null;
        }

        public void PlacePiece(Piece p, Position pos)
        {
            if (HasPiece(pos))
            {
                throw new TableException("Já existe uma peça na posição escolhida");
            }
            pieces[pos.Row, pos.Column] = p;
            p.Position = pos;
        }

        public Piece RemovePiece(Position pos)
        {
            if (Piece(pos) == null) {
                return null;
            }

            Piece aux = Piece(pos);
            aux.Position = null;
            pieces[pos.Row, pos.Column] = null;
            return aux;
        }

        public bool IsValidPosition(Position pos) 
        {
            if (pos.Row < 0 || pos.Row >= Rows || pos.Column < 0 || pos.Column >= Columns)
            {
                return false;
            }
            return true;
        }

        public void ValidatePosition(Position pos) 
        {
            if (!IsValidPosition(pos))
            {
                throw new TableException("Posição Inválida");
            }
        }
    }
}
