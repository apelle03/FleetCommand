using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Fleet_Command.Game.Objects;

namespace Fleet_Command.Game.Commands {
    public abstract class Command {
        public delegate void CommandComplete(Command command);

        protected Unit controller;

        public CommandComplete CallBack { get; private set; }

        public Command(Unit controller, CommandComplete callback) {
            this.controller = controller;
            CallBack = callback;
        }

        public abstract void Perform();
        public abstract bool Completed();
    }
}
