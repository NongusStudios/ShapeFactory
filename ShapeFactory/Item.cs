using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeFactory {
    public class Item {
        public Shape ShapeInstance;
        public Rigidbody PhysicsInstance;
        private bool queueFree;

        public Item(Shape sh) {
            ShapeInstance = sh;
            PhysicsInstance = new Rigidbody(ShapeInstance.Type, ShapeInstance.Transform, 1.0f, 0);
            queueFree = false;
        }

        public virtual void Update(double deltaTime) { }
        public virtual void QueueFree() {
            queueFree = true;
            ShapeInstance.QueueFree();
            PhysicsInstance.QueueFree();
        }

        public bool IsQueuedFree() {
            return queueFree;
        }
    }
}
