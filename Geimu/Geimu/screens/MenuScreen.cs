using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geimu {

    // Abstract class for a general menu screen
    public abstract class MenuScreen : Screen {
        // List of menu entries
        protected List<String> menuEntries = new List<string>();

        // Current choice
        protected int menuChoice = 0;

        // The controller for the menu
        protected MenuController menuController;

        public MenuController controller {
            get { return menuController; }
            set { menuController = value; }
        }

        // Where to draw the menu
        protected Vector2 pos = Vector2.Zero;

        // Scale of the menu options
        protected float menuScale = 0.5f;

        // Font
        protected SpriteFont fontCambria;

        // Constructor
        public MenuScreen() {
            Activate();
        }

        // Loads font
        public override void LoadContent() {
            fontCambria = screenManager.contentManager.Load<SpriteFont>("fonts\\Cambria");
            base.LoadContent();
        }

        // Called upon cancelling
        protected virtual void OnCancel() { }

        // Called upon selecting a menu item
        protected virtual void OnSelected(int selection) { }

        // Updates the menu selection based on controller
        public override void Update(GameTime gameTime) {
            if (controller == null)
                return;

            controller.readInput();

            if (controller.prev) {
                menuChoice--;
                if (menuChoice < 0)
                    menuChoice = menuEntries.Count - 1;
            }

            if (controller.next) {
                menuChoice++;
                if (menuChoice >= menuEntries.Count)
                    menuChoice = 0;
            }

            if (controller.select)
                OnSelected(menuChoice);

            if (controller.cancel)
                OnCancel();

            base.Update(gameTime);
        }

        // Draws the menu choices
        public override void Draw(GameTime gameTime) {
            Vector2 curPos = pos;

            screenManager.spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            for (int i = 0; i < menuEntries.Count; i++) {
                Color tint = Color.Black;
                float scale = menuScale;

                if (menuChoice == i) {
                    tint = Color.White;
                    scale *= 1 + (float)(Math.Sin(gameTime.TotalGameTime.TotalSeconds) + 1) * 0.05f;
                }

                Vector2 origin = new Vector2(0, fontCambria.LineSpacing / 2);

                screenManager.spriteBatch.DrawString(fontCambria, menuEntries[i], curPos, tint, 0, origin, scale, SpriteEffects.None, 0);

                curPos.Y += fontCambria.LineSpacing;
            }

            screenManager.spriteBatch.End();

            base.Draw(gameTime);
        }

    }

}
