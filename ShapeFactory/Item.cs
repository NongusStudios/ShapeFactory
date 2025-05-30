﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeFactory {
    public class Item {
        public Shape ShapeInstance;
        public RigidBody PhysicsInstance;
        private bool queueFree;

        public Item(Shape sh, Physics p, ShapeType col, float mass, float restitution, float friction) {
            ShapeInstance = sh;
            PhysicsInstance = p.AddBody(new RigidBody(col, ShapeInstance.Transform, mass, restitution, friction, 1));
            queueFree = false;
        }

        public virtual void Update(double deltaTime) {
            ShapeInstance.Transform = PhysicsInstance.Transform;
            if (ShapeInstance.Transform.Position.Y > Global.CanvasSizeY + ShapeInstance.Transform.Size.Y) {
                QueueFree();
            }
        }
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
