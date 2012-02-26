using System;
using System.Collections;
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
                if (File.Exists(FC.SettingsDir + "custom.xml")) {
                    FileStream file = new FileStream(FC.SettingsDir + "custom.xml", FileMode.Open);
                    XmlDictionaryReader xmlReader = XmlDictionaryReader.CreateTextReader(file, new XmlDictionaryReaderQuotas());
                    DataContractSerializer reader = new DataContractSerializer(typeof(BindingSettings));
                    bindings = (BindingSettings)reader.ReadObject(xmlReader);
                    xmlReader.Close();
                    file.Close();
                } else if (File.Exists(FC.SettingsDir + "default.xml")) {
                    FileStream file = new FileStream(FC.SettingsDir + "default.xml", FileMode.Open);
                    XmlDictionaryReader xmlReader = XmlDictionaryReader.CreateTextReader(file, new XmlDictionaryReaderQuotas());
                    DataContractSerializer reader = new DataContractSerializer(typeof(BindingSettings));
                    bindings = (BindingSettings)reader.ReadObject(xmlReader);
                    xmlReader.Close();
                    file.Close();
                }
                keyboardState = new Dictionary<Keys, KeyState>();
        }

        public void Save() {
            if (bindings.Custom) {
                FileStream file = new FileStream(FC.SettingsDir + "custom.xml", FileMode.Create);
                DataContractSerializer writer = new DataContractSerializer(typeof(BindingSettings));
                writer.WriteObject(file, bindings);
                file.Close();
            }
        }

        public void Register(Actions action) {
            if (bindings.GetBindings(action) == null) {
                // write code here to not overwrite default settings when new stuff is bound
                BitArray mouse = new BitArray(Enum.GetValues(typeof(MouseButtons)).Length);
                mouse.Set((int)MouseButtons.Right, true);
                mouse.Set((int)MouseButtons.XY, true);
                InputItem ii = new InputItem(new List<Keys>(), mouse);

                bindings.AddBinding(action, ii);

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
                    if (combo.MouseRequirements.Get((int)MouseButtons.Left) && mouseState.LeftButton == ButtonState.Released) {
                        value = 0;
                    }
                    if (combo.MouseRequirements.Get((int)MouseButtons.Middle) && mouseState.MiddleButton == ButtonState.Released) {
                        value = 0;
                    }
                    if (combo.MouseRequirements.Get((int)MouseButtons.Right) && mouseState.RightButton == ButtonState.Released) {
                        value = 0;
                    }
                    if (combo.MouseRequirements.Get((int)MouseButtons.X1) && mouseState.XButton1 == ButtonState.Released) {
                        value = 0;
                    }
                    if (combo.MouseRequirements.Get((int)MouseButtons.X2) && mouseState.XButton2 == ButtonState.Released) {
                        value = 0;
                    }
                    if (combo.MouseRequirements.Get((int)MouseButtons.XY)) {
                        Rectangle boundingBox = actor.BoundingBox;
                        if (mouseState.X < boundingBox.X || mouseState.X > boundingBox.X + boundingBox.Width ||
                            mouseState.Y < boundingBox.Y || mouseState.Y > boundingBox.Y + boundingBox.Height) {
                                value = 0;
                        }
                    }
                    if (combo.MouseRequirements.Get((int)MouseButtons.Wheel)) {
                        value = mouseState.ScrollWheelValue;
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
