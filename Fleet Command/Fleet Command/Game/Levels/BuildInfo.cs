using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Fleet_Command.Decorators;
using Fleet_Command.Game.GameInfo;
using Fleet_Command.Game.Objects;

namespace Fleet_Command.Game.Levels {
    public class BuildInfo : ControlInfo {
        protected ConstructableInfo info;
        public ConstructableInfo Info { get { return info; } }

        public CapitalShip CapitalShip { get; set; }

        public BuildInfo(FC game, ConstructableInfo ci, CapitalShip cs)
            : base(game, ci.Icon) {
                info = ci;
                CapitalShip = cs;
        }
    }
}
