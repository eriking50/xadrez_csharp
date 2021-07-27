using System;


namespace TableGame
{
    class TableException : Exception
    {
        public TableException(string msg) : base(msg) 
        {
        }
    }
}
