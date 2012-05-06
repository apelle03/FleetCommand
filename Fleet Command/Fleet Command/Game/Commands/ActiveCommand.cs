using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Fleet_Command.Game.Objects;

namespace Fleet_Command.Game.Commands {
    public abstract class ActiveCommand : Command {
        public ActiveCommand(Unit controller, CommandComplete callback)
            : base(controller, callback) {
        }
    }
}
