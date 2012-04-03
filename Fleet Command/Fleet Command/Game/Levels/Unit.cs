using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Fleet_Command.Decorators;

namespace Fleet_Command.Game.Levels {
    public class Unit : DGC {
        protected static string spriteSource = "Basestar";
        protected Vector2 pos;
        protected Vector2 center;
        protected float angle;

        protected Texture2D sprite;

        protected CircleBorder selectionBorder;
        public bool Selected { get; set; }

        public Unit(FC game, Vector2 pos, float angle)
            : base(game) {
                this.pos = pos;
                this.angle = angle;
                selectionBorder = new CircleBorder(this, "Unit");
        }

        public override void LoadContent() {
            base.LoadContent();
            sprite = FC.Content.Load<Texture2D>("Units/" + spriteSource);
            boundingBox.X = (int)pos.X - sprite.Bounds.Center.X;
            boundingBox.Y = (int)pos.Y - sprite.Bounds.Center.Y;
            boundingBox.Width = sprite.Bounds.Width;
            boundingBox.Height = sprite.Bounds.Height;
            center = new Vector2(sprite.Bounds.Center.X, sprite.Bounds.Center.Y);
            selectionBorder.LoadContent();
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            boundingBox.X = (int)pos.X - sprite.Bounds.Center.X;
            boundingBox.Y = (int)pos.Y - sprite.Bounds.Center.Y;
            boundingBox.Width = sprite.Bounds.Width;
            boundingBox.Height = sprite.Bounds.Height;
            selectionBorder.Update();
        }

        public override void Draw(GameTime gameTime) {
            base.Draw(gameTime);
            if (Selected) {
                selectionBorder.Draw();
            }
            SpriteBatch spriteBatch = FC.SpriteBatch;
            spriteBatch.Draw(sprite, pos, null, Color.White, angle, center, 1, SpriteEffects.None, 1);
        }
    }
}
