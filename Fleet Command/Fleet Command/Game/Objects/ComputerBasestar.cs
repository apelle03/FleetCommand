using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Fleet_Command.Game.GameInfo;
using Fleet_Command.Game.Levels;
using Fleet_Command.Game.Objects;
using Fleet_Command.Game.Players;

namespace Fleet_Command.Game.Objects {
    public class ComputerBasestar : Basestar {
        protected Resource resource;
        protected Random rand;
        protected List<CombatShip> combatShips;

        public ComputerBasestar(FC game, PlayArea playArea, Vector2 pos, float angle, Player controller)
            : base(game, playArea, pos, angle, controller) {
                resource = null;
                rand = new Random();
                combatShips = new List<CombatShip>();
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            // find resource
            if (resource == null) {
                Resource closest = null;
                foreach (Unit u in PlayArea.Components) {
                    if (u is Resource && u.Controller == PlayArea.Level.Players[0]) {
                        if (closest == null || (u.Pos - Pos).Length() < (closest.Pos - Pos).Length()) {
                            closest = (Resource)u;
                        }
                    }
                }
                CollectCommand(closest, true);
            }
            // build units
            if (BuildQueue.Count <= 1) {
                List<ConstructableInfo> constructables = Constructables.ConstructableList;
                BuildQueueInfo bi;
                if (rand.NextDouble() < .8f) {
                    bi = new BuildQueueInfo(FC, constructables[1], this);
                } else {
                    bi = new BuildQueueInfo(FC, constructables[4], this);
                }
                bi.LoadContent();
                BuildQueue.Add(bi);                
            }

            // launch units
            if (Docked.Count > 4) {
                foreach (CombatShip cs in Docked) {
                    cs.LaunchCommand(this, true);
                    combatShips.Add(cs);
                }
            }

            // attack enemy
            foreach (CombatShip cs in combatShips) {
                if (cs.activeCommands.Count == 0) {
                    foreach (Unit u in PlayArea.Components) {
                        if (u is Ship && u.CanBeAttacked && u.Controller == PlayArea.Level.Controller) {
                            cs.AttackCommand(u, true);
                        }
                    }
                }
            }
        }
    }
}
