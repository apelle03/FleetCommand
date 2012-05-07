using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Fleet_Command.Game.GameInfo;
using Fleet_Command.Game.Objects;
using Fleet_Command.Game.Players;
using Fleet_Command.Decorators;

namespace Fleet_Command.Game.Levels {
    public enum ControlState { Build, Launch, Select };

    public class Controls : RelativeSizeComponent<Control> {
        protected Level level;
        protected PlayArea playArea;

        protected ControlState state;
        protected CapitalShip controller;

        protected CorneredBorder border;

        protected List<Control> topButtons, bottomButtons;

        protected List<BuildInfo> buildList;
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

                state = ControlState.Build;
            
                border = new CorneredBorder(this, "GalacticaSquare");

                buildList = new List<BuildInfo>();
                foreach (ConstructableInfo ci in Constructables.ConstructableList) {
                    if (ci.Faction == level.Controller.Name) {
                        buildList.Add(new BuildInfo(game, ci, null));
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
                    new Vector2(relSize.X * .02445f, relSize.Y * .205f), TopUp));
                Components.Add(new Control(game, new ControlInfo(game, "Menu/down"), new Vector2(relPos.X + relSize.X - relSize.X * .011f - relSize.X * .025f, relPos.Y + relSize.Y * .295f),
                    new Vector2(relSize.X * .02445f, relSize.Y * .205f), TopDown));
                Components.Add(new Control(game, new ControlInfo(game, "Menu/up"), new Vector2(relPos.X + relSize.X - relSize.X * .011f - relSize.X * .025f, relPos.Y + relSize.Y * .5f),
                    new Vector2(relSize.X * .02445f, relSize.Y * .205f), BottomUp));
                Components.Add(new Control(game, new ControlInfo(game, "Menu/down"), new Vector2(relPos.X + relSize.X - relSize.X * .011f - relSize.X * .025f, relPos.Y + relSize.Y * .705f),
                    new Vector2(relSize.X * .02445f, relSize.Y * .205f), BottomDown));

                topButtons = new List<Control>();
                bottomButtons = new List<Control>();
                for (int i = 0; i < 9; i++) {
                    topButtons.Add(new Control(game, new Vector2(relPos.X + relSize.X * .08435f + relSize.X * .0978f * i, relPos.Y + relSize.Y * .09f),
                            new Vector2(relSize.X * .0978f, relSize.Y * .41f), Default));
                    bottomButtons.Add(new Control(game, new Vector2(relPos.X + relSize.X * .08435f + relSize.X * .0978f * i, relPos.Y + relSize.Y * .09f + relSize.Y * .41f),
                            new Vector2(relSize.X * .0978f, relSize.Y * .41f), Default));
                }
                foreach (Control c in topButtons) {
                    Components.Add(c);
                }
                foreach (Control c in bottomButtons) {
                    Components.Add(c);
                }
        }

        public override void LoadContent() {
            base.LoadContent();
            border.LoadContent();

            foreach (ControlInfo ci in buildList) {
                ci.LoadContent();
            }
        }

        public override void Update(GameTime gameTime) {
            foreach (Control c in topButtons) {
                c.Info = null;
                c.Action = null;
            }
            foreach (Control c in bottomButtons) {
                c.Info = null;
                c.Action= null;
            }
            controller = null;
            if (state == ControlState.Build) {
                List<Unit> selection = playArea.Selection;
                foreach (Unit u in selection) {
                    if (u is CapitalShip) {
                        CapitalShip c = (CapitalShip)u;
                        controller = c;
                        for (int i = 0; i < 9 && i < buildList.Count; i++) {
                            topButtons[i].Info = buildList[i];
                            topButtons[i].Action = BuildUnit;
                        }
                        foreach (BuildInfo bi in buildList) {
                            bi.CapitalShip = c;
                        }
                        for (int i = 0; i < 9 && i < controller.BuildQueue.Count; i++) {
                            bottomButtons[i].Info = controller.BuildQueue[i];
                            bottomButtons[i].Action = CancelUnit;
                        }
                        break;
                    }
                }
            } else if (state == ControlState.Launch) {
                List<Unit> selection = playArea.Selection;
                foreach (Unit u in selection) {
                    if (u is CapitalShip) {
                        CapitalShip c = (CapitalShip)u;
                        controller = c;
                        for (int i = 0; i < 9 && i < c.Docked.Count; i++) {
                            topButtons[i].Info = c.Docked[i].UnitInfo;
                            topButtons[i].Action = LaunchUnit;
                        }
                        break;
                    }
                }
            } else if (state == ControlState.Select) {
            }
            base.Update(gameTime);
        }

        public override void BeforeDraw(Microsoft.Xna.Framework.GameTime gameTime) {
            base.BeforeDraw(gameTime);
            border.Draw();
        }

        public void Default(Control control) {
        }

        public void Build(Control control) {
            state = ControlState.Build;
        }

        public void Launch(Control control) {
            state = ControlState.Launch;
        }

        public void Select(Control control) {
            state = ControlState.Select;
        }

        public void TopUp(Control control) {
        }

        public void TopDown(Control control) {
        }

        public void BottomUp(Control control) {
        }

        public void BottomDown(Control control) {
        }

        public void BuildUnit(Control control) {
            if (control.Info != null && control.Info is BuildInfo) {
                BuildQueueInfo bi = new BuildQueueInfo(FC, ((BuildInfo)control.Info).Info, controller);
                bi.LoadContent();
                controller.BuildQueue.Add(bi);
            }
        }

        public void CancelUnit(Control control) {
            if (control.Info != null && control.Info is BuildQueueInfo) {
                controller.CancelBuild((BuildQueueInfo)control.Info);
            }
        }

        public void LaunchUnit(Control control) {
            if (control.Info != null && control.Info is ShipInfo && ((ShipInfo)control.Info).Unit is CombatShip) {
                CombatShip cs = ((CombatShip)((ShipInfo)control.Info).Unit);
                cs.LaunchCommand(cs.Hangar, true);
            }
        }
    }
}
