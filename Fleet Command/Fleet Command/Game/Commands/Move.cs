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
            double diff = (Math.Atan2(destination.Y - controller.Pos.Y, destination.X - controller.Pos.X) - controller.Angle +
                2 * MathHelper.TwoPi) % MathHelper.TwoPi;
            if (diff > MathHelper.Pi) {
                controller.Angle -= (float)Math.Min(controller.MaxRotationalSpeed, MathHelper.TwoPi - diff);
            } else {
                controller.Angle += (float)Math.Min(controller.MaxRotationalSpeed, diff);
            }
            Vector2 delta = destination - controller.Pos;
            if (delta.Length() != 0) {
                delta.Normalize();
                controller.Pos += Vector2.Multiply(delta, Math.Min(controller.MaxSpeed, (destination - controller.Pos).Length()));
            }
        }

        public override bool Completed() {
            return (destination - controller.Pos).Length() == 0;
        }
    }
}
