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

        // Maximum health
        public const int MAX_HEALTH = 300;

        // Texture
        private static Texture2D sSprite;

        // Controller for handling player input
        protected SquareController mController;

        public SquareController controller {
            get { return mController; }
        }

        // Projectiles that the Square has fired
        protected ProjectileQueue mProj;

        // The enemy square
        protected Square mEnemySquare;

        public Square enemySquare {
            get { return mEnemySquare; }
            set { mEnemySquare = mProj.enemySquare = value; }
        }

        // Health Bar for the Square
        protected HealthBar mHealthBar;
        protected int mHealth;

        public int health {
            get { return mHealth; }
            set { mHealth = mHealthBar.health = value; }
        }

        // Boundaries of the window
        protected Rectangle mBounds;

        // Square data
        protected Vector2 mPos;
        protected Vector2 mVel;
        protected Rectangle mSize;

        protected int refire = 0;
        protected bool walk = false;

        // Location data
        public Vector2 center {
            get { return mPos + new Vector2(mSize.Width / 2, -mSize.Height / 2); }
        }

        public int top {
            get { return (int)mPos.Y - mSize.Height; }
        }

        public int bot {
            get { return (int)mPos.Y; }
        }

        public int left {
            get { return (int)mPos.X; }
        }

        public int right {
            get { return (int)mPos.X + mSize.Width; }
        }

        // Drawing data
        protected float mScale = 1.0f;
        protected float mRot = 0.0f;

        // Construct a new Square at a location with default size.
        public Square(int id, Rectangle bounds, ControlsData controls) {
            mPos = Vector2.Zero;
            mVel = Vector2.Zero;

            mController = new SquareController(id, controls);

            mBounds = new Rectangle(0, 75, bounds.Width, bounds.Height);

            mProj = new ProjectileQueue(mBounds);
            if (id == 1)
                mProj.tint = Color.Green;

            Rectangle healthBounds = new Rectangle(16, 16, 300, 44);
            if (id == 1)
                healthBounds.X = bounds.Width - 16;

            mHealth = MAX_HEALTH;
            mHealthBar = new HealthBar(mHealth, MAX_HEALTH, healthBounds, id);

            mSize = new Rectangle();
        }

        // Loads texture into memory
        public static void LoadContent(ContentManager content) {
            sSprite = content.Load<Texture2D>("images\\Square");
            ProjectileQueue.LoadContent(content);
            HealthBar.LoadContent(content);
        }

        // Set both squares as the enemies of each other
        public static void SetEnemies(Square square, Square other) {
            square.enemySquare = other;
            other.enemySquare = square;
        }

        // Damages the Square
        public void Damage(int amount) {
            health = (int)MathHelper.Clamp(mHealth - amount, 0, MAX_HEALTH);
        }

        // Sets health of the square
        public void SetHealth(int amount) {
            health = (int)MathHelper.Clamp(amount, 0, MAX_HEALTH);
        }

        // Returns true if the square is dead
        public bool IsDead() {
            return mHealth == 0;
        }

        // Returns if the vector is inside the square
        public bool IsInside(Vector2 pos) {
            if (pos.X > mPos.X && pos.X < mPos.X + mSize.Width)
                if (pos.Y > mPos.Y - mSize.Height && pos.Y < mPos.Y)
                    return true;
            return false;
        }

        // Reads data into the square
        public void LoadData(GameData.SquareData data) {
            health = data.health;
            mScale = data.scale;
            mPos = data.pos;
            mController.SetPrev(data.prevDir);
            if (data.hasProj)
                mProj = data.proj;
        }

        // Converts the square to saveable data
        public GameData.SquareData ToData() {
            GameData.SquareData data = new GameData.SquareData();

            data.health = health;
            data.scale = mScale;
            data.pos = mPos;
            data.prevDir = new Vector2(mController.xDirPrev, mController.yDirPrev);
            data.proj = mProj;
            data.hasProj = true;

            return data;
        }

        // Controls movement of Square
        public void Update() {
            if (mController == null)
                return;

            if (mSize == new Rectangle())
                mSize.Width = mSize.Height = (int)(SIZE * mScale);

            if (mController.unwalk) {
                walk = false;
                mSize.Height = (int)(SIZE * mScale);
                if (Collided())
                    mPos.Y = enemySquare.bot + mSize.Height;
            }
            if (mController.walk) {
                walk = true;
                mSize.Height = (int)(SIZE / 2 * mScale);
            }

            float vel;

            if (walk)
                vel = VEL_SLOW;
            else
                vel = VEL;

            mVel.X = mController.xDir * vel;
            mVel.Y = mController.yDir * vel;

            HandleMovement();

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

            mProj.Update();

        }

        // Handles movement of squares to prevent collisions
        protected void HandleMovement() {
            mPos.X += mVel.X;
            if (Collided()) {
                if (mController.xDir == 1)
                    mPos.X = enemySquare.left - mSize.Width;
                else if (mController.xDir == -1)
                    mPos.X = enemySquare.right;
            }

            mPos.Y += mVel.Y;
            if (Collided()) {
                if (mController.yDir == 1)
                    mPos.Y = enemySquare.top;
                else if (mController.yDir == -1)
                    mPos.Y = enemySquare.bot + mSize.Height;
            }

            HandleWalls();
        }

        // Handles collisions against walls
        protected void HandleWalls() {
            if (mPos.X < mBounds.X)
                mPos.X = mBounds.X;
            else if (mPos.X + mSize.Width > mBounds.Width)
                mPos.X = mBounds.Width - mSize.Width;

            if (mPos.Y - mSize.Height < mBounds.Y)
                mPos.Y = mBounds.Y + mSize.Height;
            else if (mPos.Y > mBounds.Height)
                mPos.Y = mBounds.Height;
        }

        // Checks for collisions against the enemy square
        protected bool Collided() {
            if (top >= enemySquare.bot)
                return false;
            if (bot <= enemySquare.top)
                return false;
            if (right <= enemySquare.left)
                return false;
            if (left >= enemySquare.right)
                return false;

            return true;
        }

        // Draws the Square along with healthbar and projectiles
        public void Draw(SpriteBatch spriteBatch) {

            mHealthBar.Draw(spriteBatch);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            Rectangle sector;
            if (!walk)
                sector = new Rectangle(0, 0, SIZE, SIZE);
            else
                sector = new Rectangle(SIZE + 2, 0, SIZE, SIZE / 2);

            Vector2 origin = new Vector2(0, sector.Height);

            spriteBatch.Draw(sSprite, mPos, sector, Color.White, mRot, origin, mScale, SpriteEffects.None, 0);

            spriteBatch.End();

            mProj.Draw(spriteBatch);
        }

    }

}
