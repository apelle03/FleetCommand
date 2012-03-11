using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fleet_Command.Game {
    public class Unit : DGC {
        protected static string spriteSource = "ship";
        protected Vector2 pos;
        protected Vector2 center;
        protected float angle;

        protected Texture2D sprite;

        public Unit(FC game, Vector2 pos, float angle)
            : base(game) {
                this.pos = pos;
                this.angle = angle;
        }

        public override void LoadContent() {
            base.LoadContent();
            sprite = FC.Content.Load<Texture2D>("Units/" + spriteSource);
            base.boundingBox = new Rectangle((int)pos.X - sprite.Bounds.Center.X, (int)pos.Y - sprite.Bounds.Center.Y,
                (int)pos.X + sprite.Bounds.Center.X, (int)pos.Y +sprite.Bounds.Center.Y);
            center = new Vector2(sprite.Bounds.Center.X, sprite.Bounds.Center.Y);
        }

        public override void Draw(GameTime gameTime) {
            base.Draw(gameTime);
            SpriteBatch spriteBatch = FC.SpriteBatch;
            spriteBatch.Draw(sprite, pos, null, Color.White, angle, center, 1, SpriteEffects.None, 1);
        }
    }
}
