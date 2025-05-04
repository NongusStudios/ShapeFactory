namespace ShapeFactory {
    partial class LayoutEditor {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.canvas = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.properties = new System.Windows.Forms.PropertyGrid();
            this.label3 = new System.Windows.Forms.Label();
            this.listItems = new System.Windows.Forms.ListBox();
            this.cbPlace = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbItemName = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.tbLayoutName = new System.Windows.Forms.TextBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.cbLoad = new System.Windows.Forms.ComboBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnPlaceRamp = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.SuspendLayout();
            // 
            // canvas
            // 
            this.canvas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.canvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.canvas.Location = new System.Drawing.Point(492, 12);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(528, 528);
            this.canvas.TabIndex = 1;
            this.canvas.TabStop = false;
            this.canvas.Click += new System.EventHandler(this.canvas_Click);
            this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.canvas_Paint);
            this.canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseMove);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Place";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Properties";
            // 
            // properties
            // 
            this.properties.Location = new System.Drawing.Point(12, 249);
            this.properties.Name = "properties";
            this.properties.Size = new System.Drawing.Size(470, 292);
            this.properties.TabIndex = 5;
            this.properties.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.properties_PropertyValueChanged);
            this.properties.Leave += new System.EventHandler(this.properties_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Items";
            // 
            // listItems
            // 
            this.listItems.FormattingEnabled = true;
            this.listItems.Location = new System.Drawing.Point(12, 70);
            this.listItems.Name = "listItems";
            this.listItems.Size = new System.Drawing.Size(472, 173);
            this.listItems.TabIndex = 8;
            this.listItems.SelectedValueChanged += new System.EventHandler(this.listItems_SelectedValueChanged);
            // 
            // cbPlace
            // 
            this.cbPlace.FormattingEnabled = true;
            this.cbPlace.Location = new System.Drawing.Point(52, 12);
            this.cbPlace.Name = "cbPlace";
            this.cbPlace.Size = new System.Drawing.Size(195, 21);
            this.cbPlace.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(255, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Name*";
            // 
            // tbItemName
            // 
            this.tbItemName.Location = new System.Drawing.Point(300, 13);
            this.tbItemName.Name = "tbItemName";
            this.tbItemName.Size = new System.Drawing.Size(182, 20);
            this.tbItemName.TabIndex = 10;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(782, 548);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tbLayoutName
            // 
            this.tbLayoutName.Location = new System.Drawing.Point(863, 550);
            this.tbLayoutName.Name = "tbLayoutName";
            this.tbLayoutName.Size = new System.Drawing.Size(157, 20);
            this.tbLayoutName.TabIndex = 12;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(12, 547);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 13;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // cbLoad
            // 
            this.cbLoad.FormattingEnabled = true;
            this.cbLoad.Location = new System.Drawing.Point(573, 548);
            this.cbLoad.Name = "cbLoad";
            this.cbLoad.Size = new System.Drawing.Size(203, 21);
            this.cbLoad.TabIndex = 14;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(492, 546);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 15;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnPlaceRamp
            // 
            this.btnPlaceRamp.Enabled = false;
            this.btnPlaceRamp.Location = new System.Drawing.Point(93, 548);
            this.btnPlaceRamp.Name = "btnPlaceRamp";
            this.btnPlaceRamp.Size = new System.Drawing.Size(75, 23);
            this.btnPlaceRamp.TabIndex = 16;
            this.btnPlaceRamp.Text = "Place Ramp";
            this.btnPlaceRamp.UseVisualStyleBackColor = true;
            this.btnPlaceRamp.Click += new System.EventHandler(this.btnPlaceRamp_Click);
            // 
            // LayoutEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1032, 577);
            this.Controls.Add(this.btnPlaceRamp);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.cbLoad);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.tbLayoutName);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tbItemName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.listItems);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbPlace);
            this.Controls.Add(this.properties);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.canvas);
            this.Name = "LayoutEditor";
            this.Text = "LayoutEditor";
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox canvas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PropertyGrid properties;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listItems;
        private System.Windows.Forms.ComboBox cbPlace;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbItemName;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox tbLayoutName;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ComboBox cbLoad;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnPlaceRamp;
    }
}