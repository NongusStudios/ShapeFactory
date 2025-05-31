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
            /*if (a.Max.X < b.Min.X || a.Min.X > b.Max.X) return Overlap.NoCollision();
            if (a.Max.Y < b.Min.Y || a.Min.Y > b.Max.Y) return Overlap.NoCollision();

            Vector2 n = Vector2.Zero;
            var dx = b.Center.X - a.Center.X;
            var dy = b.Center.Y - a.Center.Y;

            var aabb_half_extent = a.Max - a.Min;
            aabb_half_extent /= 2.0f;

            var px = aabb_half_extent.X - Math.Abs(dx);
            var py = aabb_half_extent.Y - Math.Abs(dy);

            if (px < py) {
                if (dx > 0.0f) {
                    n.X =   1.0f;
                } else {
                    n.X =  -1.0f;
                }
            } else {
                if (dy > 0.0f) {
                    n.Y =  1.0f;
                } else {
                    n.Y =  -1.0f;
                }
            }*/

            var n = b.Center - a.Center;

            // half extents
            var aExtent = (a.Max.X - a.Min.X) / 2.0f;
            var bExtent = (b.Max.X - b.Min.X) / 2.0f;

            var xOverlap = aExtent + bExtent - Math.Abs(n.X);

            if(xOverlap > 0.0f) {
                aExtent = (a.Max.Y - a.Min.Y) / 2.0f;
                bExtent = (b.Max.Y - b.Min.Y) / 2.0f;

                float yOverlap = aExtent + bExtent - Math.Abs(n.Y);

                if(yOverlap > 0.0f) {
                    if(xOverlap < yOverlap) {
                        if(n.X < 0.0f) {
                            n.X = -1.0f;
                            n.Y =  0.0f;
                        } else {
                            n.X =  1.0f;
                            n.Y =  0.0f;
                        }
                        return new Overlap(true, n, xOverlap);
                    } else {
                        if (n.Y < 0.0f) {
                            n.X =  0.0f;
                            n.Y = -1.0f;
                        }
                        else {
                            n.X =  0.0f;
                            n.Y =  1.0f;
                        }
                        return new Overlap(true, n, yOverlap);
                    }
                }
            }

            return Overlap.NoCollision();
        }

        public static Overlap IntersectCircle(Circle a, Circle b) {
            // Difference between positions
            var n = b.Position - a.Position;
            
            // radius
            var r = a.Radius + b.Radius;
            r *= r;

            if (n.LengthSquared() > r) return Overlap.NoCollision();

            // distance between circles
            float d = n.Length();

            if(d != 0.0f) {
                // penetration depth
                var depth = a.Radius+b.Radius - d;
                // normalize n
                var normal = n / d;
                return new Overlap(true, normal, depth);
            } else { // if circles are one same position try to move them apart
                return new Overlap(true, new Vector2(1.0f, 0.0f), a.Radius);
            }
        }

        public static Overlap IntersectCircleWithRamp(Circle a, Vector2[] points, float width) {
            if (points.Length < 2) return Overlap.NoCollision();
            var bestOverlap = new Overlap(false, Vector2.Zero, 0.0f);

            // combined radius
            var r = a.Radius + width / 2.0f;

            for (var i = 0; i < points.Length - 1; i++) {
                var start = points[i];
                var end = points[i + 1];

                var segmentVec = end - start;
                var segmentLen = Math.Abs(segmentVec.Length());

                // Skip segments less than a pixel long
                if (segmentLen < 1.0f) continue;

                var segmentDir = Vector2.Normalize(segmentVec);
                var circleVec = a.Position - start;

                // project circle center onto segment
                var projection = Vector2.Dot(circleVec, segmentDir);
                projection = Math.Max(0.0f, Math.Min(segmentLen, projection)); // clamp to segment 

                var closestPoint = start + segmentDir * projection;
                var penetration = closestPoint - a.Position;
                var d = penetration.Length();

                if(d < r) {
                    var depth = r - d;
                    var normal = Vector2.Normalize(penetration);

                    if(!bestOverlap.Collision || depth > bestOverlap.Depth) {
                        bestOverlap.Collision = true;
                        bestOverlap.Normal = normal;
                        bestOverlap.Depth = depth;
                    }
                }
            }

            return bestOverlap;
        }

        private static Vector2 directionVector(Vector2 target) {
            Vector2[] compass = {
                new Vector2( 0.0f, 1.0f), // down
                new Vector2( 1.0f, 0.0f), // right
                new Vector2( 0.0f,-1.0f), // up
                new Vector2(-1.0f, 0.0f), // left
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
            var clamped = new Vector2(UtilMath.Clamp(diff.X, -aabb_half_extent.X, aabb_half_extent.X), UtilMath.Clamp(diff.Y, -aabb_half_extent.Y, aabb_half_extent.Y));
            var closest = aabb_center + clamped;

            diff = closest - center;

            return new Overlap(diff.LengthSquared() < b.Radius*b.Radius, directionVector(diff), Vector2.Distance(center, aabb_center));
        }

        public static bool PointInAABB(Vector2 point, AABB rect) {
            if (point.X > rect.Min.X && point.X < rect.Max.X &&
               point.Y > rect.Min.Y && point.Y < rect.Max.Y) {
                return true;
            }
            return false;
        }

        public static bool PointInRamp(Vector2 point, Vector2[] points, float width) {
            return IntersectCircleWithRamp(new Circle(point, 0.0f), points, width).Collision;
        }
    }
}
