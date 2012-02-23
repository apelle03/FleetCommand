using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Fleet_Command.Input {
    public enum Actions { Click };

    public class InputManager : DGC {

        protected Dictionary<Actions, InputItem> bindings;
        protected Dictionary<Keys, KeyState> keyboardState;
        protected MouseState mouseState;

        public InputManager(FC game)
            : base(game) {
                bindings = new Dictionary<Actions, InputItem>();
                keyboardState = new Dictionary<Keys, KeyState>();
        }

        public void Register(Actions action) {
            BitArray mouse = new BitArray(Enum.GetValues(typeof(MouseButtons)).Length);
            mouse.Set((int)MouseButtons.Left, true);
            mouse.Set((int)MouseButtons.XY, true);
            InputItem ii = new InputItem(new List<Keys>(), mouse);
            bindings.Add(action, ii);
            foreach (Keys key in ii.KeyboardRequirements) {
                keyboardState.Add(key, KeyState.Up);
            }
        }

        public int CheckAction(Actions action, DGC actor) {
            InputItem combo;
            if ((combo = bindings[action]) != null) {
                foreach (Keys key in combo.KeyboardRequirements) {
                    if (keyboardState[key] == KeyState.Up)
                        return 0;
                }
                if (combo.MouseRequirements.Get((int)MouseButtons.Left) && mouseState.LeftButton == ButtonState.Released) {
                    return 0;
                }
                if (combo.MouseRequirements.Get((int)MouseButtons.Middle) && mouseState.MiddleButton == ButtonState.Released) {
                    return 0;
                }
                if (combo.MouseRequirements.Get((int)MouseButtons.Right) && mouseState.RightButton == ButtonState.Released) {
                    return 0;
                }
                if (combo.MouseRequirements.Get((int)MouseButtons.X1) && mouseState.XButton1 == ButtonState.Released) {
                    return 0;
                }
                if (combo.MouseRequirements.Get((int)MouseButtons.X2) && mouseState.XButton2 == ButtonState.Released) {
                    return 0;
                }
                if (combo.MouseRequirements.Get((int)MouseButtons.XY)) {
                    Rectangle boundingBox = actor.BoundingBox;
                    if (mouseState.X < boundingBox.X || mouseState.X > boundingBox.X + boundingBox.Width ||
                        mouseState.Y < boundingBox.Y || mouseState.Y > boundingBox.Y + boundingBox.Height) {
                        return 0;
                    }
                }
                if (combo.MouseRequirements.Get((int)MouseButtons.Wheel)) {
                    return mouseState.ScrollWheelValue;
                }
            }
            return 1;
        }

        public override void Update(GameTime gameTime) {
            KeyboardState keyState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            foreach (Keys key in keyboardState.Keys) {
                if (keyState.IsKeyUp(key))
                    keyboardState[key] = KeyState.Up;
                else
                    keyboardState[key] = KeyState.Down;
            }
            base.Update(gameTime);
        }
    }
}
