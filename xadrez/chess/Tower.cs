using table;

namespace chess
{
    class Tower : Piece
    {
        public Tower(Table tab, Color color) : base(tab, color)
        {
        }

        public override string ToString()
        {
            return "T";
        }
    }
}
