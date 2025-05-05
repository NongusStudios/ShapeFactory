using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShapeFactory {
    public class PhysicsBody {
        private bool queueFree;
        public bool Enabled;
        public ShapeType Collider;
        public Transform2D Transform;
        public int Layer;

        public PhysicsBody(ShapeType col, Transform2D transform, int layer) {
            queueFree = false;
            Collider = col;
            Transform = transform;
            Layer = layer;
            Enabled = true;
        }

        public virtual void PhysicsStep(double deltaTime) { }

        public virtual void QueueFree() {
            queueFree = true;
        }

        public virtual Overlap OverlapWith(PhysicsBody other) {
            // TODO Collision Calculation
            return new Overlap();
        }

        public bool IsQueuedFree() {
            return queueFree;
        }
    }
}
