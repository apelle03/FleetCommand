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

        public override void LoadContent() {
            base.LoadContent();
            font = FC.Content.Load<SpriteFont>("battlestar");
            Vector2 textSize = font.MeasureString(text);
            textLocation = new Vector2(BoundingBox.X + BoundingBox.Width / 2 - textSize.X / 2,
                BoundingBox.Y + BoundingBox.Height / 2 - textSize.Y / 2 * 0.8f);
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

        }

        public override void BeforeDraw(GameTime gameTime) {
            base.BeforeDraw(gameTime);
            SpriteBatch spriteBatch = FC.SpriteBatch;
            spriteBatch.DrawString(font, text, textLocation, textColor);
            spriteBatch.DrawString(font, text, textLocation + Vector2.One, Color.BurlyWood);
        }
    }
}
