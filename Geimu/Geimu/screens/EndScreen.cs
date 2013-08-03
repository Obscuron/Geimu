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
        // Determins whether "r" has been pressed
        protected bool retryKey = false;

        // Constructor
        public EndScreen() {
            menuEntries.Add("Retry");
            menuEntries.Add("Exit to Main Menu");

            menuJustify = 1;
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
            screenManager.RemoveAll();
            screenManager.dataReference.gameSave.Reset();
            screenManager.AddScreen(screenManager.screenReference.menu);

            base.OnCancel();
        }

        // Replays the game
        protected void Retry() {
            screenManager.RemoveScreen(this);
            screenManager.screenReference.game.Initialize();
            screenManager.screenReference.game.NewGame();
        }

        // Waits for R to reset the game
        public override void Update(GameTime gameTime) {
            if (retryKey)
                Retry();

            base.Update(gameTime);
        }

        // Checks for R key
        public override void HandleInput(InputState input) {
            retryKey = false;
            if (input.IsNewKeyPress(Keys.R))
                retryKey = true;

            base.HandleInput(input);
        }

        // Draws the game over text
        public override void Draw(GameTime gameTime) {
            screenManager.spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            Rectangle bounds = screenManager.bounds;

            String text = screenManager.screenReference.game.EndMessage();
            Vector2 pos = new Vector2(bounds.Width / 2, bounds.Height / 2);
            Vector2 origin = fontCambria.MeasureString(text) / 2;

            DrawBorderedText(text, pos, origin);

            screenManager.spriteBatch.End();

            menuPos = new Vector2(bounds.Width / 2, fontCambria.LineSpacing + bounds.Height / 2);

            base.Draw(gameTime);
        }

    }

}
