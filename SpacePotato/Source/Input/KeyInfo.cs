﻿using Microsoft.Xna.Framework.Input;

namespace SpacePotato {
    public class KeyInfo {

        private KeyboardState newState, oldState;
        
        public KeyInfo(KeyboardState newState, KeyboardState oldState) {
            this.newState = newState;
            this.oldState = oldState;
        }

        public bool down(Keys key) {
            return newState.IsKeyDown(key);
        }
        
        public bool up(Keys key) {
            return newState.IsKeyUp(key);
        }

        public bool pressed(Keys key) {
            return newState.IsKeyDown(key) && !oldState.IsKeyDown(key);
        }
    }
}