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
        Triangle,
        Ramp,
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

        public AABB ToAABB() {
            return new AABB(Position, Position - Size / 2.0f, Position + Size / 2.0f);
        }

        public Circle ToCircle() {
            return new Circle(Position, Size.X / 2.0f);
        }
    }

    public class M {
        public static float Lerp(float firstFloat, float secondFloat, float by) {
            return firstFloat * (1 - by) + secondFloat * by;
        }

        public static Vector2 Lerp(Vector2 firstVec, Vector2 secondVec, float by) {
            return new Vector2(Lerp(firstVec.X, secondVec.X, by), Lerp(firstVec.Y, secondVec.Y, by));
        }
        public static float Clamp(float v, float min, float max) {
            if (v > max) return max;
            if (v < min) return min;
            return v;
        }
    }
}