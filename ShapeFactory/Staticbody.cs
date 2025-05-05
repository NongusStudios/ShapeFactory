using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeFactory {
    public class StaticBody : PhysicsBody {
        public StaticBody(ShapeType col, Transform2D transform, int layer) : base(col, transform, layer) {}
    }
}
