using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Fleet_Command.Game.Players;

namespace Fleet_Command.Game.Levels {
    public class Level : RelativeSizeComponent<DGC> {

        protected List<Player> players;
        public List<Player> Players { get { return players; } }

        protected Player controller;
        public Player Controller { get { return controller; } }

        public Level(FC game, List<Player> players, Player controller)
            : base(game, Vector2.Zero, Vector2.One, Color.Black) {
                this.players = players;
                this.controller = controller;
                
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
                ResourceMonitor resourceMonitor = new ResourceMonitor(game, Controller, relativePos, new Vector2(relativeSize.X * .2f, relativeSize.Y * .2f));
                Controls controls = new Controls(game, new Vector2(relativePos.X, relativePos.Y + relativeSize.Y * .8f), new Vector2(relativeSize.X, relativeSize.Y * .2f), Color.Black * .75f);
                PlayArea playArea = new PlayArea(game, this, relativePos, relativeSize, relativePos, new Vector2(relativeSize.X, relativeSize.Y * .8f));
                playArea.DrawOrder = 0;
                controls.DrawOrder = 1;
                resourceMonitor.DrawOrder = 2;
                Components.Add(playArea);    
                Components.Add(controls);
                Components.Add(resourceMonitor);
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
