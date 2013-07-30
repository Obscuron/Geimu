using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geimu {

    // Abstract class for a screen
    public abstract class Screen {

        // States determining whether each screen updates or draws
        protected bool mUpdateState;
        protected bool mDrawState;

        public bool updateState {
            get { return mUpdateState; }
            set { mUpdateState = value; }
        }

        public bool drawState {
            get { return mDrawState; }
            set { mDrawState = value; }
        }

        // Reference to the screen manager
        protected ScreenManager mScreenManager;

        public ScreenManager screenManager {
            get { return mScreenManager; }
            set { mScreenManager = value; }
        }

        // Virtual methods
        public virtual void Initialize() { }

        public virtual void LoadContent() { }

        public virtual void UnloadContent() { }

        public virtual void Update(GameTime gameTime) { }

        public virtual void Draw(GameTime gameTime) { }

    }

}
