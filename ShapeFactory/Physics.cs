using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeFactory {
    public class Physics {
        public static float GRAVITY = 9.8f;
        public static float TERMINAL_VELOCITY = 330.0f;

        private List<PhysicsBody> bodies;
        private List<int> queueFree;

        public Physics() {
            bodies = new List<PhysicsBody>();
            queueFree = new List<int>();
        }

        public void Clear() {
            bodies.Clear();
            queueFree.Clear();
        }

        public T AddBody<T>(T body) where T: PhysicsBody {
            bodies.Add(body);
            return (T)bodies.Last();
        }

        public void PhysicsStep(double deltaTime) {
            var collidedBodies = new HashSet<(int, int)>();
            for(int i = 0; i < bodies.Count; i++) {
                var body = bodies[i];
                if (body.IsQueuedFree()) {
                    queueFree.Insert(0, i);
                    continue;
                }

                if (!body.Enabled || !(body is RigidBody)) continue;

                body.PhysicsStep(deltaTime);
                
                for (int j = 0; j < bodies.Count; j++) {
                    if (i == j || !bodies[j].Enabled || bodies[j].IsQueuedFree() ||
                        collidedBodies.Contains((i, j))) continue;
                    var overlap = body.OverlapWith(bodies[j]);
                    if (overlap.Collision) {
                        body.CollisionWith(bodies[j], overlap, deltaTime);
                        // keeps track of what bodies have already collided and don't need to be checked again
                        collidedBodies.Add((j, i));
                    }
                }
                // check with wall collision
                body.CollideWithBoundaries(deltaTime);
            }

            collidedBodies.Clear();
        }
    }
}
