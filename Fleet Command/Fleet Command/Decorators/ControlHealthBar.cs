using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Fleet_Command.Game.Levels;
using Fleet_Command.Game.Objects;

namespace Fleet_Command.Decorators {
    public class ControlHealthBar : ControlDecorator {
        protected Texture2D bar;
        protected Rectangle location;

        protected Unit unit;

        public ControlHealthBar(Unit unit) : base(unit.UnitInfo) {
            this.unit = unit;
        }

        public override void LoadContent() {
            bar = new Texture2D(unit.FC.GraphicsDevice, 1, 1);
            Color[] data = new Color[1];
            data[0] = Color.White;
            bar.SetData<Color>(data);
            location = new Rectangle(0, 0, 0, 0);
        }

        public override void Update(Control control) {
            location = new Rectangle((int)(control.BoundingBox.Left + control.BoundingBox.Width * .2f), (int)(control.BoundingBox.Top + control.BoundingBox.Height * .1f),
                (int)(control.BoundingBox.Width * .6f * unit.Health / unit.MaxHealth), (int)(control.BoundingBox.Height * .05f));
        }

        public override void Draw(Control control) {
            SpriteBatch spriteBatch = unit.FC.SpriteBatch;
            if (unit.Health / unit.MaxHealth > .5f) {
                spriteBatch.Draw(bar, location, Color.Green);
            } else if (unit.Health / unit.MaxHealth > .25f) {
                spriteBatch.Draw(bar, location, Color.Yellow);
            } else {
                spriteBatch.Draw(bar, location, Color.Red);
            }
        }
    }
}
