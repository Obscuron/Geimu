using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Geimu {

    // Class for handling Projectile
    public class ProjectileQueue {
        // Max constants for a single queue
        public const int MAX_AGE = 60;
        public const int MAX_NUM = 64;

        // Texture
        private static Texture2D sSprite;

        // Boundaries of the windows
        protected static Rectangle mBounds;

        // Internal Queue
        protected Projectile[] queue;
        protected int head;
        protected int tail;
        protected int size;

        // Struct for a single projectile
        public struct Projectile {
            public Vector2 pos;
            public Vector2 vel;
            public int age;
        }

        // Constructs a new projectile queue
        public ProjectileQueue(Rectangle bounds) {
            mBounds = bounds;

            queue = new Projectile[MAX_NUM];
            head = 0;
            tail = -1;
            size = 0;

            // Create a pool of projectiles
            for (int i = 0; i < MAX_NUM; i++) {
                queue[i] = new Projectile();
            }
        }

        // Loads texture into memory
        public static void LoadContent(ContentManager content) {
            sSprite = content.Load<Texture2D>("images\\Projectile");
        }

    }

}
