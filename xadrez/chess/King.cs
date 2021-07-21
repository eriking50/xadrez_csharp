using table;

namespace chess
{
    class King : Piece
    {
        public King(Table tab, Color color) : base(tab, color)
        {
        }

        public override string ToString()
        {
            return "R";
        }
    }
}
