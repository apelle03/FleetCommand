using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Fleet_Command.Game {
    public class PlayArea : RelativeSizeComponent<Unit> {
        public PlayArea(FC game)
            : this(game, Vector2.Zero, Vector2.Zero) {
        }
        
        public PlayArea(FC game, Vector2 relPos, Vector2 relSize)
            : this(game, relPos, relSize, Color.Transparent) {
        }

        public PlayArea(FC game, Vector2 relPos, Vector2 relSize, Color color)
            : base(game, relPos, relSize, color) {
                Components.Add(new Unit(game, Vector2.One * 50, 0));
        }

        public override void LoadContent() {
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime) {
            base.Draw(gameTime);
        }
    }
}
