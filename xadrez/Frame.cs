using System;
using table;

namespace xadrez
{
    class Frame
    {
        public static void printFrame(Table table)
        {
            for (int i=0; i < table.rows; i++)
            {
                for (int j=0; j < table.columns; j++)
                {
                    if (table.piece(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write(table.piece(i, j) + " ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
