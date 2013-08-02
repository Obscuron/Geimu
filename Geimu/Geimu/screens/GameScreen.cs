using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Geimu {

    // Screen during game play
    public class GameScreen : Screen {
        // Square objects
        protected Square square0, square1;
        protected SquareController squareControl0, squareControl1;

        // Constructor
        public GameScreen() {
            Activate();
        }

        // Initializes the game screen with a new game
        public override void Initialize() {
            Activate();
            NewGame();

            base.Initialize();
        }

        // Creates objects for a new game
        protected void NewGame() {
            square0 = new Square(200, 300, 0, screenManager.bounds, screenManager.dataReference.controls);
            square1 = new Square(500, 300, 1, 0.75f, 0.0f, screenManager.bounds, screenManager.dataReference.controls);

            squareControl0 = square0.controller;
            squareControl1 = square1.controller;

            Square.SetEnemies(square0, square1);
        }

        // Loads textures
        public override void LoadContent() {
            Square.LoadContent(screenManager.contentManager);

            base.LoadContent();
        }

        // Returns the player numberfor the winner
        public int Winner() {
            if (square0.IsDead())
                return 2;
            else return 1;
        }

        // Updates squares every tick
        public override void Update(GameTime gameTime) {
            square0.Update();
            square1.Update();
            if (square0.IsDead() || square1.IsDead()) {
                updateState = false;
                screenManager.AddScreen(screenManager.screenReference.end);
            }

            base.Update(gameTime);
        }

        // Handles keyboard input
        public override void HandleInput(InputState input) {
            if (input.IsNewKeyPress(Keys.Escape)) {
                Deactivate();
                screenManager.AddScreen(screenManager.screenReference.pause);
            }

            squareControl0.ReadInput(input);
            squareControl1.ReadInput(input);
            
            base.HandleInput(input);
        }

        // Draws game objects onto screen
        public override void Draw(GameTime gameTime) {
            square0.Draw(screenManager.spriteBatch);
            square1.Draw(screenManager.spriteBatch);

            base.Draw(gameTime);
        }

    }

}
