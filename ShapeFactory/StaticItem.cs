using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeFactory {
    public class StaticItem {
        private Drawable drawable;
        public Shape ShapeInstance {
            get { return (Shape)drawable; }
            set { drawable = value; }
        }

        public Line LineInstance {
            get { return (Line)drawable; }
            set { drawable = value; }
        }

        public StaticBody PhysicsInstance;

        public StaticItem(Shape sh, Physics p) {
            ShapeInstance = sh;
            PhysicsInstance = p.AddBody(new StaticBody(ShapeInstance.Type, ShapeInstance.Transform, 0));
        }
        public StaticItem(Line l, Physics p) {
            LineInstance = l;
            PhysicsInstance = p.AddBody(new StaticBody(l.Points.ToArray(), l.Width, 0));
        }

        public virtual void Update(double deltaTime) { }
    }
}
