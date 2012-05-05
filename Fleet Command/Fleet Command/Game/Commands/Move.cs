using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Fleet_Command.Game.Objects;

namespace Fleet_Command.Game.Commands {
    public class Move : ActiveCommand {
        protected Vector2 destination;

        public Move(Unit controller, Vector2 dest)
            : base(controller) {
            destination = dest;
        }

        public override void Perform() {
            controller.PointAt(destination);
            controller.MoveTo(destination);
        }

        public override bool Completed() {
            return (destination - controller.Pos).Length() == 0;
        }
    }
}
