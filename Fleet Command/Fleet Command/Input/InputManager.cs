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

        protected Dictionary<Actions, List<InputItem>> bindings;
        protected bool custom;

        protected KeyboardState keyboardState;
        protected MouseState mouseState;

        public InputManager(FC game)
            : base(game) {
                custom = false;
                bindings = new Dictionary<Actions, List<InputItem>>();
                if (File.Exists(FC.SettingsDir + "custom.ini")) {
                    LoadFromFile(FC.SettingsDir + "custom.ini");
                } else if (File.Exists(FC.SettingsDir + "default.ini")) {
                    LoadFromFile(FC.SettingsDir + "default.ini");
                }
        }

        public void LoadFromFile(string fileName) {
            StreamReader reader = new StreamReader(fileName);
            custom = false;
            bindings = new Dictionary<Actions, List<InputItem>>();
            bool c = Boolean.Parse(reader.ReadLine());
            while (reader.Peek() >= 0) {
                string[] parts = reader.ReadLine().Split(':');
                Actions action;
                if (Enum.TryParse<Actions>(parts[0], true, out action)) {
                    List<MouseButtons> mouse = new List<MouseButtons>();
                    List<Keys> keys = new List<Keys>();
                    foreach (string part in parts.Skip<string>(1)) {
                        MouseButtons mb;
                        Keys k;
                        if (Enum.TryParse<MouseButtons>(part, out mb)) {
                            mouse.Add(mb);
                        } else if (Enum.TryParse<Keys>(part, out k)) {
                            keys.Add(k);
                        }
                    }
                    AddBinding(action, new InputItem(keys, mouse));
                }
            }
            custom = c;
            reader.Close();
        }

        public void SaveToFile(string fileName) {
            StreamWriter writer = new StreamWriter(fileName, false);
            writer.WriteLine(custom);
            foreach (Actions action in bindings.Keys) {
                string keys = "";
                foreach (InputItem ii in bindings[action]) {
                    keys += ii.ToString();
                }
                writer.WriteLine("{0}:{1}", action.ToString(), keys);
            }
            writer.Close();
        }

        protected List<InputItem> GetBindings(Actions action) {
            if (bindings.ContainsKey(action)) {
                return bindings[action];
            } else {
                return null;
            }
        }

        protected void AddBinding(Actions action, InputItem ii) {
            if (!bindings.ContainsKey(action)) {
                bindings.Add(action, new List<InputItem>());
            }
            bindings[action].Add(ii);
            custom = true;
        }

        protected void RemoveBinding(Actions action, InputItem ii) {
            if (bindings.ContainsKey(action)) {
                if (bindings[action].Contains(ii)) {
                    bindings[action].Remove(ii);
                    custom = true;
                }
            }
        }

        public void Save() {
            if (custom) {
                SaveToFile(FC.SettingsDir + "custom.ini");
            }
        }

        public void Register(Actions action) {
            if (GetBindings(action) == null) {
                List<MouseButtons> mouse = new List<MouseButtons>();
                List<Keys> keys = new List<Keys>();
                keys.Add(Keys.Escape);
                InputItem ii = new InputItem(keys, mouse);

                AddBinding(action, ii);
            }
        }

        public ComboInfo CheckAction(Actions action, DGC actor) {
            List<InputItem> combos;
            if ((combos = GetBindings(action)) != null) {
                foreach (InputItem combo in combos) {
                    ComboInfo ci = combo.CheckAction(actor, keyboardState, mouseState);
                    if (ci.Active) {
                        return ci;
                    }
                }
            }
            return new ComboInfo(false, 0, 0, 0);
        }

        public override void Update(GameTime gameTime) {
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            foreach (List<InputItem> iis in bindings.Values) {
                foreach (InputItem ii in iis) {
                    ii.Update(keyboardState, mouseState);
                }
            }
            base.Update(gameTime);
        }
    }
}
