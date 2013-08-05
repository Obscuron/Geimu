using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Geimu {

    // Class for reading input from keyboard
    public class SquareController {
        // Player id
        protected int mPlayer;

        // Input state
        InputState inputState;

        // Controls for both players
        protected ControlsData mControls;

        // 1 = down/right, -1 = up/left, 0 = still
        public int xDir {
            get;
            private set;
        }

        public int yDir {
            get;
            private set;
        }

        public bool walk {
            get { return inputState.IsNewKeyPress(mControls.walk[mPlayer]); }
        }

        public bool unwalk {
            get { return inputState.IsNewKeyRelease(mControls.walk[mPlayer]); }
        }

        private bool delay = true;

        public bool fire {
            get { return !delay && inputState.IsKeyDown(mControls.fire[mPlayer]);  }
        }

        // Previous directions
        public int xDirPrev {
            get;
            private set;
        }

        public int yDirPrev {
            get;
            private set;
        }

        // Constructs a controller for a given player id
        public SquareController(int id, ControlsData controls) {
            if (id > 1) id = 0;
            mPlayer = id;
            mControls = controls;
        }

        // Sets default previous directions
        public void SetPrev(Vector2 prevDir) {
            xDirPrev = (int) prevDir.X;
            yDirPrev = (int) prevDir.Y;
        }

        public void Delay() {
            delay = true;
        }

        // Reads input from keyboard and updates fields
        public void ReadInput(InputState input) {
            inputState = input;

            xDir = yDir = 0;

            if (delay && input.IsKeyUp(mControls.fire[mPlayer]))
                delay = false;

            if (input.IsKeyDown(mControls.up[mPlayer]) && !input.IsKeyDown(mControls.down[mPlayer]))
                yDir = -1;
            else if (input.IsKeyDown(mControls.down[mPlayer]) && !input.IsKeyDown(mControls.up[mPlayer]))
                yDir = 1;

            if (input.IsKeyDown(mControls.left[mPlayer]) && !input.IsKeyDown(mControls.right[mPlayer]))
                xDir = -1;
            if (input.IsKeyDown(mControls.right[mPlayer]) && !input.IsKeyDown(mControls.left[mPlayer]))
                xDir = 1;

            if (xDir != 0 || yDir != 0) {
                xDirPrev = xDir;
                yDirPrev = yDir;
            }

        }

    }

}
