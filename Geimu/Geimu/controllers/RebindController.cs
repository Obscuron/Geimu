using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Geimu {

    // Class for handling rebinding of keys
    public class RebindController {
        // Input state
        protected InputState inputState;

        public bool cancel {
            get { return inputState.IsNewKeyPress(Keys.Escape); }
        }

        // Reads the input
        public void ReadInput(InputState input) {
            inputState = input;
        }
        
        // Returns a new key press
        public Keys? GetNewKeyPress() {
            Keys[] newKeys = inputState.GetKeys();

            foreach (Keys k in newKeys) {
                if (k == Keys.Escape)
                    continue;
                if (inputState.IsNewKeyPress(k))
                    return k;
            }

            return null;
        }

    }

}
