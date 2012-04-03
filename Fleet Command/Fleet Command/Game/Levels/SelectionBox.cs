using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Fleet_Command.Decorators;

namespace Fleet_Command.Game.Levels {
    public class SelectionBox : DGC {
        protected Border border;
        protected Fill fill;
        protected Point start, end;
        protected bool active;
        public bool Active { get { return active; } set { active = value; } }

        public SelectionBox(FC game)
            : base(game) {
                border = new Border(this, "Selection");
                fill = new Fill(this, "Selection", Color.Green * .25f);
        }

        public override void LoadContent() {
            base.LoadContent();
            border.LoadContent();
            fill.LoadContent();
        }

        public void SetStart(int x, int y) {
            start.X = x;
            start.Y = y;
            end.X = x;
            end.Y = y;
            active = true;
        }

        public void SetCorner(int x, int y) {
            end.X = x;
            end.Y = y;
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            boundingBox.X = Math.Min(start.X, end.X);
            boundingBox.Y = Math.Min(start.Y, end.Y);
            boundingBox.Width = Math.Abs(start.X - end.X);
            boundingBox.Height = Math.Abs(start.Y - end.Y);
            border.Update();
            fill.Update();
        }

        public override void Draw(GameTime gameTime) {
            if (active) {
                base.Draw(gameTime);
                border.Draw();
                fill.Draw();
            }
        }
    }
}
