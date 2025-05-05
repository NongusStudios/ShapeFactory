using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ShapeFactory.Items {
    public class LeadBall : Item {
        public LeadBall(Renderer r, Physics p, Vector2 position) : base(
            r.AddDrawable(new Shape(ShapeType.Circle,
                new Transform2D(position, new Vector2(20.0f, 20.0f)), Color.Silver)
            ), p, 20.0f
        ) {}

        public override void Update(double deltaTime) {
            ShapeInstance.Transform = PhysicsInstance.Transform;
            if (ShapeInstance.Transform.Position.Y > Global.CanvasSizeY + ShapeInstance.Transform.Size.Y) {
                QueueFree();
            }
        }
    }
}
