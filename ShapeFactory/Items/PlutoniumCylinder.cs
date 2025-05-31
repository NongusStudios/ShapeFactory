using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ShapeFactory.Items {
    public class PlutoniumCylinder : Item {
        public PlutoniumCylinder(Renderer r, Physics p, Vector2 position) : base(
            r.AddDrawable(new Shape(ShapeType.Rectangle,
                new Transform2D(position, new Vector2(10.0f, 20.0f)), Color.OrangeRed)
            ), p, ShapeType.Rectangle, 2.0f, 0.4f, 0.6f
        ) { }

        public override void Update(double deltaTime) {
            base.Update(deltaTime);
        }
    }
}
