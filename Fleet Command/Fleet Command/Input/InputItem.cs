using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Fleet_Command.Input {
    public enum MouseButtons { LeftButton, MiddleButton, RightButton, X1, X2, XY, Wheel };

    public class InputItem {
        protected List<MouseButtons> mouseRequirements;
        protected List<Keys> keyboardRequirements;

        public List<MouseButtons> MouseRequirements { get { return mouseRequirements; } }
        public List<Keys> KeyboardRequirements { get { return keyboardRequirements; } }

        public InputItem(List<Keys> keys, List<MouseButtons> mouse) {
            mouseRequirements = new List<MouseButtons>(mouse);
            keyboardRequirements = new List<Keys>(keys);
        }

        public override string ToString() {
            string tostring = "";
            foreach (MouseButtons mb in mouseRequirements) {
                tostring += mb.ToString() + ":";
            }
            foreach (Keys key in keyboardRequirements) {
                tostring += key.ToString() + ":";
            }
            return tostring;
        }
    }
}
