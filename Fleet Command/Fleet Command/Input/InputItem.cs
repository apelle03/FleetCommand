using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Microsoft.Xna.Framework.Input;

namespace Fleet_Command.Input {
    [DataContract]
    public enum MouseButtons { Left = 0, Middle, Right, X1, X2, XY, Wheel };

    [DataContract]
    public class InputItem {
        [DataMember]
        protected BitArray mouseRequirements;
        [DataMember]
        protected List<Keys> keyboardRequirements;

        public BitArray MouseRequirements { get { return mouseRequirements; } }
        public List<Keys> KeyboardRequirements { get { return keyboardRequirements; } }

        public InputItem(List<Keys> keys, BitArray mouse) {
            mouseRequirements = new BitArray(Enum.GetValues(typeof(MouseButtons)).Length);
            mouseRequirements.Or(mouse);
            keyboardRequirements = new List<Keys>(keys);
        }
    }
}
