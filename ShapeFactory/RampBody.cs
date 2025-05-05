using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ShapeFactory {
    public class RampBody : PhysicsBody {
        public List<Vector2> Points;
        public float Width;
        public RampBody(Vector2[] points, float width, int layer) : base(ShapeType.Rectangle, new Transform2D(), layer) {
            Points = new List<Vector2>(points);
            Width = width;
        }

        public override Overlap OverlapWith(PhysicsBody other) {
            return new Overlap();
        }
    }
}
