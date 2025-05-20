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
