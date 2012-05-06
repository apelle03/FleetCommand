using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Fleet_Command.Game.Objects;

namespace Fleet_Command.Game.Commands {
    public class ActiveAttack : ActiveCommand {
        protected Unit target;

        public ActiveAttack(Unit controller, CommandComplete callback, Unit target)
            : base(controller, callback) {
            this.target = target;
        }

        public override void Perform() {
            Vector2 temp = controller.Pos - target.Pos;
            if (temp.Length() != 0) {
                temp.Normalize();
            }
            Vector2 destination = Vector2.Multiply(temp, controller.Range * .9f) + target.Pos;
            controller.PointAt(destination);
            controller.MoveTo(destination);
            controller.Fire(target);
        }

        public override bool Completed() {
            return target.Health == 0;           
        }
    }
}
