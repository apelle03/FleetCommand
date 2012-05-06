using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Fleet_Command.Decorators;
using Fleet_Command.Game.GameInfo;
using Fleet_Command.Game.Objects;

namespace Fleet_Command.Game.Levels {
    public class BuildQueueInfo : ControlInfo {
        protected ConstructableInfo info;
        public ConstructableInfo Info { get { return info; } }

        public CapitalShip CapitalShip { get; private set; }

        public float Progress { get; private set; }

        protected ControlProgressBar progressBar;

        public BuildQueueInfo(FC game, ConstructableInfo ci, CapitalShip cs)
            : base(game, ci.Icon) {
                info = ci;
                CapitalShip = cs;
                Progress = 0f;
                progressBar = new ControlProgressBar(CapitalShip);
        }

        public override void LoadContent() {
            base.LoadContent();
            progressBar.LoadContent();
        }

        public override void Update(Control control) {
            base.Update(control);
            progressBar.Update(control);
        }

        public override void Draw(Control control) {
            base.Draw(control);
            progressBar.Draw(control);
        }

        public void Build(float rate) {
            Progress += rate;
        }
    }
}
