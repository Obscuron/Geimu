using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Geimu {

    // Screen for modifying key config
    public class ControlsChooser : MenuScreen {
        // List of keys
        String[] menuKeys = new String[12];

        // Constructor
        public ControlsChooser() {
            for (int i = 0; i < 12; i++)
                menuEntries.Add("");

            menuScale = 0.5f;
        }

        // Initializes the strings to display from keys
        public override void Initialize() {
            for (int i = 0; i < 2; i++) {
                menuKeys[6 * i] = screenManager.dataReference.controls.up[i].ToString();
                menuKeys[6 * i + 1] = screenManager.dataReference.controls.down[i].ToString();
                menuKeys[6 * i + 2] = screenManager.dataReference.controls.left[i].ToString();
                menuKeys[6 * i + 3] = screenManager.dataReference.controls.right[i].ToString();
                menuKeys[6 * i + 4] = screenManager.dataReference.controls.walk[i].ToString();
                menuKeys[6 * i + 5] = screenManager.dataReference.controls.fire[i].ToString();
            }

            for (int i = 0; i < 12; i++)
                menuEntries[i] = menuKeys[i];

            menuJustify = 2;
            mUpdateState = false;
            menuChoice = -1;
        }

        // Do nothing upon selection
        protected override void OnSelected(int selection) { }

        // Returns to controls menu
        protected override void OnCancel() {
            screenManager.screenReference.controls.Activate(menuChoice);
            menuEntries[menuChoice] = menuKeys[menuChoice];
            menuChoice = -1;
            mUpdateState = false;

            base.OnCancel();
        }

        public override void Update(GameTime gameTime) {
            if (controller.cancel)
                OnCancel();
        }

        public override void Draw(GameTime gameTime) {
            menuPos = new Vector2(600, 150);

            base.Draw(gameTime);
        }

        public void Activate(int choice) {
            menuChoice = choice;
            menuEntries[choice] = "Press New Key";
            Activate();
        }

    }

}
