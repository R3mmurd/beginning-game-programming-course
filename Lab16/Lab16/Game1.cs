using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

namespace Lab16
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
		const int WindowWidth = 800;
		const int WindowHeight = 600;

		const int PixelsToMove = 5;

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		Texture2D sprite;
		Rectangle rect;

		int offScreenCount = 0;

		SpriteFont font;
		Vector2 textPosition;

		bool isSpriteInside = true;

		public Game1 ()
		{
			graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";
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
			graphics.PreferredBackBufferWidth = WindowWidth;
			graphics.PreferredBackBufferHeight = WindowHeight;
            
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

			//TODO: use this.Content to load your game content here 

			sprite = Content.Load<Texture2D> (@"images\C-14");
			rect = new Rectangle (WindowWidth / 2 - sprite.Width / 2, WindowHeight / 2 - sprite.Height / 2,
				sprite.Width, sprite.Height);

			font = Content.Load<SpriteFont> (@"fonts\Arial20");
			textPosition = new Vector2 (WindowWidth / 12, WindowHeight / 12);
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
			#if !__IOS__ &&  !__TVOS__
			if (GamePad.GetState (PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState ().IsKeyDown (Keys.Escape))
				Exit ();
			#endif
            
			// TODO: Add your update logic here
            
			KeyboardState keyboard = Keyboard.GetState();

			if (keyboard.IsKeyDown (Keys.W)) 
			{
				rect.Y -= PixelsToMove;
			}

			if (keyboard.IsKeyDown (Keys.A)) 
			{
				rect.X -= PixelsToMove;
			}

			if (keyboard.IsKeyDown (Keys.S)) 
			{
				rect.Y += PixelsToMove;
			}

			if (keyboard.IsKeyDown (Keys.D)) 
			{
				rect.X += PixelsToMove;
			}

			if (keyboard.IsKeyDown (Keys.Space)) 
			{
				rect.X = WindowWidth / 2 - sprite.Width / 2;
				rect.Y = WindowHeight / 2 - sprite.Height / 2;
			}

			if ((rect.X < 0 || rect.Y < 0 ||
			    rect.Bottom > WindowHeight || rect.Right > WindowWidth) &&
			    isSpriteInside) 
			{
				offScreenCount++;
			}

			isSpriteInside = rect.X >= 0 && rect.Y >= 0 && rect.Bottom <= WindowHeight && rect.Right <= WindowWidth;

			base.Update (gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw (GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear (Color.CornflowerBlue);
            
			//TODO: Add your drawing code here
			spriteBatch.Begin();

			spriteBatch.Draw (sprite, rect, Color.White);
			spriteBatch.DrawString (font, "Off screen: " + offScreenCount, textPosition, Color.White);

			spriteBatch.End();
            
			base.Draw (gameTime);
		}
	}
}

