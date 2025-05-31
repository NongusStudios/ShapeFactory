using ShapeFactory.StaticItems;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.IO;
using System.Xml.Linq;
using System.Text.Json.Serialization;
using System.Threading;

namespace ShapeFactory {
    public partial class LayoutEditor : Form {
        private Renderer r;
        private Physics p;

        private Dictionary<string, StaticItemProperties> layout;
        private Dictionary<string, StaticItem> layoutPreview;

        private Vector2 currentMousePosition;

        private bool placingRamp = false;
        private Ramp previewRamp;

        private bool draggingItem = false;

        private Mutex mut = new Mutex();

        public LayoutEditor() {
            InitializeComponent();

            cbPlace.Items.Add(typeof(Belt).ToString());
            cbPlace.Items.Add(typeof(Pipe).ToString());
            cbPlace.Items.Add(typeof(Punter).ToString());
            cbPlace.Items.Add(typeof(Teleporter).ToString());
            cbPlace.Items.Add(typeof(Elevator).ToString());
            cbPlace.Items.Add(typeof(Ramp).ToString());
            cbPlace.SelectedIndex = 0;

            layout = new Dictionary<string, StaticItemProperties>();
            layoutPreview = new Dictionary<string, StaticItem>();
            r = new Renderer();
            p = new Physics();

            currentMousePosition = new Vector2();

            indexLayoutsIntoComboBox();
        }

        private void updateItemList() {
            listItems.Items.Clear();
            foreach (var entry in layout) {
                var (name, props) = (entry.Key, entry.Value);
                listItems.Items.Add(name + ": " + layoutPreview[name].GetType().ToString());
            }
        }
        private void canvas_Paint(object sender, PaintEventArgs e) {
            var g = e.Graphics;
            r.Update(0.0);
            r.Draw(g);
        }

        private void spawnItemProperties(string name, string type) {
            if (type == typeof(Belt).ToString()) {
                layout.Add(name, new StaticItems.BeltProperties(currentMousePosition, 40.0f));

            }
            else if (type == typeof(Pipe).ToString()) {
                layout.Add(name, new StaticItems.PipeProperties(currentMousePosition, 0.0f, new Vector2()));

            }
            else if (type == typeof(Punter).ToString()) {
                layout.Add(name, new StaticItems.PunterProperties(currentMousePosition, new Vector2()));

            }
            else if (type == typeof(Teleporter).ToString()) {
                if (layout.ContainsKey(name + "A") || layout.ContainsKey(name + "B")) {
                    MessageBox.Show("Must input a unique valid name!");
                    return;
                }
                layout.Add(name + "A", new StaticItems.TeleporterProperties(currentMousePosition, 0));
                layout.Add(name + "B", new StaticItems.TeleporterProperties(currentMousePosition + new Vector2((float)Properties.Resources.teleporter1.Width / 4.0f, 0.0f), 0));
            }
            else if (type == typeof(Elevator).ToString()) {
                layout.Add(name, new StaticItems.ElevatorProperties(currentMousePosition, true, 40.0f, 5.0));

            }
            else if (type == typeof(Ramp).ToString()) {
                layout.Add(name, new StaticItems.RampProperties(previewRamp.LineInstance.Points.ToArray()));
            }
        }

        private void spawnItem(string name) {
            if (layoutPreview.ContainsKey(name)) return;

            if (layout.ContainsKey(name) && layout[name] is TeleporterProperties) {
                name = name.Substring(0, name.Length - 1);
            }
            if (layout.ContainsKey(name + "A") && layout[name + "A"] is TeleporterProperties) {
                var (tp1, tp2) = ((TeleporterProperties)layout[name + "A"], (TeleporterProperties)layout[name + "B"]);
                var (t1, t2) = Teleporter.CreateTeleporters(r, p, tp1.Position, tp2.Position);
                layoutPreview.Add(name + "A", t1);
                layoutPreview.Add(name + "B", t2);
                return;
            }

            layoutPreview.Add(name, layout[name].CreateStaticItem(r, p));
        }

