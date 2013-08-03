using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Geimu {

    // Screen for the pause menu
    public class PauseScreen : MenuScreen {

        // Constructor
        public PauseScreen() {
            menuEntries.Add("Back to Game");
            menuEntries.Add("Restart Game");
            menuEntries.Add("Save and Back");
            menuEntries.Add("Back to Main Menu");

            menuJustify = 1;
        }

        // On menu selection
        protected override void OnSelected(int selection) {
            switch (selection) {
                case 0:
                    OnCancel();
                    break;
                case 1:
                    screenManager.RemoveScreen(this);
                    screenManager.screenReference.game.Initialize();
                    screenManager.screenReference.game.NewGame();
                    break;
                case 2:
                    screenManager.RemoveAll();
                    screenManager.dataReference.gameSave.isSave = true;
                    screenManager.screenReference.game.SaveGame();
                    screenManager.AddScreen(screenManager.screenReference.menu);
                    break;
                case 3:
                    screenManager.RemoveAll();
                    screenManager.dataReference.gameSave.isSave = false;
                    screenManager.dataReference.gameSave.Reset();
                    screenManager.AddScreen(screenManager.screenReference.menu);
                    break;
            }

            base.OnSelected(selection);
        }

        // Returns back to game
        protected override void OnCancel() {
            screenManager.RemoveScreen(this);
            screenManager.screenReference.game.Activate();

            base.OnCancel();
        }

        // Draws the pause screen
        public override void Draw(GameTime gameTime) {
            screenManager.spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            String title = "Paused";
            Vector2 pos = new Vector2(screenManager.bounds.Width / 2, 60);
            Vector2 origin = fontCambria.MeasureString(title) / 2;

            DrawBorderedText(title, pos, origin);

            screenManager.spriteBatch.End();

            menuPos = new Vector2(screenManager.bounds.Width / 2, screenManager.bounds.Height / 2 - 100);

            base.Draw(gameTime);
        }

    }

}
