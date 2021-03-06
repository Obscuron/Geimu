﻿using Microsoft.Xna.Framework;
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
        protected static MenuController menuController = new MenuController();

        public MenuController controller {
            get { return menuController; }
            set { menuController = value; }
        }

        // Where to draw the menu
        protected Vector2 menuPos = Vector2.Zero;

        // Scale of the menu options
        protected float menuScale = 0.8f;

        // Origin of the text
        protected Vector2 origin;

        // Justification of text. 0 = left, 1 = center, 2 = right
        protected int menuJustify = 0;

        // Font
        protected SpriteFont fontCambria;

        // Constructor
        public MenuScreen() {
            Activate();
        }

        public override void Initialize() {
            Activate();
            menuChoice = 0;

            base.Initialize();
        }

        // Loads font
        public override void LoadContent() {
            fontCambria = screenManager.contentManager.Load<SpriteFont>("fonts\\Cambria");
            origin = new Vector2(0, fontCambria.LineSpacing / 2);
            base.LoadContent();
        }
        // Called upon selecting a menu item
        protected virtual void OnSelected(int selection) { }

        // Called upon cancelling
        protected virtual void OnCancel() { }

        // Updates the menu selection based on controller
        public override void Update(GameTime gameTime) {
            if (controller == null)
                return;

            Vector2 curPos = menuPos;
            bool mouseSelect = false;

            for (int i = 0; i < menuEntries.Count; i++) {
                CalculateOrigin(menuEntries[i]);
                Vector2 cornerPos = curPos - origin * menuScale;
                Rectangle textBounds = new Rectangle((int)cornerPos.X, (int)cornerPos.Y, (int)(fontCambria.MeasureString(menuEntries[i]).X * menuScale), (int)(fontCambria.LineSpacing * menuScale));
                if (controller.CurBounded(textBounds)) {
                    mouseSelect = true;
                    menuChoice = i;
                    if (controller.released && controller.LastBounded(textBounds)) {
                        OnSelected(menuChoice);
                    }
                    break;
                }
                curPos.Y += fontCambria.LineSpacing * menuScale;
            }

            if (!mouseSelect) {
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
            }

            if (controller.select)
                OnSelected(menuChoice);

            if (controller.cancel)
                OnCancel();

            base.Update(gameTime);
        }

        // Handles input
        public override void HandleInput(InputState input) {
            controller.ReadInput(input);

            base.HandleInput(input);
        }

        // Draws the menu choices
        public override void Draw(GameTime gameTime) {
            Vector2 curPos = menuPos;

            screenManager.spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            for (int i = 0; i < menuEntries.Count; i++) {
                Color tint = Color.Black;
                float scale = menuScale;

                if (menuChoice == i) {
                    tint = Color.White;
                    scale *= 1 + (float)(Math.Sin(gameTime.TotalGameTime.TotalSeconds) + 1) * 0.05f;
                }

                CalculateOrigin(menuEntries[i]);

                screenManager.spriteBatch.DrawString(fontCambria, menuEntries[i], curPos, tint, 0, origin, scale, SpriteEffects.None, 0);

                curPos.Y += fontCambria.LineSpacing * menuScale;
            }

            screenManager.spriteBatch.End();

            base.Draw(gameTime);
        }

        protected void CalculateOrigin(String text) {
            if (menuJustify == 1)
                origin.X = fontCambria.MeasureString(text).X / 2;
            else if (menuJustify == 2)
                origin.X = fontCambria.MeasureString(text).X;
        }

        // Draws white text with black border, assumes spritebatch has already began. Font: Cambria
        protected void DrawBorderedText(String text, Vector2 pos, Vector2 origin) {
            Vector2[] offsets = { new Vector2(1, 1), new Vector2(1, -1), new Vector2(-1, 1), new Vector2(-1, -1) };

            foreach (Vector2 offset in offsets) {
                screenManager.spriteBatch.DrawString(fontCambria, text, pos + offset, Color.Black, 0, origin, 1, SpriteEffects.None, 1);
            }

            screenManager.spriteBatch.DrawString(fontCambria, text, pos, Color.White, 0, origin, 1, SpriteEffects.None, 0);

        }

    }

}
