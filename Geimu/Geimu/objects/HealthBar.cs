using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Geimu {

    // Class for handling health
    public class HealthBar : DrawableObject {
        // Width of the sprite
        public const int WIDTH = 300;

        // Reference to the square object
        protected Square squareReference;

        // Locations for the 3 sprites
        protected Rectangle borderLoc;
        protected Rectangle emptyLoc;
        protected Rectangle fullLoc;

        protected DrawUtil.Offset offset;

        // Creates a new healthbar set to a certain max health
        public HealthBar(Square square, Rectangle bounds, int id) {
            squareReference = square;

            borderLoc = new Rectangle(16, 16, 300, 44);
            emptyLoc = new Rectangle(21, 21, 290, 34);
            fullLoc = new Rectangle(21, 21, 290, 34);
            offset = DrawUtil.Offset.TopLeft;

            if (id == 1) {
                borderLoc.X = bounds.Width - borderLoc.X;
                emptyLoc.X = bounds.Width - emptyLoc.X;
                fullLoc.X = bounds.Width - fullLoc.X;
                offset = DrawUtil.Offset.TopRight;
            }
            
        }

        // Draws the healthbar
        public override void Draw(SpriteBatch spriteBatch) {
            double ratio = (double)squareReference.Health / squareReference.MaxHealth;
            fullLoc.Width = (int)(ratio * (290));

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            DrawUtil.DrawRectangle(spriteBatch, borderLoc, Color.Black, offset, 1);
            DrawUtil.DrawRectangle(spriteBatch, emptyLoc, Color.Gray, offset, 0.5f);
            DrawUtil.DrawRectangle(spriteBatch, fullLoc, Color.Red, offset, 0);

            spriteBatch.End();
        }

    }

}
