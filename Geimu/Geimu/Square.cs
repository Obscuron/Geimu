
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Geimu {

    public class Square {

        public const int SIZE = 91;

        public const float VEL = 3.0f;
        public const float VEL_SLOW = 1.5f;

        private static Texture2D sSprite;

        protected SquareController mController;

        public SquareController controller {
            get { return mController; }
        }

        protected Vector2 mPos;
        protected Vector2 mVel;

        protected float mScale = 1.0f;
        protected float mRot = 0.0f;

        public Square(int x, int y, int id) {
            mPos = new Vector2(x, y);
            mVel = new Vector2(0, 0);

            mController = new SquareController(id);
        }

        public Square(int x, int y, int id, float scale, float rot)
            : this(x, y, id) {
            mScale = scale;
            mRot = rot;
        }

        public static void LoadContent(ContentManager content) {
            sSprite = content.Load<Texture2D>("images\\Square");
        }

        public void Update() {
            if (mController == null) {
                return;
            }

            float vel;

            if (!mController.walk)
                vel = VEL;
            else
                vel = VEL_SLOW;

            mVel.X = mController.xDir * vel;
            mVel.Y = mController.yDir * vel;

            mPos += mVel;

        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Begin();

            Rectangle sector;
            if (!mController.walk)
                sector = new Rectangle(0, 0, SIZE, SIZE);
            else
                sector = new Rectangle(SIZE, 0, SIZE, SIZE / 2);

            Vector2 origin = new Vector2(0, sector.Height);

            spriteBatch.Draw(sSprite, mPos, sector, Color.White, mRot, origin, mScale, SpriteEffects.None, 0);

            spriteBatch.End();
        }

    }

}
