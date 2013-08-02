using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Geimu {

    // Screen for modifying controls
    public class ControlsScreen : MenuScreen {
        // Constructor
        public ControlsScreen() {
            menuEntries.Add("Up");
            menuEntries.Add("Down");
            menuEntries.Add("Left");
            menuEntries.Add("Right");
            menuEntries.Add("Duck");
            menuEntries.Add("Shoot");
            menuEntries.Add("Up");
            menuEntries.Add("Down");
            menuEntries.Add("Left");
            menuEntries.Add("Right");
            menuEntries.Add("Duck");
            menuEntries.Add("Shoot");
            menuEntries.Add("Reset All");
            menuEntries.Add("Back");

            menuScale = 0.5f;
        }

        // Select menu choice
        protected override void OnSelected(int selection) {
            if (selection == 13)
                OnCancel();
            else if (selection == 12) {
                screenManager.dataReference.controls.Reset();
                screenManager.screenReference.chooser.GetKeys();
            }
            else {
                menuChoice = -1;
                mUpdateState = false;
                screenManager.screenReference.chooser.Activate(selection);
            }

            base.OnSelected(selection);
        }

        // Returns to previous menu
        protected override void OnCancel() {
            screenManager.screenReference.options.Activate();
            screenManager.RemoveScreen(this);
            screenManager.RemoveScreen(screenManager.screenReference.chooser);
            screenManager.dataReference.controls.SaveData();

            base.OnCancel();
        }

        // Draws the menu
        public override void Draw(GameTime gameTime) {
            menuPos = new Vector2(200, 125);

            screenManager.spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            String title = "Controls";
            Vector2 titlePos = new Vector2(screenManager.bounds.Width / 2, 60);
            Vector2 titleOrigin = fontCambria.MeasureString(title) / 2;

            Vector2 pos1 = menuPos + new Vector2(-150, fontCambria.LineSpacing * 2.5f * menuScale);
            Vector2 pos2 = menuPos + new Vector2(-150, fontCambria.LineSpacing * 8.5f * menuScale);

            DrawBorderedText(title, titlePos, titleOrigin);
            DrawBorderedText("P1", pos1, new Vector2(0, fontCambria.LineSpacing / 2));
            DrawBorderedText("P2", pos2, new Vector2(0, fontCambria.LineSpacing / 2));

            screenManager.spriteBatch.End();

            base.Draw(gameTime);
        }

        // Activates menu based on a choice
        public void Activate(int choice) {
            menuChoice = choice;
            Activate();
        }

    }

}
