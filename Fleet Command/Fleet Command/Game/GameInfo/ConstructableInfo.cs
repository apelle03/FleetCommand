using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fleet_Command.Game.GameInfo {
    public class ConstructableInfo {
        protected string name = "Generic";
        public string Name { get { return name; } }
        protected string icon = "Ships/Icons/Generic";
        public string Icon { get { return icon; } }
        protected string faction = "None";
        public string Faction { get { return faction; } }

        public ConstructableInfo(string name, string icon, string faction) {
            this.name = name;
            this.icon = icon;
            this.faction = faction;
        }
    }
}
