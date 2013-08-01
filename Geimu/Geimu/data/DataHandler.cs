using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geimu {

    // Class containing reference to all data handlers
    public class DataReference {
        // References
        protected ControlsData controlsData;

        public ControlsData controls {
            get { return controlsData; }
        }

        public DataReference() {
            controlsData = new ControlsData("Content\\Controls.conf");
        }

    }

}
