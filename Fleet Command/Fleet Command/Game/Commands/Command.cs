using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Fleet_Command.Game.Objects;

namespace Fleet_Command.Game.Commands {
    public abstract class Command {
        protected Unit controller;

        public Command(Unit controller) {
            this.controller = controller;
        }

        public abstract void Perform();
        public abstract bool Completed();
    }
}
