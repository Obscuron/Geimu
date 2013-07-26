using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Geimu {

    public class SquareController {
        public readonly Keys[,] PLAYER_KEYS = { { Keys.W, Keys.S, Keys.A, Keys.D },
                                                { Keys.Up, Keys.Down, Keys.Left, Keys.Right } };

        // Player id
        protected int mPlayer;

        // 1 = down/right, -1 = up/left, 0 = still
        private int mXdir;
        private int mYdir;

        public int Xdir {
            get { return mXdir; }
        }

        public int Ydir {
            get { return mYdir; }
        }

        public SquareController(int id) {
            if (id > 1) id = 0;
            mPlayer = id;
        }

        public void readInput() {
            KeyboardState state = Keyboard.GetState();
            Keys up, down, left, right;
            up = PLAYER_KEYS[mPlayer, 0];
            down = PLAYER_KEYS[mPlayer, 1];
            left = PLAYER_KEYS[mPlayer, 2];
            right = PLAYER_KEYS[mPlayer, 3];

            mXdir = mYdir = 0;

            if (state.IsKeyDown(up) && !state.IsKeyDown(down))
                mYdir = -1;
            else if (state.IsKeyDown(down) && !state.IsKeyDown(up))
                mYdir = 1;

            if (state.IsKeyDown(left) && !state.IsKeyDown(right))
                mXdir = -1;
            if (state.IsKeyDown(right) && !state.IsKeyDown(left))
                mXdir = 1;

        }

    }

}
