using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fleet_Command.Decorators {
    public abstract class Decorator {
        protected DGC item;

        public abstract void LoadContent();
        public abstract void Draw();
    }
}
