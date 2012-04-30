using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Fleet_Command.Game.Levels;
using Fleet_Command.Game.Players;

namespace Fleet_Command.Game.Objects {
    public class Projectile : Unit {
        protected new static string sprite_source = "Projectiles/missile";
        protected override string SpriteSource { get { return sprite_source; } }

        protected new static float max_speed = 100;
        protected override float MaxSpeed { get { return max_speed; } }

        public Projectile(FC game, PlayArea playArea, Vector2 pos, float angle, Player controller)
            : base(game, playArea, pos, angle, controller) {
        }

        public override void MoveToTarget() {
            if (hasOrder && target != null) {
                dest = target.Pos;
            }
            if (hasOrder) {
                Vector2 delta = dest - pos;
                if (delta.LengthSquared() == 0) {
                    hasOrder = false;
                    //speed = 0;
                } else {
                    angle = (angle + MathHelper.TwoPi) % MathHelper.TwoPi;
                    double diff = (Math.Atan2(dest.Y - pos.Y, dest.X - pos.X) - angle + 2 * MathHelper.TwoPi) % MathHelper.TwoPi;
                    if (diff > MathHelper.Pi) {
                        angle -= (float)Math.Min(max_rotational_speed, MathHelper.TwoPi - diff);
                    } else {
                        angle += (float)Math.Min(max_rotational_speed, diff);
                    }
                    delta.Normalize();
                    //speed = Math.Min(max_speed, speed + max_speed / 100);
                    pos += Vector2.Multiply(delta, Math.Min(max_speed, (dest - pos).Length()));
                }
            }
        }
    }
}
