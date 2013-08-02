using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Geimu {

    [Serializable]
    public class GameData {
        // Save location
        [NonSerialized]
        protected String filePath;
        // Serializer
        [NonSerialized]
        protected Serializer<GameData> serializer;

        public struct SquareData {
            
        }

        public GameData(String file) {
            filePath = file;
            serializer = new Serializer<GameData>();
        }

        // Saves controls
        public void SaveData() {
            serializer.Serialize(this, filePath);
        }

    }

}
