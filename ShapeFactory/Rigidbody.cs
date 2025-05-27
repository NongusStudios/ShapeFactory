using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ShapeFactory {
    public class RigidBody : PhysicsBody {
        public Vector2 Velocity;
        public float AngularVelocity;

        private float invMass;
        public float Mass {
            get { return 1.0f / invMass; }
            set { invMass = 1.0f / value; }
        }
        public float Restitution;

        public RigidBody(ShapeType col, Transform2D transform, float mass, float restitution, int layer): base(col, transform, layer) {
            Velocity = Vector2.Zero;
            AngularVelocity = 0.0f;
            Mass = mass;
            invMass = 1.0f / mass;
            Restitution = restitution;
        }

        public override void CollisionWith(PhysicsBody other, Overlap overlap) {
            if (other == null || other is StaticBody || other is RampBody) {
                var velAlongNormal = Vector2.Dot(Velocity, overlap.Normal);
                //if (velAlongNormal > 0.0f) return;

                float imSc = -1.5f * velAlongNormal;
                imSc /= invMass;

                var impulse = imSc * overlap.Normal;
                Velocity += invMass * impulse;
                return;
            }

            if (other is RigidBody) {
                var o = (RigidBody)other;
                // relative velocity
                var rv = o.Velocity - Velocity;

                var velAlongNormal = Vector2.Dot(rv, overlap.Normal);

                if (velAlongNormal > 0.0f) return;

                // calculate restitution
                float rest = Math.Min(Restitution, o.Restitution);

                // impulse scalar
                float imSc = -(1.0f + rest) * velAlongNormal;
                imSc /= invMass + o.invMass;

                // apply impulse
                var impulse = imSc * overlap.Normal;
                Velocity   += invMass * impulse;
                o.Velocity -= o.invMass * impulse;
            }
        }

        public override void CollideWithBoundaries() {
            var aabb = Transform.ToAABB();
            var overlap = new Overlap();

            if(aabb.Min.X <= 0.0f) {
                overlap.Collision = true;
                overlap.Normal.X = 1.0f;
                overlap.Depth += 0.0f - aabb.Min.X;
            } else if(aabb.Max.X >= Global.CanvasSizeX) {
                overlap.Collision = true;
                overlap.Normal.X = -1.0f;
                overlap.Depth += aabb.Max.X - Global.CanvasSizeX;
            }

            if (aabb.Min.Y <= 0.0f) {
                overlap.Collision = true;
                overlap.Normal.Y = 1.0f;
                overlap.Depth += 0.0f - aabb.Max.Y;
            } else if (aabb.Max.Y >= Global.CanvasSizeY) {
                overlap.Collision = true;
                overlap.Normal.Y = -1.0f;
                overlap.Depth += aabb.Max.Y - Global.CanvasSizeY;
            }

            Transform.Position += overlap.Normal * overlap.Depth;
            if (overlap.Collision) {
                CollisionWith(null, overlap);
            }
        }

        public override void PhysicsStep(double deltaTime) {
            var dt = (float)deltaTime;

            Velocity.X = M.Clamp(Velocity.X, -Physics.TERMINAL_VELOCITY, Physics.TERMINAL_VELOCITY);
            Velocity.Y = M.Clamp(Velocity.Y, -Physics.TERMINAL_VELOCITY, Physics.TERMINAL_VELOCITY);

            Velocity.Y += Physics.GRAVITY * Physics.GRAVITY * dt;
            Transform.Position += Velocity * dt;
            Transform.Rotation += AngularVelocity * dt;
        }
    }
}
