using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Fleet_Command.Input {
    public enum MouseButtons { LeftButton, MiddleButton, RightButton, X1, X2, XY, Wheel };

    public class InputItem {
        protected List<MouseButtons> mouseRequirements;
        protected List<Keys> keyboardRequirements;
        protected ComboInfo comboInfo;

        public InputItem(List<Keys> keys, List<MouseButtons> mouse) {
            mouseRequirements = new List<MouseButtons>(mouse);
            keyboardRequirements = new List<Keys>(keys);
            comboInfo = new ComboInfo(false, 0, 0, 0);
        }

        public ComboInfo CheckAction(DGC actor, KeyboardState keyboardState, MouseState mouseState) {
            comboInfo.Active = true;
            foreach (Keys key in keyboardRequirements) {
                if (keyboardState[key] == KeyState.Up)
                    comboInfo.Active = false;
            }
            foreach (MouseButtons mb in mouseRequirements) {
                switch (mb) {
                    case MouseButtons.LeftButton:
                        if (mouseState.LeftButton == ButtonState.Released) comboInfo.Active = false;
                        break;
                    case MouseButtons.MiddleButton:
                        if (mouseState.MiddleButton == ButtonState.Released) comboInfo.Active = false;
                        break;
                    case MouseButtons.RightButton:
                        if (mouseState.RightButton == ButtonState.Released) comboInfo.Active = false;
                        break;
                    case MouseButtons.X1:
                        if (mouseState.XButton1 == ButtonState.Released) comboInfo.Active = false;
                        break;
                    case MouseButtons.X2:
                        if (mouseState.XButton2 == ButtonState.Released) comboInfo.Active = false;
                        break;
                    case MouseButtons.XY:
                        Rectangle boundingBox = actor.BoundingBox;
                        if (mouseState.X < boundingBox.X || mouseState.X > boundingBox.X + boundingBox.Width ||
                        mouseState.Y < boundingBox.Y || mouseState.Y > boundingBox.Y + boundingBox.Height) {
                            comboInfo.Active = false;
                        }
                        break;
                    case MouseButtons.Wheel:
                        if (mouseState.ScrollWheelValue - comboInfo.Wheel == 0) {
                            comboInfo.Active = false;
                        }
                        break;
                }
            }
            comboInfo.X = mouseState.X;
            comboInfo.Y = mouseState.Y;
            comboInfo.Wheel = mouseState.ScrollWheelValue - comboInfo.Wheel;
            return comboInfo;
        }

        public override string ToString() {
            string tostring = "";
            foreach (MouseButtons mb in mouseRequirements) {
                tostring += mb.ToString() + ":";
            }
            foreach (Keys key in keyboardRequirements) {
                tostring += key.ToString() + ":";
            }
            tostring = tostring.TrimEnd(':');
            return tostring;
        }
    }
}
