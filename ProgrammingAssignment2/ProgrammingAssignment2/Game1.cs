#region Using Statements
using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

#endregion

namespace ProgrammingAssignment2
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
		const int WindowWidth = 800;
		const int WindowHeight = 600;

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		// STUDENTS: declare variables for three sprites
		Texture2D sprite0;
		Texture2D sprite1;
		Texture2D sprite2;

		// STUDENTS: declare variables for x and y speeds
		int xSpeed;
		int ySpeed;

		// used to handle generating random values
		Random rand = new Random();
		const int ChangeDelayTime = 1000;
		int elapsedTime = 0;

		// used to keep track of current sprite and location
		Texture2D currentSprite;
		Rectangle drawRectangle = new Rectangle();

		public Game1 ()
		{
			graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";	            

			graphics.PreferredBackBufferWidth = WindowWidth;
			graphics.PreferredBackBufferHeight = WindowHeight;
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

			// STUDENTS: load the sprite images here
			sprite0 = Content.Load<Texture2D>(@"graphics\black");
			sprite1 = Content.Load<Texture2D>(@"graphics\blue");
			sprite2 = Content.Load<Texture2D>(@"graphics\green");

			// STUDENTS: set the currentSprite variable to one of your sprite variables
			currentSprite = sprite0;

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

			elapsedTime += gameTime.ElapsedGameTime.Milliseconds;
			if (elapsedTime > ChangeDelayTime)
			{
				elapsedTime = 0;

				// STUDENTS: uncomment the code below and make it generate a random number 
				// between 0 and 2 inclusive using the rand field I provided
				int spriteNumber = rand.Next(3);

				// sets current sprite
				// STUDENTS: uncomment the lines below and change sprite0, sprite1, and sprite2
				//      to the three different names of your sprite variables
				if (spriteNumber == 0)
				{
				    currentSprite = sprite0;
				}
				else if (spriteNumber == 1)
				{
				    currentSprite = sprite1;
				}
				else if (spriteNumber == 2)
				{
				    currentSprite = sprite2;
				}

				// STUDENTS: set the drawRectangle.Width and drawRectangle.Height to match the width and height of currentSprite
				drawRectangle.Width = currentSprite.Width;
				drawRectangle.Height = currentSprite.Height;

				// STUDENTS: center the draw rectangle in the window. Note that the X and Y properties of the rectangle
				// are for the upper left corner of the rectangle, not the center of the rectangle
				drawRectangle.X = WindowWidth / 2 - drawRectangle.Width / 2;
				drawRectangle.Y = WindowHeight / 2 - drawRectangle.Height / 2;

				// STUDENTS: write code below to generate random numbers  between -4 and 4 inclusive for the x and y speed 
				// using the rand field I provided
				// CAUTION: Don't redeclare the x speed and y speed variables here!
				xSpeed = rand.Next(-4, 4);
				ySpeed = rand.Next(-4, 4);
			}

			// STUDENTS: move the drawRectangle by the x speed and the y speed
			drawRectangle.X += xSpeed;
			drawRectangle.Y += ySpeed;

			base.Update(gameTime);
		}
			
		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw (GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear (Color.CornflowerBlue);

			// STUDENTS: draw current sprite here
			spriteBatch.Begin();
			spriteBatch.Draw(currentSprite, drawRectangle, Color.White);
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}