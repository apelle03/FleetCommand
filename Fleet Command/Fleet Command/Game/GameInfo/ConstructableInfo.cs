using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Fleet_Command.Game.Levels;
using Fleet_Command.Game.Objects;
using Fleet_Command.Game.Players;

namespace Fleet_Command.Game.GameInfo {
    public class ConstructableInfo {
        public string Name { get; private set; }
        public string Icon { get; private set; }
        public string Faction { get; private set; }
        public Type Type { get; private set; }
        public Dictionary<string, float> ResourceRequirements { get; private set; }
        public float BuildRate { get; private set; }

        public ConstructableInfo(string name, string icon, string faction, Type type, Dictionary<string, float> resourceRequirements, float buildRate) {
            Name = name;
            Icon = icon;
            Faction = faction;
            Type = type;
            ResourceRequirements = resourceRequirements;
            BuildRate = buildRate;
        }

        public Unit CreateNew(FC fc, PlayArea playArea, Vector2 pos, float angle, Player controller) {
            //if (Type == typeof(CombatShip)) {
            //}
            return new CombatShip(fc, playArea, pos, angle, controller);
        }
    }
}
