using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Fleet_Command.Menus;
using Fleet_Command.Decorators;

namespace Fleet_Command.Game.Levels {
    public class Control : RelativeSizeComponent<Control> {
        public delegate void ControlAction(Control control);

        public ControlInfo Info { get; set; }

        protected CorneredBorder border;
        protected CorneredFill fill;

        public ControlAction Action { get; set; }
        protected bool pressed;
        protected bool hovering;

        public Control(FC game, Vector2 relPos, Vector2 relSize, ControlAction action)
            : this(game, null, relPos, relSize, action) {
        }

        public Control(FC game, ControlInfo info, Vector2 relPos, Vector2 relSize, ControlAction action)
            : base(game, relPos, relSize) {
                Action = action;
                pressed = false;
                border = new CorneredBorder(this, "Galactica");
                fill = new CorneredFill(this, "Galactica");
                Info = info;
        }

        public override void Initialize() {
            base.Initialize();
            FC.InputManager.Register(Input.Actions.Select);
            FC.InputManager.Register(Input.Actions.Hover);
            FC.InputManager.Save();
        }

        public override void LoadContent() {
            base.LoadContent();
            if (Info != null) {
                Info.LoadContent();
            }
            border.LoadContent();
            fill.LoadContent();
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            if (Info != null) {
                Info.Update(this);
            }
            
            if (FC.InputManager.CheckAction(Input.Actions.Hover, this).Active) {
                hovering = true;
            } else {
                hovering = false;
            }
            if (pressed && !FC.InputManager.CheckAction(Input.Actions.Select, this).Active) {
                if (Action != null) {
                    Action(this);
                }
                pressed = false;
            }
            pressed = FC.InputManager.CheckAction(Input.Actions.Select, this).Active;
        }

        public override void BeforeDraw(GameTime gameTime) {
            SpriteBatch spriteBatch = FC.SpriteBatch;

            if (hovering) {
                fill.Draw();
            }

            border.Draw();
            base.BeforeDraw(gameTime);
        }

        public override void Draw(GameTime gameTime) {
            base.Draw(gameTime);
            if (Info != null) {
                Info.Draw(this);
            }
        }
    }
}
