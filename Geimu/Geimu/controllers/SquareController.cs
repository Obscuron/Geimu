using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Geimu {

    // Class for reading input from keyboard
    public class SquareController {
        // Keyboard keys for different players
        public readonly Keys[,] PLAYER_KEYS = { { Keys.W, Keys.S, Keys.A, Keys.D, Keys.LeftShift, Keys.Space },
                                                { Keys.Up, Keys.Down, Keys.Left, Keys.Right, Keys.RightShift, Keys.RightControl } };

        // Player id
        protected int mPlayer;

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
        public SquareController(int id) {
            if (id > 1) id = 0;
            mPlayer = id;
        }

        // Reads input from keyboard and updates fields
        public void readInput() {
            KeyboardState state = Keyboard.GetState();
            Keys up, down, left, right, slow, fire;
            up = PLAYER_KEYS[mPlayer, 0];
            down = PLAYER_KEYS[mPlayer, 1];
            left = PLAYER_KEYS[mPlayer, 2];
            right = PLAYER_KEYS[mPlayer, 3];
            slow = PLAYER_KEYS[mPlayer, 4];
            fire = PLAYER_KEYS[mPlayer, 5];

            mXdir = mYdir = 0;
            mWalk = false;
            mFire = false;

            if (state.IsKeyDown(up) && !state.IsKeyDown(down))
                mYdir = -1;
            else if (state.IsKeyDown(down) && !state.IsKeyDown(up))
                mYdir = 1;

            if (state.IsKeyDown(left) && !state.IsKeyDown(right))
                mXdir = -1;
            if (state.IsKeyDown(right) && !state.IsKeyDown(left))
                mXdir = 1;

            if (state.IsKeyDown(slow))
                mWalk = true;

            if (state.IsKeyDown(fire))
                mFire = true;

            if (mXdir != 0 || mYdir != 0) {
                mXdirPrev = mXdir;
                mYdirPrev = mYdir;
            }

        }

    }

}
