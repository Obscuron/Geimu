using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Geimu {

    // Class for managing game screens
    public class ScreenManager {
        // List of current screens
        protected List<Screen> screens = new List<Screen>();

        // Lists of screens to remove and add
        protected List<Screen> removeList = new List<Screen>();
        protected List<Screen> addList = new List<Screen>();

        // Managers
        protected Game mGame;
        protected SpriteBatch mSpriteBatch;
        protected ScreenReference mScreens;

        public Game game {
            get { return mGame; }
        }

        public GraphicsDevice graphicsDevice {
            get { return mGame.GraphicsDevice; }
        }

        public ContentManager contentManager {
            get { return mGame.Content; }
        }

        public SpriteBatch spriteBatch {
            get { return mSpriteBatch; }
        }

        public ScreenReference screenList {
            get { return mScreens; }
        }

        // Game boundaries
        protected Rectangle mBounds;

        public Rectangle bounds {
            get { return mBounds; }
        }

        // Input state
        public InputState input = new InputState();

        // Constructs a new screen manager
        public ScreenManager(Game game) {
            mGame = game;
            mScreens = new ScreenReference();
        }

        // Starts up the game with the main menu
        public void Initialize() {
            mBounds = new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height);
            AddScreen(new MainMenu());
        }

        // Creates a new sprite batch
        public void LoadContent() {
            mSpriteBatch = new SpriteBatch(graphicsDevice);
        }

        // Updates each screen
        public void Update(GameTime gameTime) {
            input.Update();

            foreach (Screen s in removeList) {
                screens.Remove(s);
            }
            removeList.Clear();

            foreach (Screen s in addList) {
                screens.Add(s);
            }
            addList.Clear();

            foreach (Screen s in screens) {
                if (s.updateState) {
                    s.HandleInput(input);
                    s.Update(gameTime);
                }
            }
        }

        // Draws each screen
        public void Draw(GameTime gameTime) {
            foreach (Screen s in screens) {
                if (s.drawState)
                    s.Draw(gameTime);
            }
        }

        // Adds a screen to the top of the screen manager
        public void AddScreen(Screen screen) {
            screen.screenManager = this;
            screen.Initialize();

            screen.LoadContent();

            addList.Add(screen);
        }

        // Removes a screen from the screen manager
        public void RemoveScreen(Screen screen) {
            screen.UnloadContent();

            removeList.Add(screen);
        }

    }

}