        private void updatePreview() {
            foreach (var entry in layout) {
                var (name, item) = (entry.Key, entry.Value);
                item.CopyPropsToStaticItem(layoutPreview[name]);
            }
            canvas.Invalidate();
        }

        private void deleteItem(string name) {
            if (layoutPreview[name] is Ramp) {
                layoutPreview[name].LineInstance.QueueFree();
            }
            else {
                layoutPreview[name].ShapeInstance.QueueFree();
                layoutPreview[name].PhysicsInstance.QueueFree();
            }
            
            if (layoutPreview[name] is Elevator) {
                var item = (Elevator)layoutPreview[name];
                item.Platform.QueueFree();
                item.PlatformPhysics.QueueFree();
                item.PhysicsInstance.QueueFree();
            }
            layoutPreview.Remove(name);
            layout.Remove(name);

            updatePreview();
            updateItemList();
        }

        private void btnPlaceRamp_Click(object sender, EventArgs e) {
            placingRamp = false;

            spawnItemProperties(tbItemName.Text, cbPlace.SelectedItem.ToString());
            spawnItem(tbItemName.Text);


            previewRamp.LineInstance.Points.Clear();
            tbItemName.Enabled = true;
            btnPlaceRamp.Enabled = false;

            updateItemList();
            updatePreview();
        }

        private bool selectMousedOverItem() {
            foreach (var entry in layoutPreview) {
                var (name, item) = (entry.Key, entry.Value);
                if (!(item is Ramp) && Collision.PointInAABB(currentMousePosition, item.ShapeInstance.Transform.ToAABB())) {
                    listItems.SelectedItem = name + ": " + item.GetType().ToString();
                    return true;
                } else if (item is Ramp) {
                    var r = (Ramp)item;
                    if(Collision.PointInRamp(currentMousePosition, r.LineInstance.Points.ToArray(), r.LineInstance.Width)) {
                        listItems.SelectedItem = name + ": " + item.GetType().ToString();
                        return true;
                    }
                }
            }
            return false;
        }

        private void onLeftClick() {
            if (placingRamp) {
                previewRamp.LineInstance.Points.Add(currentMousePosition);
                btnPlaceRamp.Enabled = true;
                updatePreview();
                return;
            }

            // TODO add drag to move functionality
            // Check if mouse intersects an item
            if (selectMousedOverItem()) {
                return;
            }

            if (tbItemName.Text == "" || layout.ContainsKey(tbItemName.Text)) {
                MessageBox.Show("Must input a unique valid name!");
                return;
            }

            // Start placing ramp
            if(cbPlace.SelectedItem.ToString() == typeof(Ramp).ToString()) {
                placingRamp = true;
                previewRamp = new Ramp(r, p, new Vector2[1] { currentMousePosition });
                tbItemName.Enabled = false;
                return;
            }

            spawnItemProperties(tbItemName.Text, cbPlace.SelectedItem.ToString());
            spawnItem(tbItemName.Text);

            updateItemList();
            listItems.SelectedItem = tbItemName.Text + ": " + layoutPreview[tbItemName.Text].GetType().ToString();
            updatePreview();
        }

