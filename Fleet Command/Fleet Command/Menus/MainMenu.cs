using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fleet_Command.Menus {
    public class MainMenu : Menu<MenuComponent> {
        public MainMenu(FC game)
            : this(game, Vector2.Zero, Vector2.Zero, null, Color.Transparent) {
        }

        public MainMenu(FC game, Vector2 relPos, Vector2 relSize)
            : this(game, relPos, relSize, null, Color.Transparent) {
        }

        public MainMenu(FC game, Vector2 relPos, Vector2 relSize, string bckgrnd)
            : this(game, relPos, relSize, bckgrnd, Color.Transparent) {
        }

        public MainMenu(FC game, Vector2 relPos, Vector2 relSize, Color color)
            : this(game, relPos, relSize, null, color) {
        }

        public MainMenu(FC game, Vector2 relPos, Vector2 relSize, string bckgrnd, Color color)
            : base(game, relPos, relSize, bckgrnd, color) {
            Components.Add(new Button(game, new Vector2(.25f, .5f), new Vector2(.5f, .1f), "Start Game"));
            Components.Add(new Button(game, new Vector2(.25f, .65f), new Vector2(.5f, .1f), "Options"));
            Components.Add(new Button(game, new Vector2(.25f, .8f), new Vector2(.5f, .1f), "Quit"));
        }
    }
}
