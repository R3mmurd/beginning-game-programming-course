using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Lab15
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

		SoundEffect upperRight;
		SoundEffect lowerRight;
		SoundEffect upperLeft;
		SoundEffect lowerLeft;

		bool isMousePressed = false;

		public Game1 ()
		{
			graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";

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

			//TODO: use this.Content to load your game content here 
			upperRight = Content.Load<SoundEffect>(@"audio\upperRight");
			lowerRight = Content.Load<SoundEffect>(@"audio\lowerRight");
			upperLeft = Content.Load<SoundEffect>(@"audio\upperLeft");
			lowerLeft = Content.Load<SoundEffect>(@"audio\lowerLeft");
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

			MouseState mouse = Mouse.GetState();

			if (mouse.LeftButton == ButtonState.Pressed && !isMousePressed) 
			{
				if (mouse.X > WindowWidth / 2 && mouse.Y > WindowHeight / 2) 
				{
					lowerRight.Play();
				}
				else if (mouse.X > WindowWidth / 2 && mouse.Y < WindowHeight / 2) 
				{
					upperRight.Play();
				}
				else if (mouse.X < WindowWidth / 2 && mouse.Y > WindowHeight / 2) 
				{
					lowerLeft.Play();
				}
				else if (mouse.X < WindowWidth / 2 && mouse.Y < WindowHeight / 2) 
				{
					upperLeft.Play();
				}
			}

			isMousePressed = mouse.LeftButton == ButtonState.Pressed;
            
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
            
			base.Draw (gameTime);
		}
	}
}

