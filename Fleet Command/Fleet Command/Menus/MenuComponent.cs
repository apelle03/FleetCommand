using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fleet_Command.Menus {
    public class MenuComponent : DGC {

        private Texture2D texture;
        private Color color;

        public MenuComponent(FC game)
            : base(game) {
                boundingBox = new Rectangle(20, 20, 100, 200);
                color = Color.Black;
        }

        public override void Initialize() {
            base.Initialize();
            FC.InputManager.Register(Input.Actions.Click);
        }

        public override void LoadContent() {
            base.LoadContent();
            texture = new Texture2D(GraphicsDevice, 1, 1);
            texture.SetData(new Color[] { Color.White });
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            Console.WriteLine(FC.InputManager.CheckAction(Input.Actions.Click, this));
        }

        public override void Draw(GameTime gameTime) {
            base.Draw(gameTime);
            SpriteBatch spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            spriteBatch.Draw(texture, boundingBox, color);
        }
    }
}
