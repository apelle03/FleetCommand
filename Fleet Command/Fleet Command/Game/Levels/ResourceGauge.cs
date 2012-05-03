using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Fleet_Command.Game.GameInfo;
using Fleet_Command.Game.Players;

namespace Fleet_Command.Game.Levels {
    public class ResourceGauge : RelativeSizeComponent<DGC> {
        protected ResourceInfo info;
        protected Player controller;

        protected Texture2D bar;
        
        public ResourceGauge(FC game, Player controller, ResourceInfo info, Vector2 relPos, Vector2 relSize)
            : base(game, relPos, relSize) {
                this.info = info;
                this.controller = controller;
        }
        

        public override void LoadContent() {
            base.LoadContent();
            bar = new Texture2D(FC.GraphicsDevice, 1, 1);
            Color[] data = new Color[1];
            data[0] = Color.White;
            bar.SetData<Color>(data);
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

        }

        public override void Draw(GameTime gameTime) {
            base.Draw(gameTime);
            SpriteBatch spriteBatch = FC.SpriteBatch;
            spriteBatch.Draw(bar, BoundingBox, Color.Green);
        }
    }
}
