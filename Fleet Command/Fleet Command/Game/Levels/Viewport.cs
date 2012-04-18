using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Fleet_Command.Game.Levels {
    public class Viewport {
        private static float SCROLL_RATE = 1;
        private static float ZOOM_RATE = .1f;

        public Vector2 Center { get; set; }
        private Vector2 size;
        public Vector2 Size { get { return new Vector2(size.X / Zoom, size.Y / Zoom); } private set { size = value; } }
        public float Zoom { get; set; }

        private Vector2 scrollRate;
        public float ScrollRate { get { return scrollRate.X; } set { scrollRate.X = value; scrollRate.Y = value; } }
        public float ZoomRate { get;  set; }

        private Rectangle viewArea;
        public Rectangle ViewArea { get { return viewArea; } }

        public Viewport(Vector2 center, Vector2 size, float zoom) {
            Center = center;
            Size = size;
            Zoom = zoom;
            scrollRate = Vector2.One;
            ScrollRate = SCROLL_RATE;
            ZoomRate = ZOOM_RATE;
            viewArea = new Rectangle((int)(Center.X - Size.X / 2), (int)(Center.Y - Size.Y / 2), (int)(Size.X), (int)(Size.Y));
        }

        public void Scroll(Vector2 amount) {
            Center += amount * scrollRate;
            viewArea.X = (int)(Center.X - Size.X / 2);
            viewArea.Y = (int)(Center.Y - Size.Y / 2);
        }

        public void ChangeZoom(float amount) {
            Zoom += amount * ZoomRate;
            viewArea.X = (int)(Center.X - Size.X / 2);
            viewArea.Y = (int)(Center.Y - Size.Y / 2);
            viewArea.Width = (int)(Size.X);
            viewArea .Height = (int)(Size.Y);
        }

        public Matrix Transformation() {
            return Matrix.CreateTranslation(new Vector3(-Center.X, -Center.Y, 0)) *
                Matrix.CreateScale(new Vector3(Zoom, Zoom, 1));
        }
    }
}
