using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ShapeFactory.StaticItems {
    public class Elevator : StaticItem {
        public Sprite Platform;
        public StaticBody PlatformPhysics;
        public float Speed;
        public double Interval;
        private double elapsedTime;
        private int direction;
        private float nextPos;
        private const float leaveSpeed = 10.0f;
        private float leaveDir; // direction

        public Elevator(Renderer r, Physics p, Vector2 position, bool platformIsRight, float speed, double interval) : base(r.AddDrawable(new Sprite(
            ShapeType.Rectangle, new Transform2D(position, new Vector2((float)(Properties.Resources.elevator.Width/2), (float) (Properties.Resources.elevator.Height/2))),
            Properties.Resources.elevator
        )), p, ShapeType.Rectangle) {
            var padPosition = position;
            padPosition.X += (platformIsRight)
                ?  (Properties.Resources.elevator_pad_0.Width/4 + Properties.Resources.elevator.Width/4)
                : -(Properties.Resources.elevator_pad_0.Width / 4 + Properties.Resources.elevator.Width / 4);
            padPosition.Y += Properties.Resources.elevator.Height / 4 - Properties.Resources.elevator_pad_0.Height / 4;

            Platform = r.AddDrawable(new Sprite(
                ShapeType.Rectangle, new Transform2D(padPosition, new Vector2((float)(Properties.Resources.elevator_pad_0.Width / 2), (float)(Properties.Resources.elevator_pad_0.Height / 2))),
                new Bitmap[] {Properties.Resources.elevator_pad_0, Properties.Resources.elevator_pad_1}
            ));
            PlatformPhysics = p.AddBody(new StaticBody(ShapeType.Rectangle, Platform.Transform, 0));
            PlatformPhysics.OnCollision = (body, o) => {
                if(PlatformPhysics.Transform.Position.Y == topPos() && body is RigidBody) {
                    var b = (RigidBody)body;
                    b.Velocity.X += leaveDir * leaveSpeed;
                }
                body.Transform.Position.Y = Platform.Transform.Position.Y - body.Transform.Size.Y / 1.5f;
            };

            Speed = speed;
            Interval = interval;
            elapsedTime = 0.0f;
            direction = -1;
            leaveDir = (platformIsRight) ? 1.0f : -1.0f;
            nextPos = topPos();
        }

        private float topPos() {
            return (float)((ShapeInstance.Transform.Position.Y - Properties.Resources.elevator.Height / 4) + Properties.Resources.elevator_pad_0.Height / 4);
        }
        private float bottomPos() {
            return (float)((ShapeInstance.Transform.Position.Y + Properties.Resources.elevator.Height / 4) - Properties.Resources.elevator_pad_0.Height / 4);
        }

        public override void Update(double deltaTime) {
            elapsedTime += deltaTime;
            if(elapsedTime >= Interval) {
                Platform.SetCurrentFrame(1);
                var dest = (direction > 0) ? bottomPos() : topPos();
                Platform.Transform.Position.Y += Speed * (float)deltaTime * (float)direction;
                Platform.Transform.Position.Y = UtilMath.Clamp(Platform.Transform.Position.Y, topPos(), bottomPos());
                PlatformPhysics.Transform = Platform.Transform;

                if (Platform.Transform.Position.Y == nextPos) { // Reached top or bottom
                    Platform.SetCurrentFrame(0);
                    elapsedTime = 0.0f;
                    direction = -direction;
                    leaveDir = -leaveDir;

                    if (nextPos == topPos()) nextPos = bottomPos();
                    else nextPos = topPos();
                }
            }
        }
    }

    public class ElevatorProperties : StaticItemProperties {
        public Vector2 Position;
        public float PosX { get => Position.X; set => Position.X = value; }
        public float PosY { get => Position.Y; set => Position.Y = value; }
        public bool PlatformIsRight { get; set; }
        public float Speed { get; set; }
        public double Interval { get; set; }

        public ElevatorProperties() { }
        public ElevatorProperties(Vector2 position, bool platformIsRight, float speed, double interval) : base() {
            Position = position;
            PlatformIsRight = platformIsRight;
            Speed = speed;
            Interval = interval;
        }

        public override StaticItem CreateStaticItem(Renderer r, Physics p) {
            return new Elevator(r, p, Position, PlatformIsRight, Speed, Interval);
        }

        public override void CopyPropsToStaticItem(StaticItem item) {
            if (item is Elevator) {
                var ele = (Elevator)item;
                ele.ShapeInstance.Transform.Position = Position;
                ele.Platform.Transform.Position = Position;
                ele.Platform.Transform.Position.X += (PlatformIsRight)
                ? (Properties.Resources.elevator_pad_0.Width / 4 + Properties.Resources.elevator.Width / 4)
                : -(Properties.Resources.elevator_pad_0.Width / 4 + Properties.Resources.elevator.Width / 4);
                ele.Platform.Transform.Position.Y += Properties.Resources.elevator.Height / 4 - Properties.Resources.elevator_pad_0.Height / 4;
                ele.Speed = Speed;
                ele.Interval = Interval;
            }
        }

        public override void SetPos(Vector2 p) {
            Position = p;
        }
    }
}
