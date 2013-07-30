using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Geimu {

    public class InputState {
        // Keyboard states
        public KeyboardState prevKeyboard;
        public KeyboardState curKeyboard;

        // Mouse states
        public MouseState prevMouse;
        public MouseState curMouse;

        // Reads the input
        public void Update() {
            prevKeyboard = curKeyboard;
            prevMouse = curMouse;

            curKeyboard = Keyboard.GetState();
            curMouse = Mouse.GetState();
        }

        // Checks if a key was newly pressed
        public bool IsNewKeyPress(Keys key) {
            return (curKeyboard.IsKeyDown(key) && prevKeyboard.IsKeyUp(key));
        }

        // Wraps current keyboard
        public bool IsKeyDown(Keys key) {
            return curKeyboard.IsKeyDown(key);
        }

        public bool IsKeyUp(Keys key) {
            return curKeyboard.IsKeyUp(key);
        }
        
    }

}
