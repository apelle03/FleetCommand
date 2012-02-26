using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Fleet_Command.Menus;
using Fleet_Command.Input;

namespace Fleet_Command {
    public class FC : Microsoft.Xna.Framework.Game {
        protected GraphicsDeviceManager graphics;
        protected SpriteBatch spriteBatch;

        protected InputManager inputManager;
        public InputManager InputManager { get { return inputManager; } }
        protected string settingsDir;
        public string SettingsDir { get { return settingsDir; } }

        public FC() {
            settingsDir = ".\\";

            graphics = new GraphicsDeviceManager(this);

            //graphics.PreferredBackBufferWidth = 1680;
            //graphics.PreferredBackBufferHeight = 1050;
            //graphics.IsFullScreen = true;
            this.IsMouseVisible = true;

            Content.RootDirectory = "Content";

            inputManager = new InputManager(this);
            Components.Add(new MainMenu(this, new Vector2(0.05f, 0.05f), new Vector2(0.9f, 0.9f), Color.Black));
        }

        protected override void Initialize() {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService(typeof(SpriteBatch), spriteBatch);

            foreach (IGameComponent gc in Components) {
                if (gc is DGC) {
                    DGC dgc = (DGC)gc;
                    dgc.LoadContent();
                }
            }
        }

        protected override void UnloadContent() {
            foreach (IGameComponent gc in Components) {
                if (gc is DGC) {
                    DGC dgc = (DGC)gc;
                    dgc.UnloadContent();
                }
            }
        }

        protected override void Update(GameTime gameTime) {
            inputManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
}
