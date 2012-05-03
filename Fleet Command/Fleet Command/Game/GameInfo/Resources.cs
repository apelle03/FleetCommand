using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fleet_Command.Game.GameInfo {
    public static class Resources {
        public static List<ResourceInfo> ResourceList = new List<ResourceInfo>();

        static Resources() {
            ResourceList.Add(new ResourceInfo("Air", "Resources/Icons/Air"));
            ResourceList.Add(new ResourceInfo("Water", "Resources/Icons/Water"));
            ResourceList.Add(new ResourceInfo("Food", "Resources/Icons/Food"));
            ResourceList.Add(new ResourceInfo("Fuel", "Resources/Icons/Fuel"));
            ResourceList.Add(new ResourceInfo("Common Materials", "Resources/Icons/Common"));
            ResourceList.Add(new ResourceInfo("Rare Materials", "Resources/Icons/Rare"));
        }
    }
}
