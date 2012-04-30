using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Fleet_Command.Game.Players {
    public class Player {
        protected string Name { get; set; }
        protected int Number { get; set; }
        protected Color Color { get; set;}

        public Player(int number, string name, Color color) {
            Number = number;
            Name = name;
            Color = color;
        }
    }
}
