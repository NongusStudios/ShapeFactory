using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ShapeFactory.Items {
    public class LeadBall : Item {
        private static int variantIdx = 0;
        private static Color[] colours = {
            Color.DarkSlateGray,
            Color.LightSlateGray,
        };
        private static float[] masses = {
            5.0f,
            3.0f,
        };

        public LeadBall(Renderer r, Physics p, Vector2 position) : base(
            r.AddDrawable(new Shape(ShapeType.Circle, 
                new Transform2D(position, new Vector2(20.0f, 20.0f)), colours[variantIdx])
            ), p, ShapeType.Circle, masses[variantIdx], 0.2f, 0.2f
        ) {
            variantIdx = (variantIdx + 1) % 2;
        }

        public override void Update(double deltaTime) {
            base.Update(deltaTime);
        }
    }
}
