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
        public float Mass;
        private float invMass;
        public float Restitution;

        public RigidBody(ShapeType col, Transform2D transform, float mass, float restitution, int layer): base(col, transform, layer) {
            Velocity = Vector2.Zero;
            AngularVelocity = 0.0f;
            Mass = mass;
            invMass = 1.0f / mass;
            Restitution = restitution;
        }

        public override void CollisionWith(PhysicsBody other, Overlap overlap) {
            if(other == null) {
                Transform.Position += overlap.Normal * overlap.Depth;
            } else {
                Transform.Position += overlap.Normal * overlap.Depth/2.0f;
            }

            if (overlap.Normal.X != 0.0f) {
                Velocity.X *= -1.0f;
            }

            if (overlap.Normal.Y != 0.0f) {
                Velocity.Y *= -1.0f;
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

            if (overlap.Collision) CollisionWith(null, overlap);
        }

        public override void PhysicsStep(double deltaTime) {
            var dt = (float)deltaTime;

            Velocity.X = M.Clamp(Velocity.X, -Physics.TERMINAL_VELOCITY, Physics.TERMINAL_VELOCITY);
            Velocity.Y = M.Clamp(Velocity.Y, -Physics.TERMINAL_VELOCITY, Physics.TERMINAL_VELOCITY);

            // TODO
            Velocity.Y += Physics.GRAVITY * Physics.GRAVITY * dt;
            Transform.Position += Velocity * dt;
            Transform.Rotation += AngularVelocity * dt;
        }
    }
}
