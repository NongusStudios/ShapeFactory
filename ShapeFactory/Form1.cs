using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShapeFactory {
    public partial class Form1 : Form {
        private DateTime lastFrameTime;

        public const double MaxFrameRate = 60.0;
        public const double PhysicsFrameRate = 50.0;

        public Factory factory;

        public Form1() {
            InitializeComponent();

            factory = new Factory();

            lastFrameTime = DateTime.Now;
            
            // Setup loop timer
            Timer updateTimer = new Timer();
            updateTimer.Interval = (int)Math.Floor(1000.0 / MaxFrameRate);
            updateTimer.Tick += (s, e) => this.update();
            updateTimer.Enabled = true;
        }

        private void update() {
            // deltaTime
            double dt = (DateTime.Now - lastFrameTime).TotalSeconds;
            lastFrameTime = DateTime.Now;

            factory.Update(dt);
            factory.PhysicsUpdate(dt);
            canvas.Invalidate();
        }

        private void canvas_Frame(object sender, PaintEventArgs e) {
            var g = e.Graphics;

            factory.Draw(g);
        }
    }
}
