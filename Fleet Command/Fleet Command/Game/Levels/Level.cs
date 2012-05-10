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
                PlayArea playArea = new PlayArea(game, this, relativePos, relativeSize, relativePos, new Vector2(relativeSize.X, relativeSize.Y * .8f));
                ResourceMonitor resourceMonitor = new ResourceMonitor(game, Controller, relativePos, new Vector2(relativeSize.X * .15f, relativeSize.Y * .2f));
                // debug
                //ResourceMonitor resourceMonitor2 = new ResourceMonitor(game, Players[2], relativePos + new Vector2(relativeSize.X * .15f, 0), new Vector2(relativeSize.X * .15f, relativeSize.Y * .2f));
                Controls controls = new Controls(game, this, playArea, new Vector2(relativePos.X, relativePos.Y + relativeSize.Y * .8f), new Vector2(relativeSize.X, relativeSize.Y * .2f), Color.Black * .75f);

                playArea.UpdateOrder = 0;          playArea.DrawOrder = 0;
                controls.UpdateOrder = 100;        controls.DrawOrder = 100;
                resourceMonitor.UpdateOrder = 200; resourceMonitor.DrawOrder = 200;

                //debug
                //resourceMonitor2.UpdateOrder = 200; resourceMonitor2.DrawOrder = 200;
                
                Components.Add(playArea);    
                Components.Add(controls);
                Components.Add(resourceMonitor);

                // debug
                //Components.Add(resourceMonitor2);
        }

        public override void Initialize() {
            base.Initialize();
            FC.InputManager.Register(Input.Actions.PauseMenu);
            FC.InputManager.Save();
        }

        public override void Update(GameTime gameTime) {
            foreach (Player p in players) {
                p.Update();
            }
            base.Update(gameTime);
            if (FC.InputManager.CheckAction(Input.Actions.PauseMenu, this).Active) {
                FC.Pause();
            }
        }
    }
}
