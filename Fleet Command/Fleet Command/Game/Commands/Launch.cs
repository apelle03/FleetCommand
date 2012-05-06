using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Fleet_Command.Game.Objects;

namespace Fleet_Command.Game.Commands {
    public class Launch : ActiveCommand {
        protected CapitalShip hangar;

        public Launch(Unit controller, CommandComplete callback, CapitalShip hangar)
            : base(controller, callback) {
                this.hangar = hangar;
        }

        public override void Perform() {
            if (controller is CombatShip && hangar is CapitalShip) {
                controller.MoveCommand(hangar.Pos + new Vector2((float)(200 * Math.Cos(hangar.Angle + MathHelper.PiOver2)),
                    (float)(200 * Math.Sin(hangar.Angle + MathHelper.PiOver2))), false);
                ((CombatShip)controller).Launch();
            }
        }

        public override bool Completed() {
            return !hangar.Docked.Contains(controller);
        }
    }
}
