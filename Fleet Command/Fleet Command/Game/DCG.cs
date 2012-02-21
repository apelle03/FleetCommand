using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using Fleet_Command.Utils;

namespace Fleet_Command {
    class DGC : DrawableGameComponent {

        private FC fc;
        public FC FC { get { return fc; } }
        protected GameOrderSet<DGC> components;

        public DGC(FC game)
            : base(game) {
            fc = game;
            components = new GameOrderSet<DGC>();
        }

        public override void Initialize() {
            foreach (DGC dgc in components) {
                dgc.Initialize();
            }
            base.Initialize();
        }

        public virtual new void LoadContent() {
            foreach (DGC dgc in components) {
                dgc.LoadContent();
            }
            base.LoadContent();
        }

        public virtual new void UnloadContent() {
            base.UnloadContent();
            foreach (DGC dgc in components) {
                dgc.UnloadContent();
            }
        }
    }
}
