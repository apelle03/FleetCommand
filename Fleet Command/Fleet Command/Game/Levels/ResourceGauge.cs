using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Fleet_Command.Game.Levels {
    public class ResourceGauge : RelativeSizeComponent<DGC> {
        public ResourceGauge(FC game, Level level)
            : this(game, level, Vector2.Zero, Vector2.Zero) {
        }
        
        public ResourceGauge(FC game, Level level, Vector2 relPos, Vector2 relSize)
            : this(game, level, relPos, relSize, Color.Transparent) {
        }

        public ResourceGauge(FC game, Level level, Vector2 relPos, Vector2 relSize, Color color)
            : base(game, relPos, relSize, color) {
        }




        public override void LoadContent() {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
            base.Draw(gameTime);
        }
    }
}
