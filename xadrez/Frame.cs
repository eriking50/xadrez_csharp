using System;
using TableGame;
using System.Collections.Generic;

namespace Chess
{
    class Frame
    {
        public static void PrintGame(ChessGame game) 
        {
            Frame.PrintTable(game.Table);
			Console.WriteLine();
            PrintCapturedPieces(game);
			Console.WriteLine();
			Console.WriteLine($"Turno {game.Turn}");
            if (!game.IsEnded)
            {                
                Console.WriteLine($"Aguardando jogada do jogador: {game.GetPlayerByColor(game.ActivePlayer)}");
                if (game.IsCheck)
                {
                    Console.WriteLine("VOCÊ ESTÁ EM XEQUE");
                }
            }
            else
            {
                Console.WriteLine("XEQUEMATE!");
                Console.WriteLine($"Vencedor: {game.GetPlayerByColor(game.ActivePlayer)}"); 
            }
        }


        public static void PrintCapturedPieces(ChessGame game) 
        {
            Console.WriteLine("Peças capturadas: ");
            Console.Write("Vermelho: ");
            PrintCollection(game.CapturedByColor(Color.Red), GetConsoleColor(Color.Red));
            Console.Write("Amarelo: ");
            PrintCollection(game.CapturedByColor(Color.Yellow), GetConsoleColor(Color.Yellow));
        }

        public static void PrintCollection(HashSet<Piece> col, ConsoleColor c) 
        {
            Console.Write("[ ");
            foreach (Piece p in col)
            {
                Console.ForegroundColor = c;
                Console.Write($"{p} "); 
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine("]");
        }

        public static ConsoleColor GetConsoleColor(Color c)
        {
            if (c == Color.Red)
            {
                return ConsoleColor.Red;
            }
            else
            {
                return ConsoleColor.Yellow;
            }
        }

        public static void PrintTable(Table tab)
        {
            Console.ForegroundColor = ConsoleColor.White;
            for (int i=0; i < tab.Rows; i++)
            {
                Console.Write(8 - i + " ");
                for (int j=0; j < tab.Columns; j++)
                {
                        Frame.PrintPiece(tab.Piece(i, j));
    
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void PrintTable(Table tab, bool[,] moves)
        {
            Console.ForegroundColor = ConsoleColor.White;
            ConsoleColor baseBackground = ConsoleColor.Black;
            ConsoleColor moveBackground = ConsoleColor.DarkGray;

            for (int i=0; i < tab.Rows; i++)
            {
                Console.Write(8 - i + " ");
                for (int j=0; j < tab.Columns; j++)
                {
                    if (moves[i, j])
                    {
                        Console.BackgroundColor = moveBackground;
                    } else 
                    {
                        Console.BackgroundColor = baseBackground;
                    }
                        Frame.PrintPiece(tab.Piece(i, j));
    
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = baseBackground;
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static ChessPosition ReadCurrentPosition() 
        {
            string pos = Console.ReadLine();
            char col = pos[0];
            int row = int.Parse(pos[1] + "");
            return new ChessPosition(col, row);
        }

        public static void PrintPiece(Piece p) 
        {
            if (p == null) 
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("- ");
            } else 
            {
                switch (p.Color)
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
