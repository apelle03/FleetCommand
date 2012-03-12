using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fleet_Command.Decorators {
    public class CorneredBorder : Decorator {
        protected Texture2D outlineCorner, outlineSide, outlineTop;
        protected Rectangle topLeft, topRight, bottomLeft, bottomRight, left, right, top, bottom;

        protected string borderName;

        public CorneredBorder(DGC item, string borderName) {
            this.item = item;
            this.borderName = borderName;
        }

        public override void LoadContent() {
            outlineCorner = item.FC.Content.Load<Texture2D>("Decorations/Borders/" + borderName + "/outline_corner");
            outlineSide = item.FC.Content.Load<Texture2D>("Decorations/Borders/" + borderName + "/outline_side");
            outlineTop = item.FC.Content.Load<Texture2D>("Decorations/Borders/" + borderName + "/outline_top");

            float scale = Math.Min((float)item.BoundingBox.Width / (2 * outlineCorner.Width), (float)item.BoundingBox.Height / (2 * outlineCorner.Height));
            if (scale > 1) scale = 1;
            int size = (int)(outlineCorner.Bounds.Width * scale);
            topLeft = new Rectangle(item.BoundingBox.Left, item.BoundingBox.Top, size, size);
            topRight = new Rectangle(item.BoundingBox.Right - size, item.BoundingBox.Y, size, size);
            bottomLeft = new Rectangle(item.BoundingBox.Left, item.BoundingBox.Bottom - size, size, size);
            bottomRight = new Rectangle(item.BoundingBox.Right - size, item.BoundingBox.Bottom - size, size, size);

            left = new Rectangle(item.BoundingBox.Left, item.BoundingBox.Top + size, size, item.BoundingBox.Height - 2 * size);
            right = new Rectangle(item.BoundingBox.Right - size, item.BoundingBox.Top + size, size, item.BoundingBox.Height - 2 * size);
            top = new Rectangle(item.BoundingBox.Left + size, item.BoundingBox.Top, item.BoundingBox.Width - 2 * size, size);
            bottom = new Rectangle(item.BoundingBox.Left + size, item.BoundingBox.Bottom - size, item.BoundingBox.Width - 2 * size, size);
        }

        public override void Update() {
            float scale = Math.Min((float)item.BoundingBox.Width / (2 * outlineCorner.Width), (float)item.BoundingBox.Height / (2 * outlineCorner.Height));
            if (scale > 1) scale = 1;
            int size = (int)(outlineCorner.Bounds.Width * scale);
            topLeft = new Rectangle(item.BoundingBox.Left, item.BoundingBox.Top, size, size);
            topRight = new Rectangle(item.BoundingBox.Right - size, item.BoundingBox.Y, size, size);
            bottomLeft = new Rectangle(item.BoundingBox.Left, item.BoundingBox.Bottom - size, size, size);
            bottomRight = new Rectangle(item.BoundingBox.Right - size, item.BoundingBox.Bottom - size, size, size);

            left = new Rectangle(item.BoundingBox.Left, item.BoundingBox.Top + size, size, item.BoundingBox.Height - 2 * size);
            right = new Rectangle(item.BoundingBox.Right - size, item.BoundingBox.Top + size, size, item.BoundingBox.Height - 2 * size);
            top = new Rectangle(item.BoundingBox.Left + size, item.BoundingBox.Top, item.BoundingBox.Width - 2 * size, size);
            bottom = new Rectangle(item.BoundingBox.Left + size, item.BoundingBox.Bottom - size, item.BoundingBox.Width - 2 * size, size);
        }

        public override void Draw() {
            SpriteBatch spriteBatch = item.FC.SpriteBatch;

            spriteBatch.Draw(outlineCorner, topLeft, outlineCorner.Bounds, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
            spriteBatch.Draw(outlineCorner, bottomLeft, outlineCorner.Bounds, Color.White, 0, Vector2.Zero, SpriteEffects.FlipVertically, 0);
            spriteBatch.Draw(outlineCorner, bottomRight, outlineCorner.Bounds, Color.White, 0, Vector2.Zero, SpriteEffects.FlipVertically | SpriteEffects.FlipHorizontally, 0);
            spriteBatch.Draw(outlineCorner, topRight, outlineCorner.Bounds, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);

            spriteBatch.Draw(outlineSide, left, outlineSide.Bounds, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
            spriteBatch.Draw(outlineSide, right, outlineSide.Bounds, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
            spriteBatch.Draw(outlineTop, top, outlineTop.Bounds, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
            spriteBatch.Draw(outlineTop, bottom, outlineTop.Bounds, Color.White, 0, Vector2.Zero, SpriteEffects.FlipVertically, 0);
        }
    }
}
