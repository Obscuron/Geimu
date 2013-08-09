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

        // Sprites for empty and full hp bars
        protected Sprite emptyHealth;
        protected Sprite fullHealth;

        // Locations for the 3 sprites
        protected Rectangle borderLoc;
        protected Rectangle emptyLoc;
        protected Rectangle fullLoc;

        // Creates a new healthbar set to a certain max health
        public HealthBar(Square square, Rectangle bounds, int id) {
            squareReference = square;

            SpriteObject = new Sprite(new Rectangle(0, 0, WIDTH, 44));
            emptyHealth = fullHealth = new Sprite(new Rectangle(0, 49, WIDTH, 34));

            if (id == 1) {
                SpriteObject.Origin = emptyHealth.Origin = fullHealth.Origin = new Vector2(WIDTH, 0);
            }

            borderLoc = new Rectangle(16, 16, 300, 44);
            if (id == 1)
                borderLoc.X = bounds.Width - 16;

            emptyLoc = new Rectangle(borderLoc.X + 5, 21, 290, 34);
            fullLoc = new Rectangle(borderLoc.X + 5, 21, 290, 34);
            if (id == 1) {
                emptyLoc.X -= 10;
                fullLoc.X -= 10;
            }

            emptyHealth.Tint = Color.Gray;
            fullHealth.Tint = Color.Red;

            emptyHealth.Depth = 1;
            fullHealth.Depth = 0.5f;
            
        }

        // Loads texture into memory
        public override void LoadContent(ContentManager content) {
            sprite = content.Load<Texture2D>("images\\HealthBar");
        }

        // Draws the healthbar
        public override void Draw(SpriteBatch spriteBatch) {
            double ratio = (double)squareReference.Health / squareReference.MaxHealth;

            fullLoc.Width = (int)(ratio * (290));

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            DrawSprite(spriteBatch, SpriteObject, borderLoc);
            DrawSprite(spriteBatch, emptyHealth, emptyLoc);
            DrawSprite(spriteBatch, fullHealth, fullLoc);

            DrawUtil.DrawRectangle(spriteBatch, new Rectangle(300, 300, 300, 100));

            spriteBatch.End();
        }

    }

}
