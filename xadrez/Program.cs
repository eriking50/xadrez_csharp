using System;
using table;

namespace xadrez
{
	class Program
	{
		static void Main(string[] args)
		{
			Table table = new Table(8, 8);

			Frame.printFrame(table);

		}
	}
}
