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
        protected Rectangle barLocation;

        protected Texture2D icon;
        protected Rectangle iconLocation;
        
        public ResourceGauge(FC game, Player controller, ResourceInfo info, Vector2 relPos, Vector2 relSize)
            : base(game, relPos, relSize) {
                this.info = info;
                this.controller = controller;
        }
        

        public override void LoadContent() {
            base.LoadContent();
            // amount bar
            bar = new Texture2D(FC.GraphicsDevice, 1, 1);
            Color[] data = new Color[1];
            data[0] = Color.White;
            bar.SetData<Color>(data);
            barLocation = new Rectangle((int)(BoundingBox.Left + BoundingBox.Width * .1f), (int)(BoundingBox.Top + BoundingBox.Height * .1f),
                (int)(BoundingBox.Width * .8f), (int)(BoundingBox.Height * .4f));

            // resource icon
            icon = Game.Content.Load<Texture2D>(info.Icon);
            iconLocation = new Rectangle((int)(BoundingBox.Left), (int)(BoundingBox.Top),
                (int)(BoundingBox.Width * .1f), (int)(BoundingBox.Height));
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            ResourceCounter counter = controller.Resource(info.Name);
            int fullWidth = (int)(BoundingBox.Width * .8f);
            int width = (int)(fullWidth * counter.Amount / counter.Capacity);
            barLocation = new Rectangle((int)(BoundingBox.Left + BoundingBox.Width * .1f + fullWidth - width), (int)(BoundingBox.Top + BoundingBox.Height * .1f),
                (int)(width), (int)(BoundingBox.Height * .4f));
        }

        public override void Draw(GameTime gameTime) {
            base.Draw(gameTime);
            SpriteBatch spriteBatch = FC.SpriteBatch;
            spriteBatch.Draw(bar, barLocation, Color.Green);

            spriteBatch.Draw(icon, iconLocation, Color.White);
        }
    }
}
