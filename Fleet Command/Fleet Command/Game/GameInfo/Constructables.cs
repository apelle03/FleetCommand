using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fleet_Command.Game.GameInfo {
    public static class Constructables {
        public static List<ConstructableInfo> ConstructableList = new List<ConstructableInfo>();

        static Constructables() {
            ConstructableList.Add(new ConstructableInfo("Viper Mark II", "viper-mkii", "Human"));
            ConstructableList.Add(new ConstructableInfo("Viper Mark VII", "viper-mkvii", "Human"));
            ConstructableList.Add(new ConstructableInfo("Raptor", "raptor", "Human"));
            ConstructableList.Add(new ConstructableInfo("Raider", "raider", "Cylon"));
            ConstructableList.Add(new ConstructableInfo("Heavy Raider", "raider-heavy", "Cylon"));
        }
    }
}
