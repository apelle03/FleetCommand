using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Fleet_Command.Game;

namespace Fleet_Command.Menus {
    public class MenuComponent : RelativeSizeComponent<MenuComponent> {
        public MenuComponent(FC game)
            : this(game, Vector2.Zero, Vector2.Zero, null, Color.White) {
        }

        public MenuComponent(FC game, Vector2 relPos, Vector2 relSize)
            : this(game, relPos, relSize, null, Color.White) {
        }

        public MenuComponent(FC game, Vector2 relPos, Vector2 relSize, Texture2D bckgrnd)
            : this(game, relPos, relSize, bckgrnd, Color.White) {
        }

        public MenuComponent(FC game, Vector2 relPos, Vector2 relSize, Color color)
            : this(game, relPos, relSize, null, color) {
        }

        public MenuComponent(FC game, Vector2 relPos, Vector2 relSize, Texture2D bckgrnd, Color color)
            : base(game, relPos, relSize, bckgrnd, color) {
        }

        public override void Initialize() {
            base.Initialize();
            FC.InputManager.Register(Input.Actions.Click);
        }
    }
}
