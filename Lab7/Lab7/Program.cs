using System;

namespace Lab7
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.Write("In what month were you born? ");
			string month = Console.ReadLine ();

			Console.Write ("On what day were you born? ");
			int day = int.Parse (Console.ReadLine ());

			Console.WriteLine ("Your birthday is " + month + " " + day);
			Console.WriteLine ("You'll receive an email reminder on " + month + " " + (day - 1));
		}
	}
}
