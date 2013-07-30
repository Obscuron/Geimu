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

        // Reads the input
        public void readInput(InputState input) {
            inputState = input;
        }

    }

}
