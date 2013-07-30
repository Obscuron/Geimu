using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Geimu {

    // Screen when the game is over
    public class EndScreen : MenuScreen {

        protected GameScreen gameScreen;

        // Constructor
        public EndScreen(GameScreen game) {
            Activate();
            gameScreen = game;

            menuEntries.Add("Retry");
            menuEntries.Add("Exit");

            menuScale = 0.8f;
            menuJustify = 1;

            menuController = new MenuController();
        }

        // Selected menu choice
        protected override void OnSelected(int selection) {
            switch (selection) {
                case 0:
                    Retry();
                    break;
                case 1:
                    OnCancel();
                    break;
            }

            base.OnSelected(selection);
        }

        // Canceling will exit the game
        protected override void OnCancel() {
            screenManager.game.Exit();

            base.OnCancel();
        }

        protected void Retry() {
            screenManager.RemoveScreen(this);
            gameScreen.Initialize();
        }

        // Waits for R to reset the game
        public override void Update(GameTime gameTime) {
            if (Keyboard.GetState().IsKeyDown(Keys.R))
                Retry();

            base.Update(gameTime);
        }

        // Draws the game over text
        public override void Draw(GameTime gameTime) {
            screenManager.spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            Rectangle bounds = screenManager.bounds;

            String line = String.Format("Player {0} Wins!", gameScreen.Winner());
            Vector2 pos = new Vector2(bounds.Width / 2, bounds.Height / 2);
            Vector2 origin = fontCambria.MeasureString(line) / 2;

            DrawBorderedText(line, pos, origin);

            screenManager.spriteBatch.End();

            menuPos = new Vector2(bounds.Width / 2, fontCambria.LineSpacing + bounds.Height / 2);

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
