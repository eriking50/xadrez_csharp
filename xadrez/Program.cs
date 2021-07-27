using System;
using TableGame;

namespace Chess
{
	class Program
	{
		static void Main(string[] args)
		{
			try 
			{
				ChessGame game = new ChessGame();

				while (!game.IsEnded) 
				{
					try 
					{
						Console.Clear();
						Frame.PrintGame(game);

						Console.WriteLine("");
						Console.Write("Escolha uma peça para movimentar: ");
						Position currentPosition = Frame.ReadCurrentPosition().ToPosition();
						game.ValidateCurrentPosition(currentPosition);

						bool[,] possibleMoves = game.Table.Piece(currentPosition).PossibleMoves(); 

						Console.Clear();
						Frame.PrintTable(game.Table, possibleMoves);

						Console.WriteLine("");
						Console.Write("Escolha para onde quer movimentar: ");
						Position nextPosition = Frame.ReadCurrentPosition().ToPosition();
						game.ValidateNextPosition(currentPosition, nextPosition);

						game.DoTurn(currentPosition, nextPosition);
					}
					catch (TableException e) {
						Console.WriteLine(e.Message);
						Console.WriteLine();
					}
				}
				Console.Clear();
				Frame.PrintGame(game);

			}
			catch (TableException e) 
			{
				Console.Clear();
				Console.WriteLine(e.Message);
			}
		}
	}
}
