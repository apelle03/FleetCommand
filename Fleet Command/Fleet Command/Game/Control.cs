using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Fleet_Command.Menus;

namespace Fleet_Command.Game {
    class Control : RelativeSizeComponent<Control> {
        protected Texture2D outlineCorner, outlineSide, outlineTop;
        protected Texture2D fill, fillCorner, fillSide, fillTop;

        protected Rectangle topLeft, topRight, bottomLeft, bottomRight, left, right, top, bottom;
        protected Rectangle topMiddle, bottomMiddle, leftMiddle, rightMiddle, middle;

        protected bool hovering;
        protected Color transparent;

        protected ClickAction clickAction;
        protected bool pressed;

        public Control(FC game, ClickAction action)
            : base(game) {
                hovering = false;
                clickAction = action;
                pressed = false;
        }

        public override void Initialize() {
            base.Initialize();
            FC.InputManager.Register(Input.Actions.Click);
            FC.InputManager.Register(Input.Actions.Hover);
            FC.InputManager.Save();
        }

        public override void LoadContent() {
            base.LoadContent();
            outlineCorner = FC.Content.Load<Texture2D>("Buttons/outline_corner");
            outlineSide = FC.Content.Load<Texture2D>("Buttons/outline_side");
            outlineTop = FC.Content.Load<Texture2D>("Buttons/outline_top");
            fill = FC.Content.Load<Texture2D>("Buttons/fill");
            fillCorner = FC.Content.Load<Texture2D>("Buttons/fill_corner");
            fillSide = FC.Content.Load<Texture2D>("Buttons/fill_side");
            fillTop = FC.Content.Load<Texture2D>("Buttons/fill_top");

            float scale = Math.Min((float)BoundingBox.Width / (2 * outlineCorner.Width), (float)BoundingBox.Height / (2 * outlineCorner.Height));
            if (scale > 1) scale = 1;
            int size = (int)(outlineCorner.Bounds.Width * scale);
            topLeft = new Rectangle(BoundingBox.Left, BoundingBox.Top, size, size);
            topRight = new Rectangle(BoundingBox.Right - size, BoundingBox.Y, size, size);
            bottomLeft = new Rectangle(BoundingBox.Left, BoundingBox.Bottom - size, size, size);
            bottomRight = new Rectangle(BoundingBox.Right - size, BoundingBox.Bottom - size, size, size);

            left = new Rectangle(BoundingBox.Left, BoundingBox.Top + size, size, BoundingBox.Height - 2 * size);
            right = new Rectangle(BoundingBox.Right - size, BoundingBox.Top + size, size, BoundingBox.Height - 2 * size);
            top = new Rectangle(BoundingBox.Left + size, BoundingBox.Top, BoundingBox.Width - 2 * size, size);
            bottom = new Rectangle(BoundingBox.Left + size, BoundingBox.Bottom - size, BoundingBox.Width - 2 * size, size);

            topMiddle = new Rectangle(BoundingBox.Left + size, BoundingBox.Top, BoundingBox.Width - 2 * size, size);
            bottomMiddle = new Rectangle(BoundingBox.Left + size, BoundingBox.Bottom - size, BoundingBox.Width - 2 * size, size);
            leftMiddle = new Rectangle(BoundingBox.Left, BoundingBox.Top + size, size, BoundingBox.Height - 2 * size);
            rightMiddle = new Rectangle(BoundingBox.Right - size, BoundingBox.Top + size, size, BoundingBox.Height - 2 * size);
            middle = new Rectangle(BoundingBox.Left + size, BoundingBox.Top + size, BoundingBox.Width - 2 * size, BoundingBox.Height - 2 * size);

            transparent = Color.White * .75f;
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            if (FC.InputManager.CheckAction(Input.Actions.Hover, this).Active) {
                hovering = true;
            } else {
                hovering = false;
            }
            if (pressed && !FC.InputManager.CheckAction(Input.Actions.Click, this).Active) {
                clickAction();
                pressed = false;
            }
            pressed = FC.InputManager.CheckAction(Input.Actions.Click, this).Active;
        }

        public override void BeforeDraw(GameTime gameTime) {
            SpriteBatch spriteBatch = FC.SpriteBatch;

            if (hovering) {
                spriteBatch.Draw(fillCorner, topLeft, fillCorner.Bounds, transparent, 0, Vector2.Zero, SpriteEffects.None, 0);
                spriteBatch.Draw(fillCorner, bottomLeft, fillCorner.Bounds, transparent, 0, Vector2.Zero, SpriteEffects.FlipVertically, 0);
                spriteBatch.Draw(fillCorner, bottomRight, fillCorner.Bounds, transparent, 0, Vector2.Zero, SpriteEffects.FlipVertically | SpriteEffects.FlipHorizontally, 0);
                spriteBatch.Draw(fillCorner, topRight, fillCorner.Bounds, transparent, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);

                spriteBatch.Draw(fillTop, topMiddle, fillTop.Bounds, transparent, 0, Vector2.Zero, SpriteEffects.None, 0);
                spriteBatch.Draw(fillTop, bottomMiddle, fillTop.Bounds, transparent, 0, Vector2.Zero, SpriteEffects.FlipVertically, 0);
                spriteBatch.Draw(fillSide, leftMiddle, fillSide.Bounds, transparent, 0, Vector2.Zero, SpriteEffects.None, 0);
                spriteBatch.Draw(fillSide, rightMiddle, fillSide.Bounds, transparent, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                spriteBatch.Draw(fill, middle, fill.Bounds, transparent, 0, Vector2.Zero, SpriteEffects.None, 0);
            }

            spriteBatch.Draw(outlineCorner, topLeft, outlineCorner.Bounds, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
            spriteBatch.Draw(outlineCorner, bottomLeft, outlineCorner.Bounds, Color.White, 0, Vector2.Zero, SpriteEffects.FlipVertically, 0);
            spriteBatch.Draw(outlineCorner, bottomRight, outlineCorner.Bounds, Color.White, 0, Vector2.Zero, SpriteEffects.FlipVertically | SpriteEffects.FlipHorizontally, 0);
            spriteBatch.Draw(outlineCorner, topRight, outlineCorner.Bounds, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);

            spriteBatch.Draw(outlineSide, left, outlineSide.Bounds, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
            spriteBatch.Draw(outlineSide, right, outlineSide.Bounds, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
            spriteBatch.Draw(outlineTop, top, outlineTop.Bounds, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
            spriteBatch.Draw(outlineTop, bottom, outlineTop.Bounds, Color.White, 0, Vector2.Zero, SpriteEffects.FlipVertically, 0);
            base.BeforeDraw(gameTime);
        }
    }
}
