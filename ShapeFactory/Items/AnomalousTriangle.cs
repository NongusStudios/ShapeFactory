using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShapeFactory.Items {
    public class AnomalousTriangle : Item {
        private List<AnomalousTriangle> kin;

        public AnomalousTriangle(Renderer r, Physics p, Vector2 position, List<AnomalousTriangle> others) : base(
            r.AddDrawable(new Sprite(ShapeType.Triangle,
                new Transform2D(position, new Vector2(20.0f, 24.0f)), Properties.Resources.anomalous)
            ), p, ShapeType.Triangle, 1.2f, 2.0f, 0.0f
        ) {
            kin = others;
            kin.Add(this);

            // teleports any rigidbody to colliding with this triangle to another while adding velocity to it in the triangles current direction
            // if only one triangle exists then velocity will just be added to the other body
            PhysicsInstance.OnCollision = (o, olap) => {
                if(o is RigidBody && o.Collider != ShapeType.Triangle) {
                    var rb = (RigidBody)o;
                    float min = float.PositiveInfinity;
                    int minIdx = 0;

                    int i = 0;
                    foreach(var brother in kin) {
                        if (brother == this) continue;
                        var d = Vector2.DistanceSquared(PhysicsInstance.Transform.Position, brother.PhysicsInstance.Transform.Position);
                        if (d < min) {
                            min = d;
                            minIdx = i;
                        }
                        i++;
                    }

                    var kMin = kin[minIdx];
                    rb.Transform.Position = kMin.PhysicsInstance.Transform.Position + Vector2.Normalize(PhysicsInstance.Velocity) * kMin.PhysicsInstance.Transform.Size.Y;
                    rb.Velocity += Vector2.Normalize(PhysicsInstance.Velocity) * Vector2.One * Physics.TERMINAL_VELOCITY;
                }
            };
        }

        public override void Update(double deltaTime) {
            base.Update(deltaTime);

            var angle = UtilMath.Rad2Deg((float)Math.Atan2((double)PhysicsInstance.Velocity.Y, (double)PhysicsInstance.Velocity.X)) + 90.0f;
            PhysicsInstance.Transform.Rotation = UtilMath.Lerp(PhysicsInstance.Transform.Rotation,angle, (float)deltaTime * 5.0f);
        }

        public override void QueueFree() {
            base.QueueFree();
            kin.Remove(this);
        }
    }
}
