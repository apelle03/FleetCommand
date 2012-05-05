using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Fleet_Command.Game.Objects;

namespace Fleet_Command.Decorators {
    public class FuelBar : Decorator {
        protected Texture2D bar;
        protected Rectangle location;

        protected Ship unit;

        public FuelBar(Ship unit) {
            this.unit = unit;
        }

        public override void LoadContent() {
            bar = new Texture2D(unit.FC.GraphicsDevice, 1, 1);
            Color[] data = new Color[1];
            data[0] = Color.White;
            bar.SetData<Color>(data);
            location = new Rectangle((int)(unit.Pos.X - unit.Center.X * .75f), (int)(unit.Pos.Y - unit.Center.Y * 1.8f),
                (int)(unit.Center.X * 1.5f * unit.Fuel / unit.MaxFuel), (int)(unit.Center.Y * .1f));
        }

        public override void Update() {
            location = new Rectangle((int)(unit.Pos.X - unit.Center.X * .75f), (int)(unit.Pos.Y - unit.Center.Y * 1.8f),
                (int)(unit.Center.X * 1.5f * unit.Fuel / unit.MaxFuel), (int)(unit.Center.Y * .1f));
        }

        public override void Draw() {
            SpriteBatch spriteBatch = unit.FC.SpriteBatch;

            spriteBatch.Draw(bar, location, Color.Orange);
        }
    }
}
