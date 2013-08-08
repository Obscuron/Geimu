﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Geimu {

    // Struct representing a single sprite
    public struct Sprite {
        // Top left corner
        private Vector2 pos;
        private Vector2 origin;
        private Rectangle sector;
        private float width;
        private float height;

        // Properties
        public Vector2 Pos {
            get { return pos + origin * Scale; }
            set { pos = value - origin * Scale; }
        }

        public float X {
            get { return Pos.X; }
            set { Pos = new Vector2(value, Pos.Y); }
        }
        public float Y {
            get { return Pos.Y; }
            set { Pos = new Vector2(Pos.X, value); }
        }

        public Vector2 Origin {
            get { return origin; }
            set {
                pos += (value - origin) * Scale;
                origin = value;
            }
        }
        public Rectangle Sector {
            get { return sector; }
            set {
                sector = value;
                width = sector.Width;
                height = sector.Height;
            }
        }
        public float Scale {
            get;
            set;
        }
        public Color Tint {
            get;
            set;
        }
        public float Depth {
            get;
            set;
        }

        public float Width {
            get { return width * Scale; }
            set { width = value / Scale; }
        }
        public float Height {
            get { return height * Scale; }
            set { height = value / Scale; }
        }

        public float Top {
            get { return pos.Y; }
            set { pos.Y = value; }
        }
        public float Bot {
            get { return pos.Y + Height; }
            set { pos.Y = value - Height; }
        }
        public float Left {
            get { return pos.X; }
            set { pos.X = value; }
        }
        public float Right {
            get { return pos.X + Width; }
            set { pos.X = value - Width; }
        }

        public Vector2 Center {
            get { return pos + new Vector2(Width / 2, Height / 2); }
            set { pos = value - new Vector2(Width / 2, Height / 2); }
        }

        public Sprite(Rectangle sector)
            : this() {
            Scale = 1.0f;
            Tint = Color.White;
            Depth = 0;

            Sector = sector;
        }

    }

}
