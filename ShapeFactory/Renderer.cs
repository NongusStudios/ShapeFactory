using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeFactory {
    public class Renderer {
        private List<Drawable> objects;
        private List<int> freeQueue;

        public Renderer() {
            this.objects = new List<Drawable>();
            this.freeQueue = new List<int>();
        }

        private void freeObjects() {
            foreach(int i in freeQueue) {
                objects.RemoveAt(i);
            }

            freeQueue.Clear();
        }

        public T AddDrawable<T>(T d) where T: Drawable {
            objects.Add(d);
            return (T)objects.Last();
        }

       public void Update(double dt) {
            for (int i = 0; i < objects.Count; i++) {
                var obj = objects[i];
                if (obj.IsQueuedFree()) {
                    freeQueue.Insert(0, i);
                    continue;
                }
                obj.Update(dt);
            }

            freeObjects();
        }

        public void Draw(Graphics g) {
            for(int i = 0; i < objects.Count; i++) {
                var obj = objects[i];
                obj.Draw(g);
            }
        }
    }
}
