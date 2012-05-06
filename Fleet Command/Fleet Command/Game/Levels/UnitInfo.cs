using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Fleet_Command.Decorators;
using Fleet_Command.Game.Objects;

namespace Fleet_Command.Game.Levels {
    public class UnitInfo : ControlInfo {
        protected Unit unit;
        public Unit Unit { get { return unit; } }

        protected ControlHealthBar healthBar;

        public UnitInfo(FC game, Unit unit)
            : base(game, unit.SpriteSource) {
                this.unit = unit;
                healthBar = new ControlHealthBar(unit);
        }

        public override void LoadContent() {
            base.LoadContent();
            healthBar.LoadContent();
        }

        public override void Update(Control control) {
            base.Update(control);
            healthBar.Update(control);
        }

        public override void Draw(Control control) {
            base.Draw(control);
            healthBar.Draw(control);
        }
    }
}
