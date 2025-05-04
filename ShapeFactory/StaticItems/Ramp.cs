using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShapeFactory.StaticItems {
    public class Ramp : StaticItem {
        public const float LINE_WIDTH = 5.0f;
        public Ramp(Renderer r, Physics p, Vector2[] points) : base(r.AddDrawable(new Line(points, LINE_WIDTH, Color.DarkGray)), p) {}
    }

    public class RampProperties : StaticItemProperties {
        public List<PointF> Points { get; set; }

        private PointF[] convertVector2ListToPointFList(Vector2[] points) {
            var pointsF = new PointF[points.Length];
            for (int i = 0; i < points.Length; i++) {
                pointsF[i].X = points[i].X;
                pointsF[i].Y = points[i].Y;
            }
            return pointsF;
        }

        private Vector2[] convertPointFListToVector2List(PointF[] points) {
            var pointsF = new Vector2[points.Length];
            for (int i = 0; i < points.Length; i++) {
                pointsF[i].X = points[i].X;
                pointsF[i].Y = points[i].Y;
            }
            return pointsF;
        }

        public RampProperties(Vector2[] points) : base() {
            var pointsF = convertVector2ListToPointFList(points);

            Points = new List<PointF>(pointsF);
        }

        [JsonConstructor]
        public RampProperties(List<PointF> Points) {
            this.Points = Points;
        }

        public override StaticItem CreateStaticItem(Renderer r, Physics p) {
            return new Ramp(r, p, convertPointFListToVector2List(Points.ToArray()));
        }

        public override void CopyPropsToStaticItem(StaticItem item) {
            if (item is Ramp) {
                var ramp = (Ramp)item;
                ramp.LineInstance.Points = new List<Vector2>(convertPointFListToVector2List(Points.ToArray()));
            }
        }
    }
}
