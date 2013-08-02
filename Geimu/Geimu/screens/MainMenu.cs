using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Geimu {

    // Screen for the main menu
    public class MainMenu : MenuScreen {

        // Constructor
        public MainMenu() {
            menuEntries.Add("Start Game");
            menuEntries.Add("Options");
            menuEntries.Add("Exit");
        }

        // On menu selection
        protected override void OnSelected(int selection) {
            switch (selection) {
                case 0:
                    screenManager.AddScreen(screenManager.screenReference.game);
                    screenManager.RemoveScreen(this);
                    break;
                case 1:
                    Deactivate();
                    screenManager.AddScreen(screenManager.screenReference.options);
                    break;
                case 2:
                    OnCancel();
                    break;
            }

            base.OnSelected(selection);
        }

        // Exits the game
        protected override void OnCancel() {
            screenManager.game.Exit();

            base.OnCancel();
        }

        // Draws the menu
        public override void Draw(GameTime gameTime) {
            screenManager.spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            String title = "Main Menu";
            Vector2 pos = new Vector2(screenManager.bounds.Width / 2, 60);
            Vector2 origin = fontCambria.MeasureString(title) / 2;

            DrawBorderedText(title, pos, origin);

            screenManager.spriteBatch.End();

            menuPos = new Vector2(200, screenManager.bounds.Height / 2 - 100);

            base.Draw(gameTime);
        }

    }

}
