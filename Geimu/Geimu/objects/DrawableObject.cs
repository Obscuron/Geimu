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
        protected Texture2D sprite;

        public Sprite SpriteObject;

        // Loads the texture
        public virtual void LoadContent(ContentManager content) {
            sprite = content.Load<Texture2D>("");
        }

        // Draws all the sprites
        public virtual void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            DrawSprite(spriteBatch, SpriteObject);

            spriteBatch.End();
        }

        // Draws sprite based on scale (more precise)
        public virtual void DrawSprite(SpriteBatch spriteBatch, Sprite spriteObject) {
            spriteBatch.Draw(sprite, spriteObject.Pos, spriteObject.Sector, spriteObject.Tint, 0.0f, spriteObject.Origin, spriteObject.ScaleVector, SpriteEffects.None, spriteObject.Depth);
        }

        // Draws sprite at a rectangle location
        public virtual void DrawSprite(SpriteBatch spriteBatch, Sprite spriteObject, Rectangle location) {
            spriteBatch.Draw(sprite, location, spriteObject.Sector, spriteObject.Tint, 0.0f, spriteObject.Origin, SpriteEffects.None, spriteObject.Depth);
        }

    }

}
