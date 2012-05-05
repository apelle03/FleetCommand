using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Fleet_Command.Game.Levels;
using Fleet_Command.Game.Players;
using Fleet_Command.Game.Commands;

namespace Fleet_Command.Game.Objects {
    public class CapitalShip : Ship {
        protected static int max_squadrons = 50;
        public int MaxSquadrons { get { return max_squadrons; } }

        protected List<CombatShip> docked;
        public int DockedSquadronCount { get { return docked.Count; } }

        public CapitalShip(FC game, PlayArea playArea, Vector2 pos, float angle, Player controller)
            : base(game, playArea, pos, angle, controller) {
                docked = new List<CombatShip>();
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            if (Fuel < MaxFuel) {
                ChangeFuel(controller.Resource("Fuel").Use(Math.Min(RefuelRate, MaxFuel - Fuel)));
            }
        }

        public void CollectCommand(Resource resource, bool immediate) {
            if (immediate) {
                activeCommands.Clear();
            }
            activeCommands.Enqueue(new Collect(this, resource));
        }

        public void Collect(Resource resource) {
            if ((Pos - resource.Pos).Length() < Range) {
                foreach (ResourceCounter rc in controller.Resources.Values) {
                    rc.Supply(resource.GetRate(rc.Name));
                }
            }
        }

        public bool Dock(CombatShip ship) {
            if (DockedSquadronCount < MaxSquadrons && (Pos - ship.Pos).Length() < 10) {
                docked.Add(ship);
                return true;
            }
            return false;
        }
    }
}
