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

        public SquareData Square0, Square1;

        public bool isSave;

        [Serializable]
        public struct SquareData {
            public int health;
            public Vector2 pos;
            public float scale;
            public bool hasProj;
            public ProjectileQueue proj;
        }

        public GameData(String file) {
            filePath = file;
            serializer = new Serializer<GameData>();
            LoadData();
        }

        // Default squares
        public void Reset() {
            Square0 = new SquareData();
            Square1 = new SquareData();

            Square0.health = Square1.health = Square.MAX_HEALTH;

            Square0.pos = new Vector2(200, 300);
            Square1.pos = new Vector2(500, 300);

            Square0.scale = 1.0f;
            Square1.scale = 0.75f;

            Square0.hasProj = Square1.hasProj = false;
            isSave = false;

            SaveData();
        }

        // Saves file
        public void SaveData() {
            serializer.Serialize(this, filePath);
        }

        // Loads file
        public void LoadData() {
            if (serializer.Exists(filePath))
                CopyData(serializer.Deserialize(filePath));
            else {
                Reset();
            }
        }

        // Copies file info
        public void CopyData(GameData gameSave) {
            Square0 = gameSave.Square0;
            Square1 = gameSave.Square1;
            isSave = gameSave.isSave;
        }

    }

}
