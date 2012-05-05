using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Fleet_Command.Game.Objects;

namespace Fleet_Command.Game.Commands {
    public abstract class PassiveCommand : Command {
        public PassiveCommand(Unit controller)
            : base(controller) {
        }
    }
}
