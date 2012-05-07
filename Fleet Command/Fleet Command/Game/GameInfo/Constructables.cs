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
            resourceRequirements.Add("Common Materials", 500);
            resourceRequirements.Add("Rare Materials", 100);
            ConstructableList.Add(new ConstructableInfo("Viper Mark II", "Ships/viper-mkii", "Human", typeof(ViperMKII), resourceRequirements, .01f));
            ConstructableList.Add(new ConstructableInfo("Raider", "Ships/raider", "Cylon", typeof(Raider), resourceRequirements, .01f));
            resourceRequirements = new Dictionary<string, float>();
            resourceRequirements.Add("Common Materials", 700);
            resourceRequirements.Add("Rare Materials", 150);
            ConstructableList.Add(new ConstructableInfo("Viper Mark VII", "Ships/viper-mkvii", "Human", typeof(ViperMKVII), resourceRequirements, .01f));
            resourceRequirements = new Dictionary<string, float>();
            resourceRequirements.Add("Common Materials", 1000);
            resourceRequirements.Add("Rare Materials", 400);
            ConstructableList.Add(new ConstructableInfo("Raptor", "Ships/raptor", "Human", typeof(Raptor), resourceRequirements, .01f));
            ConstructableList.Add(new ConstructableInfo("Heavy Raider", "Ships/raider-heavy", "Cylon", typeof(HeavyRaider), resourceRequirements, .01f));
        }
    }
}
