using System;
using table;
using chess;

namespace xadrez
{
	class Program
	{
		static void Main(string[] args)
		{
			try {
				ChessGame game = new ChessGame();

				while (!game.isEnded) {
					Console.Clear();
					Frame.printTable(game.table);

					Console.WriteLine("");
					Console.Write("Escolha uma peça para movimentar: ");
					Position currentPosition = Frame.readCurrentPosition().toPosition();

					Console.Write("Escolha para onde quer movimentar: ");
					Position nextPosition = Frame.readCurrentPosition().toPosition();

					game.doMove(currentPosition, nextPosition);
				}

				
			}
			catch (TableException e) {
				Console.WriteLine(e.Message);
			}
		}
	}
}
