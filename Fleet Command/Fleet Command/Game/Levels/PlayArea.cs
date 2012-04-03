﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Fleet_Command.Input;

namespace Fleet_Command.Game.Levels {
    public class PlayArea : RelativeSizeComponent<Unit> {
        protected Vector2 selectablePos, selectableSize;
        protected Rectangle selectableArea;
        protected SelectionBox selectionBox;
        protected List<Unit> selection;

        protected Viewport viewport;

        public PlayArea(FC game)
            : this(game, Vector2.Zero, Vector2.Zero) {
        }
        
        public PlayArea(FC game, Vector2 relPos, Vector2 relSize)
            : this(game, relPos, relSize, Color.Transparent) {
        }

        public PlayArea(FC game, Vector2 relPos, Vector2 relSize, Color color)
            : this(game, relPos, relSize, relPos, relSize, color) {
        }

        public PlayArea(FC game, Vector2 relPos, Vector2 relSize, Vector2 selectPos, Vector2 selectSize)
            : this(game, relPos, relSize, selectPos, selectSize, Color.Transparent) {
        }

        public PlayArea(FC game, Vector2 relPos, Vector2 relSize, Vector2 selectPos, Vector2 selectSize, Color color)
            : base(game, relPos, relSize, color) {
                selectablePos = selectPos;
                selectableSize = selectSize;
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

            int height = BoundingBox.Height;
            int width = BoundingBox.Width;
            selectableArea = new Rectangle((int)(width * selectablePos.X), (int)(height * selectablePos.Y),
                (int)(width * selectableSize.X), (int)(height * selectableSize.Y));

            selectionBox.LoadContent();

            viewport = new Viewport(Vector2.Zero, new Vector2(width, height), 1);
        }

        public override void Update(GameTime gameTime) {
            ComboInfo click = FC.InputManager.CheckAction(Actions.Click, this);
            if (click.Active && !selectionBox.Active) {
                if (selectableArea.Contains((int)click.X, (int)click.Y)) {
                    selectionBox.SetStart((int)click.X, (int)click.Y);
                }
            } else if (click.Active && selectionBox.Active) {
                int x = (int)click.X;
                int y = (int)click.Y;
                if (x < selectableArea.Left) x = selectableArea.Left;
                if (x > selectableArea.Right) x = selectableArea.Right;
                if (y < selectableArea.Top) y = selectableArea.Top;
                if (y > selectableArea.Bottom) y = selectableArea.Bottom;
                selectionBox.SetCorner(x, y);
            } else if (!click.Active && selectionBox.Active) {
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
            //SpriteBatch spriteBatch = FC.SpriteBatch;
            //spriteBatch.End();

            //RenderTarget2D buffer = new RenderTarget2D(spriteBatch.GraphicsDevice, 100 /*map.width*/, 100 /*map.height*/);

            //spriteBatch.GraphicsDevice.SetRenderTarget(buffer);
            //spriteBatch.Begin();
            base.Draw(gameTime);
            //spriteBatch.End();

            //spriteBatch.GraphicsDevice.SetRenderTarget(null);
            //spriteBatch.Begin();
            //spriteBatch.Draw(buffer, BoundingBox, Color.White); // use offset of map to calculate region to draw

            if (selectionBox.Active) {
                selectionBox.Draw(gameTime);
            }
        }
    }
}
