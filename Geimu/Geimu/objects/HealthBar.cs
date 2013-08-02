using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Geimu {

    // Class for handling health
    public class HealthBar {
        // Texture
        private static Texture2D sSprite;

        // Location of the rectangle
        protected Rectangle mLoc;
        protected int mPlayer;

        // Health
        protected int mMaxHealth;
        protected int mHealth;

        public int health {
            set { mHealth = value; }
        }

        // Creates a new healthbar set to a certain max health
        public HealthBar(int curHealth, int maxHealth, Rectangle location, int id) {
            mMaxHealth = maxHealth;
            mHealth = curHealth;

            mLoc = location;
            mPlayer = id;
        }

        // Loads texture into memory
        public static void LoadContent(ContentManager content) {
            sSprite = content.Load<Texture2D>("images\\HealthBar");
        }

        // Draws the healthbar
        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            Rectangle source = new Rectangle(0, 0, sSprite.Width, 44);
            Rectangle sourceHealth = new Rectangle(0, 49, sSprite.Width, 34);

            Vector2 origin = Vector2.Zero;
            if (mPlayer == 1)
                origin.X = sSprite.Width;

            double ratio = (double)mHealth / mMaxHealth;

            Rectangle empty = new Rectangle(mLoc.X + 5, mLoc.Y + 5, mLoc.Width - 10, mLoc.Height - 10);
            Rectangle full = new Rectangle(mLoc.X + 5, mLoc.Y + 5, (int)(ratio * (mLoc.Width - 10)), mLoc.Height - 10);
            if (mPlayer == 1) {
                empty.X -= 10;
                full.X -= 10;
            }

            spriteBatch.Draw(sSprite, empty, sourceHealth, Color.Gray, 0, origin, SpriteEffects.None, 1);
            spriteBatch.Draw(sSprite, full, sourceHealth, Color.Red, 0, origin, SpriteEffects.None, 0.5f);

            spriteBatch.Draw(sSprite, mLoc, source, Color.White, 0, origin, SpriteEffects.None, 0);

            spriteBatch.End();
        }

    }

}
