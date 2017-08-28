using System;
using System.Collections.Generic;
using ConsoleCards;

namespace Lab13
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.Write("Write the lower value: ");

			int lowerValue = int.Parse(Console.ReadLine());

			Console.Write("Write the upper value: ");

			int upperValue = int.Parse(Console.ReadLine());

			for (int i = lowerValue; i <= upperValue; i++) 
			{
				Console.WriteLine(i);
			}

			Deck deck = new Deck();

			List<Card> cardHand = new List<Card>();

			deck.Shuffle();

			for (int i = 0; i < 5; i++) 
			{
				cardHand.Add(deck.TakeTopCard());
			}

			for (int i = 0; i < cardHand.Count; i++) 
			{
				cardHand[i].FlipOver();
			}

			foreach (Card card in cardHand) 
			{
				card.Print();
			}
		}
	}
}
