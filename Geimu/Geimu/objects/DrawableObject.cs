using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Geimu {
    
    // Abstract class for all drawable game objects
    public abstract class DrawableObject {

        protected static Texture2D sSprite;

        public static virtual String FileName() { return null; }

        public static void LoadContent(ContentManager content) {
            if (FileName() != null)
                sSprite = content.Load<Texture2D>(FileName());
        }

    }

}
