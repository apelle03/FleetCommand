﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Fleet_Command.Decorators;

namespace Fleet_Command.Menus {
    public delegate void ClickAction();
    class Button : MenuComponent {

        protected Border border;
        protected Fill fill;


        protected bool hovering;
        protected bool pressed;
        protected ClickAction clickAction;

        public Button(FC game, Vector2 relPos, Vector2 relSize, string text, ClickAction action)
            : base(game, relPos, relSize, text) {
                hovering = false;
                clickAction = action;
                pressed = false;
                border = new Border(this, "Basic");
                fill = new Fill(this, "Basic");
        }

        public override void Initialize() {
            base.Initialize();
            FC.InputManager.Register(Input.Actions.Click);
            FC.InputManager.Register(Input.Actions.Hover);
            FC.InputManager.Save();
        }

        public override void LoadContent() {
            base.LoadContent();
            border.LoadContent();
            fill.LoadContent();
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
                fill.Draw();
            }

            border.Draw();

            base.BeforeDraw(gameTime);
        }
    }
}
