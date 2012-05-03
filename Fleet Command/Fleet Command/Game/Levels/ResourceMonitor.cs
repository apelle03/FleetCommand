using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Fleet_Command.Game.Players;
using Fleet_Command.Game.GameInfo;

namespace Fleet_Command.Game.Levels {
    public class ResourceMonitor : RelativeSizeComponent<ResourceGauge> {
        protected Player controller;

        public ResourceMonitor(FC game, Player controller, Vector2 relPos, Vector2 relSize)
            : base(game, relPos, relSize, null, Color.Transparent) {
                Vector2 gaugeSize = new Vector2(relSize.X, relSize.Y / Resources.ResourceList.Count);
                Vector2 gaugeOffset = Vector2.UnitY * relSize.Y / Resources.ResourceList.Count;
                int i = 0;
                foreach (ResourceInfo ri in Resources.ResourceList) {
                    Components.Add(new ResourceGauge(game, controller, ri, relPos + gaugeOffset * i, gaugeSize));
                    i++;
                }
        }
    }
}
