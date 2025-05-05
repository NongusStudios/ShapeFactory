using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ShapeFactory.StaticItems {
    public class Teleporter : StaticItem {
        public Teleporter Destination;

        // Utility function for instancing 2 teleporters and linking them
        public static (Teleporter, Teleporter) CreateTeleporters(Renderer r, Physics p, Vector2 positionA, float rotationA, Vector2 positionB, float rotationB) {
            var a = new Teleporter(r, p, positionA, rotationA);
            var b = new Teleporter(r, p, positionB, rotationB);
            a.Link(b);
            return (a, b);
        }

        public Teleporter(Renderer r, Physics p, Vector2 position, float rotation) : base(r.AddDrawable(new Sprite(
            ShapeType.Rectangle, new Transform2D(position, new Vector2((float)(Properties.Resources.teleporter.Width / 2), (float)(Properties.Resources.teleporter.Height / 2)), rotation),
            Properties.Resources.teleporter
        )), p) {}

        public void Link(Teleporter dest) {
            Destination = dest;
            dest.Destination = this;
        }
    }

    public class TeleporterProperties : StaticItemProperties {
        public Vector2 Position;
        public float PosX { get => Position.X; set => Position.X = value; }
        public float PosY { get => Position.Y; set => Position.Y = value; }
        public float Rotation { get; set; }

        public TeleporterProperties() { }
        public TeleporterProperties(Vector2 position, float rotation) : base() {
            Position = position;
            Rotation = rotation;
        }

        public override void CopyPropsToStaticItem(StaticItem item) {
            if (item is Teleporter) {
                var teleporter = (Teleporter)item;
                teleporter.ShapeInstance.Transform.Position = Position;
                teleporter.ShapeInstance.Transform.Rotation = Rotation;
            }
        }

        public override void SetPos(Vector2 p) {
            Position = p;
        }
    }
}
