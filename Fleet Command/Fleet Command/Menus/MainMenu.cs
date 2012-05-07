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
                MenuComponent title = new MenuComponent(game,
                    new Vector2(relPos.X + relSize.X * .1f, relPos.Y),
                    new Vector2(relSize.X * .8f, relSize.Y * .5f), "Fleet Command");
                title.ScaleText = true;
                Components.Add(title);
                Components.Add(new Button(game,
                    new Vector2(relPos.X + relSize.X * .25f, relPos.Y + relSize.Y * .5f),
                    new Vector2(relSize.X * .5f, relSize.Y * .1f), "Start Game", StartGame));
                Components.Add(new Button(game,
                    new Vector2(relPos.X + relSize.X * .25f, relPos.Y + relSize.Y * .65f),
                    new Vector2(relSize.X * .5f, relSize.Y * .1f), "Options", Options));
                Components.Add(new Button(game,
                    new Vector2(relPos.X + relSize.X * .25f, relPos.Y + relSize.Y * .8f),
                    new Vector2(relSize.X * .5f, relSize.Y * .1f), "Quit", Quit));
        }

        private void StartGame() {
            FC.StartGame();
        }
        private void Options() {
            FC.Options();
        }
        private void Quit() {
            FC.Quit();
        }
    }
}
