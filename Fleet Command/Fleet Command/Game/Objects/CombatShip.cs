using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Fleet_Command.Game.Levels;
using Fleet_Command.Game.Players;
using Fleet_Command.Game.Commands;

namespace Fleet_Command.Game.Objects {
    public class CombatShip : Ship {
        protected new static string sprite_source = "Ships/viper-mkii";
        public override string SpriteSource { get { return sprite_source; } }

        protected new static float max_speed = 100;
        public override float MaxSpeed { get { return max_speed; } }
        protected new static float max_rotational_speed = (float)Math.PI / 4;
        public override float MaxRotationalSpeed { get { return max_rotational_speed; } }
        protected new static float range = 200;
        public override float Range { get { return range; } }

        protected new static float max_fuel = 10;
        public override float MaxFuel { get { return max_fuel; } }
        protected new static float fuel_rate = .0001f;
        public override float FuelRate { get { return fuel_rate; } }
        protected new static float refuel_rate = .01f;
        public override float RefuelRate { get { return refuel_rate; } }

        protected bool docked;
        public bool Docked { get { return docked; } }
        protected CapitalShip hangar;
        public CapitalShip Hangar { get { return hangar; } }

        public CombatShip(FC game, PlayArea playArea, Vector2 pos, float angle, Player controller)
            : base(game, playArea, pos, angle, controller) {
                docked = false;
        }

        public void DockCommand(CapitalShip captialShip, bool immediate) {
            if (immediate) {
                activeCommands.Clear();
            }
            activeCommands.Enqueue(new Dock(this, null, captialShip));
        }

        public void Dock(CapitalShip hanger) {
            this.hangar = hanger;
            docked = true;
        }

        public void LaunchCommand(CapitalShip captialShip, bool immediate) {
            if (immediate) {
                activeCommands.Clear();
            }
            activeCommands.Enqueue(new Launch(this, null, captialShip));
        }

        public void Launch() {
            docked = false;
            hangar.Docked.Remove(this);
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            if (Docked) {
                Pos = hangar.Pos;
                if (Fuel < MaxFuel) {
                    ChangeFuel(controller.Resource("Fuel").Use(Math.Min(RefuelRate, MaxFuel - Fuel)));
                }
            }
        }

        public override void Draw(GameTime gameTime) {
            if (!Docked) {
                base.Draw(gameTime);
            }
        }
    }
}
