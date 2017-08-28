using System;

namespace Lab4
{
	/// <summary>
	/// Implements Lab 4 functionality
	/// </summary>
	class MainClass
	{
		/// <summary>
		/// Implements Lab 4 functionality
		/// </summary>
		/// <param name="args">command-line args</param>
		public static void Main (string[] args)
		{
			// create a new deck and print the contents of the deck
			Deck deck = new Deck();
			deck.Print();

			// shuffle the deck and print the contents of the deck
			deck.Shuffle();
			deck.Print();

			// take the top card from the deck and print the card rank and suit
			Card c1 = deck.TakeTopCard();
			Console.WriteLine(c1.Rank + " of " + c1.Suit);

			// take the top card from the deck and print the card rank and suit
			Card c2 = deck.TakeTopCard();
			Console.WriteLine(c2.Rank + " of " + c2.Suit);
		}
	}
}