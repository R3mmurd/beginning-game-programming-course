using System;

namespace Lab2
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			int age = 34;

			Console.WriteLine ("Age = " + age);

			const int MaxScore = 100;

			int score = 80;

			float percent = (float)score / (float)MaxScore;

			Console.WriteLine ("Percentage = " + percent);
		}
	}
}
