using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Geimu {

    // Screen for modifying key config
    public class ControlsChooser : MenuScreen {
        // List of keys
        String[] menuKeys = new String[12];

        // The controller for this screen
        protected static RebindController rebindController = new RebindController();

        public new RebindController controller {
            get { return rebindController; }
            set { rebindController = value; }
        }

        // Constructor
        public ControlsChooser() {
            for (int i = 0; i < 12; i++)
                menuEntries.Add("");

            menuScale = 0.5f;
        }

        // Initializes the strings to display from keys
        public override void Initialize() {
            GetKeys();

            menuJustify = 2;
            mUpdateState = false;
            menuChoice = -1;
        }

        // Gets the keys bindings from data
        public void GetKeys() {
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

        // Sets the key in data
        protected void SetKey(int num, Keys newKey) {
            int i = num / 6;
            num %= 6;
            switch (num) {
                case 0:
                    screenManager.dataReference.controls.up[i] = newKey;
                    break;
                case 1:
                    screenManager.dataReference.controls.down[i] = newKey;
                    break;
                case 2:
                    screenManager.dataReference.controls.left[i] = newKey;
                    break;
                case 3:
                    screenManager.dataReference.controls.right[i] = newKey;
                    break;
                case 4:
                    screenManager.dataReference.controls.walk[i] = newKey;
                    break;
                case 5:
                    screenManager.dataReference.controls.fire[i] = newKey;
                    break;
            }
        }

        // Gets the key inputted by keyboard
        public override void Update(GameTime gameTime) {
            if (controller.cancel) {
                OnCancel();
                return;
            }

            Keys? newKey = controller.GetNewKeyPress();
            if (newKey != null) {
                menuKeys[menuChoice] = newKey.ToString();
                SetKey(menuChoice, (Keys)newKey);
                OnCancel();
            }
        }

        // Reads the input
        public override void HandleInput(InputState input) {
            controller.ReadInput(input);
        }

        // Draws the key bindings
        public override void Draw(GameTime gameTime) {
            menuPos = new Vector2(600, 125);

            base.Draw(gameTime);
        }

        // Activates menu based on a choice
        public void Activate(int choice) {
            menuChoice = choice;
            menuEntries[choice] = "Press New Key";
            Activate();
        }

    }

}
