using System;
using table;
using chess;
using System.Collections.Generic;

namespace chess
{
    class Frame
    {
        public static void printGame(ChessGame game) 
        {
            Frame.printTable(game.table);
			Console.WriteLine();
            printCapturedPieces(game);
			Console.WriteLine();
			Console.WriteLine($"Turno {game.turn}"); 
			Console.WriteLine($"Aguardando jogada do jogador: {getPlayerByColor(game.activePlayer)}");
        }

        public static string getPlayerByColor(Color c) 
        {
            switch (c)    
            {
                case Color.Blue: 
                {
                    return "Azul";
                }
                case Color.Red: 
                {
                    return "Vermelho";
                }
                case Color.Green: 
                {
                    return "Verde";
                }
                case Color.Yellow: 
                {
                    return "Amarelo";
                }
                default:
                    return "Inválido";
            }
        }

        public static void printCapturedPieces(ChessGame game)
        {
            Console.WriteLine("Peças capturadas: ");
            Console.Write("Vermelho: ");
            printCollection(game.capturedByColor(Color.Red));
            Console.Write("Amarelo: ");
            printCollection(game.capturedByColor(Color.Yellow));
        }

        public static void printCollection(HashSet<Piece> col) 
        {
            Console.Write("[");
            foreach (Piece p in col)
            {
                Console.Write($"{p} "); 
            }
            Console.WriteLine("] ");
        }

        public static void printTable(Table tab)
        {
            Console.ForegroundColor = ConsoleColor.White;
            for (int i=0; i < tab.rows; i++)
            {
                Console.Write(8 - i + " ");
                for (int j=0; j < tab.columns; j++)
                {
                        Frame.printPiece(tab.piece(i, j));
    
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void printTable(Table tab, bool[,] moves)
        {
            Console.ForegroundColor = ConsoleColor.White;
            ConsoleColor baseBackground = ConsoleColor.Black;
            ConsoleColor moveBackground = ConsoleColor.DarkGray;

            for (int i=0; i < tab.rows; i++)
            {
                Console.Write(8 - i + " ");
                for (int j=0; j < tab.columns; j++)
                {
                    if (moves[i, j])
                    {
                        Console.BackgroundColor = moveBackground;
                    } else 
                    {
                        Console.BackgroundColor = baseBackground;
                    }
                        Frame.printPiece(tab.piece(i, j));
    
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = baseBackground;
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static ChessPosition readCurrentPosition() 
        {
            string pos = Console.ReadLine();
            char col = pos[0];
            int row = int.Parse(pos[1] + "");
            return new ChessPosition(col, row);
        }

        public static void printPiece(Piece p) 
        {
            if (p == null) 
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("- ");
            } else 
            {
                switch (p.color)
                {
                    case Color.Blue: 
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(p+" ");
                        break;
                    }
                    case Color.Red: 
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(p+" ");
                        break;
                    }
                    case Color.Green: 
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(p+" ");
                        break;
                    }
                    case Color.Yellow: 
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(p+" ");
                        break;
                    }
                }
            }

        }
    }
}
