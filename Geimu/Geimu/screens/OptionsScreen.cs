using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Geimu {

    public class OptionsScreen : MenuScreen {

        // Constructor
        public OptionsScreen() {
            menuEntries.Add("Controls");
            menuEntries.Add("Back");
        }

        protected override void OnSelected(int selection) {
            switch (selection) {
                case 0:
                    break;
                case 1:
                    OnCancel();
                    break;
            }

            base.OnSelected(selection);
        }

        // Returns to main menu
        protected override void OnCancel() {
            screenManager.screenList.menu.Activate();
            screenManager.RemoveScreen(this);

            base.OnCancel();
        }

        // Draws the options menu
        public override void Draw(GameTime gameTime) {
            screenManager.spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            String title = "Options";
            Vector2 pos = new Vector2(screenManager.bounds.Width / 2, 75);
            Vector2 origin = fontCambria.MeasureString(title) / 2;

            DrawBorderedText(title, pos, origin);

            screenManager.spriteBatch.End();

            menuPos = new Vector2(200, screenManager.bounds.Height / 2 - 100);

            base.Draw(gameTime);
        }

    }

}
