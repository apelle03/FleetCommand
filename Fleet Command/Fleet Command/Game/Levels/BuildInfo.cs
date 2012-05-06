using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Fleet_Command.Game.Levels {
    public class BuildInfo : ControlInfo {
        protected int count;
        public int Count { get { return count; } set { count = (int)MathHelper.Clamp(value, 0, 200); } }


        public BuildInfo(FC game, string iconSource)
            : base(game, iconSource) {
            count = 0;
        }

        public override void LoadContent() {
            
        }
    }
}
