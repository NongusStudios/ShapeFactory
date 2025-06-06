﻿using ShapeFactory.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ShapeFactory.StaticItems
{

    public class Belt : StaticItem {
        public static double AnimationInterval = 0.15;
        public float Speed;
        public Belt(Renderer r, Physics p, Vector2 position, float speed) : base(r.AddDrawable(
            new Sprite(ShapeType.Rectangle, new Transform2D(position, new Vector2(Properties.Resources.belt1.Width/2, Properties.Resources.belt1.Height/2)), new Image[] { Properties.Resources.belt1,  Properties.Resources.belt2 }, AnimationInterval)
        ), p, ShapeType.Rectangle) {
            Speed = speed;
            PhysicsInstance.OnCollision = (other, overlap) => {
                if (other is RigidBody) {
                    var o = (RigidBody)other;
                    if (Speed > 0) {
                        o.Velocity.X = Math.Max(o.Velocity.X, Speed);
                    } else {
                        o.Velocity.X = Math.Min(o.Velocity.X, Speed);
                    }
                }
            };
        }

        public override void Update(double deltaTime) {}
    }

    public class BeltProperties : StaticItemProperties {
        public Vector2 Position;
        public float PosX { get => Position.X; set => Position.X = value; }
        public float PosY { get => Position.Y; set => Position.Y = value; }
        public float Speed { get; set; }

        public BeltProperties() { }
        public BeltProperties(Vector2 position, float speed) : base() {
            Position = position;
            Speed = speed;
        }

        public override StaticItem CreateStaticItem(Renderer r, Physics p) {
            return new Belt(r, p, Position, Speed);
        }

        public override void CopyPropsToStaticItem(StaticItem item) {
            if(item is Belt) {
                var belt = (Belt)item;
                belt.ShapeInstance.Transform.Position = Position;
                belt.Speed = Speed;
            }
        }

        public override void SetPos(Vector2 p) {
            Position = p;
        }
    }
}
