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

        // Checks if mouse buttons are newly pressed
        public bool IsNewMouseLeft() {
            return curMouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton == ButtonState.Released;
        }

        public bool IsNewMouseRight() {
            return curMouse.RightButton == ButtonState.Pressed && prevMouse.RightButton == ButtonState.Released;
        }

        public bool IsNewMouseMiddle() {
            return curMouse.MiddleButton == ButtonState.Pressed && prevMouse.MiddleButton == ButtonState.Released;
        }

        // Checks if mouse buttons are newly released
        public bool IsNewMouseLeftUp() {
            return curMouse.LeftButton == ButtonState.Released && prevMouse.LeftButton == ButtonState.Pressed;
        }

        public bool IsNewMouseRightUp() {
            return curMouse.RightButton == ButtonState.Released && prevMouse.RightButton == ButtonState.Pressed;
        }

        public bool IsNewMouseMiddleUp() {
            return curMouse.MiddleButton == ButtonState.Released && prevMouse.MiddleButton == ButtonState.Pressed;
        }

        // Wraps the current mouse
        public int MouseX() {
            return curMouse.X;
        }

        public int MouseY() {
            return curMouse.Y;
        }

        public Vector2 MousePos() {
            return new Vector2(curMouse.X, curMouse.Y);
        }

        public bool IsMouseLeft() {
            return curMouse.LeftButton == ButtonState.Pressed;
        }

        public bool IsMouseRight() {
            return curMouse.RightButton == ButtonState.Pressed;
        }

        public bool IsMouseMiddle() {
            return curMouse.MiddleButton == ButtonState.Pressed;
        }

        public bool IsMouseLeftUp() {
            return curMouse.LeftButton == ButtonState.Released;
        }

        public bool IsMouseRightUp() {
            return curMouse.RightButton == ButtonState.Released;
        }

        public bool IsMouseMiddleUp() {
            return curMouse.MiddleButton == ButtonState.Released;
        }

    }

}
