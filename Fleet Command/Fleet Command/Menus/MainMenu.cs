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
            Components.Add(new MenuComponent(game, new Vector2(.1f, .1f), new Vector2(.3f, .1f), "Start Game"));
            Components.Add(new MenuComponent(game, new Vector2(.1f, .25f), new Vector2(.3f, .1f), "Options"));
            Components.Add(new MenuComponent(game, new Vector2(.1f, .4f), new Vector2(.3f, .1f), "Quit"));
        }
    }
}
