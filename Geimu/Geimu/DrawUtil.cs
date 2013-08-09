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

        // Offset values for changing origin of drawn rectangle
        public enum Offset { TopLeft, Top, TopRight, Left, Center, Right, BotLeft, Bot, BotRight };

        private static Vector2 OffsetToVector(Offset offset) {
            int val = (int)offset;
            return new Vector2(0.5f * (val % 3), 0.5f * (val / 3));
        }

        // Draws a Rectangle
        public static void DrawRectangle(SpriteBatch spriteBatch, Rectangle location, Color tint, Vector2 origin, float depth) {
            spriteBatch.Draw(sprite, location, new Rectangle(0, 0, 1, 1), tint, 0.0f, new Vector2(origin.X / location.Width, origin.Y / location.Height), SpriteEffects.None, depth);
        }

        // Overloaded methods
        public static void DrawRectangle(SpriteBatch spriteBatch, Rectangle location, Color tint, Vector2 origin) {
            DrawRectangle(spriteBatch, location, tint, origin, 0);
        }

        public static void DrawRectangle(SpriteBatch spriteBatch, Rectangle location, Color tint) {
            DrawRectangle(spriteBatch, location, tint, Vector2.Zero, 0);
        }

        public static void DrawRectangle(SpriteBatch spriteBatch, Rectangle location) {
            DrawRectangle(spriteBatch, location, Color.White, Vector2.Zero, 0);
        }

        // Draws a Rectangle using offset
        public static void DrawRectangle(SpriteBatch spriteBatch, Rectangle location, Color tint, Offset origin, float depth) {
            spriteBatch.Draw(sprite, location, new Rectangle(0, 0, 1, 1), tint, 0.0f, OffsetToVector(origin), SpriteEffects.None, depth);
        }

        public static void DrawRectangle(SpriteBatch spriteBatch, Rectangle location, Color tint, Offset origin) {
            spriteBatch.Draw(sprite, location, new Rectangle(0, 0, 1, 1), tint, 0.0f, OffsetToVector(origin), SpriteEffects.None, 0);
        }

    }

}
