using System;
using table;
using chess;

namespace xadrez
{
	class Program
	{
		static void Main(string[] args)
		{
			// try {
			// 	Table table = new Table(8, 8);
			// 	table.placePiece(new Tower(table, Color.Blue), new Position(0, 0));
			// 	table.placePiece(new Tower(table, Color.Blue), new Position(0, 7));
			// 	table.placePiece(new King(table, Color.Blue), new Position(0, 3));

			// 	table.placePiece(new Tower(table, Color.Red), new Position(7, 0));
			// 	table.placePiece(new Tower(table, Color.Red), new Position(7, 7));
			// 	table.placePiece(new King(table, Color.Red), new Position(7, 4));


			// 	Frame.printFrame(table);
			// }
			// catch (TableException e) {
			// 	Console.WriteLine(e.Message);
			// }

			ChessPosition pos = new ChessPosition('a', 1);
			Console.WriteLine(pos);

		}
	}
}
