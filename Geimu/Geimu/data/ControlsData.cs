using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Geimu {

    // Class for holding controls during game
    [Serializable]
    public class ControlsData {
        // Save location
        [NonSerialized]
        protected String filePath;
        // Serializer
        [NonSerialized]
        protected Serializer<ControlsData> serializer;

        // Key bindings
        public Keys[] up, down, left, right, walk, fire;

        // Loads up controls
        public ControlsData(String file) {
            filePath = file;
            serializer = new Serializer<ControlsData>();
            LoadData();
        }

        // Default keys
        public void Reset() {
            up = new Keys[] { Keys.W, Keys.Up };
            down = new Keys[] { Keys.S, Keys.Down };
            left = new Keys[] { Keys.A, Keys.Left };
            right = new Keys[] { Keys.D, Keys.Right };
            walk = new Keys[] { Keys.LeftShift, Keys.RightShift };
            fire = new Keys[] { Keys.Space, Keys.RightControl };
            SaveData();
        }

        // Saves controls
        public void SaveData() {
            serializer.Serialize(this, filePath);
        }

        // Loads controls
        public void LoadData() {
            if (serializer.Exists(filePath))
                copyData(serializer.Deserialize(filePath));
            else {
                Reset();
            }
        }

        // Copies data from save file
        public void copyData(ControlsData data) {
            up = data.up;
            down = data.down;
            left = data.left;
            right = data.right;
            walk = data.walk;
            fire = data.fire;
        }

    }

}
