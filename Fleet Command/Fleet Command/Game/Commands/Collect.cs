using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Fleet_Command.Game.Objects;
using Fleet_Command.Game.Players;

namespace Fleet_Command.Game.Commands {
    public class Collect : ActiveCommand {
        protected Resource resource;

        public Collect(Unit controller, CommandComplete callback, Resource resource)
            : base(controller, callback) {
            this.resource = resource;
        }

        public override void Perform() {
            Vector2 temp = controller.Pos - resource.Pos;
            if (temp.Length() != 0) {
                temp.Normalize();
            }
            Vector2 destination = Vector2.Multiply(temp, controller.Range * .9f) + resource.Pos;
            controller.PointAt(destination);
            controller.MoveTo(destination);
            ((CapitalShip)controller).Collect(resource);
        }

        public override bool Completed() {
            foreach (ResourceCounter rc in controller.Controller.Resources.Values) {
                if (rc.Amount < rc.Capacity) {
                    return false;
                }
            }
            return true;
        }
    }
}
