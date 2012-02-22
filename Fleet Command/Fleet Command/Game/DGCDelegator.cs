using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using Fleet_Command.Utils;

namespace Fleet_Command {
    class DGCDelegator<T> : DGC where T : DGC {

        protected GameOrderSet<T> components;
        public GameOrderSet<T> Components { get { return components; } }

        public DGCDelegator(FC game)
            : base(game) {
            components = new GameOrderSet<T>();
        }

        public override void Initialize() {
            base.Initialize();
            foreach (DGC dgc in components) {
                dgc.Initialize();
            }
        }

        public override void LoadContent() {
            base.LoadContent();
            foreach (DGC dgc in components) {
                dgc.LoadContent();
            }
        }

        public override void UnloadContent() {
            base.UnloadContent();
            foreach (DGC dgc in components) {
                dgc.UnloadContent();
            }
        }

        public override void Update(GameTime gameTime) {
            if (Enabled) {
                BeforeUpdate(gameTime);
                foreach (DGC dgc in components) {
                    if (dgc.Enabled) {
                        dgc.Update(gameTime);
                    }
                }
                AfterUpdate(gameTime);
            }
        }

        public override void Draw(GameTime gameTime) {
            if (Visible) {
                BeforeDraw(gameTime);
                foreach (DGC dgc in components) {
                    if (dgc.Visible) {
                        dgc.Draw(gameTime);
                    }
                }
                AfterDraw(gameTime);
            }
        }

        public virtual void BeforeUpdate(GameTime gameTime) { }
        public virtual void AfterUpdate(GameTime gameTime) { }
        public virtual void BeforeDraw(GameTime gameTime) { }
        public virtual void AfterDraw(GameTime gameTime) { }
    }
}
