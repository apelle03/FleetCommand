using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Fleet_Command.Game.Levels;
using Fleet_Command.Game.Players;

namespace Fleet_Command.Game.Objects {
    public class Galactica : CapitalShip {
        protected new static string sprite_source = "Ships/Galactica";
        public override string SpriteSource { get { return sprite_source; } }

        public Galactica(FC game, PlayArea playArea, Vector2 pos, float angle, Player controller)
            : base(game, playArea, pos, angle, controller) {
        }
    }
}
