using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Geimu {

    /// <summary>
    /// This is the main type for your game
    /// </summary>

    public class GameEngine : Microsoft.Xna.Framework.Game {
        // States
        public const int STATE_PLAY = 0;
        public const int STATE_END = 1;

        // Current game state
        protected int state;

        // Graphics Managers
        protected GraphicsDeviceManager graphics;
        protected SpriteBatch spriteBatch;

        // TODO: audio

        // Fonts
        protected SpriteFont fontCambria;

        // Boundaries of the window
        protected Rectangle bounds;

        // Square objects
        protected Square square0, square1;
        protected SquareController squareControl0, squareControl1;

        public GameEngine() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            // TODO: Add your initialization logic here
            bounds = new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);

            NewGame();

            base.Initialize();

            this.IsMouseVisible = true;

            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromSeconds(1.0 / 60.0);
        }

        // Starts a new game
        protected void NewGame() {
            square0 = new Square(200, 300, 0, bounds);
            square1 = new Square(500, 300, 1, 0.75f, 0.0f, bounds);

            squareControl0 = square0.controller;
            squareControl1 = square1.controller;

            Square.SetEnemies(square0, square1);

            state = STATE_PLAY;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            fontCambria = Content.Load<SpriteFont>("fonts\\Cambria");

            // TODO: use this.Content to load your game content here
            Square.LoadContent(this.Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
            Content.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            // Allows the game to exit
#if XBOX
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) {
#else
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) {
#endif
                this.Exit();
            }

            // TODO: Add your update logic here
            switch (state) {
                case STATE_PLAY:
                    squareControl0.readInput();
                    squareControl1.readInput();

                    square0.Update();
                    square1.Update();
                    if (square0.IsDead() || square1.IsDead())
                        state = STATE_END;
                    break;
                case STATE_END:
                    if (Keyboard.GetState().IsKeyDown(Keys.R))
                        NewGame();
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            square0.Draw(spriteBatch);
            square1.Draw(spriteBatch);

            if (state == STATE_END)
                DrawEnd(square0.IsDead());

            base.Draw(gameTime);
        }

        // Draws the gameover text
        protected void DrawEnd(bool p1dead) {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            int winner = 1;
            if (p1dead)
                winner = 2;

            String line1 = String.Format("Player {0} Wins!", winner);
            String line2 = "Press R to play again";

            Vector2 pos1 = new Vector2(bounds.Width / 2, bounds.Height / 2);
            Vector2 pos2 = new Vector2(bounds.Width / 2, 55 + bounds.Height / 2);

            Vector2 origin1 = fontCambria.MeasureString(line1) / 2;
            Vector2 origin2 = fontCambria.MeasureString(line2) / 2;

            DrawBorderedText(line1, pos1, origin1);
            DrawBorderedText(line2, pos2, origin2);

            spriteBatch.End();
        }

        // Draws white text with black border, assumes spritebatch has already began. Font: Cambria
        private void DrawBorderedText(String text, Vector2 pos, Vector2 origin) {
            Vector2[] offsets = { new Vector2(1, 1), new Vector2(1, -1), new Vector2(-1, 1), new Vector2(-1, -1) };

            foreach (Vector2 offset in offsets) {
                spriteBatch.DrawString(fontCambria, text, pos + offset, Color.Black, 0, origin, 1, SpriteEffects.None, 1);
            }

            spriteBatch.DrawString(fontCambria, text, pos, Color.White, 0, origin, 1, SpriteEffects.None, 0);

        }

    }

}