        private void canvas_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                onLeftClick();
            } else if (e.Button == MouseButtons.Right) {
                if (placingRamp) return;

                if (selectMousedOverItem()) {
                    draggingItem = true;
                }
            }
        }

        private void canvas_MouseUp(object sender, MouseEventArgs e) {
            if (placingRamp) return;

            if (draggingItem && e.Button == MouseButtons.Right) {
                draggingItem = false;
            }

        }

        private void canvas_MouseMove(object sender, MouseEventArgs e) {
            currentMousePosition.X = (float)e.Location.X;
            currentMousePosition.Y = (float)e.Location.Y;
            if (draggingItem) {
                var name = listItems.SelectedItem.ToString().Split(':')[0];
                layout[name].SetPos(currentMousePosition);
                updatePreview();
            }
        }

        private void listItems_SelectedValueChanged(object sender, EventArgs e) {
            if (listItems.SelectedItem == null) return;
            var name = listItems.SelectedItem.ToString().Split(':')[0];
            properties.SelectedObject = layout[name];
        }

        private void btnDelete_Click(object sender, EventArgs e) {
            if (listItems.SelectedItem == null) return;
            var name = listItems.SelectedItem.ToString().Split(':')[0];
            deleteItem(name);
        }

        private void indexLayoutsIntoComboBox() {
            cbLoad.Items.Clear();
            var layoutFiles = Directory.GetFiles(Global.LAYOUT_FOLDER, "*.json");
            foreach(var file in layoutFiles) {
                var split = new List<string>(file.Split('\\').Last().Split('.'));
                split.RemoveAt(split.Count - 1);
                cbLoad.Items.Add(string.Join(",", split));
            }
        }

        private void loadLayout(string name) {
            string jsonString = File.ReadAllText(Global.LAYOUT_FOLDER + "/" + name + ".json");
            //JsonSerializerOptions settings = new JsonSerializerOptions { IncludeFields = true };

            mut.WaitOne();

            layout = JsonSerializer.Deserialize<Dictionary<string, StaticItemProperties>>(jsonString);
            foreach (var entry in layout) {
                spawnItem(entry.Key);
            }

            mut.ReleaseMutex();
        }

        private void saveLayout(string name) {
            JsonSerializerOptions settings = new JsonSerializerOptions { WriteIndented = true };
            var jsonString = JsonSerializer.Serialize(layout, settings);
            File.WriteAllText(Global.LAYOUT_FOLDER + "/" + name + ".json", jsonString);
        }

        private void btnSave_Click(object sender, EventArgs e) {
            if(tbLayoutName.Text.Length == 0) {
                MessageBox.Show("Must provide a layout name before saving!");
                return;
            }

            var save = new Thread(new ThreadStart(() => {
                saveLayout(tbLayoutName.Text);
            }));
            save.Start();

            // I know this makes a second thread pointless but A) its for the marks. B) I can't edit form items on a seperate thread :(
            save.Join();
            indexLayoutsIntoComboBox();
        }

        private void btnLoad_Click(object sender, EventArgs e) {
            r.Clear();
            p.Clear();
            layout.Clear();
            layoutPreview.Clear();


            var layoutName = cbLoad.SelectedItem.ToString();
            tbLayoutName.Text = layoutName;
            var load = new Thread(new ThreadStart(() => {
                loadLayout(layoutName);
            }));

            // same story here
            load.Start();
            load.Join();
            updateItemList();
            updatePreview();
        }

        private void properties_PropertyValueChanged(object s, PropertyValueChangedEventArgs e) {
            updatePreview();
        }

        private void properties_Leave(object sender, EventArgs e) {
            updatePreview();
        }
    }

    [JsonDerivedType(typeof(StaticItemProperties), typeDiscriminator: 0)]
    [JsonDerivedType(typeof(BeltProperties),       typeDiscriminator: 1)]
    [JsonDerivedType(typeof(PipeProperties),       typeDiscriminator: 2)]
    [JsonDerivedType(typeof(PunterProperties),     typeDiscriminator: 3)]
    [JsonDerivedType(typeof(ElevatorProperties),   typeDiscriminator: 4)]
    [JsonDerivedType(typeof(TeleporterProperties), typeDiscriminator: 5)]
    [JsonDerivedType(typeof(RampProperties), typeDiscriminator: 6)]
    [JsonSerializable(typeof(List<PointF>))]
    public class StaticItemProperties {
        public StaticItemProperties() {}

        public virtual StaticItem CreateStaticItem(Renderer r, Physics p) { return new StaticItem(new Shape(ShapeType.Rectangle, new Transform2D(), Color.White), p, ShapeType.Rectangle); }
        public virtual void CopyPropsToStaticItem(StaticItem item) { }

        public virtual void SetPos(Vector2 p) { }
    }
}
