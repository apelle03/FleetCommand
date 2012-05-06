using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Fleet_Command.Game.Objects;

namespace Fleet_Command.Game.Commands {
    public class Dock : ActiveCommand {
        protected CapitalShip target;

        public Dock(Unit controller, CommandComplete callback, CapitalShip target)
            : base(controller, callback) {
                this.target = target;
        }

        public override void Perform() {
            if (controller is CombatShip && target is CapitalShip) {
                controller.PointAt(target.Pos);
                controller.MoveTo(target.Pos);
                if (((CapitalShip)target).Dock((CombatShip)controller)) {
                    ((CombatShip)controller).Dock((CapitalShip)target);
                }
            }
        }

        public override bool Completed() {
            return ((CombatShip)controller).Docked;
        }
    }
}
