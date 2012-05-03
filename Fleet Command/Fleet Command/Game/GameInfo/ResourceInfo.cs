using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fleet_Command.Game.GameInfo {
    public class ResourceInfo {
        protected string name = "Generic";
        public string Name { get { return name; } }
        protected string icon = "Resources/Icons/Generic";
        public string Icon { get { return icon; } }

        public ResourceInfo(string name, string icon) {
            this.name = name;
            this.icon = icon;
        }
    }
}
