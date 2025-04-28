using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeFactory {
    public class Staticbody : PhysicsBody {
        public Staticbody(ShapeType col, Transform2D transform, int layer) : base(col, transform, layer) {}
    }
}
