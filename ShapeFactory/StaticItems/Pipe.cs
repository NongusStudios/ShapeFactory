using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ShapeFactory.StaticItems {
    public class Pipe : StaticItem {
        public Vector2 AddedSpawnVelocity;
        public Pipe(Renderer r, Physics p, Vector2 position, float rotation, Vector2 AddedSpawnVelocity) : base(r.AddDrawable(new Sprite(
            ShapeType.Rectangle, new Transform2D(position, new Vector2(Properties.Resources.spawn_pipe.Width / 2, Properties.Resources.spawn_pipe.Height / 2), rotation),
            Properties.Resources.spawn_pipe
        )), p, ShapeType.Rectangle) {
            PhysicsInstance.QueueFree();
        }
    }

    public class PipeProperties : StaticItemProperties {
        public Vector2 Position;
        public Vector2 AddedSpawnVelocity;
        public float PosX { get => Position.X; set => Position.X = value; }
        public float PosY { get => Position.Y; set => Position.Y = value; }

        public float AddedSpawnVelX { get => AddedSpawnVelocity.X; set => AddedSpawnVelocity.X = value; }
        public float AddedSpawnVelY { get => AddedSpawnVelocity.Y; set => AddedSpawnVelocity.Y = value; }

        public float Rotation { get; set; }

        public PipeProperties() { }
        public PipeProperties(Vector2 position, float rotation, Vector2 addedSpawnVel) : base() {
            Position = position;
            Rotation = rotation;
            AddedSpawnVelocity = addedSpawnVel;
        }

        public override StaticItem CreateStaticItem(Renderer r, Physics p) {
            return new Pipe(r, p, Position, Rotation, AddedSpawnVelocity);
        }

        public override void CopyPropsToStaticItem(StaticItem item) {
            if (item is Pipe) {
                var pipe = (Pipe)item;
                pipe.ShapeInstance.Transform.Position = Position;
                pipe.ShapeInstance.Transform.Rotation = Rotation;
            }
        }

        public override void SetPos(Vector2 p) {
            Position = p;
        }
    }
}
