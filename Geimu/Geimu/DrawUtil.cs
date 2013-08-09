using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Geimu {

    // Class for handling drawing of shapes using a pixel
    public class DrawUtil {
        // Texture of a single white pixel;
        private static Texture2D sprite;

        // Loads pixel
        public static void LoadContent(ContentManager content) {
            sprite = content.Load<Texture2D>("Pixel");
        }

        // Draws a Rectangle
        public static void DrawRectangle(SpriteBatch spriteBatch, Rectangle location, Color tint, float depth) {
            spriteBatch.Draw(sprite, location, new Rectangle(0, 0, 1, 1), tint, 0.0f, Vector2.Zero, SpriteEffects.None, depth);
        }

        public static void DrawRectangle(SpriteBatch spriteBatch, Rectangle location, Color tint) {
            DrawRectangle(spriteBatch, location, tint, 0);
        }

        public static void DrawRectangle(SpriteBatch spriteBatch, Rectangle location, float depth) {
            DrawRectangle(spriteBatch, location, Color.White, depth);
        }

        public static void DrawRectangle(SpriteBatch spriteBatch, Rectangle location) {
            DrawRectangle(spriteBatch, location, Color.White, 0);
        }

    }

}
