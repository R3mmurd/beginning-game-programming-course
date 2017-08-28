using System;

namespace Lab3
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			float originalFahrenheit;
			originalFahrenheit = float.Parse (Console.ReadLine ());

			float celsius = (float)5.0 * (originalFahrenheit - 32) / (float)9;
			float fahrentheit = (celsius * (float)9 / (float)5) + 32;

			Console.WriteLine (originalFahrenheit + " degrees Fahrenheit " +
			"is " + celsius + " degrees Celsius");
			Console.WriteLine (celsius + " degrees Celsius " +
				"is " + fahrentheit + " degrees Fahrenheit");
		}
	}
}
