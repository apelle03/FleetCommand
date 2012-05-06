using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Fleet_Command.Game.Objects;

namespace Fleet_Command.Game.GameInfo {
    public static class Constructables {
        public static List<ConstructableInfo> ConstructableList = new List<ConstructableInfo>();

        static Constructables() {
            Dictionary<string, float> resourceRequirements = new Dictionary<string, float>();
            foreach (ResourceInfo ri in Resources.ResourceList) {
                resourceRequirements.Add(ri.Name, 500);
            }
            ConstructableList.Add(new ConstructableInfo("Viper Mark II", "Ships/viper-mkii", "Human", typeof(CombatShip), resourceRequirements, .01f));
            ConstructableList.Add(new ConstructableInfo("Viper Mark VII", "Ships/viper-mkvii", "Human", typeof(CombatShip), resourceRequirements, .01f));
            ConstructableList.Add(new ConstructableInfo("Raptor", "Ships/raptor", "Human", typeof(CombatShip), resourceRequirements, .01f));
            ConstructableList.Add(new ConstructableInfo("Raider", "Ships/raider", "Cylon", typeof(CombatShip), resourceRequirements, .01f));
            ConstructableList.Add(new ConstructableInfo("Heavy Raider", "Ships/raider-heavy", "Cylon", typeof(CombatShip), resourceRequirements, .01f));
        }
    }
}
