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

            base.Initialize();
        }

        // Creates objects for a new game
        public void NewGame() {
            screenManager.dataReference.gameData.Reset();
            LoadGame();
        }

        // Starts a game with data
        public void LoadGame() {
            square0 = new Square(0, screenManager.bounds, screenManager.dataReference.controlsData);
            square1 = new Square(1, screenManager.bounds, screenManager.dataReference.controlsData);

            screenManager.dataReference.gameData.LoadData();

            square0.LoadData(screenManager.dataReference.gameData.Square0);
            square1.LoadData(screenManager.dataReference.gameData.Square1);

            squareControl0 = square0.Controller;
            squareControl1 = square1.Controller;

            square0.EnemySquare = square1;
            square1.EnemySquare = square0;
        }

        // Saves the game
        public void SaveGame() {
            screenManager.dataReference.gameData.Square0 = square0.ToData();
            screenManager.dataReference.gameData.Square1 = square1.ToData();

            screenManager.dataReference.gameData.SaveData();
        }

        // Loads textures
        public override void LoadContent() {
            Square.LoadContent(screenManager.contentManager);

            base.LoadContent();
        }

        // Returns the string for the end message
        public String EndMessage() {
            if (square0.IsDead() && square1.IsDead())
                return "Draw Game!";
            else if (square0.IsDead())
                return "Player 2 Wins!";
            else
                return "Player 1 Wins!";
        }

        // Updates squares every tick
        public override void Update(GameTime gameTime) {
            square0.Update();
            square1.Update();
            if (square0.IsDead() || square1.IsDead()) {
                updateState = false;
                screenManager.AddScreen(screenManager.screenReference.endScreen);
            }

            base.Update(gameTime);
        }

        // Handles keyboard input
        public override void HandleInput(InputState input) {
            if (input.IsNewKeyPress(Keys.Escape)) {
                Deactivate();
                screenManager.AddScreen(screenManager.screenReference.pauseScreen);
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

        public override void Activate() {
            if (squareControl0 != null && squareControl1 != null) {
                squareControl0.Delay();
                squareControl1.Delay();
            }

            base.Activate();
        }

    }

}
