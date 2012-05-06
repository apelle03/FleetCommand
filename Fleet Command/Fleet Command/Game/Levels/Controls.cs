using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Fleet_Command.Game.GameInfo;
using Fleet_Command.Game.Players;
using Fleet_Command.Decorators;

namespace Fleet_Command.Game.Levels {
    class Controls : RelativeSizeComponent<Control> {
        protected Level level;
        protected PlayArea playArea;

        protected CorneredBorder border;

        protected List<Control> buttonList;

        protected List<ControlInfo> buildList;
        protected List<ControlInfo> launchList;
        protected List<ControlInfo> selectList;

        public Controls(FC game, Level level, PlayArea playArea)
            : this(game, level, playArea, Vector2.Zero, Vector2.Zero) {
        }

        public Controls(FC game, Level level, PlayArea playArea, Vector2 relPos, Vector2 relSize)
            : this(game, level, playArea, relPos, relSize, Color.Transparent) {
        }

        public Controls(FC game, Level level, PlayArea playArea, Vector2 relPos, Vector2 relSize, Color color)
            : base(game, relPos, relSize, color) {
                this.level = level;
                this.playArea = playArea;
            
                border = new CorneredBorder(this, "GalacticaSquare");

                buildList = new List<ControlInfo>();
                foreach (ConstructableInfo ci in Constructables.ConstructableList) {
                    if (ci.Faction == level.Controller.Name) {
                        //buildList.Add();
                    }
                }
                launchList = new List<ControlInfo>();
                selectList = new List<ControlInfo>();

                Components.Add(new Control(game, new ControlInfo(game, "Menu/build"), new Vector2(relPos.X + relSize.X * .011f, relPos.Y + relSize.Y * .09f),
                    new Vector2(relSize.X * .07335f, relSize.Y * .273f), Build));
                Components.Add(new Control(game, new ControlInfo(game, "Menu/launch"), new Vector2(relPos.X + relSize.X * .011f, relPos.Y + relSize.Y * .368f),
                    new Vector2(relSize.X * .07335f, relSize.Y * .273f), Launch));
                Components.Add(new Control(game, new ControlInfo(game, "Menu/select"), new Vector2(relPos.X + relSize.X * .011f, relPos.Y + relSize.Y * .646f),
                    new Vector2(relSize.X * .07335f, relSize.Y * .273f), Select));

                Components.Add(new Control(game, new ControlInfo(game, "Menu/up"), new Vector2(relPos.X + relSize.X - relSize.X * .011f - relSize.X * .025f, relPos.Y + relSize.Y * .09f),
                    new Vector2(relSize.X * .02445f, relSize.Y * .41f), Up));
                Components.Add(new Control(game, new ControlInfo(game, "Menu/down"), new Vector2(relPos.X + relSize.X - relSize.X * .011f - relSize.X * .025f, relPos.Y + relSize.Y * .5f),
                    new Vector2(relSize.X * .02445f, relSize.Y * .41f), Down));

                buttonList = new List<Control>();
                for (int i = 0; i < 2; i++) {
                    for (int j = 0; j < 9; j++) {
                        buttonList.Add(new Control(game, new Vector2(relPos.X + relSize.X * .08435f + relSize.X * .0978f * j, relPos.Y + relSize.Y * .09f + relSize.Y * .41f * i),
                                new Vector2(relSize.X * .0978f, relSize.Y * .41f), Default));
                    }
                }
                foreach (Control c in buttonList) {
                    Components.Add(c);
                }
        }

        public override void LoadContent() {
            base.LoadContent();
            border.LoadContent();
        }

        public override void BeforeDraw(Microsoft.Xna.Framework.GameTime gameTime) {
            base.BeforeDraw(gameTime);
            border.Draw();
        }

        public void Default() {
        }

        public void Build() {
        }

        public void Launch() {
        }

        public void Select() {
        }

        public void Up() {
        }

        public void Down() {
        }
    }
}
