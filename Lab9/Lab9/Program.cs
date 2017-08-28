using System;

namespace Lab9
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine("**************");
			Console.WriteLine("Menu:");
			Console.WriteLine();
			Console.WriteLine("N - New Game");
			Console.WriteLine("L - Load Game");
			Console.WriteLine("O - Options");
			Console.WriteLine("Q - Quit");
			Console.WriteLine("**************");

			Console.WriteLine();
			Console.Write("Select a character: ");
			char opt = Console.ReadLine()[0];

			Console.WriteLine ("Versión con if");

			if (opt == 'n' || opt == 'N')
				Console.WriteLine("Creating new game...");
			else if (opt == 'l' || opt == 'L')
				Console.WriteLine("Loading game...");

			else if (opt == 'o' || opt == 'O')
				Console.WriteLine("Options");

			else if (opt == 'q' || opt == 'Q')
				Console.WriteLine("Exiting...");
			else
				Console.WriteLine ("You're a n00b");

			Console.WriteLine ("Versión con switch");

			switch (opt)
			{
			case 'n':
			case 'N':
				Console.WriteLine("Creating new game...");
				break;
			case 'l':
			case 'L':
				Console.WriteLine("Loading game...");
				break;
			case 'o':
			case 'O':
				Console.WriteLine("Options");
				break;
			case 'q':
			case 'Q':
				Console.WriteLine("Exiting...");
				break;
			default:
				Console.WriteLine ("You're a n00b");
				break;
			}
		}
	}
}
