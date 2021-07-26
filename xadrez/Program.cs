using System;
using table;
using chess;

namespace xadrez
{
	class Program
	{
		static void Main(string[] args)
		{
			try 
			{
				ChessGame game = new ChessGame();

				while (!game.isEnded) 
				{
					try 
					{
						Console.Clear();
						Frame.printGame(game);

						Console.WriteLine("");
						Console.Write("Escolha uma peça para movimentar: ");
						Position currentPosition = Frame.readCurrentPosition().toPosition();
						game.validateCurrentPosition(currentPosition);

						bool[,] possibleMoves = game.table.piece(currentPosition).possibleMoves(); 

						Console.Clear();
						Frame.printTable(game.table, possibleMoves);

						Console.WriteLine("");
						Console.Write("Escolha para onde quer movimentar: ");
						Position nextPosition = Frame.readCurrentPosition().toPosition();
						game.validateNextPosition(currentPosition, nextPosition);

						game.doTurn(currentPosition, nextPosition);
					}
					catch (TableException e) {
						Console.WriteLine(e.Message);
						Console.WriteLine();
					}
				}
				Console.Clear();
				Frame.printGame(game);

			}
			catch (TableException e) 
			{
				Console.Clear();
				Console.WriteLine(e.Message);
			}
		}
	}
}
