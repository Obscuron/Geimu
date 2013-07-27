
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Geimu {

    // Class representing the player object
    public class Square {

        // Sprite constants
        public const int SIZE = 91;

        // Velocity constants
        public const float VEL = 3.0f;
        public const float VEL_SLOW = 1.5f;

        // Firing rate
        public const int PROJ_RATE = 4;
        public const float PROJ_VEL = 5.0f;

        // Texture
        private static Texture2D sSprite;

        // Controller for handling player input
        protected SquareController mController;

        public SquareController controller {
            get { return mController; }
        }

        // Projectiles that the Square has fired
        protected ProjectileQueue mProj;

        // Boundaries of the window
        protected Rectangle mBounds;

        // Square data
        protected Vector2 mPos;
        protected Vector2 mVel;
        protected Rectangle mSize;

        protected int refire = 0;

        // Drawing data
        protected float mScale = 1.0f;
        protected float mRot = 0.0f;

        // Construct a new Square at a location with default size.
        public Square(int x, int y, int id, Rectangle bounds) {
            mPos = new Vector2(x, y);
            mVel = new Vector2(0, 0);

            mController = new SquareController(id);

            mProj = new ProjectileQueue(bounds);

            mBounds = bounds;

            mSize = new Rectangle();
        }

        // Constructs a new Square given size and rotation
        public Square(int x, int y, int id, float scale, float rot, Rectangle bounds)
            : this(x, y, id, bounds) {
            mScale = scale;
            mRot = rot;
        }

        // Loads texture into memory
        public static void LoadContent(ContentManager content) {
            sSprite = content.Load<Texture2D>("images\\Square");
            ProjectileQueue.LoadContent(content);
        }

        // Controls movement of Square
        public void Update() {
            if (mController == null) {
                return;
            }

            float vel;

            if (!mController.walk) {
                vel = VEL;
                mSize.Height = (int)(SIZE * mScale);
            }
            else {
                vel = VEL_SLOW;
                mSize.Height = (int)(SIZE / 2 * mScale);
            }

            mSize.Width = (int)(SIZE * mScale);

            mVel.X = mController.xDir * vel;
            mVel.Y = mController.yDir * vel;

            mPos += mVel;

            if (refire == 0 && mController.fire) {
                Vector2 pPos = new Vector2(mPos.X + (mSize.Width / 2), mPos.Y - (mSize.Height / 2));
                Vector2 pVel = new Vector2(PROJ_VEL * mController.xDirPrev, PROJ_VEL * mController.yDirPrev);

                pPos.X += mController.xDirPrev * (mSize.Width / 2);
                pPos.Y += mController.yDirPrev * (mSize.Height / 2);

                mProj.AddProjectile(pPos, pVel);
                refire = PROJ_RATE;
            }

            if (refire > 0)
                refire--;

            HandleWalls();

            mProj.Update();

        }

        // Handles collisions against walls
        private void HandleWalls() {
            if (mPos.X < 0)
                mPos.X = 0;
            else if (mPos.X + mSize.Width > mBounds.Width)
                mPos.X = mBounds.Width - mSize.Width;

            if (mPos.Y < mSize.Height)
                mPos.Y = mSize.Height;
            else if (mPos.Y > mBounds.Height)
                mPos.Y = mBounds.Height;
        }

        // Draws the Square
        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            Rectangle sector;
            if (!mController.walk)
                sector = new Rectangle(0, 0, SIZE, SIZE);
            else
                sector = new Rectangle(SIZE, 0, SIZE, SIZE / 2);

            Vector2 origin = new Vector2(0, sector.Height);

            spriteBatch.Draw(sSprite, mPos, sector, Color.White, mRot, origin, mScale, SpriteEffects.None, 0);

            spriteBatch.End();

            mProj.Draw(spriteBatch);
        }

    }

}
