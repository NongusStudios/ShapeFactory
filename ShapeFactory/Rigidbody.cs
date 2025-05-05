using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ShapeFactory {
    public class RigidBody : PhysicsBody {
        public Vector2 Velocity;
        public float Mass;

        public RigidBody(ShapeType col, Transform2D transform, float mass, int layer): base(col, transform, layer) {
            Velocity = Vector2.Zero;
            Mass = mass;
        }

        public override void PhysicsStep(double deltaTime) {
            var dt = (float)deltaTime;

            // TODO
            Velocity.Y += Physics.GRAVITY * Physics.GRAVITY * dt;
            Transform.Position += Velocity * dt;
        }
    }
}
