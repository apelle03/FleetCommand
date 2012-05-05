using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Fleet_Command.Game.Players {
    public class ResourceCounter {
        protected string name;
        public string Name { get { return name; } }
        public int Amount { get; set;}
        public int Capacity { get; set; }
        protected int increases, decreases;
        public int Increases { get { return increases; }
            set { increases += value; Amount = (int)MathHelper.Clamp(Amount + value, 0, Capacity); }
        }
        public int Decreases { get { return decreases; } set { decreases += value; MathHelper.Clamp(Amount - value, 0, Capacity); } }

        public ResourceCounter(string name)
            : this(name, 0) {
        }

        public ResourceCounter(string name, int initialCapacity) {
            this.name = name;
            Amount = 0;
            Capacity = initialCapacity;
            increases = 0;
            decreases = 0;
        }

        public void Clear() {
            increases = 0;
            decreases = 0;
        }
    }
}
