namespace PolygonPainter
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.addPolygonButton = new System.Windows.Forms.RadioButton();
            this.setRelationButton = new System.Windows.Forms.RadioButton();
            this.ShapesDrawnLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.selectButton = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.addVertexToPolygonButton = new System.Windows.Forms.RadioButton();
            this.canvas = new System.Windows.Forms.PictureBox();
            this.automaticRelationBox = new System.Windows.Forms.CheckBox();
            this.fillPolygonButton = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.SuspendLayout();
            // 
            // addPolygonButton
            // 
            this.addPolygonButton.AutoSize = true;
            this.addPolygonButton.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.addPolygonButton.Location = new System.Drawing.Point(26, 54);
            this.addPolygonButton.Name = "addPolygonButton";
            this.addPolygonButton.Size = new System.Drawing.Size(106, 23);
            this.addPolygonButton.TabIndex = 0;
            this.addPolygonButton.TabStop = true;
            this.addPolygonButton.Text = "Add a polygon";
            this.addPolygonButton.UseVisualStyleBackColor = true;
            this.addPolygonButton.CheckedChanged += new System.EventHandler(this.addPolygonButton_CheckedChanged);
            // 
            // setRelationButton
            // 
            this.setRelationButton.AutoSize = true;
            this.setRelationButton.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.setRelationButton.Location = new System.Drawing.Point(26, 180);
            this.setRelationButton.Name = "setRelationButton";
            this.setRelationButton.Size = new System.Drawing.Size(101, 23);
            this.setRelationButton.TabIndex = 4;
            this.setRelationButton.TabStop = true;
            this.setRelationButton.Text = "Set a relation";
            this.setRelationButton.UseVisualStyleBackColor = true;
            this.setRelationButton.CheckedChanged += new System.EventHandler(this.setRelationButton_CheckedChanged);
            // 
            // ShapesDrawnLabel
            // 
            this.ShapesDrawnLabel.AutoSize = true;
            this.ShapesDrawnLabel.Location = new System.Drawing.Point(99, 306);
            this.ShapesDrawnLabel.Name = "ShapesDrawnLabel";
            this.ShapesDrawnLabel.Size = new System.Drawing.Size(13, 13);
            this.ShapesDrawnLabel.TabIndex = 7;
            this.ShapesDrawnLabel.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 307);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Shapes drawn:";
            // 
            // selectButton
            // 
            this.selectButton.AutoSize = true;
            this.selectButton.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.selectButton.Location = new System.Drawing.Point(26, 128);
            this.selectButton.Name = "selectButton";
            this.selectButton.Size = new System.Drawing.Size(60, 23);
            this.selectButton.TabIndex = 9;
            this.selectButton.TabStop = true;
            this.selectButton.Text = "Select";
            this.selectButton.UseVisualStyleBackColor = true;
            this.selectButton.CheckedChanged += new System.EventHandler(this.selectButton_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe Print", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(10, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 35);
            this.label2.TabIndex = 10;
            this.label2.Text = "Modes:";
            // 
            // addVertexToPolygonButton
            // 
            this.addVertexToPolygonButton.AutoSize = true;
            this.addVertexToPolygonButton.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.addVertexToPolygonButton.Location = new System.Drawing.Point(26, 92);
            this.addVertexToPolygonButton.Name = "addVertexToPolygonButton";
            this.addVertexToPolygonButton.Size = new System.Drawing.Size(170, 23);
            this.addVertexToPolygonButton.TabIndex = 11;
            this.addVertexToPolygonButton.TabStop = true;
            this.addVertexToPolygonButton.Text = "Add a vertex to a polygon";
            this.addVertexToPolygonButton.UseVisualStyleBackColor = true;
            this.addVertexToPolygonButton.CheckedChanged += new System.EventHandler(this.addVertexToPolygonButton_CheckedChanged);
            // 
            // canvas
            // 
            this.canvas.BackColor = System.Drawing.Color.White;
            this.canvas.Location = new System.Drawing.Point(202, 12);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(822, 613);
            this.canvas.TabIndex = 12;
            this.canvas.TabStop = false;
            this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.canvas_Paint);
            this.canvas.MouseClick += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseClick);
            this.canvas.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.canvas_MousefloatClick);
            this.canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseMove);
            this.canvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseUp);
            // 
            // automaticRelationBox
            // 
            this.automaticRelationBox.AutoSize = true;
            this.automaticRelationBox.Location = new System.Drawing.Point(47, 157);
            this.automaticRelationBox.Name = "automaticRelationBox";
            this.automaticRelationBox.Size = new System.Drawing.Size(115, 17);
            this.automaticRelationBox.TabIndex = 13;
            this.automaticRelationBox.Text = "Automatic relations";
            this.automaticRelationBox.UseVisualStyleBackColor = true;
            this.automaticRelationBox.CheckedChanged += new System.EventHandler(this.automaticRelationBox_CheckedChanged);
            // 
            // fillPolygonButton
            // 
            this.fillPolygonButton.AutoSize = true;
            this.fillPolygonButton.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.fillPolygonButton.Location = new System.Drawing.Point(26, 209);
            this.fillPolygonButton.Name = "fillPolygonButton";
            this.fillPolygonButton.Size = new System.Drawing.Size(99, 23);
            this.fillPolygonButton.TabIndex = 14;
            this.fillPolygonButton.TabStop = true;
            this.fillPolygonButton.Text = "Fill a polygon";
            this.fillPolygonButton.UseVisualStyleBackColor = true;
            this.fillPolygonButton.CheckedChanged += new System.EventHandler(this.fillPolygonButton_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.LightGreen;
            this.ClientSize = new System.Drawing.Size(1036, 637);
            this.Controls.Add(this.fillPolygonButton);
            this.Controls.Add(this.automaticRelationBox);
            this.Controls.Add(this.canvas);
            this.Controls.Add(this.addVertexToPolygonButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.selectButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ShapesDrawnLabel);
            this.Controls.Add(this.setRelationButton);
            this.Controls.Add(this.addPolygonButton);
            this.Name = "Form1";
            this.Text = "PolygonDrawer";
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton addPolygonButton;
        private System.Windows.Forms.RadioButton setRelationButton;
        private System.Windows.Forms.Label ShapesDrawnLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton selectButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton addVertexToPolygonButton;
        private System.Windows.Forms.PictureBox canvas;
        private System.Windows.Forms.CheckBox automaticRelationBox;
        private System.Windows.Forms.RadioButton fillPolygonButton;
    }
}

