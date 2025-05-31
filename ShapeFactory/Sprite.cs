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

        // 'interval' is the time in seconds that will elapse before the next from is displayed
        public Sprite(ShapeType type, Transform2D transform, Image[] frames, double interval) : base(type, transform, Color.White) {
            Animated = true;
            Interval = interval;
            Frames = new List<Image>(frames);

            elapsed = 0.0;
            currentFrame = 0;
        }
        // Non-animated sprite with multiple frames for manual changing.
        public Sprite(ShapeType type, Transform2D transform, Image[] frames) : base(type, transform, Color.White) {
            Animated = false;
            Frames = new List<Image>(frames);

            elapsed = 0.0;
            currentFrame = 0;
        }

        public void SetCurrentFrame(int frame) {
            if(frame < Frames.Count && frame >= 0) {
                currentFrame = frame;
            }
        }

        public override void Update(double dt) {
            if (!Animated) return;

            // Change frames
            elapsed += dt;
            if(elapsed >= Interval) {
                elapsed = 0.0;

                currentFrame = (currentFrame + 1) % Frames.Count;
            }
        }

        public override void Draw(Graphics g) {
            if (Type == ShapeType.Rectangle) {
                /* This was a solution for the uv offset when using the texture brush with a rectangle
                 * I later decided to just draw the image when using a rectangle so I could resize the rectangle while retaining the entire image
                tb.WrapMode = WrapMode.Clamp;

                // This accounts for offset when using texture brush
                var displayArea = new Rectangle((int)(Transform.Position.X - Transform.Size.X / 2.0f), (int)(Transform.Position.Y - Transform.Size.Y / 2.0f), (int)Transform.Size.X, (int)Transform.Size.Y);
                Point xDisplayCenterRelative = new Point(displayArea.Width / 2, displayArea.Height / 2); //Find the relative center location of DisplayArea
                Point xImageCenterRelative = new Point(Frames[currentFrame].Width / 2, Frames[currentFrame].Height / 2); //Find the relative center location of Image
                Point xOffSetRelative = new Point(xDisplayCenterRelative.X - xImageCenterRelative.X, xDisplayCenterRelative.Y - xImageCenterRelative.Y);
                Point xAbsolutePixel = xOffSetRelative + new Size(displayArea.Location); //Find the absolute location
                tb.TranslateTransform(xAbsolutePixel.X, xAbsolutePixel.Y); */

                int x, y, w, h;
                x = (int)(Transform.Position.X - Transform.Size.X / 2.0);
                y = (int)(Transform.Position.Y - Transform.Size.Y / 2.0);
                w = (int)Transform.Size.X;
                h = (int)Transform.Size.Y;

                // Save state to restore it after drawing
                var state = g.Save();

                g.TranslateTransform(Transform.Position.X, Transform.Position.Y);
                g.RotateTransform(Transform.Rotation);
                g.TranslateTransform(-Transform.Position.X, -Transform.Position.Y);

                g.DrawImage(Frames[currentFrame], new Rectangle(x, y, w, h), new Rectangle(0, 0, Frames[currentFrame].Width, Frames[currentFrame].Height), GraphicsUnit.Pixel);

                g.Restore(state);
                return;
            }

            // Use texture brush for triangles and circles
            var tb = new TextureBrush(Frames[currentFrame]);
            FillBrush = tb;
            base.Draw(g);
        }
    }
}
