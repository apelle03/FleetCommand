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
        public List<CombatShip> Docked { get { return docked; } }
        public int DockedSquadronCount { get { return docked.Count; } }

        protected Build buildCommand;
        protected List<BuildQueueInfo> buildQueue;
        public List<BuildQueueInfo> BuildQueue { get { return buildQueue; } }

        public CapitalShip(FC game, PlayArea playArea, Vector2 pos, float angle, Player controller)
            : base(game, playArea, pos, angle, controller) {
                docked = new List<CombatShip>();
                buildQueue = new List<BuildQueueInfo>();
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

            if (Fuel < MaxFuel) {
                ChangeFuel(controller.Resource("Fuel").Use(Math.Min(RefuelRate, MaxFuel - Fuel)));
            }
            bool building = false;
            foreach (PassiveCommand pc in passiveCommands) {
                if (pc is Build) {
                    building = true;
                }
            }
            if (!building && buildQueue.Count > 0) {
                buildCommand = new Build(this, DoneBuild, buildQueue[0]);
                passiveCommands.Add(buildCommand);
            }
        }

        public void CancelBuild(BuildQueueInfo bi) {
            if (buildCommand.Info == bi) {
                passiveCommands.Remove(buildCommand);
            }
            buildQueue.Remove(bi);
        }

        public void CollectCommand(Resource resource, bool immediate) {
            if (immediate) {
                activeCommands.Clear();
            }
            activeCommands.Enqueue(new Collect(this, null, resource));
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

        public void DoneBuild(Command command) {
            if (command is Build) {
                BuildQueue.Remove(((Build)command).Info);
                Unit u = ((Build)command).Info.Info.CreateNew(FC, playArea, Pos, Angle, Controller);
                if (u is CombatShip) {
                    CombatShip cs = (CombatShip)u;
                    u.LoadContent();
                    playArea.Add(u);
                    cs.DockCommand(this, true);
                }
                buildCommand = null;
            }
        }

        public override void Fire(Unit target) {
            if (coolDown == 0 && (Pos - target.Pos).Length() < Range) {
                Missile missile = new Missile(fc, playArea, Pos, (float)Math.Atan2(target.Pos.Y - Pos.Y, target.Pos.X - Pos.X), controller);
                missile.AttackCommand(target, true);
                playArea.Add(missile);
                coolDown = FireRate;
            }
        }
    }
}
