﻿using System;
using table;

namespace xadrez
{
    class Frame
    {
        public static void printFrame(Table tab)
        {

            for (int i=0; i < tab.rows; i++)
            {
                Console.Write(8 - i + " ");
                for (int j=0; j < tab.columns; j++)
                {
                    if (tab.piece(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Frame.printPiece(tab.piece(i, j));
                        Console.Write(" ");
                        
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void printPiece(Piece p) {
            ConsoleColor foregroundBase = Console.ForegroundColor;

            switch (p.color)
            {
                case Color.Blue: 
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(p);
                    Console.ForegroundColor = foregroundBase;
                    break;
                }
                case Color.Red: 
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(p);
                    Console.ForegroundColor = foregroundBase;
                    break;
                }
                case Color.Green: 
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(p);
                    Console.ForegroundColor = foregroundBase;
                    break;
                }
                case Color.Yellow: 
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(p);
                    Console.ForegroundColor = foregroundBase;
                    break;
                }
            }
        }
    }
}
