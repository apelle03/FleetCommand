using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Fleet_Command.Game.GameInfo;

namespace Fleet_Command.Game.Players {
    public class Player {
        public string Name { get; set; }
        public int Number { get; set; }
        public Color Color { get; set;}

        protected Dictionary<string, ResourceCounter> resources;
        public Dictionary<string, ResourceCounter> Resources { get { return resources; } }

        public Player(int number, string name, Color color) {
            Number = number;
            Name = name;
            Color = color;

            resources = new Dictionary<string, ResourceCounter>();
            foreach (ResourceInfo ri in Fleet_Command.Game.GameInfo.Resources.ResourceList) {
                resources.Add(ri.Name, new ResourceCounter(ri.Name, 1000));
            }
        }

        public ResourceCounter Resource(string name) {
            return resources[name];
        }

        public float TestUse(string name, float amount) {
            return resources[name].TestUse(amount);
        }

        public float Use(string name, float amount) {
            return resources[name].Use(amount);
        }

        public void Supply(string name, float amount) {
            resources[name].Supply(amount);
        }

        public void Update() {
            foreach (ResourceCounter rc in resources.Values) {
                rc.Clear();
            }
        }
    }
}
