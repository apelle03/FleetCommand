using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Fleet_Command.Input;

namespace Fleet_Command.Game {
    public class PlayArea : RelativeSizeComponent<Unit> {
        protected SelectionBox selectionBox;
        protected List<Unit> selection;

        public PlayArea(FC game)
            : this(game, Vector2.Zero, Vector2.Zero) {
        }
        
        public PlayArea(FC game, Vector2 relPos, Vector2 relSize)
            : this(game, relPos, relSize, Color.Transparent) {
        }

        public PlayArea(FC game, Vector2 relPos, Vector2 relSize, Color color)
            : base(game, relPos, relSize, color) {
                selectionBox = new SelectionBox(game);
                selection = new List<Unit>();
                Components.Add(new Unit(game, Vector2.One * 500, 0));
        }

        public override void Initialize() {
            base.Initialize();
            FC.InputManager.Register(Input.Actions.Click);
            FC.InputManager.Save();
        }

        public override void LoadContent() {
            base.LoadContent();
            selectionBox.LoadContent();
        }

        public override void Update(GameTime gameTime) {
            ComboInfo ci = FC.InputManager.CheckAction(Actions.Click, this);
            if (ci.Active && !selectionBox.Active) {
                selectionBox.SetStart((int)ci.X, (int)ci.Y);
            } else if (ci.Active && selectionBox.Active) {
                selectionBox.SetCorner((int)ci.X, (int)ci.Y);
            } else if (!ci.Active && selectionBox.Active) {
                selectionBox.Active = false;
                selectionBox.Update(gameTime);
                foreach (Unit u in Components) {
                    u.Selected = false;
                    if (u.BoundingBox.Intersects(selectionBox.BoundingBox)) {
                        u.Selected = true;
                        selection.Add(u);
                        Console.WriteLine(u);
                    }
                }
            }
            selectionBox.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
            base.Draw(gameTime);
            if (selectionBox.Active) {
                selectionBox.Draw(gameTime);
            }
        }
    }
}
