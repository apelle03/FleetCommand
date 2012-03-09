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

using Fleet_Command.Game;
using Fleet_Command.Menus;
using Fleet_Command.Input;

namespace Fleet_Command {
    public class FC : Microsoft.Xna.Framework.Game {
        protected GraphicsDeviceManager graphics;
        protected SpriteBatch spriteBatch;
        public SpriteBatch SpriteBatch { get { return spriteBatch; } }

        protected InputManager inputManager;
        public InputManager InputManager { get { return inputManager; } }
        protected string settingsDir;
        public string SettingsDir { get { return settingsDir; } }

        protected MainMenu mainMenu;
        protected PauseMenu pauseMenu;
        protected Level level;

        public FC() {
            settingsDir = ".\\";

            graphics = new GraphicsDeviceManager(this);
            
            //graphics.PreferMultiSampling = true;
            //graphics.PreferredBackBufferWidth = 1680;
            //graphics.PreferredBackBufferHeight = 1050;
            //graphics.IsFullScreen = true;
            this.IsMouseVisible = true;

            Content.RootDirectory = "Content";

            inputManager = new InputManager(this);
            mainMenu = new MainMenu(this, Vector2.Zero, Vector2.One, Color.Black);
            mainMenu.DrawOrder = 10;
            pauseMenu = new PauseMenu(this, new Vector2(.35f, .25f), new Vector2(.3f, .5f), Color.Black);
            pauseMenu.Enabled = false;
            pauseMenu.Visible = false;
            pauseMenu.DrawOrder = 1;
            Components.Add(mainMenu);
            Components.Add(pauseMenu);
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

        // Main Menu functionality
        public void StartGame() {
            mainMenu.Enabled = false;
            mainMenu.Visible = false;
            level = new Level(this);
            level.DrawOrder = 10;
            level.Initialize();
            level.LoadContent();
            Components.Add(level);
        }

        public void Options() {
        }

        public void Quit() {
            Exit();
        }

        // Pause Menu funcitonality
        public void Resume() {
            pauseMenu.Visible = false;
            pauseMenu.Enabled = false;
            level.Enabled = true;
        }
        
        public void MainMenu() {
            Components.Remove(level);
            level = null;
            pauseMenu.Visible = false;
            pauseMenu.Enabled = false;
            mainMenu.Enabled = true;
            mainMenu.Visible = true;
        }

        // Level functionality
        public void Pause() {
            level.Enabled = false;
            pauseMenu.Enabled = true;
            pauseMenu.Visible = true;
        }
    }
}
