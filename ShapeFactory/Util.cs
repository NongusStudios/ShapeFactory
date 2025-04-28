using System.Numerics;

namespace ShapeFactory {
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
}