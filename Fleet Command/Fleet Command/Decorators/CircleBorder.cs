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
        }

        public override void LoadContent() {
            outline = item.FC.Content.Load<Texture2D>("Decorations/Borders/" + borderName + "/outline");
            location = item.BoundingBox;
        }

        public override void Update() {
            location = item.BoundingBox;
        }

        public override void Draw() {
            SpriteBatch spriteBatch = item.FC.SpriteBatch;

            spriteBatch.Draw(outline, location, outline.Bounds, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
        }
    }
}
