using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ShapeFactory.Items {
    public class AnomalousTriangle : Item {
        public AnomalousTriangle(Renderer r, Physics p, Vector2 position) : base(
            r.AddDrawable(new Sprite(ShapeType.Triangle,
                new Transform2D(position, new Vector2(20.0f, 20.0f)), Properties.Resources.anomalous)
            ), p, ShapeType.Rectangle, 1.0f, 1.0f
        ) {}

        public override void Update(double deltaTime) {
            base.Update(deltaTime);
        }
    }
}
