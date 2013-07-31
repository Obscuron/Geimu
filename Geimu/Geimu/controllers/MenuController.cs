using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Geimu {

    // Class for handling Menu input
    public class MenuController {
        // Input state
        protected InputState inputState;

        // Locations of where the left mouse button was last held down
        protected Vector2 mouseDownLocation = new Vector2(-1, -1);

        // Selection properties
        public bool prev {
            get { return inputState.IsNewKeyPress(Keys.Up) || inputState.IsNewKeyPress(Keys.Left); }
        }

        public bool next {
            get { return inputState.IsNewKeyPress(Keys.Down) || inputState.IsNewKeyPress(Keys.Right); }
        }

        public bool select {
            get { return inputState.IsNewKeyPress(Keys.Space) || inputState.IsNewKeyPress(Keys.Enter); }
        }

        public bool cancel {
            get { return inputState.IsNewKeyPress(Keys.Escape); }
        }

        public bool released {
            get { return inputState.IsNewMouseLeftUp(); }
        }

        // Reads the input
        public void readInput(InputState input) {
            inputState = input;

            if (inputState.IsNewMouseLeft())
                mouseDownLocation = inputState.MousePos();
        }

        // Checks if current mouse is bounded by a rectangle
        public bool curBounded(Rectangle bounds) {
            return Bounded(inputState.MousePos(), bounds);
        }

        // Checks if mouse was previously bounded by rectangle when held down
        public bool lastBounded(Rectangle bounds) {
            return Bounded(mouseDownLocation, bounds);
        }

        // Checks if the a vector is within certain bounds
        protected bool Bounded(Vector2 pos, Rectangle bounds) {
            if (pos.X >= bounds.X && pos.X <= bounds.X + bounds.Width)
                if (pos.Y >= bounds.Y && pos.Y <= bounds.Y + bounds.Height)
                    return true;
            return false;
        }

    }

}
