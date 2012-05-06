using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Fleet_Command.Game.Levels;
using Fleet_Command.Game.Objects;

namespace Fleet_Command.Game.Commands {
    public class Build : PassiveCommand {
        public BuildQueueInfo Info { get; private set; }

        public Build(Unit controller, CommandComplete callback, BuildQueueInfo info)
            : base(controller, callback) {
                this.Info = info;
        }
        
        public override void Perform() {
            float minRate = Math.Min(Info.Info.BuildRate, 1 - Info.Progress);
            foreach (KeyValuePair<string, float> amount in Info.Info.ResourceRequirements) {
                minRate = Math.Min(minRate, controller.Controller.TestUse(amount.Key, amount.Value * minRate) / amount.Value);
            }
            Info.Build(minRate);
            foreach (KeyValuePair<string, float> amount in Info.Info.ResourceRequirements) {
                controller.Controller.Use(amount.Key, amount.Value * minRate);
            }
        }

        public override bool Completed() {
            return Info.Progress == 1;
        }
    }
}
