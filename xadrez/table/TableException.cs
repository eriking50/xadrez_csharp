using System;
using System.Collections.Generic;
using System.Text;

namespace table
{
    class TableException : Exception
    {
        public TableException(string msg) : base(msg) {
        }
    }
}
