using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fleet_Command.Decorators {
    public class Fill : Decorator {
        protected Texture2D fill;
        protected Rectangle middle;
        protected Color color;

        protected string fillName;

        public Fill(DGC item, string fillName)
            : this(item, fillName, Color.White) {
        }

        public Fill(DGC item, string fillName, Color color) {
            this.item = item;
            this.fillName = fillName;
            this.color = color;
        }

        public override void LoadContent() {
            fill = item.FC.Content.Load<Texture2D>("Decorations/Fills/" + fillName + "/fill");
            middle = new Rectangle(item.BoundingBox.Left, item.BoundingBox.Top, item.BoundingBox.Width, item.BoundingBox.Height);
        }

        public override void Update() {
            middle = new Rectangle(item.BoundingBox.Left, item.BoundingBox.Top, item.BoundingBox.Width, item.BoundingBox.Height);
        }

        public override void Draw() {
            SpriteBatch spriteBatch = item.FC.SpriteBatch;
            spriteBatch.Draw(fill, middle, fill.Bounds, color, 0, Vector2.Zero, SpriteEffects.None, 0);
        }
    }
}
