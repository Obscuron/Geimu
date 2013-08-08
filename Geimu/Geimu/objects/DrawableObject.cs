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

        public Sprite SpriteObject;

        // Loads the texture
        public static void LoadContent(ContentManager content, string FileName) {
            sprite = content.Load<Texture2D>(FileName);
        }

        // Draws all the sprites
        public virtual void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            spriteBatch.Draw(sprite, SpriteObject.Pos, SpriteObject.Sector, SpriteObject.Tint, 0.0f, SpriteObject.Origin, SpriteObject.Scale, SpriteEffects.None, SpriteObject.Depth);

            spriteBatch.End();
        }

    }

}
