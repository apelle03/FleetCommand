using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Microsoft.Xna.Framework.Input;

using Fleet_Command.Input;

namespace Fleet_Command.Input {
    public class BindingSettings {
        protected bool custom;
        public bool Custom { get { return custom; } }
        protected Dictionary<Actions, List<InputItem>> bindings;

        public BindingSettings() {
            custom = false;
            bindings = new Dictionary<Actions, List<InputItem>>();
        }

        public List<InputItem> GetBindings(Actions action) {
            if (bindings.ContainsKey(action)) {
                return bindings[action];
            } else {
                return null;
            }
        }

        public void AddBinding(Actions action, InputItem ii) {
            if (!bindings.ContainsKey(action)) {
                bindings.Add(action, new List<InputItem>());
            }
            bindings[action].Add(ii);
            custom = true;
        }

        public void RemoveBinding(Actions action, InputItem ii) {
            if (bindings.ContainsKey(action)) {
                if (bindings[action].Contains(ii)) {
                    bindings[action].Remove(ii);
                    custom = true;
                }
            }
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
                    foreach (string part in parts) {
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
    }
}
