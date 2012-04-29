using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Fleet_Command.Input;

namespace Fleet_Command.Game.Levels {
    public class PlayArea : RelativeSizeComponent<Unit> {
        protected Vector2 selectablePos, selectableSize;
        protected Rectangle selectableArea;
        protected SelectionBox selectionBox;
        protected List<Unit> selection;

        protected Viewport viewport;

        public PlayArea(FC game)
            : this(game, Vector2.Zero, Vector2.Zero) {
        }
        
        public PlayArea(FC game, Vector2 relPos, Vector2 relSize)
            : this(game, relPos, relSize, Color.Transparent) {
        }

        public PlayArea(FC game, Vector2 relPos, Vector2 relSize, Color color)
            : this(game, relPos, relSize, relPos, relSize, color) {
        }

        public PlayArea(FC game, Vector2 relPos, Vector2 relSize, Vector2 selectPos, Vector2 selectSize)
            : this(game, relPos, relSize, selectPos, selectSize, Color.Transparent) {
        }

        public PlayArea(FC game, Vector2 relPos, Vector2 relSize, Vector2 selectPos, Vector2 selectSize, Color color)
            : base(game, relPos, relSize, color) {
                selectablePos = selectPos;
                selectableSize = selectSize;
                selectionBox = new SelectionBox(game);
                selection = new List<Unit>();
                Components.Add(new Unit(game, Vector2.One * 500, -MathHelper.PiOver2));
                Components.Add(new Unit(game, Vector2.One * 200, -MathHelper.PiOver2));
                Components.Add(new Unit(game, Vector2.One * 1000, -MathHelper.PiOver2));
        }

        public override void Initialize() {
            base.Initialize();
            FC.InputManager.Register(Input.Actions.Click);
            FC.InputManager.Register(Input.Actions.RightClick);
            FC.InputManager.Register(Input.Actions.Left);
            FC.InputManager.Register(Input.Actions.Right);
            FC.InputManager.Register(Input.Actions.Up);
            FC.InputManager.Register(Input.Actions.Down);
            FC.InputManager.Register(Input.Actions.Scroll);
            FC.InputManager.Save();
        }

        public override void LoadContent() {
            base.LoadContent();

            int height = BoundingBox.Height;
            int width = BoundingBox.Width;
            selectableArea = new Rectangle((int)(width * selectablePos.X), (int)(height * selectablePos.Y),
                (int)(width * selectableSize.X), (int)(height * selectableSize.Y));

            selectionBox.LoadContent();

            viewport = new Viewport(new Vector2(500, 500), new Vector2(width, height), 0.5f);
        }

        public double ScreenToWorldX(double x) {
            return x / BoundingBox.Width * viewport.Size.X + viewport.ViewArea.X;
        }

        public double ScreenToWorldY(double y) {
            return y / BoundingBox.Height * viewport.Size.Y + viewport.ViewArea.Y;
        }

        public override void Update(GameTime gameTime) {
            ComboInfo click = FC.InputManager.CheckAction(Actions.Click, this);
            ComboInfo rightClick = FC.InputManager.CheckAction(Actions.RightClick, this);
            if (click.Active && !selectionBox.Active) {
                if (selectableArea.Contains((int)click.X, (int)click.Y)) {
                    selectionBox.SetStart((int)click.X, (int)click.Y);
                }
            } else if (click.Active && selectionBox.Active) {
                int x = (int)click.X;
                int y = (int)click.Y;
                if (x < selectableArea.Left) x = selectableArea.Left;
                if (x > selectableArea.Right) x = selectableArea.Right;
                if (y < selectableArea.Top) y = selectableArea.Top;
                if (y > selectableArea.Bottom) y = selectableArea.Bottom;
                selectionBox.SetCorner(x, y);
            } else if (!click.Active && selectionBox.Active) {
                selectionBox.Active = false;
                selectionBox.Update(gameTime);
                Rectangle selectionArea = new Rectangle((int)ScreenToWorldX(selectionBox.BoundingBox.X),
                                                        (int)ScreenToWorldY(selectionBox.BoundingBox.Y),
                                                        (int)(ScreenToWorldX(selectionBox.BoundingBox.Right) - ScreenToWorldX(selectionBox.BoundingBox.Left)),
                                                        (int)(ScreenToWorldX(selectionBox.BoundingBox.Bottom) - ScreenToWorldX(selectionBox.BoundingBox.Top)));
                foreach (Unit u in Components) {
                    u.Selected = false;
                    if (u.BoundingBox.Intersects(selectionArea)) {
                        u.Selected = true;
                        selection.Add(u);
                        Console.WriteLine(u);
                    }
                }
            }
            selectionBox.Update(gameTime);

            if (rightClick.Active) {
                foreach (Unit u in selection) {
                    u.MoveTo(new Vector2((float)ScreenToWorldX(rightClick.X), (float)ScreenToWorldY(rightClick.Y)));
                }
            }

            Vector2 amount = Vector2.Zero;
            if (FC.InputManager.CheckAction(Actions.Left, this).Active) { amount.X += -1; }
            if (FC.InputManager.CheckAction(Actions.Right, this).Active) { amount.X += 1; }
            if (FC.InputManager.CheckAction(Actions.Up, this).Active) { amount.Y += -1; }
            if (FC.InputManager.CheckAction(Actions.Down, this).Active) { amount.Y += 1; }
            viewport.Scroll(amount);

            ComboInfo scroll = FC.InputManager.CheckAction(Actions.Scroll, this);
            if (scroll.Active) {
                viewport.ChangeZoom(scroll.WheelDelta);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
            SpriteBatch spriteBatch = FC.SpriteBatch;
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null,
                    viewport.Transformation() * Matrix.CreateTranslation(new Vector3(BoundingBox.Width / 2, BoundingBox.Height / 2, 0)));
            base.Draw(gameTime);
            spriteBatch.End();

            spriteBatch.Begin();
            if (selectionBox.Active) {
                selectionBox.Draw(gameTime);
            }
        }
    }
}
