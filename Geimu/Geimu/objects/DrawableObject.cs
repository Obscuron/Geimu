using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Geimu {
    
    // Abstract class for all drawable game objects
    public abstract class DrawableObject {
        // Texture
        protected static Texture2D sprite;

        protected List<Sprite> spriteList = new List<Sprite>();

        // Filename - set this
        public abstract String FileName {
            get { return ""; }
        }

        // Loads the texture
        public void LoadContent(ContentManager content) {
            if (sprite == null)
                sprite = content.Load<Texture2D>(FileName);
        }

        // Draws the sprite
        public virtual void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            //spriteBatch.Draw(sSprite, mPos, sector, Color.White, mRot, origin, mScale, SpriteEffects.None, 0);

            spriteBatch.End();
        }

    }

}
