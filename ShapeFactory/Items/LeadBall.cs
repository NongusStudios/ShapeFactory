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
                new Transform2D(position, new Vector2(20.0f, 20.0f)), Color.DarkSlateGray)
            ), p, 5.0f, 0.1f
        ) {}

        public override void Update(double deltaTime) {
            base.Update(deltaTime);
        }
    }
}
