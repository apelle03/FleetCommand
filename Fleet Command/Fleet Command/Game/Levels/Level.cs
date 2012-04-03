using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Fleet_Command.Game.Levels {
    public class Level : RelativeSizeComponent<DGC> {

        public Level(FC game)
            : base(game, Vector2.Zero, Vector2.One, Color.Black) {
                int width = FC.GraphicsDevice.PresentationParameters.BackBufferWidth;
                int height = FC.GraphicsDevice.PresentationParameters.BackBufferHeight;
                Random rand = new Random();
                for (int i = 0; i < 50; i++) {
                    int type = rand.Next(4);
                    int x = rand.Next(width);
                    int y = rand.Next(height);
                    switch (type) {
                        case 0:
                            Components.Add(new LevelComponent(game, "Background/star_15", new Vector2(x, y), 0));
                            break;
                        case 1:
                            Components.Add(new LevelComponent(game, "Background/star_10", new Vector2(x, y), 0));
                            break;
                        case 2:
                            Components.Add(new LevelComponent(game, "Background/star_5", new Vector2(x, y), 0));
                            break;
                        case 3:
                        default:
                            Components.Add(new LevelComponent(game, "Background/star_2", new Vector2(x, y), 0));
                            break;
                    }
                }
                Controls controls = new Controls(game, new Vector2(relativePos.X, relativePos.Y + relativeSize.Y * .8f), new Vector2(relativeSize.X, relativeSize.Y * .2f), Color.Black * .75f);
                PlayArea playArea = new PlayArea(game, relativePos, relativeSize, relativePos, new Vector2(relativeSize.X, relativeSize.Y * .8f));
                Components.Add(playArea);    
                Components.Add(controls);
        }

        public override void Initialize() {
            base.Initialize();
            FC.InputManager.Register(Input.Actions.PauseMenu);
            FC.InputManager.Save();
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            if (FC.InputManager.CheckAction(Input.Actions.PauseMenu, this).Active) {
                FC.Pause();
            }
        }
    }
}
