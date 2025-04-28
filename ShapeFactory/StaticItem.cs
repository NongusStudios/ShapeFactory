using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeFactory {
    public class StaticItem {
        public Shape ShapeInstance;
        public Staticbody PhysicsInstance;

        public StaticItem(Shape sh) {
            ShapeInstance = sh;
            PhysicsInstance = new Staticbody(ShapeInstance.Type, ShapeInstance.Transform, 0);
        }

        public virtual void Update(double deltaTime) { }
    }
}
