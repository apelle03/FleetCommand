using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using Fleet_Command.Utils;

namespace Fleet_Command {
    class DGCDelegator<T> : DGC where T : DGC {

        protected GameOrderSet<T> components;

        public DGCDelegator(FC game)
            : base(game) {
            components = new GameOrderSet<T>();
        }

        public override void Initialize() {
            foreach (DGC dgc in components) {
                dgc.Initialize();
            }
            base.Initialize();
        }

        public override void LoadContent() {
            foreach (DGC dgc in components) {
                dgc.LoadContent();
            }
            base.LoadContent();
        }

        public override void UnloadContent() {
            base.UnloadContent();
            foreach (DGC dgc in components) {
                dgc.UnloadContent();
            }
        }

        public override void Update(GameTime gameTime) {
            if (Enabled) {
                foreach (DGC dgc in components) {
                    if (dgc.Enabled) {
                        dgc.Update(gameTime);
                    }
                }
            }
        }

        public override void Draw(GameTime gameTime) {
            if (Visible) {
                foreach (DGC dgc in components) {
                    if (dgc.Visible) {
                        dgc.Draw(gameTime);
                    }
                }
            }
        }
    }
}
