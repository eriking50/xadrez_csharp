using System;


namespace table
{
    class TableException : Exception
    {
        public TableException(string msg) : base(msg) 
        {
        }
    }
}
