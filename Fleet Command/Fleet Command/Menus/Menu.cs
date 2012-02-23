using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fleet_Command.Menus {
    public class Menu : DGCDelegator<MenuComponent> {

        private Rectangle area;
        private Texture2D texture;
        private Color color;

        public Menu(FC game)
            : base(game) {
                area = new Rectangle(10, 10, 500, 500);
                color = Color.WhiteSmoke;
        }

        public override void LoadContent() {
            base.LoadContent();
            texture = new Texture2D(GraphicsDevice, 1, 1);
            texture.SetData(new Color[] { Color.White });
        }

        public override void Draw(GameTime gameTime) {
            base.Draw(gameTime);
        }

        public override void BeforeDraw(GameTime gameTime) {
            base.BeforeDraw(gameTime);
            SpriteBatch spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            spriteBatch.Draw(texture, area, color);
        }
    }
}
