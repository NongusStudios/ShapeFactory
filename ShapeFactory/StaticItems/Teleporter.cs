using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ShapeFactory.StaticItems {
    public class Teleporter : StaticItem {
        public Teleporter Destination;
        private bool justTeleportedTo = false;
        private double lastTeleport = 0.0;

        // Utility function for instancing 2 teleporters and linking them
        public static (Teleporter, Teleporter) CreateTeleporters(Renderer r, Physics p, Vector2 positionA, Vector2 positionB) {
            var a = new Teleporter(r, p, positionA);
            var b = new Teleporter(r, p, positionB);
            a.Link(b);
            return (a, b);
        }

        public Teleporter(Renderer r, Physics p, Vector2 position) : base(r.AddDrawable(new Sprite(
            ShapeType.Rectangle, new Transform2D(position, new Vector2((float)(Properties.Resources.teleporter1.Width / 2), (float)(Properties.Resources.teleporter1.Height / 2))),
            new Image[] { Properties.Resources.teleporter1, Properties.Resources.teleporter2 }
        )), p, ShapeType.Rectangle) {
            PhysicsInstance.Transform.Size.X = (float)(Properties.Resources.teleporter1.Width / 4);
            PhysicsInstance.Transform.Size.Y = (float)(Properties.Resources.teleporter1.Height / 4);

            PhysicsInstance.OnCollision = (o, olap) => {
                lastTeleport = 0.0;
                justTeleportedTo = true;
                ((Sprite)ShapeInstance).SetCurrentFrame(1);
                PhysicsInstance.Enabled = false;

                Destination.lastTeleport = 0.0;
                Destination.justTeleportedTo = true;
                ((Sprite)Destination.ShapeInstance).SetCurrentFrame(1);
                Destination.PhysicsInstance.Enabled = false;

                o.Transform.Position = Destination.PhysicsInstance.Transform.Position;
                ((RigidBody)o).Velocity = Vector2.Zero;
            };
        }

        public void Link(Teleporter dest) {
            Destination = dest;
            dest.Destination = this;
        }

        public override void Update(double deltaTime) {
            if (justTeleportedTo && lastTeleport >= 0.75) {
                ((Sprite)ShapeInstance).SetCurrentFrame(0);
                PhysicsInstance.Enabled = true;
                justTeleportedTo = false;
            }
            else { lastTeleport += deltaTime; }
        }
    }

    public class TeleporterProperties : StaticItemProperties {
        public Vector2 Position;
        public float PosX { get => Position.X; set => Position.X = value; }
        public float PosY { get => Position.Y; set => Position.Y = value; }

        public TeleporterProperties() { }
        public TeleporterProperties(Vector2 position, int rotation) : base() {
            Position = position;
        }

        public override void CopyPropsToStaticItem(StaticItem item) {
            if (item is Teleporter) {
                var teleporter = (Teleporter)item;
                teleporter.ShapeInstance.Transform.Position = Position;
            }
        }

        public override void SetPos(Vector2 p) {
            Position = p;
        }
    }
}
