#region Using Statements
using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

using XnaCards;

#endregion

namespace ProgrammingAssignment6
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		const int WindowWidth = 800;
		const int WindowHeight = 600;

		// max valid blockjuck score for a hand
		const int MaxHandValue = 21;

		// deck and hands
		Deck deck;
		List<Card> dealerHand = new List<Card>();
		List<Card> playerHand = new List<Card>();

		// hand placement
		const int TopCardOffset = 100;
		const int HorizontalCardOffset = 150;
		const int VerticalCardSpacing = 125;

		// messages
		SpriteFont messageFont;
		const string ScoreMessagePrefix = "Score: ";
		Message playerScoreMessage;
		Message dealerScoreMessage;
		Message winnerMessage;
		List<Message> messages = new List<Message>();

		// message placement
		const int ScoreMessageTopOffset = 25;
		const int HorizontalMessageOffset = HorizontalCardOffset;
		Vector2 winnerMessageLocation = new Vector2(WindowWidth / 2,
			WindowHeight / 2);

		// menu buttons
		Texture2D quitButtonSprite;
		List<MenuButton> menuButtons = new List<MenuButton>();

		// menu button placement
		const int TopMenuButtonOffset = TopCardOffset;
		const int QuitMenuButtonOffset = WindowHeight - TopCardOffset;
		const int HorizontalMenuButtonOffset = WindowWidth / 2;
		const int VerticalMenuButtonSpacing = 125;

		// use to detect hand over when player and dealer didn't hit
		bool playerHit = false;
		bool dealerHit = false;

		// game state tracking
		static GameState currentState = GameState.WaitingForPlayer;

		public Game1 ()
		{
			graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";	            

			// set resolution and show mouse
			graphics.PreferredBackBufferWidth = WindowWidth;
			graphics.PreferredBackBufferHeight = WindowHeight;
			IsMouseVisible = true;
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize ()
		{
			// TODO: Add your initialization logic here
			base.Initialize ();
				
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent ()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch (GraphicsDevice);

			// create and shuffle deck
			deck = new Deck(Content, 0, 0);
			deck.Shuffle();

			// first player card
			Card playerCard1 = deck.TakeTopCard();
			playerCard1.X = HorizontalCardOffset;
			playerCard1.Y = TopCardOffset;
			playerCard1.FlipOver();
			playerHand.Add(playerCard1);

			// first dealer card
			Card dealerCard1 = deck.TakeTopCard();
			dealerCard1.X = WindowWidth - HorizontalCardOffset;
			dealerCard1.Y = TopCardOffset;
			dealerHand.Add(dealerCard1);

			// second player card
			Card playerCard2 = deck.TakeTopCard();
			playerCard2.X = HorizontalCardOffset;
			playerCard2.Y = TopCardOffset + VerticalCardSpacing;
			playerCard2.FlipOver();
			playerHand.Add(playerCard2);

			// second dealer card
			Card dealerCard2 = deck.TakeTopCard();
			dealerCard2.X = WindowWidth - HorizontalCardOffset;
			dealerCard2.Y = TopCardOffset + VerticalCardSpacing;
			dealerCard2.FlipOver();
			dealerHand.Add(dealerCard2);


			// load sprite font, create message for player score and add to list
			messageFont = Content.Load<SpriteFont>(@"fonts\Arial24");
			playerScoreMessage = new Message(ScoreMessagePrefix + GetBlockjuckScore(playerHand).ToString(),
				messageFont,
				new Vector2(HorizontalMessageOffset, ScoreMessageTopOffset));
			messages.Add(playerScoreMessage);

			// load quit button sprite for later use
			quitButtonSprite = Content.Load<Texture2D>(@"graphics\quitbutton");

			// create hit button and add to list
			Texture2D hitButtonSprite = Content.Load<Texture2D>(@"graphics\hitbutton");
			MenuButton hitButton = new MenuButton (hitButtonSprite, 
				new Vector2 ((float) HorizontalMenuButtonOffset, (float) TopMenuButtonOffset),
				GameState.PlayerHitting);
			menuButtons.Add(hitButton);

			// create stand button and add to list
			Texture2D standButtonSprite = Content.Load<Texture2D>(@"graphics\standbutton");
			MenuButton standButton = new MenuButton (standButtonSprite, 
				new Vector2 ((float) HorizontalMenuButtonOffset, 
					(float) (TopMenuButtonOffset + VerticalMenuButtonSpacing)),
				GameState.WaitingForDealer);
			menuButtons.Add(standButton);

		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update (GameTime gameTime)
		{
			// For Mobile devices, this logic will close the Game when the Back button is pressed
			// Exit() is obsolete on iOS
			#if !__IOS__
			if (GamePad.GetState (PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
			    Keyboard.GetState ().IsKeyDown (Keys.Escape)) {
				Exit ();
			}
			#endif

			MouseState mouse = Mouse.GetState();

			// update menu buttons as appropriate
			if (currentState == GameState.WaitingForPlayer || currentState == GameState.DisplayingHandResults) 
			{
				foreach (MenuButton button in menuButtons) 
				{
					button.Update (mouse);
				}
			}

			// game state-specific processing
			switch (currentState) 
			{
			case GameState.PlayerHitting:
				{
					playerHit = true;
					Card c = deck.TakeTopCard();
					c.FlipOver();
					c.X = HorizontalCardOffset;
					c.Y = TopCardOffset + playerHand.Count * VerticalCardSpacing;
					playerHand.Add(c);

					playerScoreMessage.Text = ScoreMessagePrefix + GetBlockjuckScore(playerHand).ToString();

					ChangeState(GameState.WaitingForDealer);
				}
				break;
			
			case GameState.WaitingForDealer:

				if (GetBlockjuckScore(dealerHand) <= 16)
				{
					dealerHit = true;
					ChangeState(GameState.DealerHitting);
				}
				else
				{
					ChangeState(GameState.CheckingHandOver);
				}

				break;

			case GameState.DealerHitting:
				{
					Card c = deck.TakeTopCard();
					c.FlipOver();
					c.X = WindowWidth - HorizontalCardOffset;
					c.Y = TopCardOffset + dealerHand.Count * VerticalCardSpacing;
					dealerHand.Add(c);

					ChangeState (GameState.CheckingHandOver);
				}

				break;

			case GameState.CheckingHandOver:
				if ((GetBlockjuckScore(playerHand) > MaxHandValue || GetBlockjuckScore(dealerHand) > MaxHandValue)
					|| (!playerHit && !dealerHit))
				{
					dealerHand[0].FlipOver();

					dealerScoreMessage = new Message(ScoreMessagePrefix + GetBlockjuckScore(dealerHand).ToString(),
						messageFont,
						new Vector2(WindowWidth - HorizontalMessageOffset, ScoreMessageTopOffset));
					messages.Add(dealerScoreMessage);

					menuButtons.Clear();

					MenuButton quitButton = new MenuButton (quitButtonSprite, 
						new Vector2 ((float) HorizontalMenuButtonOffset, (float) QuitMenuButtonOffset),
						GameState.Exiting);
					menuButtons.Add(quitButton);

					winnerMessage = new Message("", messageFont, winnerMessageLocation);
					messages.Add(winnerMessage);

					if (GetBlockjuckScore(playerHand) <= MaxHandValue && GetBlockjuckScore(dealerHand) <= MaxHandValue) 
					{
						if (GetBlockjuckScore(playerHand) > GetBlockjuckScore(dealerHand)) 
						{
							winnerMessage.Text = "Player wins!";
						}
						else if (GetBlockjuckScore(playerHand) < GetBlockjuckScore(dealerHand))
						{
							winnerMessage.Text = "Dealer wins!";
						} 
						else
						{
							winnerMessage.Text = "Tie!";
						}

					} 
					else if (GetBlockjuckScore(playerHand) <= MaxHandValue) 
					{
						winnerMessage.Text = "Player wins!";
					} 
					else if (GetBlockjuckScore(dealerHand) <= MaxHandValue)
					{
						winnerMessage.Text = "Dealer wins!";
					}
					else 
					{
						winnerMessage.Text = "Tie!";
					}

					ChangeState(GameState.DisplayingHandResults);

				} 
				else 
				{
					ChangeState(GameState.WaitingForPlayer);
					playerHit = false;
					dealerHit = false;
				}
				break;

			case GameState.Exiting:
				Exit();
				break;
			}


			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw (GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Goldenrod);

			spriteBatch.Begin();

			// draw hands
			foreach (Card c in playerHand) 
			{
				c.Draw(spriteBatch);
			}

			foreach (Card c in dealerHand) 
			{
				c.Draw(spriteBatch);
			}

			// draw messages
			foreach (Message message in messages) 
			{
				message.Draw(spriteBatch);
			}

			// draw menu buttons
			foreach (MenuButton button in menuButtons) 
			{
				button.Draw(spriteBatch);
			}


			spriteBatch.End();

			base.Draw(gameTime);
		}

		/// <summary>
		/// Calculates the Blockjuck score for the given hand
		/// </summary>
		/// <param name="hand">the hand</param>
		/// <returns>the Blockjuck score for the hand</returns>
		private int GetBlockjuckScore(List<Card> hand)
		{
			// add up score excluding Aces
			int numAces = 0;
			int score = 0;
			foreach (Card card in hand)
			{
				if (card.Rank != Rank.Ace)
				{
					score += GetBlockjuckCardValue(card);
				}
				else
				{
					numAces++;
				}
			}

			// if more than one ace, only one should ever be counted as 11
			if (numAces > 1)
			{
				// make all but the first ace count as 1
				score += numAces - 1;
				numAces = 1;
			}

			// if there's an Ace, score it the best way possible
			if (numAces > 0)
			{
				if (score + 11 <= MaxHandValue)
				{
					// counting Ace as 11 doesn't bust
					score += 11;
				}
				else
				{
					// count Ace as 1
					score++;
				}
			}

			return score;
		}

		/// <summary>
		/// Gets the Blockjuck value for the given card
		/// </summary>
		/// <param name="card">the card</param>
		/// <returns>the Blockjuck value for the card</returns>
		private int GetBlockjuckCardValue(Card card)
		{
			switch (card.Rank)
			{
			case Rank.Ace:
				return 11;
			case Rank.King:
			case Rank.Queen:
			case Rank.Jack:
			case Rank.Ten:
				return 10;
			case Rank.Nine:
				return 9;
			case Rank.Eight:
				return 8;
			case Rank.Seven:
				return 7;
			case Rank.Six:
				return 6;
			case Rank.Five:
				return 5;
			case Rank.Four:
				return 4;
			case Rank.Three:
				return 3;
			case Rank.Two:
				return 2;
			default:
				return 0;
			}
		}

		/// <summary>
		/// Changes the state of the game
		/// </summary>
		/// <param name="newState">the new game state</param>
		public static void ChangeState(GameState newState)
		{
			currentState = newState;
		}
	}
}