using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fleet_Command.Game {
    public class RelativeSizeComponent<T> : DGCDelegator<T> where T : DGC {
        protected Vector2 relativePos, relativeSize;
        protected string backgroundSource;
        protected Texture2D background;
        protected Color color;

        public RelativeSizeComponent(FC game)
            : this(game, Vector2.Zero, Vector2.Zero, null, Color.Transparent) {
        }

        public RelativeSizeComponent(FC game, Vector2 relPos, Vector2 relSize)
            : this(game, relPos, relSize, null, Color.Transparent) {
        }

        public RelativeSizeComponent(FC game, Vector2 relPos, Vector2 relSize, string bckgrnd)
            : this(game, relPos, relSize, bckgrnd, Color.Transparent) {
        }

        public RelativeSizeComponent(FC game, Vector2 relPos, Vector2 relSize, Color color)
            : this(game, relPos, relSize, null, color) {
        }

        public RelativeSizeComponent(FC game, Vector2 relPos, Vector2 relSize, string bckgrnd, Color color)
            : base(game) {
            relativePos = relPos;
            relativeSize = relSize;
            backgroundSource = bckgrnd;
            this.color = color;
        }

        public override void LoadContent() {
            base.LoadContent();
            int height = FC.GraphicsDevice.PresentationParameters.BackBufferHeight;
            int width = FC.GraphicsDevice.PresentationParameters.BackBufferWidth;

            boundingBox = new Rectangle((int)(width * relativePos.X), (int)(height * relativePos.Y), (int)(width * relativeSize.X), (int)(height * relativeSize.Y));

            if (backgroundSource == null) {
                background = new Texture2D(GraphicsDevice, 1, 1);
                background.SetData(new Color[] { Color.White });
            } else {
                background = FC.Content.Load<Texture2D>(backgroundSource);
            }
        }

        public override void BeforeDraw(GameTime gameTime) {
            base.BeforeDraw(gameTime);
            SpriteBatch spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            spriteBatch.Draw(background, boundingBox, color);
        }
    }
}
