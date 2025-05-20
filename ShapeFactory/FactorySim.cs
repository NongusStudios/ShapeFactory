using ShapeFactory.Items;
using ShapeFactory.StaticItems;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShapeFactory {
    public partial class FactorySim : Form {
        private Renderer renderer;
        private Physics physics;
        private DateTime lastFrameTime;
        private Random random;

        public const double MaxFrameRate = 60.0;
        public const double PhysicsFrameRate = 50.0;
        private const string DefaultLayout = "default";

        private Dictionary<string, (Vector2, Vector2)> spawnPoints;

        public Factory factory;

        public FactorySim() {
            InitializeComponent();

            random = new Random();

            cbShape.Items.Add(typeof(LeadBall).ToString());
            cbShape.Items.Add(typeof(AnomalousTriangle).ToString());
            cbShape.Items.Add(typeof(PlutoniumCylinder).ToString());
            cbShape.SelectedIndex = 0;

            if (!Directory.Exists(Global.LAYOUT_FOLDER)) Directory.CreateDirectory(Global.LAYOUT_FOLDER);

            factory = new Factory();

            lastFrameTime = DateTime.Now;
            spawnPoints = new Dictionary<string, (Vector2, Vector2)>();

            renderer = new Renderer();
            physics = new Physics();

            indexLayoutsIntoComboBox();

            // Setup loop timer
            Timer updateTimer = new Timer();
            updateTimer.Interval = (int)Math.Floor(1000.0 / MaxFrameRate);
            updateTimer.Tick += (s, e) => this.update();
            updateTimer.Enabled = true;

            loadLayout(DefaultLayout);
        }
        private void indexLayoutsIntoComboBox() {
            cbLayout.Items.Clear();
            var layoutFiles = Directory.GetFiles(Global.LAYOUT_FOLDER, "*.json");
            foreach (var file in layoutFiles) {
                var split = new List<string>(file.Split('\\').Last().Split('.'));
                split.RemoveAt(split.Count - 1);
                cbLayout.Items.Add(string.Join(",", split));
            }
        }

        private void update() {
            // deltaTime
            double dt = (DateTime.Now - lastFrameTime).TotalSeconds;
            lastFrameTime = DateTime.Now;

            physics.PhysicsStep(dt);
            factory.Update(dt);
            renderer.Update(dt);

            canvas.Invalidate();
        }

        private void canvas_Frame(object sender, PaintEventArgs e) {
            var g = e.Graphics;

            renderer.Draw(g);
        }

        private void btnOpenEditor_Click(object sender, EventArgs e) {
            var editor = new LayoutEditor();
            editor.FormClosed += (s, ee) => indexLayoutsIntoComboBox();
            editor.Show();
        }

        private void loadLayout(string name) {
            cbSpawnPoint.Items.Clear();
            
            string jsonString = File.ReadAllText(Global.LAYOUT_FOLDER + "/" + name + ".json");
            var layout = JsonSerializer.Deserialize<Dictionary<string, StaticItemProperties>>(jsonString);

            foreach(var entry in layout) {
                var props = entry.Value;
                if(props is TeleporterProperties && entry.Key.Last() == 'A') {
                    var itemName = entry.Key.Substring(0, entry.Key.Length - 1);
                    var (tp1, tp2) = ((TeleporterProperties)layout[itemName + "A"], (TeleporterProperties)layout[itemName + "B"]);
                    var (t1, t2) = Teleporter.CreateTeleporters(renderer, physics, tp1.Position, tp1.Rotation, tp2.Position, tp2.Rotation);
                    factory.AddStaticItem(t1);
                    factory.AddStaticItem(t2);
                    continue;
                }
                factory.AddStaticItem(props.CreateStaticItem(renderer, physics));

                if(props is PipeProperties) {
                    var p = props as PipeProperties;
                    cbSpawnPoint.Items.Add(entry.Key);
                    spawnPoints[entry.Key] = (p.Position, p.AddedSpawnVelocity);
                }
            }
            cbSpawnPoint.SelectedIndex = 0;
        }

        private void btnLoadlayout_Click(object sender, EventArgs e) {
            factory.Clear();
            renderer.Clear();
            physics.Clear();
            loadLayout(cbLayout.SelectedItem.ToString());
        }

        private void btnSpawn_Click(object sender, EventArgs e) {
            var selectedItem = cbShape.SelectedItem.ToString();
            var selectedSpawn = cbSpawnPoint.SelectedItem.ToString();

            if (selectedItem == typeof(LeadBall).ToString()) {
                var lb = new LeadBall(renderer, physics, spawnPoints[selectedSpawn].Item1);
                lb.PhysicsInstance.Velocity = spawnPoints[selectedSpawn].Item2;
                factory.AddItem(lb);
            } else if (selectedItem == typeof(AnomalousTriangle).ToString()) {
                var at = new AnomalousTriangle(renderer, physics, spawnPoints[selectedSpawn].Item1);
                at.PhysicsInstance.Velocity = spawnPoints[selectedSpawn].Item2;
                at.PhysicsInstance.AngularVelocity = (float)(random.NextDouble() * 2.0 - 1.0) * spawnPoints[selectedSpawn].Item2.Length() * 4.0f;
                factory.AddItem(at);
            } else if (selectedItem == typeof(PlutoniumCylinder).ToString()) {
                var pc = new PlutoniumCylinder(renderer, physics, spawnPoints[selectedSpawn].Item1);
                pc.PhysicsInstance.Velocity = spawnPoints[selectedSpawn].Item2;
                factory.AddItem(pc);
            } else {
                MessageBox.Show(selectedItem + " is not a valid item!");
            }
        }

        private void btnReset_Click(object sender, EventArgs e) {
            factory.ClearItemsOnly();
        }
    }
}
