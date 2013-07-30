using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Geimu {

    // Screen during game play
    public class GameScreen : Screen {
        // Square objects
        protected Square square0, square1;
        protected SquareController squareControl0, squareControl1;

        protected EndScreen gameEnd;

        // Constructor
        public GameScreen() {
            Activate();
            gameEnd = new EndScreen(this);
        }

        // Initializes the game screen with a new game
        public override void Initialize() {
            Activate();
            NewGame();

            base.Initialize();
        }

        // Creates objects for a new game
        protected void NewGame() {
            square0 = new Square(200, 300, 0, screenManager.bounds);
            square1 = new Square(500, 300, 1, 0.75f, 0.0f, screenManager.bounds);

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
            squareControl0.readInput();
            squareControl1.readInput();

            square0.Update();
            square1.Update();
            if (square0.IsDead() || square1.IsDead()) {
                updateState = false;
                screenManager.AddScreen(gameEnd);
            }

            base.Update(gameTime);
        }

        // Draws game objects onto screen
        public override void Draw(GameTime gameTime) {
            square0.Draw(screenManager.spriteBatch);
            square1.Draw(screenManager.spriteBatch);

            base.Draw(gameTime);
        }

    }

}
