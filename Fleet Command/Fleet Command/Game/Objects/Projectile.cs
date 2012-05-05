﻿using System;
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
        public override float MaxSpeed { get { return max_speed; } }

        protected new static float range = 10f;
        public override float Range { get { return range; } }

        protected new static float damage = 2f;
        public override float Damage { get { return damage; } }

        protected override Vector2 Dest {
            get {
                if (target != null) {
                    return target.Pos;
                } else {
                    return dest;
                }
            }
        }

        public Projectile(FC game, PlayArea playArea, Vector2 pos, float angle, Player controller)
            : base(game, playArea, pos, angle, controller) {
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            if (Vector2.Distance(Pos, target.Pos) < Range) {
                target.InflictDamage(Damage);
                this.InflictDamage(MaxHealth);
            }
        }
    }
}
