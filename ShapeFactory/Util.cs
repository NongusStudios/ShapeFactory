using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Numerics;
using System.Text.Json;

namespace ShapeFactory {
    public class Global {
        public static string LAYOUT_FOLDER = "../../Layouts";
        public static int CanvasSizeX = 528;
        public static int CanvasSizeY = 528;
    }

    public enum ShapeType {
        Rectangle = 0,
        Circle,
        Triangle
    }
    
    public struct Overlap {
        public Overlap(bool Collision, Vector2 Normal, float Depth) {
            this.Collision = Collision;
            this.Normal = Normal;
            this.Depth = Depth;
        }

        public bool Collision;
        public Vector2 Normal;
        public float Depth;
    }

    public struct Transform2D {
        public Transform2D(Vector2 Position, Vector2 Size, float Rotation = 0.0f) {
            this.Position = Position;
            this.Size = Size;
            this.Rotation = Rotation;
        }

        public Vector2 Position;
        public Vector2 Size;
        public float Rotation;
    }

    public class M {
        public static float Lerp(float firstFloat, float secondFloat, float by) {
            return firstFloat * (1 - by) + secondFloat * by;
        }
        public static float Clamp(float v, float min, float max) {
            if (v > max) return max;
            if (v < min) return min;
            return v;
        }

        public static bool CheckPointInRect(Vector2 point, Vector2 rectPos, Vector2 rectSize) {
            var minX = rectPos.X - rectSize.X / 2.0f;
            var maxX = rectPos.X + rectSize.X / 2.0f;
            var minY = rectPos.Y - rectSize.Y / 2.0f;
            var maxY = rectPos.Y + rectSize.Y / 2.0f;

            if(point.X > minX && point.X < maxX &&
               point.Y > minY && point.Y < maxY) {
                return true;
            }
            return false;
        }
    }
}