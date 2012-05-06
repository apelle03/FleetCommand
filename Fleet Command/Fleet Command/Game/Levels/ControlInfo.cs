using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
