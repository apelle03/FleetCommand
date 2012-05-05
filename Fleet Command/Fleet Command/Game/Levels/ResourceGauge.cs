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

        protected SpriteFont font;
        protected float amountScale;
        protected Vector2 amountLocation;
        protected float changeScale;
        protected Vector2 changeLocation;
        protected float usageScale;
        protected Vector2 usageLocation;
        
        public ResourceGauge(FC game, Player controller, ResourceInfo info, Vector2 relPos, Vector2 relSize)
            : base(game, relPos, relSize) {
                this.info = info;
                this.controller = controller;
        }
        

        public override void LoadContent() {
            base.LoadContent();
            // resource icon
            int size = (int)Math.Min(BoundingBox.Width * .1f, BoundingBox.Height * .8f);
            icon = Game.Content.Load<Texture2D>(info.Icon);
            iconLocation = new Rectangle((int)(BoundingBox.Left + (BoundingBox.Width * .1f - size) / 2), (int)(BoundingBox.Top + (BoundingBox.Height - size) / 2),
                size, size);

            // amount bar
            bar = new Texture2D(FC.GraphicsDevice, 1, 1);
            Color[] data = new Color[1];
            data[0] = Color.White;
            bar.SetData<Color>(data);
            barLocation = new Rectangle((int)(BoundingBox.Left + BoundingBox.Width * .1f), (int)(BoundingBox.Top + BoundingBox.Height * .1f),
                (int)(BoundingBox.Width * .7f), (int)(BoundingBox.Height * .4f));

            font = FC.Content.Load<SpriteFont>("mono");
            // amount details
            Vector2 amountSize = font.MeasureString(controller.Resource(info.Name).Amount + "|" + controller.Resource(info.Name).Capacity);
            amountScale = Math.Min(BoundingBox.Width * .7f / amountSize.X, BoundingBox.Height * .6f / amountSize.Y);
            amountLocation = new Vector2(BoundingBox.Right - BoundingBox.Width * .25f - amountSize.X * amountScale, BoundingBox.Top + BoundingBox.Height * .4f);

            // change details
            Vector2 changeSize = font.MeasureString((controller.Resource(info.Name).Increases - controller.Resource(info.Name).Decreases).ToString("+#;-#;0"));
            changeScale = Math.Min(BoundingBox.Width * .2f / changeSize.X, BoundingBox.Height * .6f / changeSize.Y);
            changeLocation = new Vector2(BoundingBox.Right - changeSize.X * changeScale, BoundingBox.Top);

            // usage details
            Vector2 usageSize = font.MeasureString(controller.Resource(info.Name).Increases.ToString("+#;-#;0") + "|" + controller.Resource(info.Name).Decreases.ToString("+#;-#;0"));
            usageScale = Math.Min(BoundingBox.Width * .2f / usageSize.X, BoundingBox.Height * .6f / usageSize.Y);
            usageLocation = new Vector2(BoundingBox.Right - usageSize.X * usageScale, BoundingBox.Top + BoundingBox.Height * .4f);
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            ResourceCounter counter = controller.Resource(info.Name);
            int fullWidth = (int)(BoundingBox.Width * .7f);
            int width = (int)(fullWidth * counter.Amount / counter.Capacity);
            barLocation = new Rectangle((int)(BoundingBox.Left + BoundingBox.Width * .1f + fullWidth - width), (int)(BoundingBox.Top + BoundingBox.Height * .1f),
                (int)(width), (int)(BoundingBox.Height * .4f));

            // amount details
            Vector2 amountSize = font.MeasureString(controller.Resource(info.Name).Amount + "|" + controller.Resource(info.Name).Capacity);
            amountScale = Math.Min(BoundingBox.Width * .7f / amountSize.X, BoundingBox.Height * .6f / amountSize.Y);
            amountLocation = new Vector2(BoundingBox.Right - BoundingBox.Width * .25f - amountSize.X * amountScale, BoundingBox.Top + BoundingBox.Height * .4f);

            // change details
            Vector2 changeSize = font.MeasureString((controller.Resource(info.Name).Increases - controller.Resource(info.Name).Decreases).ToString("+#;-#;0"));
            changeScale = Math.Min(BoundingBox.Width * .2f / changeSize.X, BoundingBox.Height * .6f / changeSize.Y);
            changeLocation = new Vector2(BoundingBox.Right - changeSize.X * changeScale, BoundingBox.Top);

            // usage details
            Vector2 usageSize = font.MeasureString(controller.Resource(info.Name).Increases.ToString("+#;-#;0") + "|" + controller.Resource(info.Name).Decreases.ToString("+#;-#;0"));
            usageScale = Math.Min(BoundingBox.Width * .2f / usageSize.X, BoundingBox.Height * .6f / usageSize.Y);
            usageLocation = new Vector2(BoundingBox.Right - usageSize.X * usageScale, BoundingBox.Top + BoundingBox.Height * .4f);
        }

        public override void Draw(GameTime gameTime) {
            base.Draw(gameTime);
            SpriteBatch spriteBatch = FC.SpriteBatch;

            spriteBatch.Draw(icon, iconLocation, Color.White);

            Color color = Color.Green * .6f;
            if ((float)controller.Resource(info.Name).Amount / controller.Resource(info.Name).Capacity < .25f) {
                color = Color.Red * .6f;
            } else if ((float)controller.Resource(info.Name).Amount / controller.Resource(info.Name).Capacity < .75f) {
                color = Color.Yellow * .6f;
            }
            spriteBatch.Draw(bar, barLocation, color);

            spriteBatch.DrawString(font, controller.Resource(info.Name).Amount + "|" + controller.Resource(info.Name).Capacity, amountLocation, Color.White, 0, Vector2.Zero, amountScale, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, (controller.Resource(info.Name).Increases - controller.Resource(info.Name).Decreases).ToString("+#;-#;0"), changeLocation, Color.White, 0, Vector2.Zero, changeScale, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, controller.Resource(info.Name).Increases.ToString("+#;-#;0") + "|" + controller.Resource(info.Name).Decreases.ToString("+#;-#;0"), usageLocation, Color.White, 0, Vector2.Zero, usageScale, SpriteEffects.None, 0);
        }
    }
}
