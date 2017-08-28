using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

namespace Lab6
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
		public const int WindowWidth = 800;
		public const int WindowHeight = 600;
		
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		TeddyBear teddyBear0;
		TeddyBear teddyBear1;

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
			// Loading teddy bears
			teddyBear0 = new TeddyBear(Content, @"graphics\teddybear0", 150, 100);
			teddyBear1 = new TeddyBear(Content, @"graphics\teddybear1", 300, 100);
			base.Initialize();
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
			teddyBear0.Update();
			teddyBear1.Update();
            
			base.Update (gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw (GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear (Color.CornflowerBlue);
            
			spriteBatch.Begin();

			teddyBear0.Draw(spriteBatch);
			teddyBear1.Draw(spriteBatch);

			spriteBatch.End();
            
			base.Draw (gameTime);
		}
	}
}

