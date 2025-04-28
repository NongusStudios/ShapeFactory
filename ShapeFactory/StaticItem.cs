using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeFactory {
    public class StaticItem {
        public Shape ShapeInstance { get; set; }
        // public StaticBody PhysicsInstance;

        public StaticItem(Shape sh) {
            ShapeInstance = sh;
        }

        public virtual void Update(double deltaTime) { }
    }
}
