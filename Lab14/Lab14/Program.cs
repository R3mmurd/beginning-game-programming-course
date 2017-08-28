using System;
using System.Collections.Generic;

namespace Lab14
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			int value = 0;

			List<int> data = new List<int>();

			while (value != -1) 
			{
				Console.Write("Insert a value: ");
				value = int.Parse(Console.ReadLine());

				while (value < -1) 
				{
					Console.WriteLine("Invalid value");
					Console.Write("Insert a value: ");
					value = int.Parse(Console.ReadLine());
				}
			
				if (value >= 0) 
				{
					data.Add(value);
				}
			}

			int max = 0;

			foreach (int x in data) 
			{
				if (x > max)
				{
					max = x;
				}
			}

			Console.WriteLine("The max value is " + max);

			int sum = 0;

			foreach (int x in data) 
			{
				sum += x;
			}

			float average = (float)sum / (float)data.Count;

			Console.WriteLine("The average is " + average);
		}
	}
}
