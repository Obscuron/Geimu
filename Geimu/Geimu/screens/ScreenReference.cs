using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geimu {

    public class ScreenReference {
        // References to screens
        protected MainMenu menuScreen;
        protected GameScreen gameScreen;
        protected EndScreen endScreen;

        public MainMenu menu {
            get { return menuScreen; }
        }

        public GameScreen game {
            get { return gameScreen; }
        }

        public EndScreen end {
            get { return endScreen; }
        }

        public ScreenReference() {
            menuScreen = new MainMenu();
            gameScreen = new GameScreen();
            endScreen = new EndScreen();
        }

    }

}
