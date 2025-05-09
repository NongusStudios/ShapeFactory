﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeFactory {
    public class Physics {
        public static float GRAVITY = 9.8f;

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
            for(int i = 0; i < bodies.Count; i++) {
                var body = bodies[i];
                if (body.IsQueuedFree()) {
                    queueFree.Insert(0, i);
                    continue;
                }
                body.PhysicsStep(deltaTime);
            }
        }
    }
}
