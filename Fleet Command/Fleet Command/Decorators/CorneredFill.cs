using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fleet_Command.Decorators {
    public class CorneredFill : Decorator {
        protected Texture2D fill, fillCorner, fillSide, fillTop;
        protected Rectangle topLeft, topRight, bottomLeft, bottomRight;
        protected Rectangle topMiddle, bottomMiddle, leftMiddle, rightMiddle, middle;
        protected Color color;

        protected string fillName;

        public CorneredFill(DGC item, string fillName)
            : this(item, fillName, Color.White) {
        }

        public CorneredFill(DGC item, string fillName, Color color) {
            this.item = item;
            this.fillName = fillName;
            this.color = color;
        }

        public override void LoadContent() {
            fill = item.FC.Content.Load<Texture2D>("Decorations/Fills/" + fillName + "/fill");
            fillCorner = item.FC.Content.Load<Texture2D>("Decorations/Fills/" + fillName + "/fill_corner");
            fillSide = item.FC.Content.Load<Texture2D>("Decorations/Fills/" + fillName + "/fill_side");
            fillTop = item.FC.Content.Load<Texture2D>("Decorations/Fills/" + fillName + "/fill_top");

            float scale = Math.Min((float)item.BoundingBox.Width / (2 * fillCorner.Width), (float)item.BoundingBox.Height / (2 * fillCorner.Height));
            if (scale > 1) scale = 1;
            int size = (int)(fillCorner.Bounds.Width * scale);
            topLeft = new Rectangle(item.BoundingBox.Left, item.BoundingBox.Top, size, size);
            topRight = new Rectangle(item.BoundingBox.Right - size, item.BoundingBox.Y, size, size);
            bottomLeft = new Rectangle(item.BoundingBox.Left, item.BoundingBox.Bottom - size, size, size);
            bottomRight = new Rectangle(item.BoundingBox.Right - size, item.BoundingBox.Bottom - size, size, size);

            topMiddle = new Rectangle(item.BoundingBox.Left + size, item.BoundingBox.Top, item.BoundingBox.Width - 2 * size, size);
            bottomMiddle = new Rectangle(item.BoundingBox.Left + size, item.BoundingBox.Bottom - size, item.BoundingBox.Width - 2 * size, size);
            leftMiddle = new Rectangle(item.BoundingBox.Left, item.BoundingBox.Top + size, size, item.BoundingBox.Height - 2 * size);
            rightMiddle = new Rectangle(item.BoundingBox.Right - size, item.BoundingBox.Top + size, size, item.BoundingBox.Height - 2 * size);
            middle = new Rectangle(item.BoundingBox.Left + size, item.BoundingBox.Top + size, item.BoundingBox.Width - 2 * size, item.BoundingBox.Height - 2 * size);
        }

        public override void Update() {
            float scale = Math.Min((float)item.BoundingBox.Width / (2 * fillCorner.Width), (float)item.BoundingBox.Height / (2 * fillCorner.Height));
            if (scale > 1) scale = 1;
            int size = (int)(fillCorner.Bounds.Width * scale);
            topLeft = new Rectangle(item.BoundingBox.Left, item.BoundingBox.Top, size, size);
            topRight = new Rectangle(item.BoundingBox.Right - size, item.BoundingBox.Y, size, size);
            bottomLeft = new Rectangle(item.BoundingBox.Left, item.BoundingBox.Bottom - size, size, size);
            bottomRight = new Rectangle(item.BoundingBox.Right - size, item.BoundingBox.Bottom - size, size, size);

            topMiddle = new Rectangle(item.BoundingBox.Left + size, item.BoundingBox.Top, item.BoundingBox.Width - 2 * size, size);
            bottomMiddle = new Rectangle(item.BoundingBox.Left + size, item.BoundingBox.Bottom - size, item.BoundingBox.Width - 2 * size, size);
            leftMiddle = new Rectangle(item.BoundingBox.Left, item.BoundingBox.Top + size, size, item.BoundingBox.Height - 2 * size);
            rightMiddle = new Rectangle(item.BoundingBox.Right - size, item.BoundingBox.Top + size, size, item.BoundingBox.Height - 2 * size);
            middle = new Rectangle(item.BoundingBox.Left + size, item.BoundingBox.Top + size, item.BoundingBox.Width - 2 * size, item.BoundingBox.Height - 2 * size);
        }

        public override void Draw() {
            SpriteBatch spriteBatch = item.FC.SpriteBatch;

            spriteBatch.Draw(fillCorner, topLeft, fillCorner.Bounds, color, 0, Vector2.Zero, SpriteEffects.None, 0);
            spriteBatch.Draw(fillCorner, bottomLeft, fillCorner.Bounds, color, 0, Vector2.Zero, SpriteEffects.FlipVertically, 0);
            spriteBatch.Draw(fillCorner, bottomRight, fillCorner.Bounds, color, 0, Vector2.Zero, SpriteEffects.FlipVertically | SpriteEffects.FlipHorizontally, 0);
            spriteBatch.Draw(fillCorner, topRight, fillCorner.Bounds, color, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);

            spriteBatch.Draw(fillTop, topMiddle, fillTop.Bounds, color, 0, Vector2.Zero, SpriteEffects.None, 0);
            spriteBatch.Draw(fillTop, bottomMiddle, fillTop.Bounds, color, 0, Vector2.Zero, SpriteEffects.FlipVertically, 0);
            spriteBatch.Draw(fillSide, leftMiddle, fillSide.Bounds, color, 0, Vector2.Zero, SpriteEffects.None, 0);
            spriteBatch.Draw(fillSide, rightMiddle, fillSide.Bounds, color, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
            spriteBatch.Draw(fill, middle, fill.Bounds, color, 0, Vector2.Zero, SpriteEffects.None, 0);
        }
    }
}
