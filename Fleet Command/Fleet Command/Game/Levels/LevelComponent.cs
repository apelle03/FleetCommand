using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fleet_Command.Game.Levels {
    public class LevelComponent : DGCDelegator<LevelComponent> {

        protected Texture2D sprite;
        protected string spriteSource;
        protected Vector2 position;
        protected float angle;

        public LevelComponent(FC game)
            : this(game, null, Vector2.Zero, 0) {
        }

        public LevelComponent(FC game, string source, Vector2 pos, float ang)
            : base(game) {
                this.spriteSource = source;
                position = pos;
                angle = ang;
        }

        public override void LoadContent() {
            base.LoadContent();
            if (spriteSource == null) {
                sprite = new Texture2D(GraphicsDevice, 1, 1);
                sprite.SetData(new Color[] { Color.White });
            } else {
                sprite = FC.Content.Load<Texture2D>(spriteSource);
            }
            boundingBox = new Rectangle((int)(position.X - sprite.Bounds.Width / 2), (int)(position.Y - sprite.Bounds.Height / 2),
                sprite.Bounds.Width, sprite.Bounds.Height);
        }

        public override void BeforeDraw(GameTime gameTime) {
            base.BeforeDraw(gameTime);
            SpriteBatch spriteBatch = FC.SpriteBatch;
            spriteBatch.Draw(sprite, BoundingBox, Color.White);
        }
    }
}
