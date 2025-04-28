using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeFactory {
    public class Sprite : Shape {
        public bool Animated;
        public List<Image> Frames;
        public double Interval;
        private double elapsed;
        private int currentFrame;

        public Sprite(ShapeType type, Transform2D transform, Image image): base(type, transform, Color.White) {
            Animated = false;
            Frames = new List<Image>();
            Frames.Add(image);

            elapsed = 0.0;
            currentFrame = 0;
        }
        public Sprite(ShapeType type, Transform2D transform, Image[] frames, double interval) : base(type, transform, Color.White) {
            Animated = true;
            Interval = interval;
            Frames = new List<Image>(frames);

            elapsed = 0.0;
            currentFrame = 0;
        }

        public override void Update(double dt) {
            if (!Animated) return;

            // Change frames
            elapsed += dt;
            if(elapsed >= Interval) {
                elapsed = 0.0;

                currentFrame++;
                if(currentFrame >= Frames.Count) {
                    currentFrame = 0;
                }
            }
        }

        public override void Draw(Graphics g) {
            var tb = new TextureBrush(Frames[currentFrame]);

            if (Type == ShapeType.Rectangle) {
                tb.WrapMode = WrapMode.Clamp;

                // This accounts for offset when using texture brush
                var displayArea = new Rectangle((int)(Transform.Position.X - Transform.Size.X / 2.0f), (int)(Transform.Position.Y - Transform.Size.Y / 2.0f), (int)Transform.Size.X, (int)Transform.Size.Y);
                Point xDisplayCenterRelative = new Point(displayArea.Width / 2, displayArea.Height / 2); //Find the relative center location of DisplayArea
                Point xImageCenterRelative = new Point(Frames[currentFrame].Width / 2, Frames[currentFrame].Height / 2); //Find the relative center location of Image
                Point xOffSetRelative = new Point(xDisplayCenterRelative.X - xImageCenterRelative.X, xDisplayCenterRelative.Y - xImageCenterRelative.Y);
                Point xAbsolutePixel = xOffSetRelative + new Size(displayArea.Location); //Find the absolute location
                tb.TranslateTransform(xAbsolutePixel.X, xAbsolutePixel.Y);
            }

            FillBrush = tb;
            base.Draw(g);
        }
    }
}
