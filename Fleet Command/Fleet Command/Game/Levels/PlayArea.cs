using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Fleet_Command.Input;
using Fleet_Command.Game.Objects;

namespace Fleet_Command.Game.Levels {
    public class PlayArea : RelativeSizeComponent<Unit> {
        private static bool IsDead(Unit u) {
            return u.Health <= 0f;
        }

        protected Level level;
        protected List<Unit> toAdd, toRemove;

        protected Vector2 selectablePos, selectableSize;
        protected Rectangle selectableArea;
        protected SelectionBox selectionBox;
        protected List<Unit> selection;

        protected bool lastAct;

        protected Viewport viewport;

        public PlayArea(FC game, Level level)
            : this(game, level, Vector2.Zero, Vector2.Zero) {
        }
        
        public PlayArea(FC game, Level level, Vector2 relPos, Vector2 relSize)
            : this(game, level, relPos, relSize, Color.Transparent) {
        }

        public PlayArea(FC game, Level level, Vector2 relPos, Vector2 relSize, Color color)
            : this(game, level, relPos, relSize, relPos, relSize, color) {
        }

        public PlayArea(FC game, Level level, Vector2 relPos, Vector2 relSize, Vector2 selectPos, Vector2 selectSize)
            : this(game, level, relPos, relSize, selectPos, selectSize, Color.Transparent) {
        }

        public PlayArea(FC game, Level level, Vector2 relPos, Vector2 relSize, Vector2 selectPos, Vector2 selectSize, Color color)
            : base(game, relPos, relSize, color) {
                this.level = level;
                toAdd = new List<Unit>();
                toRemove = new List<Unit>();

                selectablePos = selectPos;
                selectableSize = selectSize;
                selectionBox = new SelectionBox(game);
                selection = new List<Unit>();

                lastAct = false;

                Random rand = new Random();

                Components.Add(new Resource(game, this, new Vector2((float)(rand.NextDouble() * 10000), (float)(rand.NextDouble() * 10000)),
                    (float)(rand.NextDouble() * MathHelper.TwoPi), level.Players[0]));
                Components.Add(new Galactica(game, this, Vector2.One * 0, -MathHelper.PiOver2, level.Controller));
                Components.Add(new Basestar(game, this, Vector2.One * -2000, -MathHelper.PiOver2, level.Players[2]));
        }

        public override void Initialize() {
            base.Initialize();
            FC.InputManager.Register(Input.Actions.Select);
            FC.InputManager.Register(Input.Actions.Act);
            FC.InputManager.Register(Input.Actions.QueueAct);
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

            viewport = new Viewport(new Vector2(500, 500), new Vector2(width, height), 0.2f);
        }

        public double ScreenToWorldX(double x) {
            return x / BoundingBox.Width * viewport.Size.X + viewport.ViewArea.X;
        }

        public double ScreenToWorldY(double y) {
            return y / BoundingBox.Height * viewport.Size.Y + viewport.ViewArea.Y;
        }

        public override void Update(GameTime gameTime) {
            ComboInfo select = FC.InputManager.CheckAction(Actions.Select, this);
            ComboInfo act = FC.InputManager.CheckAction(Actions.Act, this);
            ComboInfo queueAct = FC.InputManager.CheckAction(Actions.QueueAct, this);
            if (select.Active && !selectionBox.Active) {
                if (selectableArea.Contains((int)select.X, (int)select.Y)) {
                    selectionBox.SetStart((int)select.X, (int)select.Y);
                }
            } else if (select.Active && selectionBox.Active) {
                int x = (int)select.X;
                int y = (int)select.Y;
                if (x < selectableArea.Left) x = selectableArea.Left;
                if (x > selectableArea.Right) x = selectableArea.Right;
                if (y < selectableArea.Top) y = selectableArea.Top;
                if (y > selectableArea.Bottom) y = selectableArea.Bottom;
                selectionBox.SetCorner(x, y);
            } else if (!select.Active && selectionBox.Active) {
                selectionBox.Active = false;
                selectionBox.Update(gameTime);
                Rectangle selectionArea = new Rectangle((int)ScreenToWorldX(selectionBox.BoundingBox.X),
                                                        (int)ScreenToWorldY(selectionBox.BoundingBox.Y),
                                                        (int)(ScreenToWorldX(selectionBox.BoundingBox.Right) - ScreenToWorldX(selectionBox.BoundingBox.Left)),
                                                        (int)(ScreenToWorldX(selectionBox.BoundingBox.Bottom) - ScreenToWorldX(selectionBox.BoundingBox.Top)));
                selection.Clear();
                foreach (Unit u in Components) {
                    if (u is Ship) {
                        ((Ship)u).Selected = false;
                        if (u.BoundingBox.Intersects(selectionArea) && u.Controller == level.Controller) {
                            ((Ship)u).Selected = true;
                            selection.Add(u);
                            Console.WriteLine(u);
                        }
                    }
                }
            }
            selectionBox.Update(gameTime);

            Point actLoc = new Point((int)ScreenToWorldX(act.X), (int)ScreenToWorldY(act.Y));
            bool move = true;
            if ((act.Active || queueAct.Active) && !lastAct) {
                foreach (Unit u in Components) {
                    if (u.Controller == level.Players[0] && u.BoundingBox.Contains(actLoc)) {
                        foreach (Unit s in selection) {
                            if (s is Ship) {
                                ((Ship)s).Collect(u);
                            }
                        }
                        move = false;
                    } else if (u.Controller != level.Controller && u.BoundingBox.Contains(actLoc)) {
                        foreach (Unit s in selection) {
                            s.Attack(u);
                        }
                        move = false;
                    }
                }
                if (move) {
                    foreach (Unit u in selection) {
                        u.MoveTo(new Vector2((float)ScreenToWorldX(act.X), (float)ScreenToWorldY(act.Y)), !queueAct.Active);
                    }
                }
            }
            lastAct = act.Active || queueAct.Active;

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

            // Remove dead units
            components.RemoveWhere(IsDead);

            // Add new units
            foreach (Unit u in toAdd) {
                u.LoadContent();
            }
            components.UnionWith(toAdd);
            toAdd.Clear();

            // Check for victory and loss
            bool loss = true, win = true;
            foreach (Unit u in components) {
                if (u.Controller == level.Controller) {
                    loss = false;
                }
                if (u.Controller != level.Controller) {
                    win = false;
                }
                if (!loss && !win) {
                    break;
                }
            }
            if (loss) {
                // Show loss screen here
                FC.MainMenu();
            }
            if (win) {
                // Show win screen here
                FC.MainMenu();
            }
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

        public void Add(Unit u) {
            toAdd.Add(u);
        }
    }
}
