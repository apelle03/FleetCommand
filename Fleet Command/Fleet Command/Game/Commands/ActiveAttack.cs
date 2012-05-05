using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Fleet_Command.Game.Objects;

namespace Fleet_Command.Game.Commands {
    public class ActiveAttack : ActiveCommand {
        protected Unit target;

        public ActiveAttack(Unit controller, Unit target)
            : base(controller) {
            this.target = target;
        }

        public override void Perform() {
            Vector2 temp = controller.Pos - target.Pos;
            if (temp.Length() != 0) {
                temp.Normalize();
            }
            Vector2 destination = Vector2.Multiply(temp, controller.Range * .9f) + target.Pos;
            Vector2 delta = destination - controller.Pos;
            if (delta.Length() != 0) {
                delta.Normalize();
                double diff = (Math.Atan2(destination.Y - controller.Pos.Y, destination.X - controller.Pos.X) - controller.Angle +
                2 * MathHelper.TwoPi) % MathHelper.TwoPi;
                if (diff > MathHelper.Pi) {
                    controller.Angle -= (float)Math.Min(controller.MaxRotationalSpeed, MathHelper.TwoPi - diff);
                } else {
                    controller.Angle += (float)Math.Min(controller.MaxRotationalSpeed, diff);
                }
                controller.Pos += Vector2.Multiply(delta, Math.Min(controller.MaxSpeed, (destination - controller.Pos).Length()));
            }
            controller.Fire(target);
        }

        public override bool Completed() {
            return target.Health == 0;           
        }
    }
}
