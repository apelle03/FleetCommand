using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Fleet_Command.Game.Levels;
using Fleet_Command.Game.Players;
using Fleet_Command.Game.Commands;
using Fleet_Command.Decorators;

namespace Fleet_Command.Game.Objects {
    public class Unit : DGC {
        protected static string sprite_source = "Units/Galactica";
        protected virtual string SpriteSource { get { return sprite_source; } }

        protected static float max_speed = 5;
        public virtual float MaxSpeed { get { return max_speed; } }
        protected static float max_rotational_speed = (float)Math.PI/256;
        public virtual float MaxRotationalSpeed { get { return max_rotational_speed; } }
        protected static float range = 2500;
        public virtual float Range { get { return range; } }
        protected static float damage = 0;
        public virtual float Damage { get { return damage; } }
        protected static float max_health = 100;
        public virtual float MaxHealth { get { return max_health; } }

        protected PlayArea playArea;

        protected Player controller;
        public Player Controller { get { return controller; } }

        public Vector2 Pos { get; set; }
        public Vector2 Center { get; set; }
        public float Angle { get; set; }

        protected float health;
        public float Health { get { return health; } }

        protected Queue<ActiveCommand> activeCommands;
        protected List<PassiveCommand> passiveCommands;

        protected Texture2D sprite;

        public Unit(FC game, PlayArea playArea, Vector2 pos, float angle, Player controller)
            : base(game) {
                this.playArea = playArea;
                
                this.controller = controller;
                
                this.Pos = pos;
                this.Angle = angle;

                health = MaxHealth;

                activeCommands = new Queue<ActiveCommand>();
                passiveCommands = new List<PassiveCommand>();
        }

        public override void LoadContent() {
            base.LoadContent();
            sprite = FC.Content.Load<Texture2D>(SpriteSource);
            boundingBox.X = (int)Pos.X - sprite.Bounds.Center.X;
            boundingBox.Y = (int)Pos.Y - sprite.Bounds.Center.Y;
            boundingBox.Width = sprite.Bounds.Width;
            boundingBox.Height = sprite.Bounds.Height;
            Center = new Vector2(sprite.Bounds.Center.X, sprite.Bounds.Center.Y);
        }

        public void MoveTo(Vector2 dest, bool immediate) {
            if (immediate) {
                activeCommands.Clear();
            }
            activeCommands.Enqueue(new Move(this, dest));
        }

        public void Attack(Unit target, bool immediate) {
            if (immediate) {
                activeCommands.Clear();
            }
            activeCommands.Enqueue(new ActiveAttack(this, target));
        }

        public virtual void Fire(Unit target) {
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

            if (activeCommands.Count > 0) {
                activeCommands.Peek().Perform();
                if (activeCommands.Peek().Completed()) {
                    activeCommands.Dequeue();
                }
            }

            for (int i = 0; i < passiveCommands.Count; i++) {
                passiveCommands[i].Perform();
                if (passiveCommands[i].Completed()) {
                    passiveCommands.Remove(passiveCommands[i]);
                    i--;
                }
            }
            

            boundingBox.X = (int)Pos.X - sprite.Bounds.Center.X;
            boundingBox.Y = (int)Pos.Y - sprite.Bounds.Center.Y;
            boundingBox.Width = sprite.Bounds.Width;
            boundingBox.Height = sprite.Bounds.Height;
        }

        public override void Draw(GameTime gameTime) {
            base.Draw(gameTime);
            SpriteBatch spriteBatch = FC.SpriteBatch;
            spriteBatch.Draw(sprite, Pos, null, Color.White, Angle, Center, 1, SpriteEffects.None, 1);
        }

        public virtual void InflictDamage(float amount) {
            health = MathHelper.Clamp(health - amount, 0, MaxHealth);
        }
    }
}
