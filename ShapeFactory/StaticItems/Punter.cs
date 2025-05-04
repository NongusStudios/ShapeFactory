using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ShapeFactory.StaticItems {
    public class Punter : StaticItem {
        public Vector2 AddedVelocity;
        public Punter(Renderer r, Physics p, Vector2 position, Vector2 addedVelocity) : base(r.AddDrawable(new Sprite(
            ShapeType.Rectangle, new Transform2D(position, new Vector2((float)(Properties.Resources.punter.Width/2), (float)(Properties.Resources.punter.Height/2))),
            Properties.Resources.punter
        )), p) {
            AddedVelocity = addedVelocity;
        }

        public override void Update(double deltaTime) {
            
        }
    }

    public class PunterProperties : StaticItemProperties {
        public Vector2 Position;
        public float PosX { get => Position.X; set => Position.X = value; }
        public float PosY { get => Position.Y; set => Position.Y = value; }
        public Vector2 AddedVelocity;
        public float AddedVelX { get => AddedVelocity.X; set => AddedVelocity.X = value; }
        public float AddedVelY { get => AddedVelocity.Y; set => AddedVelocity.Y = value; }

        public PunterProperties() { }
        public PunterProperties(Vector2 position, Vector2 addedVelocity) : base() {
            Position = position;
            AddedVelocity = addedVelocity;
        }

        public override StaticItem CreateStaticItem(Renderer r, Physics p) {
            return new Punter(r, p, Position, AddedVelocity);
        }

        public override void CopyPropsToStaticItem(StaticItem item) {
            if (item is Punter) {
                var punter = (Punter)item;
                punter.ShapeInstance.Transform.Position = Position;
                punter.AddedVelocity = AddedVelocity;
            }
        }
    }
}
