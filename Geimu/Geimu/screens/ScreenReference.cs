using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geimu {

    // Class containing references to all screens
    public class ScreenReference {
        // References to screens
        public MainMenu menuScreen {
            get;
            private set;
        }

        public OptionsScreen optionsScreen {
            get;
            private set;
        }

        public ControlsScreen controlsScreen {
            get;
            private set;
        }

        public ControlsChooser chooserScreen {
            get;
            private set;
        }

        public GameScreen gameScreen {
            get;
            private set;
        }

        public PauseScreen pauseScreen {
            get;
            private set;
        }

        public EndScreen endScreen {
            get;
            private set;
        }

        public ScreenReference() {
            menuScreen = new MainMenu();
            optionsScreen = new OptionsScreen();
            controlsScreen = new ControlsScreen();
            chooserScreen = new ControlsChooser();
            gameScreen = new GameScreen();
            pauseScreen = new PauseScreen();
            endScreen = new EndScreen();
        }

    }

}
