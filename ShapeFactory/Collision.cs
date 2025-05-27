using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace ShapeFactory {
    public struct AABB {
        public Vector2 Center;
        public Vector2 Min;
        public Vector2 Max;

        public AABB(Vector2 center, Vector2 min, Vector2 max) {
            Center = center;
            Min = min;
            Max = max;
        }
    }

    public struct Circle {
        public Vector2 Position;
        public float Radius;

        public Circle(Vector2 pos, float radius) {
            Position = pos;
            Radius = radius;
        }
    }

    public struct Overlap {
        public Overlap(bool Collision, Vector2 Normal, float Depth) {
            this.Collision = Collision;
            this.Normal = Normal;
            this.Depth = Depth;
        }

        public static Overlap NoCollision() {
            return new Overlap(false, new Vector2(), 0.0f);
        }

        public bool Collision;
        public Vector2 Normal;
        public float Depth;
    }

    public class Collision {
        public static Overlap IntersectAABB(AABB a, AABB b) {
            if (a.Max.X < b.Min.X || a.Min.X > b.Max.X) return Overlap.NoCollision();
            if (a.Max.Y < b.Min.Y || a.Min.Y > b.Max.Y) return Overlap.NoCollision();

            Vector2 n = Vector2.Zero;
            var dx = a.Center.X - b.Center.X;
            var dy = a.Center.Y - b.Center.Y;

            var aabb_half_extent = a.Max - a.Min;
            aabb_half_extent /= 2.0f;

            var px = aabb_half_extent.X - Math.Abs(dx);
            var py = aabb_half_extent.Y - Math.Abs(dy);

            if (px < py) {
                if (dx > 0.0f) {
                    n.X =  1.0f;
                } else {
                    n.X = -1.0f;
                }
            } else {
                if (dy > 0.0f) {
                    n.Y = 1.0f;
                } else {
                    n.Y = -1.0f;
                }
            }

                return new Overlap(true, n, Vector2.Distance(a.Center, b.Center));
        }

        public static Overlap IntersectCircle(Circle a, Circle b) {
            float r = a.Radius + b.Radius;
            r *= r;

            float d = Vector2.DistanceSquared(a.Position, b.Position);
            return new Overlap(r > d, Vector2.Normalize(a.Position-b.Position), (float)Math.Sqrt(d));
        }

        public static Overlap IntersectCircleWithRamp(Circle a, Vector2[] points, float width) {
            return new Overlap();
        }

        public static Overlap IntersectAABBWithRamp(AABB a, Vector2[] points, float width) {
            return new Overlap();
        }

        private static Vector2 directionVector(Vector2 target) {
            Vector2[] compass = {
                new Vector2( 0.0f, 1.0f),
                new Vector2( 1.0f, 0.0f),
                new Vector2( 0.0f,-1.0f),
                new Vector2(-1.0f, 0.0f),
            };

            float max = 0.0f;
            int best_match = -1;
            for (int i = 0; i < compass.Length; i++) {
                float dot = Vector2.Dot(Vector2.Normalize(target), compass[i]);
                if (dot > max) {
                    max = dot;
                    best_match = i;
                }
            }

            if (best_match == -1) return Vector2.Zero;

            return compass[best_match];
        }

        public static Overlap IntersectAABBwithCircle(AABB a, Circle b) {
            var center = b.Position;
            var aabb_center = a.Center;
            var diff = center - aabb_center;
            
            var aabb_half_extent = a.Max - a.Min;
            aabb_half_extent /= 2.0f;
            var clamped = new Vector2(M.Clamp(diff.X, -aabb_half_extent.X, aabb_half_extent.X), M.Clamp(diff.Y, -aabb_half_extent.Y, aabb_half_extent.Y));
            var closest = aabb_center + clamped;

            diff = closest - center;

            return new Overlap(diff.LengthSquared() < b.Radius*b.Radius, directionVector(diff), Vector2.Distance(center, aabb_center));
        }

        public static bool PointInRect(Vector2 point, AABB rect) {
            if (point.X > rect.Min.X && point.X < rect.Max.X &&
               point.Y > rect.Min.Y && point.Y < rect.Max.Y) {
                return true;
            }
            return false;
        }
    }
}
