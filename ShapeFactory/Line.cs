using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ShapeFactory {
    public class Line : Drawable {
        public List<Vector2> Points;
        public Pen DrawPen;

        public Line(Vector2[] points, float width, Color colour) : base() {
            Points = new List<Vector2>(points);
            ChangeColourAndWidth(colour, width);
        }

        public void ChangeColourAndWidth(Color colour, float width) {
            DrawPen = new Pen(colour, width);
        }

        public override void Draw(Graphics g) {
            if (Points.Count <= 1) return;
            var pointsF = new PointF[Points.Count];
            for (int i = 0; i < Points.Count; i++) {
                pointsF[i] = new PointF(Points[i].X, Points[i].Y);
            }
            g.DrawLines(DrawPen, pointsF);
        }
    }
}
