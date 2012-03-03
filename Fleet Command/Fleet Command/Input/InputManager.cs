using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Fleet_Command.Input {
    public class InputManager : DGC {

        protected BindingSettings bindings;
        protected Dictionary<Keys, KeyState> keyboardState;
        protected MouseState mouseState;

        public InputManager(FC game)
            : base(game) {
                bindings = new BindingSettings();
                if (File.Exists(FC.SettingsDir + "custom.ini")) {
                    bindings.LoadFromFile(FC.SettingsDir + "custom.ini");
                } else if (File.Exists(FC.SettingsDir + "default.ini")) {
                    bindings.LoadFromFile(FC.SettingsDir + "default.ini");
                }
                keyboardState = new Dictionary<Keys, KeyState>();
        }

        public void Save() {
            if (bindings.Custom) {
                bindings.SaveToFile(FC.SettingsDir + "custom.ini");
            }
        }

        public void Register(Actions action) {
            if (bindings.GetBindings(action) == null) {
                List<MouseButtons> mouse = new List<MouseButtons>();
                mouse.Add(MouseButtons.LeftButton);
                mouse.Add(MouseButtons.XY);
                InputItem ii = new InputItem(new List<Keys>(), mouse);

                bindings.AddBinding(action, ii);

                // need to add keys no mater what
                foreach (Keys key in ii.KeyboardRequirements) {
                    keyboardState.Add(key, KeyState.Up);
                }
            }
        }

        public int CheckAction(Actions action, DGC actor) {
            List<InputItem> combos;
            if ((combos = bindings.GetBindings(action)) != null) {
                foreach (InputItem combo in combos) {
                    int value = 1;
                    foreach (Keys key in combo.KeyboardRequirements) {
                        if (keyboardState[key] == KeyState.Up)
                            value = 0;
                    }
                    foreach (MouseButtons mb in combo.MouseRequirements) {
                        switch (mb) {
                            case MouseButtons.LeftButton:
                                if (mouseState.LeftButton == ButtonState.Released) value = 0;
                                break;
                            case MouseButtons.MiddleButton:
                                if (mouseState.MiddleButton == ButtonState.Released) value = 0;
                                break;
                            case MouseButtons.RightButton:
                                if (mouseState.RightButton == ButtonState.Released) value = 0;
                                break;
                            case MouseButtons.X1:
                                if (mouseState.XButton1 == ButtonState.Released) value = 0;
                                break;
                            case MouseButtons.X2:
                                if (mouseState.XButton2 == ButtonState.Released) value = 0;
                                break;                                
                            case MouseButtons.XY:
                                Rectangle boundingBox = actor.BoundingBox;
                                if (mouseState.X < boundingBox.X || mouseState.X > boundingBox.X + boundingBox.Width ||
                                mouseState.Y < boundingBox.Y || mouseState.Y > boundingBox.Y + boundingBox.Height) {
                                    value = 0;
                                }
                                break;
                            case MouseButtons.Wheel:
                                value = mouseState.ScrollWheelValue;
                                break;
                        }
                    }
                    if (value != 0) {
                        return value;
                    }
                }
            }
            return 0;
        }

        public override void Update(GameTime gameTime) {
            KeyboardState keyState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            foreach (Keys key in keyboardState.Keys) {
                if (keyState.IsKeyUp(key))
                    keyboardState[key] = KeyState.Up;
                else
                    keyboardState[key] = KeyState.Down;
            }
            base.Update(gameTime);
        }
    }
}
