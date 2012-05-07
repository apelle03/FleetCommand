using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Fleet_Command.Game.Levels;
using Fleet_Command.Game.Players;
using Fleet_Command.Game.GameInfo;

namespace Fleet_Command.Game.Objects {
    public class Mars : Resource {
        protected new static string sprite_source = "Resources/Mars";
        public override string SpriteSource { get { return sprite_source; } }

        public Mars(FC game, PlayArea playArea, Vector2 pos, float angle, Player controller)
            : base(game, playArea, pos, angle, controller) {
            collectionRates = new Dictionary<string, int>();
            collectionRates.Add("Fuel", 1);
            collectionRates.Add("Common Materials", 1);
            collectionRates.Add("Rare Materials", 1);
        }

        public override int GetRate(string name) {
            int rate = 0;
            collectionRates.TryGetValue(name, out rate);
            return rate;
        }
    }
}
