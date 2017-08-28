using System;

namespace Lab8
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.Write("Insert the string: ");
			string csvString = Console.ReadLine();

			int commaLocation1 = csvString.IndexOf(',');

			int pyramidSlotNumber = int.Parse(csvString.Substring(0, commaLocation1));
			Console.WriteLine("Pyramid slot number: " + pyramidSlotNumber);

			string res = csvString.Substring(commaLocation1 + 1);

			char blockLetter = char.Parse(res.Substring(0, 1));
			Console.WriteLine("Block letter: " + blockLetter);

			bool lit = bool.Parse(res.Substring(2));
			Console.WriteLine("The block should be lit: " + lit);
		}
	}
}
