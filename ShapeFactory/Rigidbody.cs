﻿using ShapeFactory.StaticItems;
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
        public Action<PhysicsBody, Overlap> OnCollision = (o, ov) => { };

        private float invMass;
        public float Mass {
            get { return 1.0f / invMass; }
            set { invMass = 1.0f / value; }
        }
        public float Friction;
        public float Restitution;

        // restitution controls bounce, friction is 0-1
        public RigidBody(ShapeType col, Transform2D transform, float mass, float restitution, float friction, int layer): base(col, transform, layer) {
            Velocity = Vector2.Zero;
            AngularVelocity = 0.0f;
            Mass = mass;
            invMass = 1.0f / mass;
            Restitution = restitution;
            Friction = friction;
        }

        private const float posCorrectionPercent = 0.05f;
        private const float posCorrectionSlop = 0.1f;

        private void positionalCorrection(RigidBody o, Overlap overlap) {
            var correction = Math.Max(overlap.Depth - posCorrectionSlop, 0.0f) / (invMass + o.invMass) * posCorrectionPercent * overlap.Normal;
            Transform.Position -= invMass * correction;
            o.Transform.Position += o.invMass * correction;
        }

        private void positionalCorrection(Overlap overlap) {
            var correction = Math.Max(overlap.Depth - posCorrectionSlop, 0.0f) / invMass * posCorrectionPercent * overlap.Normal;
            Transform.Position -= invMass * correction;
        }

        public override void CollisionWith(PhysicsBody other, Overlap overlap, double deltaTime) {
            var dt = (float)deltaTime;
            
            AngularVelocity *= -1.0f;

            if (other is RigidBody) { // rigid on rigid
                var o = (RigidBody)other;

                // relative velocity
                var rv = o.Velocity - Velocity;

                var velAlongNormal = Vector2.Dot(rv, overlap.Normal);

                if (velAlongNormal > 0.0f) return;

                // calculate restitution
                float rest = Math.Max(Restitution, o.Restitution);

                // impulse scalar
                float imSc = -(1.0f + rest) * velAlongNormal;
                imSc /= invMass + o.invMass;

                // apply impulse
                var impulse = imSc * overlap.Normal;

                var massSum = Mass + o.Mass;
                var massRatio = o.Mass / massSum;

                Velocity   -= massRatio * impulse;

                massRatio = Mass / massSum;
                o.Velocity += massRatio * impulse;

                positionalCorrection(o, overlap);

                if(Collider == ShapeType.Triangle) {
                    OnCollision(other, overlap);
                } else if(o.Collider == ShapeType.Triangle) {
                    o.OnCollision(this, overlap);
                }

                return;
            }

            { // collision with boundary or static body
                var velAlongNormal = Vector2.Dot(Velocity, overlap.Normal);
                if (other is RampBody && velAlongNormal < 0.0f) return;

                float imSc = -(1.0f + Restitution) * velAlongNormal;
                imSc /= invMass;

                var impulse = imSc * overlap.Normal;
                Velocity += invMass * impulse;

                if (other == null) {
                    Transform.Position += overlap.Normal * overlap.Depth;
                    overlap.Normal *= -1.0f;
                } else {
                    positionalCorrection(overlap);
                }

                // apply friction when on ground
                if (overlap.Normal.Y > 0.0f) {
                    Velocity.X -= Velocity.X * Friction * overlap.Normal.Y * (dt * 4.0f);
                }

                if (other is StaticBody) {
                    var o = (StaticBody)other;
                    o.OnCollision(this, overlap);
                }
            }
        }

        public override void CollideWithBoundaries(bool hasBottomBorder, double deltaTime) {
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
                overlap.Depth += 0.0f - aabb.Min.Y;
            } else if (hasBottomBorder && aabb.Max.Y >= Global.CanvasSizeY) {
                overlap.Collision = true;
                overlap.Normal.Y = -1.0f;
                overlap.Depth += aabb.Max.Y - Global.CanvasSizeY;
            }

            if (overlap.Collision) {
                CollisionWith(null, overlap, deltaTime);
            }
        }

        public override void PhysicsStep(double deltaTime) {
            var dt = (float)deltaTime;

            Velocity.X = UtilMath.Clamp(Velocity.X, -Physics.TERMINAL_VELOCITY, Physics.TERMINAL_VELOCITY);
            Velocity.Y = UtilMath.Clamp(Velocity.Y, -Physics.TERMINAL_VELOCITY, Physics.TERMINAL_VELOCITY);

            Velocity.Y += Physics.GRAVITY * Physics.GRAVITY * dt;
            Transform.Position += Velocity * dt;
            Transform.Rotation += AngularVelocity * dt;
        }
    }
}
