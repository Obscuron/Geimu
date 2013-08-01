using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Geimu {

    public class ControlsScreen : MenuScreen {

        public ControlsScreen() {
            menuEntries.Add("Up");
            menuEntries.Add("Down");
            menuEntries.Add("Left");
            menuEntries.Add("Right");
            menuEntries.Add("Duck");
            menuEntries.Add("Shoot");

            menuScale = 0.5f;
        }

        protected override void OnSelected(int selection) {
            base.OnSelected(selection);
        }

        protected override void OnCancel() {
            screenManager.screenReference.options.Activate();
            screenManager.RemoveScreen(this);

            base.OnCancel();
        }

        public override void Draw(GameTime gameTime) {
            screenManager.spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            String title = "Controls";
            Vector2 pos = new Vector2(screenManager.bounds.Width / 2, 75);
            Vector2 origin = fontCambria.MeasureString(title) / 2;

            DrawBorderedText(title, pos, origin);

            screenManager.spriteBatch.End();

            menuPos = new Vector2(200, 150);

            base.Draw(gameTime);
        }

    }

}
