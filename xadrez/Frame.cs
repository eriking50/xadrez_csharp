using System;
using table;

namespace xadrez
{
    class Frame
    {
        public static void printFrame(Table tab)
        {
            for (int i=0; i < tab.rows; i++)
            {
                for (int j=0; j < tab.columns; j++)
                {
                    if (tab.piece(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write(tab.piece(i, j) + " ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
