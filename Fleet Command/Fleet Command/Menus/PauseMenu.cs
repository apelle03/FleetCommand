using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Fleet_Command.Decorators;

namespace Fleet_Command.Menus {
    public class PauseMenu : Menu<MenuComponent> {
        protected CorneredBorder border;
        protected CorneredFill fill;

        public PauseMenu(FC game)
            : this(game, Vector2.Zero, Vector2.Zero, null, Color.Transparent) {
        }

        public PauseMenu(FC game, Vector2 relPos, Vector2 relSize)
            : this(game, relPos, relSize, null, Color.Transparent) {
        }

        public PauseMenu(FC game, Vector2 relPos, Vector2 relSize, string bckgrnd)
            : this(game, relPos, relSize, bckgrnd, Color.Transparent) {
        }

        public PauseMenu(FC game, Vector2 relPos, Vector2 relSize, Color color)
            : this(game, relPos, relSize, null, color) {
        }

        public PauseMenu(FC game, Vector2 relPos, Vector2 relSize, string bckgrnd, Color color)
            : base(game, relPos, relSize, bckgrnd, Color.Transparent) {
            Components.Add(new Button(game,
                new Vector2(relPos.X + relSize.X * .1f, relPos.Y + relSize.Y * .1f),
                new Vector2(relSize.X * .8f, relSize.Y * .15f), "Resume", Resume));
            Components.Add(new Button(game,
                new Vector2(relPos.X + relSize.X * .1f, relPos.Y + relSize.Y * .31f),
                new Vector2(relSize.X * .8f, relSize.Y * .15f), "Options", Options));
            Components.Add(new Button(game,
                new Vector2(relPos.X + relSize.X * .1f, relPos.Y + relSize.Y * .52f),
                new Vector2(relSize.X * .8f, relSize.Y * .15f), "Main Menu", MainMenu));
            Components.Add(new Button(game,
                new Vector2(relPos.X + relSize.X * .1f, relPos.Y + relSize.Y * .73f),
                new Vector2(relSize.X * .8f, relSize.Y * .15f), "Quit", Quit));
            border = new CorneredBorder(this, "Galactica");
            fill = new CorneredFill(this, "Galactica_White", color);
        }

        public override void LoadContent() {
            base.LoadContent();
            border.LoadContent();
            fill.LoadContent();
        }

        public override void Draw(GameTime gameTime) {
            fill.Draw();
            base.Draw(gameTime);
            border.Draw();
        }

        private void Resume() {
            FC.Resume();
        }
        private void Options() {
            FC.Options();
        }
        private void MainMenu() {
            FC.MainMenu();
        }
        private void Quit() {
            FC.Quit();
        }
    }
}
