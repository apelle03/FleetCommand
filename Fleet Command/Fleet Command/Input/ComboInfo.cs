using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fleet_Command.Input {
    public struct ComboInfo {
        public bool Active;
        public float X, Y, Wheel;

        public ComboInfo(bool active, float x, float y, float wheel) {
            Active = active;
            X = x;
            Y = y;
            Wheel = wheel;
        }
    }
}
