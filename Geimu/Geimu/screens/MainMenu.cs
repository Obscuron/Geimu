using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Geimu {

    // Screen for the main menu
    public class MainMenu : MenuScreen {
        // List with continue
        protected List<String> withCont = new List<string>();
        protected List<String> noCont = new List<string>();

        protected bool isCont;

        // Constructor
        public MainMenu() {
            noCont.Add("Start Game");
            noCont.Add("Options");
            noCont.Add("Exit");

            withCont.Add("Continue");
            withCont.Add("New Game");
            withCont.Add("Options");
            withCont.Add("Exit");
        }

        public override void Initialize() {
            screenManager.dataReference.gameSave.LoadData();
            if (screenManager.dataReference.gameSave.isSave)
                menuEntries = withCont;
            else
                menuEntries = noCont;

            base.Initialize();
        }

        // On menu selection
        protected override void OnSelected(int selection) {
            if (!screenManager.dataReference.gameSave.isSave)
                selection++;

            switch (selection) {
                case 0:
                    screenManager.AddScreen(screenManager.screenReference.game);
                    screenManager.screenReference.game.LoadGame();
                    screenManager.RemoveScreen(this);
                    break;
                case 1:
                    screenManager.AddScreen(screenManager.screenReference.game);
                    screenManager.screenReference.game.NewGame();
                    screenManager.RemoveScreen(this);
                    break;
                case 2:
                    Deactivate();
                    screenManager.AddScreen(screenManager.screenReference.options);
                    break;
                case 3:
                    OnCancel();
                    break;
            }

            base.OnSelected(selection);
        }

        // Exits the game
        protected override void OnCancel() {
            screenManager.game.Exit();

            base.OnCancel();
        }

        // Draws the menu
        public override void Draw(GameTime gameTime) {
            screenManager.spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            String title = "Main Menu";
            Vector2 pos = new Vector2(screenManager.bounds.Width / 2, 60);
            Vector2 origin = fontCambria.MeasureString(title) / 2;

            DrawBorderedText(title, pos, origin);

            screenManager.spriteBatch.End();

            menuPos = new Vector2(200, screenManager.bounds.Height / 2 - 100);

            base.Draw(gameTime);
        }

    }

}
