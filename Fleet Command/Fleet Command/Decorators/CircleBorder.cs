using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fleet_Command.Decorators {
    public class CircleBorder : Decorator {
        protected Texture2D outline;
        protected Rectangle location;

        protected string borderName;

        public CircleBorder(DGC item, string borderName) {
            this.item = item;
            this.borderName = borderName;
            this.location = new Rectangle();
        }

        public override void LoadContent() {
            outline = item.FC.Content.Load<Texture2D>("Decorations/Borders/" + borderName + "/outline");
            int fromCenter = Math.Min(item.BoundingBox.Width, item.BoundingBox.Height) / 2;
            location.X = item.BoundingBox.Center.X - fromCenter;
            location.Y = item.BoundingBox.Center.Y - fromCenter;
            location.Width = fromCenter * 2;
            location.Height = fromCenter * 2;
        }

        public override void Update() {
            int fromCenter = Math.Min(item.BoundingBox.Width, item.BoundingBox.Height) / 2;
            location.X = item.BoundingBox.Center.X - fromCenter;
            location.Y = item.BoundingBox.Center.Y - fromCenter;
        }

        public override void Draw() {
            SpriteBatch spriteBatch = item.FC.SpriteBatch;

            spriteBatch.Draw(outline, location, outline.Bounds, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
        }
    }
}
