using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fleet_Command.Decorators {
    public class Border : Decorator {
        protected Texture2D outlineSide, outlineTop;
        protected Rectangle left, right, top, bottom;

        protected string borderName;

        public Border(DGC item, string borderName) {
            this.item = item;
            this.borderName = borderName;
        }

        public override void LoadContent() {
            outlineSide = item.FC.Content.Load<Texture2D>("Decorations/Borders/" + borderName + "/outline_side");
            outlineTop = item.FC.Content.Load<Texture2D>("Decorations/Borders/" + borderName + "/outline_top");

            left = new Rectangle(item.BoundingBox.Left, item.BoundingBox.Top, outlineSide.Width, item.BoundingBox.Height);
            right = new Rectangle(item.BoundingBox.Right - outlineSide.Width, item.BoundingBox.Top, outlineSide.Width, item.BoundingBox.Height);
            top = new Rectangle(item.BoundingBox.Left, item.BoundingBox.Top, item.BoundingBox.Width, outlineTop.Height);
            bottom = new Rectangle(item.BoundingBox.Left, item.BoundingBox.Bottom - outlineTop.Height, item.BoundingBox.Width, outlineTop.Height);
        }

        public override void Update() {
            left = new Rectangle(item.BoundingBox.Left, item.BoundingBox.Top, outlineSide.Width, item.BoundingBox.Height);
            right = new Rectangle(item.BoundingBox.Right - outlineSide.Width, item.BoundingBox.Top, outlineSide.Width, item.BoundingBox.Height);
            top = new Rectangle(item.BoundingBox.Left, item.BoundingBox.Top, item.BoundingBox.Width, outlineTop.Height);
            bottom = new Rectangle(item.BoundingBox.Left, item.BoundingBox.Bottom - outlineTop.Height, item.BoundingBox.Width, outlineTop.Height);
        }

        public override void Draw() {
            SpriteBatch spriteBatch = item.FC.SpriteBatch;

            spriteBatch.Draw(outlineSide, left, outlineSide.Bounds, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
            spriteBatch.Draw(outlineSide, right, outlineSide.Bounds, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
            spriteBatch.Draw(outlineTop, top, outlineTop.Bounds, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
            spriteBatch.Draw(outlineTop, bottom, outlineTop.Bounds, Color.White, 0, Vector2.Zero, SpriteEffects.FlipVertically, 0);
        }
    }
}
