namespace ShapeFactory {
    partial class Form1 {
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
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.SuspendLayout();
            // 
            // canvas
            // 
            this.canvas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.canvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.canvas.Location = new System.Drawing.Point(172, 12);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(800, 600);
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
            this.btnReset.Location = new System.Drawing.Point(16, 588);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 6;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 621);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.cbSpawnPoint);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbShape);
            this.Controls.Add(this.lbl1);
            this.Controls.Add(this.btnSpawn);
            this.Controls.Add(this.canvas);
            this.Name = "Form1";
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
    }
}

