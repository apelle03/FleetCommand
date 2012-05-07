using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Fleet_Command.Game;
using Fleet_Command.Game.GameInfo;
using Fleet_Command.Game.Levels;
using Fleet_Command.Game.Players;
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
        protected WinMenu winMenu;
        protected LoseMenu loseMenu;
        protected Level level;

        public FC() {
            settingsDir = "./";

            graphics = new GraphicsDeviceManager(this);

            StreamReader reader = new StreamReader(settingsDir + "resolution.ini");
            int width = Int32.Parse(reader.ReadLine());
            int height = Int32.Parse(reader.ReadLine());
            reader.Close();

            DisplayModeCollection dmc = GraphicsAdapter.DefaultAdapter.SupportedDisplayModes;
            bool good = false;
            foreach (DisplayMode dm in dmc) {
                if (dm.Width == width && dm.Height == height) {
                    graphics.PreferredBackBufferWidth = width;
                    graphics.PreferredBackBufferHeight = height;
                    good = true;
                    break;
                }
            }
            
            if (!good) {
                graphics.PreferredBackBufferWidth = 1280;
                graphics.PreferredBackBufferHeight = 800;
            }
            graphics.PreferMultiSampling = true;
            graphics.IsFullScreen = true;
            
            this.IsMouseVisible = true;

            Content.RootDirectory = "Content";

            inputManager = new InputManager(this);
            mainMenu = new MainMenu(this, Vector2.Zero, Vector2.One, Color.Black);
            mainMenu.DrawOrder = 10;
            pauseMenu = new PauseMenu(this, new Vector2(.35f, .25f), new Vector2(.3f, .5f), Color.Black * .75f);
            pauseMenu.Enabled = false;
            pauseMenu.Visible = false;
            pauseMenu.DrawOrder = 10;
            winMenu = new WinMenu(this, Vector2.Zero, Vector2.One, Color.Black);
            winMenu.Enabled = false;
            winMenu.Visible = false;
            winMenu.DrawOrder = 10;
            loseMenu = new LoseMenu(this, Vector2.Zero, Vector2.One, Color.Black);
            loseMenu.Enabled = false;
            loseMenu.Visible = false;
            loseMenu.DrawOrder = 10;
            Components.Add(mainMenu);
            Components.Add(pauseMenu);
            Components.Add(winMenu);
            Components.Add(loseMenu);
        }

        protected override void Initialize() {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent() {
            GraphicsDevice.PresentationParameters.RenderTargetUsage = RenderTargetUsage.PreserveContents;

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
            List<Player> players = new List<Player>();
            players.Add(new Player(0, "Neutral", Color.Gray));
            players.Add(new Player(1, "Human", Color.Blue));
            players.Add(new Player(2, "Cylon", Color.Red));
            foreach (ResourceInfo ri in Resources.ResourceList) {
                players[1].Supply(ri.Name, 500);
                players[2].Supply(ri.Name, 500);
            }
            level = new Level(this, players, players[1]);
            level.DrawOrder = 1;
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

        public void WinMenu() {
            if (level != null) {
                Components.Remove(level);
                level = null;
            }
            winMenu.Enabled = true;
            winMenu.Visible = true;
        }

        public void LoseMenu() {
            if (level != null) {
                Components.Remove(level);
                level = null;
            }
            loseMenu.Enabled = true;
            loseMenu.Visible = true;
        }
        
        public void MainMenu() {
            if (level != null) {
                Components.Remove(level);
                level = null;
            }
            pauseMenu.Visible = false;
            pauseMenu.Enabled = false;
            winMenu.Visible = false;
            winMenu.Enabled = false;
            loseMenu.Visible = false;
            loseMenu.Visible = false;
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
