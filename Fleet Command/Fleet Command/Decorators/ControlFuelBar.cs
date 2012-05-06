using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Fleet_Command.Game.Levels;
using Fleet_Command.Game.Objects;

namespace Fleet_Command.Decorators {
    public class ControlFuelBar : ControlDecorator {
        protected Texture2D bar;
        protected Rectangle location;

        protected Ship ship;

        public ControlFuelBar(Ship ship)
            : base(ship.UnitInfo) {
            this.ship = ship;
        }

        public override void LoadContent() {
            bar = new Texture2D(ship.FC.GraphicsDevice, 1, 1);
            Color[] data = new Color[1];
            data[0] = Color.White;
            bar.SetData<Color>(data);
            location = new Rectangle(0, 0, 0, 0);
        }

        public override void Update(Control control) {
            location = new Rectangle((int)(control.BoundingBox.Left + control.BoundingBox.Width * .2f), (int)(control.BoundingBox.Top + control.BoundingBox.Height * .15f),
                (int)(control.BoundingBox.Width * .6f * ship.Fuel / ship.MaxFuel), (int)(control.BoundingBox.Height * .05f));
        }

        public override void Draw(Control control) {
            SpriteBatch spriteBatch = ship.FC.SpriteBatch;
            spriteBatch.Draw(bar, location, Color.Orange);
        }
    }
}
