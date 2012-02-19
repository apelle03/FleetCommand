using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Fleet_Command {
    class DGCDelegator<T> : DGC where T : DGC {

        protected List<T> drawableGameComponents;

        public DGCDelegator(FC game)
            : base(game) {
            drawableGameComponents = new List<T>();
        }

        public override void Initialize() {
            foreach (DGC dgc in drawableGameComponents) {
                dgc.Initialize();
            }
            base.Initialize();
        }

        public override void LoadContent() {
            foreach (DGC dgc in drawableGameComponents) {
                dgc.LoadContent();
            }
            base.LoadContent();
        }

        public override void UnloadContent() {
            base.UnloadContent();
            foreach (DGC dgc in drawableGameComponents) {
                dgc.UnloadContent();
            }
        }

        public override void Update(GameTime gameTime) {
            if (Enabled) {
                foreach (DGC dgc in drawableGameComponents) {
                    if (dgc.Enabled) {
                        dgc.Update(gameTime);
                    }
                }
            }
        }

        public override void Draw(GameTime gameTime) {
            if (Visible) {
                foreach (DGC dgc in drawableGameComponents) {
                    if (dgc.Visible) {
                        dgc.Draw(gameTime);
                    }
                }
            }
        }
    }
}
