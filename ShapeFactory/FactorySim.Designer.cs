namespace ShapeFactory {
    partial class FactorySim {
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
            this.btnSpawn = new System.Windows.Forms.Button();
            this.lbl1 = new System.Windows.Forms.Label();
            this.cbShape = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbSpawnPoint = new System.Windows.Forms.ComboBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.cbLayout = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOpenEditor = new System.Windows.Forms.Button();
            this.btnLoadlayout = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.SuspendLayout();
            // 
            // canvas
            // 
            this.canvas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.canvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.canvas.Location = new System.Drawing.Point(172, 12);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(528, 528);
            this.canvas.TabIndex = 0;
            this.canvas.TabStop = false;
            this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.canvas_Frame);
            // 
            // btnSpawn
            // 
            this.btnSpawn.Location = new System.Drawing.Point(13, 13);
            this.btnSpawn.Name = "btnSpawn";
            this.btnSpawn.Size = new System.Drawing.Size(75, 23);
            this.btnSpawn.TabIndex = 1;
            this.btnSpawn.Text = "Spawn";
            this.btnSpawn.UseVisualStyleBackColor = true;
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Location = new System.Drawing.Point(13, 43);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(83, 13);
            this.lbl1.TabIndex = 2;
            this.lbl1.Text = "Selected Shape";
            // 
            // cbShape
            // 
            this.cbShape.FormattingEnabled = true;
            this.cbShape.Location = new System.Drawing.Point(16, 60);
            this.cbShape.Name = "cbShape";
            this.cbShape.Size = new System.Drawing.Size(121, 21);
            this.cbShape.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Spawn Point";
            // 
            // cbSpawnPoint
            // 
            this.cbSpawnPoint.FormattingEnabled = true;
            this.cbSpawnPoint.Location = new System.Drawing.Point(13, 105);
            this.cbSpawnPoint.Name = "cbSpawnPoint";
            this.cbSpawnPoint.Size = new System.Drawing.Size(121, 21);
            this.cbSpawnPoint.TabIndex = 5;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(91, 13);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 6;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            // 
            // cbLayout
            // 
            this.cbLayout.FormattingEnabled = true;
            this.cbLayout.Location = new System.Drawing.Point(12, 145);
            this.cbLayout.Name = "cbLayout";
            this.cbLayout.Size = new System.Drawing.Size(121, 21);
            this.cbLayout.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 129);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Layout";
            // 
            // btnOpenEditor
            // 
            this.btnOpenEditor.Location = new System.Drawing.Point(8, 518);
            this.btnOpenEditor.Name = "btnOpenEditor";
            this.btnOpenEditor.Size = new System.Drawing.Size(75, 23);
            this.btnOpenEditor.TabIndex = 9;
            this.btnOpenEditor.Text = "Open Editor";
            this.btnOpenEditor.UseVisualStyleBackColor = true;
            this.btnOpenEditor.Click += new System.EventHandler(this.btnOpenEditor_Click);
            // 
            // btnLoadlayout
            // 
            this.btnLoadlayout.Location = new System.Drawing.Point(12, 172);
            this.btnLoadlayout.Name = "btnLoadlayout";
            this.btnLoadlayout.Size = new System.Drawing.Size(75, 23);
            this.btnLoadlayout.TabIndex = 10;
            this.btnLoadlayout.Text = "Load";
            this.btnLoadlayout.UseVisualStyleBackColor = true;
            this.btnLoadlayout.Click += new System.EventHandler(this.btnLoadlayout_Click);
            // 
            // FactorySim
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 553);
            this.Controls.Add(this.btnLoadlayout);
            this.Controls.Add(this.btnOpenEditor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbLayout);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.cbSpawnPoint);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbShape);
            this.Controls.Add(this.lbl1);
            this.Controls.Add(this.btnSpawn);
            this.Controls.Add(this.canvas);
            this.Name = "FactorySim";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox canvas;
        private System.Windows.Forms.Button btnSpawn;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.ComboBox cbShape;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbSpawnPoint;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.ComboBox cbLayout;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOpenEditor;
        private System.Windows.Forms.Button btnLoadlayout;
    }
}

