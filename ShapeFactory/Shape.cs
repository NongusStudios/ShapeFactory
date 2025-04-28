using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;

namespace ShapeFactory {
    public class Shape : Drawable {
        public ShapeType Type;
        public Transform2D Transform;
        public Brush FillBrush;

        public Shape(ShapeType type, Transform2D transform, Color colour) : base() {
            this.Type = type;
            this.Transform = transform;
            ChangeColour(colour);
        }

        public void ChangeColour(Color colour) {
            FillBrush = new SolidBrush(colour);
        }

        public override void Draw(Graphics g) {
            // Transform to pixel coords
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

            switch (Type) {
                case ShapeType.Rectangle:
                    g.FillRectangle(FillBrush, new Rectangle(x, y, w, h));
                    break;
                case ShapeType.Circle:
                    g.FillEllipse(FillBrush, new Rectangle(x, y, w, w));
                    break;
                case ShapeType.Triangle:
                    x = (int)Transform.Position.X;
                    y = (int)Transform.Position.Y;
                    Point[] points = new Point[3];
                    points[0] = new Point(x, y - h / 2);
                    points[1] = new Point(x + w / 2, y + h / 2);
                    points[2] = new Point(x - w / 2, y + h / 2);
                    g.FillPolygon(FillBrush, points);
                    break;
            }

            g.Restore(state);
        }
    }
}
