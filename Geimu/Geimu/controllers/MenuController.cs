using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Geimu {

    // Class for handling Menu input
    public class MenuController {

        // Keyboard states
        protected KeyboardState prevKeyboard;
        protected KeyboardState curKeyboard;

        // Mouse states
        protected MouseState prevMouse;
        protected MouseState curMouse;

        // Selection properties
        public bool prev {
            get { return IsNewKeyPress(Keys.Up) || IsNewKeyPress(Keys.Left); }
        }

        public bool next {
            get { return IsNewKeyPress(Keys.Down) || IsNewKeyPress(Keys.Right); }
        }

        public bool select {
            get { return IsNewKeyPress(Keys.Space) || IsNewKeyPress(Keys.Enter); }
        }

        public bool cancel {
            get { return IsNewKeyPress(Keys.Escape); }
        }

        // Reads the input
        public void readInput() {
            prevKeyboard = curKeyboard;
            prevMouse = curMouse;

            curKeyboard = Keyboard.GetState();
            curMouse = Mouse.GetState();
        }

        // Checks if a key was newly pressed
        public bool IsNewKeyPress(Keys key) {
            return (curKeyboard.IsKeyDown(key) && prevKeyboard.IsKeyUp(key));
        }

    }

}
