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

        public virtual void AddSprite(Rectangle sector) {
            spriteList.Add(new Sprite(sector));
        }

        // Draws all the sprites
        public virtual void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            spriteBatch.End();
        }

        // Draws a single sprite
        public virtual void DrawSprite(SpriteBatch spriteBatch, int num) {
            spriteBatch.Draw(sprite, spriteList[num].Pos, spriteList[num].Sector, spriteList[num].Tint, 0.0f, spriteList[num].Origin, spriteList[num].Scale, SpriteEffects.None, 0);
        }

    }

}
