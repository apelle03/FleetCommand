using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Fleet_Command.Game;

namespace Fleet_Command.Menus {
    public class MenuComponent : RelativeSizeComponent<MenuComponent> {

        protected string text;
        protected SpriteFont font;
        protected Vector2 textLocation;
        protected float textScale;
        protected Color textColor;

        public string Text { get; set; }

        public MenuComponent(FC game, Vector2 relPos, Vector2 relSize, string text)
            : this(game, relPos, relSize, text, Color.Red) {
        }

        public MenuComponent(FC game, Vector2 relPos, Vector2 relSize, string text, Color textColor)
            : base(game, relPos, relSize) {
            this.text = text;
            this.textColor = textColor;
        }

        public override void Initialize() {
            base.Initialize();
            FC.InputManager.Register(Input.Actions.Click);
            FC.InputManager.Save();
        }

        public override void LoadContent() {
            base.LoadContent();
            font = FC.Content.Load<SpriteFont>("battlestar");
            Vector2 textSize = font.MeasureString(text);
            textScale = Math.Min(BoundingBox.Width * 0.9f / textSize.X, BoundingBox.Height * 0.9f / textSize.Y);
            textLocation = new Vector2(BoundingBox.X + BoundingBox.Width / 2 - textSize.X / 2 * textScale,
                BoundingBox.Y + BoundingBox.Height / 2 - textSize.Y / 2 * textScale);
        }

        public override void Draw(GameTime gameTime) {
            base.Draw(gameTime);
            SpriteBatch spriteBatch = (SpriteBatch)FC.Services.GetService(typeof(SpriteBatch));
            spriteBatch.DrawString(font, text, textLocation, textColor, 0, Vector2.Zero, textScale, SpriteEffects.None, 0);
        }
    }
}
