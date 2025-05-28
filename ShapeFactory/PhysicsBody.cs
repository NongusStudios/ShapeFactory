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
            // Shape on shape
            if (Collider == other.Collider || (      Collider == ShapeType.Rectangle && other.Collider == ShapeType.Triangle) ||
                                              (other.Collider == ShapeType.Rectangle && Collider == ShapeType.Triangle)) {
                if (Collider == ShapeType.Circle) {
                    return Collision.IntersectCircle(Transform.ToCircle(), other.Transform.ToCircle());
                }
                else {
                    var olap = Collision.IntersectAABB(Transform.ToAABB(), other.Transform.ToAABB());
                    if (other is StaticBody) olap.Normal = -olap.Normal;
                    return olap;
                }
            }

            // Shape on other shape
            if (Collider == ShapeType.Circle && other.Collider == ShapeType.Rectangle) {
                var olap = Collision.IntersectAABBwithCircle(other.Transform.ToAABB(), Transform.ToCircle());
                if (other is RigidBody) olap.Normal = -olap.Normal; // I really should of taken the time to understand this math before using it
                                                                    // because this is seriously getting out of hand
                return olap;
            }

            return new Overlap();
        }

        public virtual void CollisionWith(PhysicsBody other, Overlap overlap) { }
        public virtual void CollideWithBoundaries() { }

        public bool IsQueuedFree() {
            return queueFree;
        }
    }
}
