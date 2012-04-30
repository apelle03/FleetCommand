using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Fleet_Command.Game.Levels;
using Fleet_Command.Game.Players;

namespace Fleet_Command.Game.Objects {
    public class Galactica : Ship {
        protected new static string sprite_source = "Units/Galactica";
        protected override string SpriteSource { get { return sprite_source; } }

        public Galactica(FC game, PlayArea playArea, Vector2 pos, float angle, Player controller)
            : base(game, playArea, pos, angle, controller) {
        }

        public override void Update(GameTime gameTime) {
            if (!moving) {
                Random gen = new Random();
                MoveTo(new Vector2((float)(gen.NextDouble() * 10000 - 5000), (float)(gen.NextDouble() * 10000 - 5000)));
            }
            base.Update(gameTime);
        }
    }
}
