using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shooter.GameStates {
    class GameStateNotFoundException :Exception{
        private string state;
        private string msg = "Game State not found or not implemented";
        
        public GameStateNotFoundException(string s) {
            state = s;
        }
        public string State {
            get {
                return state;
            }
        }
        public override string ToString() {
            return state + " : " + msg;
        }
    }
}
