using ShapeFactory.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ShapeFactory
{
    public class Belt : StaticItem {
        public float Speed = 20.0f;
        public Belt(Renderer r, Vector2 position, float speed) : base(r.AddDrawable(
            new Sprite(ShapeType.Rectangle, new Transform2D(position, new Vector2(Properties.Resources.belt.Width/2, Properties.Resources.belt.Height/2)), Properties.Resources.belt)
        )) {
            Speed = speed;
        }

        public override void Update(double deltaTime) {}
    }
}
