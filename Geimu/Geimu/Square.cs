
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Geimu {

    public class Square {

        private static Texture2D mSprite;

        protected Vector2 mPos;

        public Square(int x, int y) {
            mPos = new Vector2(x, y);
        }

        public static void LoadContent(ContentManager content) {
            mSprite = content.Load<Texture2D>("images\\Square");
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Begin();
            spriteBatch.Draw(mSprite, mPos, Color.White);
            spriteBatch.End();
        }

    }

}
