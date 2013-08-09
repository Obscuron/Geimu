using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Geimu {

    // Class representing the player object
    public class Square : DrawableObject{
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

        public static Rectangle UNWALK_SECTOR = new Rectangle(0, 0, SIZE, SIZE);
        public static Rectangle WALK_SECTOR = new Rectangle(SIZE + 2, 0, SIZE, SIZE / 2);

        // Controller for handling player input
        public SquareController Controller {
            get;
            protected set;
        }

        // Projectiles that the Square has fired
        protected ProjectileQueue mProj;

        // The enemy square
        protected Square mEnemySquare;

        public Square EnemySquare {
            get { return mEnemySquare; }
            set { mEnemySquare = mProj.enemySquare = value; }
        }

        // Health Bar for the Square
        protected HealthBar mHealthBar;
        public int Health {
            get;
            protected set;
        }
        public int MaxHealth {
            get;
            protected set;
        }

        // Boundaries of the window
        protected Rectangle mBounds;

        // Square data
        protected Vector2 mVel;
        protected int refire = 0;
        protected bool walk = false;

        // Construct a new Square at a location with default size.
        public Square(int id, Rectangle bounds, ControlsData controls) {
            SpriteObject = new Sprite(UNWALK_SECTOR);
            SpriteObject.Origin = new Vector2(0, SpriteObject.Sector.Height);

            mVel = Vector2.Zero;

            Controller = new SquareController(id, controls);

            mBounds = new Rectangle(0, 75, bounds.Width, bounds.Height);

            mProj = new ProjectileQueue(mBounds);
            if (id == 1)
                mProj.tint = Color.Green;

            Health = MaxHealth = MAX_HEALTH;
            mHealthBar = new HealthBar(this, bounds, id);
        }

        // Loads texture into memory
        public override void LoadContent(ContentManager content) {
            sprite = content.Load<Texture2D>("images\\Square");
            mProj.LoadContent(content);
        }

        // Damages the Square
        public void Damage(int amount) {
            Health = (int)MathHelper.Clamp(Health - amount, 0, MaxHealth);
        }

        // Sets health of the square
        public void SetHealth(int amount) {
            Health = (int)MathHelper.Clamp(amount, 0, MaxHealth);
        }

        // Returns true if the square is dead
        public bool IsDead() {
            return Health == 0;
        }

        // Returns if the vector is inside the square
        public bool IsInside(Vector2 pos) {
            if (pos.X > SpriteObject.Left && pos.X < SpriteObject.Right)
                if (pos.Y > SpriteObject.Top && pos.Y < SpriteObject.Bot)
                    return true;
            return false;
        }

        // Reads data into the square
        public void LoadData(GameData.SquareData data) {
            Health = data.health;
            SpriteObject.Scale = data.scale;
            SpriteObject.Pos = data.pos;
            Controller.SetPrev(data.prevDir);
            if (data.hasProj)
                mProj = data.proj;
        }

        // Converts the square to saveable data
        public GameData.SquareData ToData() {
            GameData.SquareData data = new GameData.SquareData();

            data.health = Health;
            data.scale = SpriteObject.ScaleVector.X;
            data.pos = SpriteObject.Pos;
            data.prevDir = new Vector2(Controller.xDirPrev, Controller.yDirPrev);
            data.proj = mProj;
            data.hasProj = true;

            return data;
        }

        // Controls movement of Square
        public void Update() {
            if (Controller == null)
                return;

            if (Controller.unwalk) {
                walk = false;
                SpriteObject.Sector = UNWALK_SECTOR;
                SpriteObject.Origin = new Vector2(0, SpriteObject.Sector.Height);
                //if (Collided())
                //    mPos.Y = enemySquare.bot + mSize.Height;
            }
            if (Controller.walk) {
                walk = true;
                SpriteObject.Sector = WALK_SECTOR;
                SpriteObject.Origin = new Vector2(0, SpriteObject.Sector.Height);
            }

            float vel;

            if (walk)
                vel = VEL_SLOW;
            else
                vel = VEL;

            mVel.X = Controller.xDir * vel;
            mVel.Y = Controller.yDir * vel;

            HandleMovement();

            if (refire == 0 && Controller.fire) {
                Vector2 pPos = SpriteObject.Center;
                Vector2 pVel = new Vector2(PROJ_VEL * Controller.xDirPrev, PROJ_VEL * Controller.yDirPrev);

                pPos.X += Controller.xDirPrev * (SpriteObject.Width / 2);
                pPos.Y += Controller.yDirPrev * (SpriteObject.Height / 2);

                mProj.AddProjectile(pPos, pVel);
                refire = PROJ_RATE;
            }

            if (refire > 0)
                refire--;

            mProj.Update();

        }

        // Handles movement of squares to prevent collisions
        protected void HandleMovement() {
            SpriteObject.X += mVel.X;
            if (Collided()) {
                if (Controller.xDir == 1)
                    SpriteObject.Right = EnemySquare.SpriteObject.Left;
                else if (Controller.xDir == -1)
                    SpriteObject.Left = EnemySquare.SpriteObject.Right;
            }

            SpriteObject.Y += mVel.Y;
            if (Collided()) {
                if (Controller.yDir == 1)
                    SpriteObject.Bot = EnemySquare.SpriteObject.Top;
                else if (Controller.yDir == -1)
                    SpriteObject.Top = EnemySquare.SpriteObject.Bot;
            }

            HandleWalls();
        }

        // Handles collisions against walls
        protected void HandleWalls() {
            if (SpriteObject.Left < mBounds.X)
                SpriteObject.Left = mBounds.X;
            else if (SpriteObject.Right > mBounds.Width)
                SpriteObject.Right = mBounds.Width;

            if (SpriteObject.Top < mBounds.Y)
                SpriteObject.Top = mBounds.Y;
            else if (SpriteObject.Bot > mBounds.Height)
                SpriteObject.Bot = mBounds.Height;
        }

        // Checks for collisions against the enemy square
        protected bool Collided() {
            if (SpriteObject.Top >= EnemySquare.SpriteObject.Bot)
                return false;
            if (SpriteObject.Bot <= EnemySquare.SpriteObject.Top)
                return false;
            if (SpriteObject.Right <= EnemySquare.SpriteObject.Left)
                return false;
            if (SpriteObject.Left >= EnemySquare.SpriteObject.Right)
                return false;

            return true;
        }

        // Draws the Square along with healthbar and projectiles
        public override void Draw(SpriteBatch spriteBatch) {
            mHealthBar.Draw(spriteBatch);

            base.Draw(spriteBatch);

            mProj.Draw(spriteBatch);
        }

    }

}
