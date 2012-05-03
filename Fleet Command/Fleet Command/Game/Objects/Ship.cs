using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Fleet_Command.Game.Levels;
using Fleet_Command.Game.Players;
using Fleet_Command.Decorators;

namespace Fleet_Command.Game.Objects {
    public class Ship : Unit {
        protected new static string sprite_source = "Ships/Basestar";
        protected override string SpriteSource { get { return sprite_source; } }
        protected static int fire_rate = 20;

        protected int coolDown;

        protected CircleBorder selectionBorder;
        public bool Selected { get; set; }

        public HealthBar healthBar;

        public Ship(FC game, PlayArea playArea, Vector2 pos, float angle, Player controller)
            : base(game, playArea, pos, angle, controller) {
                selectionBorder = new CircleBorder(this, "Unit");
                healthBar = new HealthBar(this);
                coolDown = 0;
        }

        public override void LoadContent() {
            base.LoadContent();
            selectionBorder.LoadContent();
            healthBar.LoadContent();
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

            coolDown = (int)MathHelper.Clamp(--coolDown, 0, fire_rate);
            if (attacking && target != null && Vector2.Distance(target.Pos, pos) < range && coolDown == 0) {
                Fire();
                if (target.Health == 0) {
                    attacking = false;
                }
            }

            selectionBorder.Update();
            healthBar.Update();
        }

        public override void Draw(GameTime gameTime) {
            if (Selected) {
                selectionBorder.Draw();
            }
            base.Draw(gameTime);
            healthBar.Draw();
        }

        private void Fire() {
            Projectile projectile = new Projectile(fc, playArea, pos, (float)Math.Atan2(target.Pos.Y - pos.Y, target.Pos.X - pos.X), controller);
            projectile.Attack(target);
            playArea.Add(projectile);
            coolDown = fire_rate;
        }
    }
}
