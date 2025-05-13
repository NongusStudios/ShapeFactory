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
            if (Collider == other.Collider) {
                if (Collider == ShapeType.Circle) {
                    return Collision.IntersectCircle(Transform.ToCircle(), other.Transform.ToCircle());
                }
                else if (Collider == ShapeType.Rectangle) {
                    return Collision.IntersectAABB(Transform.ToAABB(), other.Transform.ToAABB());
                }
            }

            // Shape on other shape
            if (Collider == ShapeType.Circle && other.Collider == ShapeType.Rectangle) {
                return Collision.IntersectAABBwithCircle(other.Transform.ToAABB(), Transform.ToCircle());
            } else if(Collider == ShapeType.Rectangle && other.Collider == ShapeType.Circle) {
                return Collision.IntersectAABBwithCircle(Transform.ToAABB(), other.Transform.ToCircle());
            }

            

            return new Overlap(false, new Vector2(), 0.0f);
        }

        public virtual void CollisionWith(PhysicsBody other, Overlap overlap) { }

        public bool IsQueuedFree() {
            return queueFree;
        }
    }
}
