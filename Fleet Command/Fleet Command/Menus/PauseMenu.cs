using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Fleet_Command.Decorators;

namespace Fleet_Command.Menus {
    public class PauseMenu : Menu<MenuComponent> {
        protected Border border;

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
            : base(game, relPos, relSize, bckgrnd, color) {
            Components.Add(new Button(game,
                new Vector2(relPos.X + relSize.X * .1f, relPos.Y + relSize.Y * .2f),
                new Vector2(relSize.X * .8f, relSize.Y * .15f), "Resume", Resume));
            Components.Add(new Button(game,
                new Vector2(relPos.X + relSize.X * .1f, relPos.Y + relSize.Y * .375f),
                new Vector2(relSize.X * .8f, relSize.Y * .15f), "Options", Options));
            Components.Add(new Button(game,
                new Vector2(relPos.X + relSize.X * .1f, relPos.Y + relSize.Y * .575f),
                new Vector2(relSize.X * .8f, relSize.Y * .15f), "Exit to Main Menu", MainMenu));
            Components.Add(new Button(game,
                new Vector2(relPos.X + relSize.X * .1f, relPos.Y + relSize.Y * .75f),
                new Vector2(relSize.X * .8f, relSize.Y * .15f), "Quit", Quit));
            border = new Border(this, "Basic");
        }

        public override void LoadContent() {
            base.LoadContent();
            border.LoadContent();
        }

        public override void Draw(GameTime gameTime) {
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
