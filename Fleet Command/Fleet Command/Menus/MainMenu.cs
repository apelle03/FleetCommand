using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fleet_Command.Menus {
    public class MainMenu : Menu<MenuComponent> {
        public MainMenu(FC game)
            : this(game, Vector2.Zero, Vector2.Zero, null, Color.White) {
        }

        public MainMenu(FC game, Vector2 relPos, Vector2 relSize)
            : this(game, relPos, relSize, null, Color.White) {
        }

        public MainMenu(FC game, Vector2 relPos, Vector2 relSize, Texture2D bckgrnd)
            : this(game, relPos, relSize, bckgrnd, Color.White) {
        }

        public MainMenu(FC game, Vector2 relPos, Vector2 relSize, Color color)
            : this(game, relPos, relSize, null, color) {
        }

        public MainMenu(FC game, Vector2 relPos, Vector2 relSize, Texture2D bckgrnd, Color color)
            : base(game, relPos, relSize, bckgrnd, color) {
            Components.Add(new MenuComponent(game, new Vector2(.1f, .1f), new Vector2(.3f, .2f), Color.White));
        }

        public override void Initialize() {
            base.Initialize();
            FC.InputManager.Register(Input.Actions.Click);
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            Console.WriteLine(FC.InputManager.CheckAction(Input.Actions.Click, this));
        }
    }
}
