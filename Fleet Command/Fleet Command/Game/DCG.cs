using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using Fleet_Command.Utils;

namespace Fleet_Command {
    public class DGC : DrawableGameComponent {

        protected FC fc;
        public FC FC { get { return fc; } }

        protected Rectangle boundingBox;
        public Rectangle BoundingBox { get { return boundingBox; } }

        public DGC(FC game)
            : base(game) {
            fc = game;
            boundingBox = Rectangle.Empty;
        }

        public virtual new void LoadContent() {
            base.LoadContent();
        }

        public virtual new void UnloadContent() {
            base.UnloadContent();
        }
    }
}
