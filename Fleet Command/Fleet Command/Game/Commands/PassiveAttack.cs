using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Fleet_Command.Game.Objects;
using Fleet_Command.Utils;

namespace Fleet_Command.Game.Commands {
    public class PassiveAttack : PassiveCommand {
        protected GameOrderSet<Unit> targets;

        public PassiveAttack(Unit controller, CommandComplete callback, GameOrderSet<Unit> targets)
            : base(controller, callback) {
                this.targets = targets;
        }

        public override void Perform() {
            Unit closest = null;
            foreach (Unit u in targets) {
                if (u is Ship && u.Controller != controller.Controller && u.Controller != controller.PlayArea.Level.Players[0]) {
                    if (closest == null || (u.Pos - controller.Pos).Length() < (closest.Pos - controller.Pos).Length()) {
                        closest = u;
                    }
                }
            }
            if (closest != null && (closest.Pos - controller.Pos).Length() < controller.Range) {
                controller.Fire(closest);
            }
        }

        public override bool Completed() {
            return false;
        }
    }
}
