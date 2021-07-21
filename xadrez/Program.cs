using System;
using table;
using chess;

namespace xadrez
{
	class Program
	{
		static void Main(string[] args)
		{
			Table table = new Table(8, 8);
			table.putAPiece(new Tower(table, Color.Blue), new Position(0, 0));
			table.putAPiece(new Tower(table, Color.Blue), new Position(0, 7));
			table.putAPiece(new King(table, Color.Blue), new Position(0, 3));

			table.putAPiece(new Tower(table, Color.Red), new Position(7, 0));
			table.putAPiece(new Tower(table, Color.Red), new Position(7, 7));
			table.putAPiece(new King(table, Color.Red), new Position(7, 4));


			Frame.printFrame(table);


		}
	}
}
