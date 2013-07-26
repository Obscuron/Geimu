
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Geimu {

    public class Square {

        public const float VEL = 2.0f;

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

            mVel.X = mController.Xdir * VEL;
            mVel.Y = mController.Ydir * VEL;

            mPos += mVel;

        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Begin();

            Rectangle sector = new Rectangle(0, 0, sSprite.Width, sSprite.Height);
            spriteBatch.Draw(sSprite, mPos, sector, Color.White, mRot, Vector2.Zero, mScale, SpriteEffects.None, 0);

            spriteBatch.End();
        }

    }

}
