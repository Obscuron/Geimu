using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geimu {

    // Class containing reference to all data handlers
    public class DataReference {
        // References
        public ControlsData controlsData {
            get;
            private set;
        }

        public GameData gameData {
            get;
            private set;
        }

        public DataReference() {
            controlsData = new ControlsData("Content\\Controls.conf");
            gameData = new GameData("Content\\GameSave.sav");
        }

    }

}
