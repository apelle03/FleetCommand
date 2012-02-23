using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fleet_Command.Menus {
    public class MainMenu : Menu {
        public MainMenu(FC game)
            : base(game) {
            Components.Add(new MenuComponent(game));
        }

        public override void Initialize() {
            base.Initialize();
        }

        public override void LoadContent() {
            base.LoadContent();
        }
    }
}
