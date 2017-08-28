using System;
using System.Collections.Generic;
using TeddyMineExplosion;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

namespace ProgrammingAssignment5
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

		// Mines support
		Texture2D mineSprite;	
		List<Mine> mines = new List<Mine>();

		// Teddy support
		Texture2D teddySprite;
		List<TeddyBear> teddys = new List<TeddyBear>();
		int elapsedTimeForSpawningTeddyMilliseconds = 0;
		int teddySpawnDelay;
		Random rand = new Random();

		// Explosion support
		Texture2D explosionStrip;
		List<Explosion> explosions = new List<Explosion>();

		ButtonState leftButtonLastState = ButtonState.Released;

		public Game1 ()
		{
			graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";

			graphics.PreferredBackBufferWidth = WindowWidth;
			graphics.PreferredBackBufferHeight = WindowHeight;
			IsMouseVisible = true;

			teddySpawnDelay = rand.Next(1, 4) * 1000;
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

			// Load mine sprite
			mineSprite = Content.Load<Texture2D>(@"graphics\mine");

			// Load teddybear sprite
			teddySprite = Content.Load<Texture2D>(@"graphics\teddybear");

			// Load explosion sprite
			explosionStrip = Content.Load<Texture2D>(@"graphics\explosion");
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

			// Collision detection
			foreach (TeddyBear teddy in teddys) 
			{
				foreach (Mine mine in mines) 
				{
					if (teddy.Active && mine.Active && teddy.CollisionRectangle.Intersects(mine.CollisionRectangle))
					{
						teddy.Active = false;
						mine.Active = false;

						Explosion explosion = new Explosion(explosionStrip, teddy.CollisionRectangle.X,
							                      teddy.CollisionRectangle.Y);
						explosions.Add(explosion);
					}
				}
			}

			// Updating explosions
			foreach (Explosion explosion in explosions) 
			{
				explosion.Update(gameTime);
			}

			// Updating teddys
			foreach (TeddyBear teddy in teddys) 
			{
				teddy.Update(gameTime);
			}

			// Placing mines
			MouseState mouse = Mouse.GetState();

			if (mouse.LeftButton == ButtonState.Released && leftButtonLastState == ButtonState.Pressed) 
			{
				Mine mine = new Mine(mineSprite, mouse.X, mouse.Y);
				mines.Add(mine);
			}

			leftButtonLastState = mouse.LeftButton;

			// Spawning teddy bear
			elapsedTimeForSpawningTeddyMilliseconds += gameTime.ElapsedGameTime.Milliseconds;

			if (elapsedTimeForSpawningTeddyMilliseconds >= teddySpawnDelay) 
			{
				elapsedTimeForSpawningTeddyMilliseconds = 0;
				teddySpawnDelay = rand.Next(1, 4) * 1000;

				float xVelocity = (float) rand.NextDouble() - 0.5f;
				float yVelocity = (float) rand.NextDouble() - 0.5f;

				TeddyBear teddy = new TeddyBear(teddySprite, new Vector2 (xVelocity, yVelocity),
					                  WindowWidth, WindowHeight);
				teddys.Add(teddy);
			}

			// Removing inactive teddys
			for (int i = teddys.Count - 1; i >= 0; --i)
			{
				if (!teddys[i].Active)
				{
					teddys.RemoveAt(i);
				}
			}

			// Removing inactive mines
			for (int i = mines.Count - 1; i >= 0; --i)
			{
				if (!mines[i].Active)
				{
					mines.RemoveAt(i);
				}
			}

			// Removing inactive explosions
			for (int i = explosions.Count - 1; i >= 0; --i)
			{
				if (!explosions[i].Playing)
				{
					explosions.RemoveAt(i);
				}
			}
            
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

			// Drawing mines
			foreach (Mine mine in mines)
			{
				mine.Draw(spriteBatch);	
			}

			// Drawing teddys
			foreach (TeddyBear teddy in teddys)
			{
				teddy.Draw(spriteBatch);
			}

			// Drawing explosions
			foreach (Explosion explosion in explosions)
			{
				explosion.Draw(spriteBatch);
			}

			spriteBatch.End();

			base.Draw (gameTime);
		}
	}
}

