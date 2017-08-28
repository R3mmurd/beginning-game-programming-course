using System;
using ConsoleCards;

namespace Lab12
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Deck deck = new Deck();
			Card[] cards = new Card[5];
			deck.Shuffle();

			cards[0] = deck.TakeTopCard();
			cards[1] = deck.TakeTopCard();
			cards[1].FlipOver();
			cards[0].Print();
			cards[1].Print();
		}
	}
}
