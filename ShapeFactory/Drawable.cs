using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeFactory {
    public class Drawable {
        private bool queueFree;

        public Drawable() {
            this.queueFree = false;
        }

        public virtual void Update(double dt) {}
        public virtual void Draw(Graphics g) {}
        public virtual void QueueFree() {
            this.queueFree = true;
        }

        public bool IsQueuedFree() {
            return this.queueFree;
        }
    }
}
