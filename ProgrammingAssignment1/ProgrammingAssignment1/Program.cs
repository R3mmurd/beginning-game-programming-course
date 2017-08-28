using System;

namespace ProgrammingAssignment1
{
	/// <summary>
	/// Calculates the distance and the angle between two points.
	/// those points.
	/// </summary>
	class MainClass
	{
		/// <summary>
		/// Calculates the distance and the angle between two points.
		/// </summary>
		/// <param name="args">The command-line arguments.</param>
		public static void Main (string[] args)
		{
			// Print welcome message
			Console.WriteLine("Welcome my friend! This program will" +
				" calculate the distance and the angle between two points");

			// Prompt for the x value for the first point
			Console.Write("Insert the x value for the first point: ");

			// Read, parse and store the value
			float point1X = float.Parse(Console.ReadLine());

			// Prompt for the y value for the first point
			Console.Write("Insert the y value for the first point: ");

			// Read, parse and store the value
			float point1Y = float.Parse(Console.ReadLine());

			// Prompt for the x value for the second point
			Console.Write("Insert the x value for the second point: ");

			// Read, parse and store the value
			float point2X = float.Parse(Console.ReadLine());

			// Prompt for the y value for the second point
			Console.Write("Insert the y value for the second point: ");

			// Read, parse and store the value
			float point2Y = float.Parse(Console.ReadLine());

			// Calculates delta x
			float deltaX = point2X - point1X;

			// Calculates delta y
			float deltaY = point2Y - point1Y;

			// Calculates the distance by using Pythagorean Theorem
			float distance = (float) Math.Sqrt(deltaX * deltaX + deltaY * deltaY);

			// Calculates the angle
			float radAngle = (float) Math.Atan2(deltaY, deltaX);

			// Converts the angle from radians to degrees
			float angle = radAngle * 180 / (float) Math.PI;

			// Prints the distance
			Console.WriteLine ("The distance between the points is: " + distance.ToString("N3"));

			// Prints the angle
			Console.WriteLine ("The angle between the points is: " + angle.ToString("N3"));

			Console.WriteLine();
		}
	}
}
