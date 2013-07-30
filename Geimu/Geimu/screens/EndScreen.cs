using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Geimu {

    // Screen when the game is over
    public class EndScreen : Screen {

        protected GameScreen gameScreen;

        protected SpriteFont fontCambria;

        // Constructor
        public EndScreen(GameScreen game) {
            Activate();
            gameScreen = game;
        }

        // Loads font into memory
        public override void LoadContent() {
            fontCambria = screenManager.contentManager.Load<SpriteFont>("fonts\\Cambria");

            base.LoadContent();
        }

        // Waits for R to reset the game
        public override void Update(GameTime gameTime) {
            if (Keyboard.GetState().IsKeyDown(Keys.R)) {
                screenManager.RemoveScreen(this);
                gameScreen.Initialize();
            }

            base.Update(gameTime);
        }

        // Draws the game over text
        public override void Draw(GameTime gameTime) {
            screenManager.spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            Rectangle bounds = screenManager.bounds;

            String line1 = String.Format("Player {0} Wins!", gameScreen.Winner());
            String line2 = "Press R to play again";

            Vector2 pos1 = new Vector2(bounds.Width / 2, bounds.Height / 2);
            Vector2 pos2 = new Vector2(bounds.Width / 2, 55 + bounds.Height / 2);

            Vector2 origin1 = fontCambria.MeasureString(line1) / 2;
            Vector2 origin2 = fontCambria.MeasureString(line2) / 2;

            DrawBorderedText(line1, pos1, origin1);
            DrawBorderedText(line2, pos2, origin2);

            screenManager.spriteBatch.End();

            base.Draw(gameTime);
        }

        // Draws white text with black border, assumes spritebatch has already began. Font: Cambria
        private void DrawBorderedText(String text, Vector2 pos, Vector2 origin) {
            Vector2[] offsets = { new Vector2(1, 1), new Vector2(1, -1), new Vector2(-1, 1), new Vector2(-1, -1) };

            foreach (Vector2 offset in offsets) {
                screenManager.spriteBatch.DrawString(fontCambria, text, pos + offset, Color.Black, 0, origin, 1, SpriteEffects.None, 1);
            }

            screenManager.spriteBatch.DrawString(fontCambria, text, pos, Color.White, 0, origin, 1, SpriteEffects.None, 0);

        }

    }

}
