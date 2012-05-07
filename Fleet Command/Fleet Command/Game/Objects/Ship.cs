using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Fleet_Command.Game.Levels;
using Fleet_Command.Game.Players;
using Fleet_Command.Game.Commands;
using Fleet_Command.Decorators;

namespace Fleet_Command.Game.Objects {
    public class Ship : Unit {
        protected new static string sprite_source = "Ships/Basestar";
        public override string SpriteSource { get { return sprite_source; } }

        protected static float max_fuel = 1000;
        public virtual float MaxFuel { get { return max_fuel; } }
        protected static float fuel_rate = .002f;
        public virtual float FuelRate { get { return fuel_rate; } }
        protected static float refuel_rate = 50;
        public virtual float RefuelRate { get { return refuel_rate; } }

        protected static int fire_rate = 20;
        public virtual int FireRate { get { return fire_rate; } }

        protected int coolDown;

        protected CircleBorder selectionBorder;
        public bool Selected { get; set; }

        public HealthBar healthBar;

        protected float fuel;
        public float Fuel { get { return fuel; } }

        public FuelBar fuelBar;

        public Ship(FC game, PlayArea playArea, Vector2 pos, float angle, Player controller)
            : base(game, playArea, pos, angle, controller) {
                unitInfo = new ShipInfo(game, this);
                selectionBorder = new CircleBorder(this, "Unit");
                healthBar = new HealthBar(this);
                fuelBar = new FuelBar(this);
                coolDown = 0;
                fuel = MaxFuel;
                passiveCommands.Add(new PassiveAttack(this, null, PlayArea.Components));
        }

        public override void LoadContent() {
            base.LoadContent();
            selectionBorder.LoadContent();
            healthBar.LoadContent();
            fuelBar.LoadContent();
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

            coolDown = (int)MathHelper.Clamp(--coolDown, 0, FireRate);

            selectionBorder.Update();
            healthBar.Update();
            fuelBar.Update();
        }

        public override void Draw(GameTime gameTime) {
            if (Selected) {
                selectionBorder.Draw();
            }
            base.Draw(gameTime);
            healthBar.Draw();
            fuelBar.Draw();
        }

        public override void PointAt(Vector2 dest) {
            if (Fuel > 0) {
                base.PointAt(dest);
            }
        }

        public override void MoveTo(Vector2 dest) {
            if ((dest - Pos).Length() > 0) {
                float dist = Math.Min((dest - Pos).Length(), Math.Min(MaxSpeed, Fuel / FuelRate));
                Vector2 amount = dest - Pos;
                amount.Normalize();
                amount *= dist;
                ChangeFuel(-FuelRate * dist);
                Pos += amount;
            }
        }

        public virtual void ChangeFuel(float amount) {
            fuel = MathHelper.Clamp(fuel + amount, 0, MaxFuel);
        }

        public override void Fire(Unit target) {
            if (coolDown == 0 && (Pos - target.Pos).Length() < Range) {
                Projectile projectile = new Projectile(fc, playArea, Pos, (float)Math.Atan2(target.Pos.Y - Pos.Y, target.Pos.X - Pos.X), controller);
                projectile.AttackCommand(target, true);
                playArea.Add(projectile);
                coolDown = FireRate;
            }
        }
    }
}
