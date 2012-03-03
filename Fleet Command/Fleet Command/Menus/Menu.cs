using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Fleet_Command.Game;

namespace Fleet_Command.Menus {
    public class Menu<T> : RelativeSizeComponent<T> where T : DGC {
        public Menu(FC game)
            : this(game, Vector2.Zero, Vector2.Zero, null, Color.Transparent) {
        }

        public Menu(FC game, Vector2 relPos, Vector2 relSize)
            : this(game, relPos, relSize, null, Color.Transparent) {
        }

        public Menu(FC game, Vector2 relPos, Vector2 relSize, string bckgrnd)
            : this(game, relPos, relSize, bckgrnd, Color.Transparent) {
        }

        public Menu(FC game, Vector2 relPos, Vector2 relSize, Color color)
            : this(game, relPos, relSize, null, color) {
        }

        public Menu(FC game, Vector2 relPos, Vector2 relSize, string bckgrnd, Color color)
            : base(game, relPos, relSize, bckgrnd, color) {
        }
    }
}
