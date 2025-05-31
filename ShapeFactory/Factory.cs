using ShapeFactory.Items;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShapeFactory {
    public class Factory {
        public List<AnomalousTriangle> AnomTriangles; // reference for triangles to access each other
        private List<Item> items;
        private List<StaticItem> staticItems;
        private List<int> freeQueue;

        public Factory() {
            items = new List<Item>();
            staticItems = new List<StaticItem>();
            freeQueue = new List<int>();
            AnomTriangles = new List<AnomalousTriangle>();
        }

        private void freeItems() {
            foreach (int i in freeQueue) {
                items.RemoveAt(i);
            }

            freeQueue.Clear();
        }

        public void Clear() {
            AnomTriangles.Clear();
            items.Clear();
            staticItems.Clear();
            freeQueue.Clear();
        }

        public void ClearItemsOnly() {
            foreach(var item in items) {
                item.QueueFree();
            }
            items.Clear();
            freeQueue.Clear();
        }

        public void AddItem(Item item) {
            items.Add(item);
        }

        public void AddStaticItem(StaticItem item) {
            staticItems.Add(item);
        }

        public void Update(double deltaTime) {
            for(int i = 0; i < items.Count; i ++) {
                var item = items[i];
                if (item.IsQueuedFree()) {
                    freeQueue.Insert(0, i);
                    continue;
                }
                item.Update(deltaTime);
            }

            foreach(StaticItem item in staticItems) {
                item.Update(deltaTime);
            }

            freeItems();
        }
    }
}
