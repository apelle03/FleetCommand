using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Fleet_Command.Decorators;

namespace Fleet_Command.Game.Levels {
    public class Unit : DGC {
        protected static string spriteSource = "Basestar";
        protected static float max_speed = 5;
        protected static float max_rotational_speed = (float)Math.PI/256;
        protected Vector2 pos;
        protected Vector2 center;
        protected float angle;

        protected Vector2 dest;
        protected Unit target;
        protected float speed, rotational_speed;
        protected bool hasOrder;

        protected Texture2D sprite;

        protected CircleBorder selectionBorder;
        public bool Selected { get; set; }

        public Unit(FC game, Vector2 pos, float angle)
            : base(game) {
                this.pos = pos;
                this.angle = angle;
                selectionBorder = new CircleBorder(this, "Unit");
                target = null;
                speed = 0;
                rotational_speed = 0;
                hasOrder = false;
        }

        public override void LoadContent() {
            base.LoadContent();
            sprite = FC.Content.Load<Texture2D>("Units/" + spriteSource);
            boundingBox.X = (int)pos.X - sprite.Bounds.Center.X;
            boundingBox.Y = (int)pos.Y - sprite.Bounds.Center.Y;
            boundingBox.Width = sprite.Bounds.Width;
            boundingBox.Height = sprite.Bounds.Height;
            center = new Vector2(sprite.Bounds.Center.X, sprite.Bounds.Center.Y);
            selectionBorder.LoadContent();
        }

        public void MoveTo(Vector2 dest) {
            this.dest = dest;
            hasOrder = true;
        }

        public void Attack(Unit target) {
            this.target = target;
            hasOrder = true;
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

            if (hasOrder) {
                Vector2 delta = dest - pos;
                if (delta.LengthSquared() == 0) {
                    hasOrder = false;
                    //speed = 0;
                } else {
                    angle = (angle + MathHelper.TwoPi) % MathHelper.TwoPi;
                    double diff = (Math.Atan2(dest.Y - pos.Y, dest.X - pos.X) - angle + 2 * MathHelper.TwoPi) % MathHelper.TwoPi;
                    if (diff > MathHelper.Pi) {
                        angle -= (float)Math.Min(max_rotational_speed, MathHelper.TwoPi - diff);
                    } else {
                        angle += (float)Math.Min(max_rotational_speed, diff);
                    }
                    delta.Normalize();
                    //speed = Math.Min(max_speed, speed + max_speed / 100);
                    pos += Vector2.Multiply(delta, Math.Min(max_speed, (dest - pos).Length()));
                }
            }

            boundingBox.X = (int)pos.X - sprite.Bounds.Center.X;
            boundingBox.Y = (int)pos.Y - sprite.Bounds.Center.Y;
            boundingBox.Width = sprite.Bounds.Width;
            boundingBox.Height = sprite.Bounds.Height;
            selectionBorder.Update();
        }

        public override void Draw(GameTime gameTime) {
            base.Draw(gameTime);
            if (Selected) {
                selectionBorder.Draw();
            }
            SpriteBatch spriteBatch = FC.SpriteBatch;
            spriteBatch.Draw(sprite, pos, null, Color.White, angle, center, 1, SpriteEffects.None, 1);
        }
    }
}
