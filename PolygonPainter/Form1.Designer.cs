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
            this.components = new System.ComponentModel.Container();
            this.lightChangeTimer = new System.Windows.Forms.Timer(this.components);
            this.canvas = new System.Windows.Forms.PictureBox();
            this.ShapesDrawnLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.modesBox = new System.Windows.Forms.GroupBox();
            this.addPolygonButton = new System.Windows.Forms.RadioButton();
            this.lightingOptionsGroup = new System.Windows.Forms.GroupBox();
            this.fixedVectorLightButton = new System.Windows.Forms.RadioButton();
            this.animatedLightButton = new System.Windows.Forms.RadioButton();
            this.manualLightChangeButton = new System.Windows.Forms.RadioButton();
            this.intersectionButton = new System.Windows.Forms.RadioButton();
            this.fillButton = new System.Windows.Forms.RadioButton();
            this.automaticRelationBox = new System.Windows.Forms.CheckBox();
            this.addVertexToPolygonButton = new System.Windows.Forms.RadioButton();
            this.selectButton = new System.Windows.Forms.RadioButton();
            this.setRelationButton = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.modesBox.SuspendLayout();
            this.lightingOptionsGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // lightChangeTimer
            // 
            this.lightChangeTimer.Enabled = true;
            this.lightChangeTimer.Tick += new System.EventHandler(this.lightChangeTimer_Tick);
            // 
            // canvas
            // 
            this.canvas.BackColor = System.Drawing.Color.White;
            this.canvas.Location = new System.Drawing.Point(240, 12);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(600, 600);
            this.canvas.TabIndex = 12;
            this.canvas.TabStop = false;
            this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.canvas_Paint);
            this.canvas.MouseClick += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseClick);
            this.canvas.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.canvas_MousedoubleClick);
            this.canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseMove);
            this.canvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseUp);
            // 
            // ShapesDrawnLabel
            // 
            this.ShapesDrawnLabel.AutoSize = true;
            this.ShapesDrawnLabel.Location = new System.Drawing.Point(112, 397);
            this.ShapesDrawnLabel.Name = "ShapesDrawnLabel";
            this.ShapesDrawnLabel.Size = new System.Drawing.Size(13, 13);
            this.ShapesDrawnLabel.TabIndex = 7;
            this.ShapesDrawnLabel.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 398);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Shapes drawn:";
            // 
            // modesBox
            // 
            this.modesBox.Controls.Add(this.addPolygonButton);
            this.modesBox.Controls.Add(this.lightingOptionsGroup);
            this.modesBox.Controls.Add(this.intersectionButton);
            this.modesBox.Controls.Add(this.fillButton);
            this.modesBox.Controls.Add(this.automaticRelationBox);
            this.modesBox.Controls.Add(this.addVertexToPolygonButton);
            this.modesBox.Controls.Add(this.selectButton);
            this.modesBox.Controls.Add(this.setRelationButton);
            this.modesBox.Location = new System.Drawing.Point(0, 12);
            this.modesBox.Name = "modesBox";
            this.modesBox.Size = new System.Drawing.Size(234, 355);
            this.modesBox.TabIndex = 13;
            this.modesBox.TabStop = false;
            this.modesBox.Text = "Modes";
            // 
            // addPolygonButton
            // 
            this.addPolygonButton.AutoSize = true;
            this.addPolygonButton.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.addPolygonButton.Location = new System.Drawing.Point(13, 26);
            this.addPolygonButton.Name = "addPolygonButton";
            this.addPolygonButton.Size = new System.Drawing.Size(106, 23);
            this.addPolygonButton.TabIndex = 17;
            this.addPolygonButton.TabStop = true;
            this.addPolygonButton.Text = "Add a polygon";
            this.addPolygonButton.UseVisualStyleBackColor = true;
            this.addPolygonButton.CheckedChanged += new System.EventHandler(this.addPolygonButton_CheckedChanged);
            // 
            // lightingOptionsGroup
            // 
            this.lightingOptionsGroup.Controls.Add(this.fixedVectorLightButton);
            this.lightingOptionsGroup.Controls.Add(this.animatedLightButton);
            this.lightingOptionsGroup.Controls.Add(this.manualLightChangeButton);
            this.lightingOptionsGroup.Location = new System.Drawing.Point(23, 209);
            this.lightingOptionsGroup.Name = "lightingOptionsGroup";
            this.lightingOptionsGroup.Size = new System.Drawing.Size(198, 89);
            this.lightingOptionsGroup.TabIndex = 24;
            this.lightingOptionsGroup.TabStop = false;
            this.lightingOptionsGroup.Text = "Lighting options";
            // 
            // fixedVectorLightButton
            // 
            this.fixedVectorLightButton.AutoSize = true;
            this.fixedVectorLightButton.Location = new System.Drawing.Point(6, 65);
            this.fixedVectorLightButton.Name = "fixedVectorLightButton";
            this.fixedVectorLightButton.Size = new System.Drawing.Size(187, 17);
            this.fixedVectorLightButton.TabIndex = 2;
            this.fixedVectorLightButton.TabStop = true;
            this.fixedVectorLightButton.Text = "A fixed vector to the light - [0, 0, 1]";
            this.fixedVectorLightButton.UseVisualStyleBackColor = true;
            this.fixedVectorLightButton.CheckedChanged += new System.EventHandler(this.fixedVectorLightButton_CheckedChanged);
            // 
            // animatedLightButton
            // 
            this.animatedLightButton.AutoSize = true;
            this.animatedLightButton.Location = new System.Drawing.Point(6, 42);
            this.animatedLightButton.Name = "animatedLightButton";
            this.animatedLightButton.Size = new System.Drawing.Size(128, 17);
            this.animatedLightButton.TabIndex = 1;
            this.animatedLightButton.TabStop = true;
            this.animatedLightButton.Text = "Animated on a sphere";
            this.animatedLightButton.UseVisualStyleBackColor = true;
            this.animatedLightButton.CheckedChanged += new System.EventHandler(this.animatedLightButton_CheckedChanged);
            // 
            // manualLightChangeButton
            // 
            this.manualLightChangeButton.AutoSize = true;
            this.manualLightChangeButton.Location = new System.Drawing.Point(6, 19);
            this.manualLightChangeButton.Name = "manualLightChangeButton";
            this.manualLightChangeButton.Size = new System.Drawing.Size(179, 17);
            this.manualLightChangeButton.TabIndex = 0;
            this.manualLightChangeButton.TabStop = true;
            this.manualLightChangeButton.Text = "Change the light\'s point manually";
            this.manualLightChangeButton.UseVisualStyleBackColor = true;
            this.manualLightChangeButton.CheckedChanged += new System.EventHandler(this.manualLightChangeButton_CheckedChanged);
            // 
            // intersectionButton
            // 
            this.intersectionButton.AutoSize = true;
            this.intersectionButton.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.intersectionButton.Location = new System.Drawing.Point(13, 304);
            this.intersectionButton.Name = "intersectionButton";
            this.intersectionButton.Size = new System.Drawing.Size(92, 23);
            this.intersectionButton.TabIndex = 23;
            this.intersectionButton.TabStop = true;
            this.intersectionButton.Text = "Intersection";
            this.intersectionButton.UseVisualStyleBackColor = true;
            this.intersectionButton.CheckedChanged += new System.EventHandler(this.intersectionButton_CheckedChanged);
            // 
            // fillButton
            // 
            this.fillButton.AutoSize = true;
            this.fillButton.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.fillButton.Location = new System.Drawing.Point(13, 185);
            this.fillButton.Name = "fillButton";
            this.fillButton.Size = new System.Drawing.Size(43, 23);
            this.fillButton.TabIndex = 22;
            this.fillButton.TabStop = true;
            this.fillButton.Text = "Fill";
            this.fillButton.UseVisualStyleBackColor = true;
            this.fillButton.CheckedChanged += new System.EventHandler(this.fillButton_CheckedChanged);
            // 
            // automaticRelationBox
            // 
            this.automaticRelationBox.AutoSize = true;
            this.automaticRelationBox.Location = new System.Drawing.Point(34, 133);
            this.automaticRelationBox.Name = "automaticRelationBox";
            this.automaticRelationBox.Size = new System.Drawing.Size(115, 17);
            this.automaticRelationBox.TabIndex = 21;
            this.automaticRelationBox.Text = "Automatic relations";
            this.automaticRelationBox.UseVisualStyleBackColor = true;
            this.automaticRelationBox.CheckedChanged += new System.EventHandler(this.automaticRelationBox_CheckedChanged);
            // 
            // addVertexToPolygonButton
            // 
            this.addVertexToPolygonButton.AutoSize = true;
            this.addVertexToPolygonButton.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.addVertexToPolygonButton.Location = new System.Drawing.Point(13, 68);
            this.addVertexToPolygonButton.Name = "addVertexToPolygonButton";
            this.addVertexToPolygonButton.Size = new System.Drawing.Size(170, 23);
            this.addVertexToPolygonButton.TabIndex = 20;
            this.addVertexToPolygonButton.TabStop = true;
            this.addVertexToPolygonButton.Text = "Add a vertex to a polygon";
            this.addVertexToPolygonButton.UseVisualStyleBackColor = true;
            this.addVertexToPolygonButton.CheckedChanged += new System.EventHandler(this.addVertexToPolygonButton_CheckedChanged);
            // 
            // selectButton
            // 
            this.selectButton.AutoSize = true;
            this.selectButton.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.selectButton.Location = new System.Drawing.Point(13, 104);
            this.selectButton.Name = "selectButton";
            this.selectButton.Size = new System.Drawing.Size(60, 23);
            this.selectButton.TabIndex = 19;
            this.selectButton.TabStop = true;
            this.selectButton.Text = "Select";
            this.selectButton.UseVisualStyleBackColor = true;
            this.selectButton.CheckedChanged += new System.EventHandler(this.selectButton_CheckedChanged);
            // 
            // setRelationButton
            // 
            this.setRelationButton.AutoSize = true;
            this.setRelationButton.Font = new System.Drawing.Font("Segoe Print", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.setRelationButton.Location = new System.Drawing.Point(13, 156);
            this.setRelationButton.Name = "setRelationButton";
            this.setRelationButton.Size = new System.Drawing.Size(101, 23);
            this.setRelationButton.TabIndex = 18;
            this.setRelationButton.TabStop = true;
            this.setRelationButton.Text = "Set a relation";
            this.setRelationButton.UseVisualStyleBackColor = true;
            this.setRelationButton.CheckedChanged += new System.EventHandler(this.setRelationButton_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.LightGreen;
            this.ClientSize = new System.Drawing.Size(846, 617);
            this.Controls.Add(this.modesBox);
            this.Controls.Add(this.canvas);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ShapesDrawnLabel);
            this.Name = "Form1";
            this.Text = "PolygonDrawer";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.modesBox.ResumeLayout(false);
            this.modesBox.PerformLayout();
            this.lightingOptionsGroup.ResumeLayout(false);
            this.lightingOptionsGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer lightChangeTimer;
        private System.Windows.Forms.PictureBox canvas;
        private System.Windows.Forms.Label ShapesDrawnLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox modesBox;
        private System.Windows.Forms.RadioButton addPolygonButton;
        private System.Windows.Forms.GroupBox lightingOptionsGroup;
        private System.Windows.Forms.RadioButton fixedVectorLightButton;
        private System.Windows.Forms.RadioButton animatedLightButton;
        private System.Windows.Forms.RadioButton manualLightChangeButton;
        private System.Windows.Forms.RadioButton intersectionButton;
        private System.Windows.Forms.RadioButton fillButton;
        private System.Windows.Forms.CheckBox automaticRelationBox;
        private System.Windows.Forms.RadioButton addVertexToPolygonButton;
        private System.Windows.Forms.RadioButton selectButton;
        private System.Windows.Forms.RadioButton setRelationButton;
    }
}

