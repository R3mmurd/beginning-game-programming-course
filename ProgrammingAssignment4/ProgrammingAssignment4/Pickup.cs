using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProgrammingAssignment4
{
	/// <summary>
	/// A pickup
	/// </summary>
	public class Pickup
	{
		#region Fields

		// drawing support
		Texture2D sprite;
		Rectangle drawRectangle;

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="sprite">sprite for the pickup</param>
		/// <param name="location">location of the center of the pickup</param>
		public Pickup(Texture2D sprite, Vector2 location)
		{
			this.sprite = sprite;

			// STUDENTS: set draw rectangle so pickup is centered on location
			drawRectangle.X = (int) (location.X - sprite.Width / 2);
			drawRectangle.Y = (int) (location.Y - sprite.Height / 2);
			drawRectangle.Width = sprite.Width;
			drawRectangle.Height = sprite.Height;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the collision rectangle for the pickup
		/// </summary>
		public Rectangle CollisionRectangle
		{
			get { return drawRectangle; }
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Draws the pickup
		/// </summary>
		/// <param name="spriteBatch">sprite batch</param>
		public void Draw(SpriteBatch spriteBatch)
		{
			// STUDENTS: use the sprite batch to draw the pickup
			spriteBatch.Draw(sprite, drawRectangle, Color.White);
		}

		#endregion
	}
}