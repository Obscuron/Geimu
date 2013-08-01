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

        // Controls for both players
        protected ControlsData mControls;

        // 1 = down/right, -1 = up/left, 0 = still
        private int mXdir;
        private int mYdir;

        // true = walking
        private bool mWalk;

        // true = firing
        private bool mFire;

        public int xDir {
            get { return mXdir; }
        }

        public int yDir {
            get { return mYdir; }
        }

        public bool walk {
            get { return mWalk; }
        }

        public bool fire {
            get { return mFire; }
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
            KeyboardState state = Keyboard.GetState();

            mXdir = mYdir = 0;
            mWalk = false;
            mFire = false;

            if (state.IsKeyDown(mControls.up[mPlayer]) && !state.IsKeyDown(mControls.down[mPlayer]))
                mYdir = -1;
            else if (state.IsKeyDown(mControls.down[mPlayer]) && !state.IsKeyDown(mControls.up[mPlayer]))
                mYdir = 1;

            if (state.IsKeyDown(mControls.left[mPlayer]) && !state.IsKeyDown(mControls.right[mPlayer]))
                mXdir = -1;
            if (state.IsKeyDown(mControls.right[mPlayer]) && !state.IsKeyDown(mControls.left[mPlayer]))
                mXdir = 1;

            if (state.IsKeyDown(mControls.walk[mPlayer]))
                mWalk = true;

            if (state.IsKeyDown(mControls.fire[mPlayer]))
                mFire = true;

            if (mXdir != 0 || mYdir != 0) {
                mXdirPrev = mXdir;
                mYdirPrev = mYdir;
            }

        }

    }

}
