using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lab6
{
    /// <summary>
    /// A teddy bear
    /// </summary>
    public class TeddyBear
    {
        #region Fields

        Random rand = new Random();

        // graphic and drawing info
        string spriteName;
        Texture2D sprite;
        Rectangle drawRectangle = new Rectangle();

        // velocity information
        Vector2 velocity = new Vector2(0, 0);

        #endregion

        #region Constructors

        /// <summary>
        ///  Constructs a teddy bear with random direction and speed
        /// </summary>
        /// <param name="contentManager">the content manager for loading content</param>
        /// <param name="spriteName">the name of the sprite for the teddy bear</param>
        /// <param name="x">the x location</param>
        /// <param name="y">the y location</param>
        public TeddyBear(ContentManager contentManager, string spriteName, int x, int y)
        {
            this.spriteName = spriteName;
            drawRectangle.X = x;
            drawRectangle.Y = y;
            LoadContent(contentManager);

            // generate random velocity
            int speed = rand.Next(5) + 3;
            double angle = 2 * Math.PI * rand.NextDouble();
            velocity.X = (float)Math.Cos(angle) * speed;
            velocity.Y = (float)Math.Sin(angle) * speed;
        }

        /// <summary>
        /// Constructs a teddy bear with the given characteristics
        /// </summary>
        /// <param name="contentManager">the content manager for loading content</param>
        /// <param name="spriteName">the name of the sprite for the teddy bear</param>
        /// <param name="x">the x location</param>
        /// <param name="y">the y location</param>
        /// <param name="velocity">the velocity vector for the teddy bear</param>
        public TeddyBear(ContentManager contentManager, string spriteName, int x, int y, Vector2 velocity)
        {
            this.spriteName = spriteName;
            drawRectangle.X = x;
            drawRectangle.Y = y;
            LoadContent(contentManager);
            this.velocity = velocity;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Updates the teddy bear's location, bouncing if necessary
        /// </summary>
        public void Update()
        {
            // move the teddy bear
            drawRectangle.X += (int)(velocity.X);
            drawRectangle.Y += (int)(velocity.Y);

            // bounce as necessary
            TopBottomBounce();
            LeftRightBounce();
        }

        /// <summary>
        /// Draws the teddy bear
        /// </summary>
        /// <param name="spriteBatch">the sprite batch to use</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, drawRectangle, Color.White);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Loads the content for the teddy bear
        /// </summary>
        /// <param name="contentManager">the content manager to use</param>
        private void LoadContent(ContentManager contentManager)
        {
            // load content and set remainder of draw rectangle
            sprite = contentManager.Load<Texture2D>(spriteName);
            drawRectangle.Width = sprite.Width;
            drawRectangle.Height = sprite.Height;
        }

        /// <summary>
        /// Bounces the weapon off the top and bottom window borders if necessary
        /// </summary>
        private void TopBottomBounce()
        {
            if (drawRectangle.Top < 0)
            {
                // bounce off top
                drawRectangle.Y = 0;
                velocity.Y *= -1;
            }
            else if (drawRectangle.Bottom > Game1.WindowHeight)
            {
                // bounce off bottom
                drawRectangle.Y = Game1.WindowHeight - drawRectangle.Height;
                velocity.Y *= -1;
            }
        }
        /// <summary>
        /// Bounces the weapon off the left and right window borders if necessary
        /// </summary>
        private void LeftRightBounce()
        {
            if (drawRectangle.Left < 0)
            {
                // bounce off left
                drawRectangle.X = 0;
                velocity.X *= -1;
            }
            else if (drawRectangle.Right > Game1.WindowWidth)
            {
                // bounce off right
                drawRectangle.X = Game1.WindowWidth - drawRectangle.Width;
                velocity.X *= -1;
            }
        }

        #endregion
    }
}
