using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Fleet_Command.Input;

namespace Fleet_Command.Input {
    [DataContract]
    public class BindingSettings {
        [DataMember]
        protected bool custom;
        public bool Custom { get { return custom; } }
        [DataMember]
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
    }
}
