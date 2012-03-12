using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Fleet_Command.Decorators;

namespace Fleet_Command.Game {
    class Controls : RelativeSizeComponent<Control> {
        protected CorneredBorder border;

        public Controls(FC game)
            : this(game, Vector2.Zero, Vector2.Zero) {
        }
        
        public Controls(FC game, Vector2 relPos, Vector2 relSize)
            : this(game, relPos, relSize, Color.Transparent) {
        }

        public Controls(FC game, Vector2 relPos, Vector2 relSize, Color color)
            : base(game, relPos, relSize, color) {
                border = new CorneredBorder(this, "Galactica");
        }

        public override void LoadContent() {
            base.LoadContent();
            border.LoadContent();
        }

        public override void BeforeDraw(Microsoft.Xna.Framework.GameTime gameTime) {
            base.BeforeDraw(gameTime);
            border.Draw();
        }
    }
}
