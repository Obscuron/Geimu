using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Geimu {

    // Class for reading and writing to files
    public class Serializer<T> {

        // Writes to file
        public void Serialize(T obj, String fileName) {
            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);

            try {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, obj);
            }
            finally {
                fs.Close();
            }

        }

        // Reads from file
        public T Deserialize(String fileName) {
            T obj;

            if (!File.Exists(fileName)) {
                return default(T); ;
            }

            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);

            try {
                BinaryFormatter bf = new BinaryFormatter();
                obj = (T) bf.Deserialize(fs);
            }
            finally {
                fs.Close();
            }

            return obj;
        }

        // Checks if file exists
        public bool Exists(String fileName) {
            return File.Exists(fileName);
        }

    }

}
