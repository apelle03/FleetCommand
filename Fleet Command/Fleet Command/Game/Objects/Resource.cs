﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Fleet_Command.Game.Levels;
using Fleet_Command.Game.Players;
using Fleet_Command.Game.GameInfo;

namespace Fleet_Command.Game.Objects {
    public class Resource : Unit {
        protected new static string sprite_source = "Resources/Earth";
        public override string SpriteSource { get { return sprite_source; } }

        protected Dictionary<string, int> collectionRates;

        public Resource(FC game, PlayArea playArea, Vector2 pos, float angle, Player controller)
            : base(game, playArea, pos, angle, controller) {
                collectionRates = new Dictionary<string, int>();
                foreach (ResourceInfo ri in Resources.ResourceList) {
                    collectionRates.Add(ri.Name, 10);
                }
        }

        public virtual int GetRate(string name) {
            int rate = 0;
            collectionRates.TryGetValue(name, out rate);
            return rate;
        }
    }
}
