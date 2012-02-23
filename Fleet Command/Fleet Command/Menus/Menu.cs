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
            : this(game, Vector2.Zero, Vector2.Zero, null, Color.White) {
        }

        public Menu(FC game, Vector2 relPos, Vector2 relSize)
            : this(game, relPos, relSize, null, Color.White) {
        }

        public Menu(FC game, Vector2 relPos, Vector2 relSize, Texture2D bckgrnd)
            : this(game, relPos, relSize, bckgrnd, Color.White) {
        }

        public Menu(FC game, Vector2 relPos, Vector2 relSize, Color color)
            : this(game, relPos, relSize, null, color) {
        }

        public Menu(FC game, Vector2 relPos, Vector2 relSize, Texture2D bckgrnd, Color color)
            : base(game, relPos, relSize, bckgrnd, color) {
        }
    }
}
