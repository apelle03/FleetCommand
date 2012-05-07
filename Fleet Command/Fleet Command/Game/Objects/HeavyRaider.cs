using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Fleet_Command.Game.Levels;
using Fleet_Command.Game.Players;

namespace Fleet_Command.Game.Objects {
    public class HeavyRaider : CombatShip {
        protected new static string sprite_source = "Ships/raider-heavy";
        public override string SpriteSource { get { return sprite_source; } }

        protected new static float max_speed = 100;
        public override float MaxSpeed { get { return max_speed; } }
        protected new static float max_rotational_speed = (float)Math.PI / 4;
        public override float MaxRotationalSpeed { get { return max_rotational_speed; } }
        protected new static float range = 1000;
        public override float Range { get { return range; } }

        protected new static float max_health = 4;
        public override float MaxHealth { get { return max_health; } }

        protected new static float max_fuel = 10;
        public override float MaxFuel { get { return max_fuel; } }
        protected new static float fuel_rate = .0001f;
        public override float FuelRate { get { return fuel_rate; } }
        protected new static float refuel_rate = .01f;
        public override float RefuelRate { get { return refuel_rate; } }

        protected new static int fire_rate = 10;
        public override int FireRate { get { return fire_rate; } }

        public HeavyRaider(FC game, PlayArea playArea, Vector2 pos, float angle, Player controller)
            : base(game, playArea, pos, angle, controller) {
        }

        public override void Fire(Unit target) {
            if (coolDown == 0 && (Pos - target.Pos).Length() < Range) {
                LightMissile lightMissile = new LightMissile(fc, playArea, Pos, (float)Math.Atan2(target.Pos.Y - Pos.Y, target.Pos.X - Pos.X), controller);
                lightMissile.AttackCommand(target, true);
                playArea.Add(lightMissile);
                coolDown = fire_rate;
            }
        }
    }
}
