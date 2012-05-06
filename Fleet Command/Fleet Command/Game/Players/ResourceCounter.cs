using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Fleet_Command.Game.Players {
    public class ResourceCounter {
        protected string name;
        public string Name { get { return name; } }
        public float Amount { get; set;}
        public float Capacity { get; set; }
        protected float increases, decreases;
        public float Increases { get { return increases; } set { increases += value; Amount = MathHelper.Clamp(Amount + value, 0, Capacity); } }
        public float Decreases { get { return decreases; } set { decreases += value; Amount = MathHelper.Clamp(Amount - value, 0, Capacity); } }

        public ResourceCounter(string name)
            : this(name, 0) {
        }

        public ResourceCounter(string name, float initialCapacity) {
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

        public void Supply(float amount) {
            Increases = amount;
        }

        public float Use(float amount) {
            float amt = Math.Min(amount, Amount);
            Decreases = amt;
            return amt;
        }

        public float TestUse(float amount) {
            return Math.Min(amount, Amount);
        }
    }
}
