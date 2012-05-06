using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Fleet_Command.Game.Levels;

namespace Fleet_Command.Decorators {
    public abstract class ControlDecorator {
        protected ControlInfo controlInfo;

        public ControlDecorator(ControlInfo controlInfo) {
            this.controlInfo = controlInfo;
        }

        public abstract void LoadContent();
        public abstract void Update(Control control);
        public abstract void Draw(Control control);

    }
}
