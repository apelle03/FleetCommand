using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Fleet_Command.Decorators;
using Fleet_Command.Game.Objects;

namespace Fleet_Command.Game.Levels {
    public class ShipInfo : UnitInfo {
        protected ControlFuelBar fuelBar;

        public ShipInfo(FC game, Ship ship)
            : base(game, ship) {
                fuelBar = new ControlFuelBar(ship);
        }

        public override void LoadContent() {
            base.LoadContent();
            fuelBar.LoadContent();
        }

        public override void Update(Control control) {
            base.Update(control);
            fuelBar.Update(control);
        }

        public override void Draw(Control control) {
            base.Draw(control);
            fuelBar.Draw(control);
        }
    }
}
