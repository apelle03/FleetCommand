using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fleet_Command.Menus {
    class MainMenu : Menu {
        public MainMenu(FC game)
            : base(game) {
            Components.Add(new MenuComponent(game));
        }

        public override void Initialize() {
            base.Initialize();
            Console.WriteLine("init Mainmenu");
        }

        public override void LoadContent() {
            base.LoadContent();
            Console.WriteLine("load mainmenu");
        }
    }
}
