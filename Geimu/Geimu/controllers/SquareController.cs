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
        private int mXdir;
        private int mYdir;

        public int xDir {
            get { return mXdir; }
        }

        public int yDir {
            get { return mYdir; }
        }

        public bool walk {
            get { return inputState.IsNewKeyPress(mControls.walk[mPlayer]); }
        }

        public bool unwalk {
            get { return inputState.IsNewKeyRelease(mControls.walk[mPlayer]); }
        }

        public bool fire {
            get { return inputState.IsKeyDown(mControls.fire[mPlayer]);  }
        }

        // Previous directions
        private int mXdirPrev = 1;
        private int mYdirPrev = 0;

        public int xDirPrev {
            get { return mXdirPrev; }
        }

        public int yDirPrev {
            get { return mYdirPrev; }
        }

        // Constructs a controller for a given player id
        public SquareController(int id, ControlsData controls) {
            if (id > 1) id = 0;
            mPlayer = id;
            mControls = controls;
        }

        // Reads input from keyboard and updates fields
        public void readInput(InputState input) {
            inputState = input;

            mXdir = mYdir = 0;

            if (input.IsKeyDown(mControls.up[mPlayer]) && !input.IsKeyDown(mControls.down[mPlayer]))
                mYdir = -1;
            else if (input.IsKeyDown(mControls.down[mPlayer]) && !input.IsKeyDown(mControls.up[mPlayer]))
                mYdir = 1;

            if (input.IsKeyDown(mControls.left[mPlayer]) && !input.IsKeyDown(mControls.right[mPlayer]))
                mXdir = -1;
            if (input.IsKeyDown(mControls.right[mPlayer]) && !input.IsKeyDown(mControls.left[mPlayer]))
                mXdir = 1;

            if (mXdir != 0 || mYdir != 0) {
                mXdirPrev = mXdir;
                mYdirPrev = mYdir;
            }

        }

    }

}
