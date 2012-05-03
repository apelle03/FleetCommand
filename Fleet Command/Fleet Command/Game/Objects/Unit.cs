using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Fleet_Command.Game.Levels;
using Fleet_Command.Game.Players;
using Fleet_Command.Decorators;

namespace Fleet_Command.Game.Objects {
    public class Unit : DGC {
        protected static string sprite_source = "Units/Galactica";
        protected virtual string SpriteSource { get { return sprite_source; } }
        protected static float max_speed = 5;
        protected virtual float MaxSpeed { get { return max_speed; } }
        protected static float max_rotational_speed = (float)Math.PI/256;
        protected virtual float MaxRotationalSpeed { get { return max_rotational_speed; } }
        protected static float range = 2500;
        public virtual float Range { get { return range; } }
        protected static float damage = 0;
        protected virtual float Damage { get { return damage; } }
        protected static float max_health = 100;
        public virtual float MaxHealth { get { return max_health; } }

        protected PlayArea playArea;

        protected Player controller;
        public Player Controller { get { return controller; } }

        protected Vector2 pos;
        public Vector2 Pos { get { return pos; } }
        protected Vector2 center;
        public Vector2 Center { get { return center; } }
        protected float angle;

        protected float health;
        public float Health { get { return health; } }

        protected Vector2 dest;
        protected virtual Vector2 Dest {
            get {
                if (moving) {
                    return dest;
                } else if (attacking && target != null) {
                    Vector2 temp = pos - target.pos;
                    temp.Normalize();
                    return Vector2.Multiply(temp, range * .9f) + target.pos;
                } else {
                    return Vector2.Zero;
                }
            }
        }
        protected Unit target;
        protected bool moving, attacking;

        protected Texture2D sprite;

        public Unit(FC game, PlayArea playArea, Vector2 pos, float angle, Player controller)
            : base(game) {
                this.playArea = playArea;
                
                this.controller = controller;
                
                this.pos = pos;
                this.angle = angle;

                health = MaxHealth;

                target = null;
                moving = false;
                attacking = false;
        }

        public override void LoadContent() {
            base.LoadContent();
            sprite = FC.Content.Load<Texture2D>(SpriteSource);
            boundingBox.X = (int)pos.X - sprite.Bounds.Center.X;
            boundingBox.Y = (int)pos.Y - sprite.Bounds.Center.Y;
            boundingBox.Width = sprite.Bounds.Width;
            boundingBox.Height = sprite.Bounds.Height;
            center = new Vector2(sprite.Bounds.Center.X, sprite.Bounds.Center.Y);
        }

        public void MoveTo(Vector2 dest) {
            this.dest = dest;
            moving = true;
        }

        public void Attack(Unit target) {
            this.target = target;
            moving = false;
            attacking = true;
        }

        public virtual void MoveToTarget() {
            if (moving || attacking) {
                dest = Dest;
                Vector2 delta = Dest - pos;
                if (delta.LengthSquared() == 0) {
                    moving = false;
                } else {
                    angle = (angle + MathHelper.TwoPi) % MathHelper.TwoPi;
                    double diff = (Math.Atan2(dest.Y - pos.Y, dest.X - pos.X) - angle + 2 * MathHelper.TwoPi) % MathHelper.TwoPi;
                    if (diff > MathHelper.Pi) {
                        angle -= (float)Math.Min(MaxRotationalSpeed, MathHelper.TwoPi - diff);
                    } else {
                        angle += (float)Math.Min(MaxRotationalSpeed, diff);
                    }
                    delta.Normalize();
                    pos += Vector2.Multiply(delta, Math.Min(MaxSpeed, (dest - pos).Length()));
                }
            }
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

            MoveToTarget();

            boundingBox.X = (int)pos.X - sprite.Bounds.Center.X;
            boundingBox.Y = (int)pos.Y - sprite.Bounds.Center.Y;
            boundingBox.Width = sprite.Bounds.Width;
            boundingBox.Height = sprite.Bounds.Height;
        }

        public override void Draw(GameTime gameTime) {
            base.Draw(gameTime);
            SpriteBatch spriteBatch = FC.SpriteBatch;
            spriteBatch.Draw(sprite, pos, null, Color.White, angle, center, 1, SpriteEffects.None, 1);
        }

        public virtual void InflictDamage(float amount) {
            health = MathHelper.Clamp(health - amount, 0, MaxHealth);
        }
    }
}
