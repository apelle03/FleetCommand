using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fleet_Command.Game.Levels {
    public class ControlInfo {
        protected FC game;
        protected string iconSource;
        protected Texture2D icon;
        public Texture2D Icon { get { return icon; } }

        public ControlInfo(FC game, string iconSource) {
            this.game = game;
            this.iconSource = iconSource;
        }

        public virtual void LoadContent() {
            icon = game.Content.Load<Texture2D>(iconSource);
        }

        public virtual void Update(Control control) {
        }

        public virtual void Draw(Control control) {
            SpriteBatch spriteBatch = game.SpriteBatch;
            float scale = Math.Min(control.BoundingBox.Width * .6f / icon.Bounds.Width, control.BoundingBox.Height * .6f / icon.Bounds.Height);
            Rectangle dest = new Rectangle((int)(control.BoundingBox.Left + (control.BoundingBox.Width - icon.Bounds.Width * scale) / 2),
                (int)(control.BoundingBox.Top + (control.BoundingBox.Height - icon.Bounds.Height * scale) / 2),
                (int)(icon.Bounds.Width * scale), (int)(icon.Bounds.Height * scale));
            spriteBatch.Draw(icon, dest, Color.White);
        }
    }
}
