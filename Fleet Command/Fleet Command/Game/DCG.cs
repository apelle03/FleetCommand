using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Fleet_Command {
    class DGC : DrawableGameComponent {

        private FC fc;
        public FC FC { get { return fc; } }

        public DGC(FC game)
            : base(game) {
            fc = game;
        }

        public virtual new void LoadContent() {
            base.LoadContent();
        }

        public virtual new void UnloadContent() {
            base.UnloadContent();
        }
    }
}
