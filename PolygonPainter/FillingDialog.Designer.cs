namespace PolygonPainter
{
    partial class FillingDialog
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
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.lightColorButton = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.lightColorLabel = new System.Windows.Forms.Label();
            this.textureLabel = new System.Windows.Forms.Label();
            this.colorPanel = new System.Windows.Forms.Panel();
            this.textureButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.texturePanel = new System.Windows.Forms.Panel();
            this.normalVectorsLabel = new System.Windows.Forms.Label();
            this.normalVectorsPanel = new System.Windows.Forms.Panel();
            this.normalVectorsButton = new System.Windows.Forms.Button();
            this.heightMapLabel = new System.Windows.Forms.Label();
            this.heightMapPanel = new System.Windows.Forms.Panel();
            this.heightMapButton = new System.Windows.Forms.Button();
            this.pyramidCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // colorDialog
            // 
            this.colorDialog.Color = System.Drawing.Color.Blue;
            // 
            // lightColorButton
            // 
            this.lightColorButton.Location = new System.Drawing.Point(232, 37);
            this.lightColorButton.Name = "lightColorButton";
            this.lightColorButton.Size = new System.Drawing.Size(75, 23);
            this.lightColorButton.TabIndex = 0;
            this.lightColorButton.Text = "Choose";
            this.lightColorButton.UseVisualStyleBackColor = true;
            this.lightColorButton.Click += new System.EventHandler(this.lightColorButton_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            this.openFileDialog.InitialDirectory = "..\\..\\..\\sample_images\\";
            // 
            // lightColorLabel
            // 
            this.lightColorLabel.AutoSize = true;
            this.lightColorLabel.Font = new System.Drawing.Font("Sylfaen", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lightColorLabel.Location = new System.Drawing.Point(12, 37);
            this.lightColorLabel.Name = "lightColorLabel";
            this.lightColorLabel.Size = new System.Drawing.Size(99, 22);
            this.lightColorLabel.TabIndex = 1;
            this.lightColorLabel.Text = "Color of light";
            // 
            // textureLabel
            // 
            this.textureLabel.AutoSize = true;
            this.textureLabel.Font = new System.Drawing.Font("Sylfaen", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textureLabel.Location = new System.Drawing.Point(12, 116);
            this.textureLabel.Name = "textureLabel";
            this.textureLabel.Size = new System.Drawing.Size(60, 22);
            this.textureLabel.TabIndex = 2;
            this.textureLabel.Text = "Texture";
            // 
            // colorPanel
            // 
            this.colorPanel.Location = new System.Drawing.Point(165, 12);
            this.colorPanel.Name = "colorPanel";
            this.colorPanel.Size = new System.Drawing.Size(49, 48);
            this.colorPanel.TabIndex = 3;
            // 
            // textureButton
            // 
            this.textureButton.Location = new System.Drawing.Point(232, 128);
            this.textureButton.Name = "textureButton";
            this.textureButton.Size = new System.Drawing.Size(75, 23);
            this.textureButton.TabIndex = 4;
            this.textureButton.Text = "Choose";
            this.textureButton.UseVisualStyleBackColor = true;
            this.textureButton.Click += new System.EventHandler(this.textureButton_Click);
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(63, 338);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 5;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(412, 338);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 6;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // texturePanel
            // 
            this.texturePanel.Location = new System.Drawing.Point(165, 103);
            this.texturePanel.Name = "texturePanel";
            this.texturePanel.Size = new System.Drawing.Size(49, 48);
            this.texturePanel.TabIndex = 7;
            // 
            // normalVectorsLabel
            // 
            this.normalVectorsLabel.AutoSize = true;
            this.normalVectorsLabel.Font = new System.Drawing.Font("Sylfaen", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.normalVectorsLabel.Location = new System.Drawing.Point(12, 201);
            this.normalVectorsLabel.Name = "normalVectorsLabel";
            this.normalVectorsLabel.Size = new System.Drawing.Size(112, 22);
            this.normalVectorsLabel.TabIndex = 8;
            this.normalVectorsLabel.Text = "Normal vectors";
            // 
            // normalVectorsPanel
            // 
            this.normalVectorsPanel.Location = new System.Drawing.Point(165, 184);
            this.normalVectorsPanel.Name = "normalVectorsPanel";
            this.normalVectorsPanel.Size = new System.Drawing.Size(49, 48);
            this.normalVectorsPanel.TabIndex = 9;
            // 
            // normalVectorsButton
            // 
            this.normalVectorsButton.Location = new System.Drawing.Point(232, 209);
            this.normalVectorsButton.Name = "normalVectorsButton";
            this.normalVectorsButton.Size = new System.Drawing.Size(75, 23);
            this.normalVectorsButton.TabIndex = 10;
            this.normalVectorsButton.Text = "Choose";
            this.normalVectorsButton.UseVisualStyleBackColor = true;
            this.normalVectorsButton.Click += new System.EventHandler(this.normalVectorsButton_Click);
            // 
            // heightMapLabel
            // 
            this.heightMapLabel.AutoSize = true;
            this.heightMapLabel.Font = new System.Drawing.Font("Sylfaen", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.heightMapLabel.Location = new System.Drawing.Point(12, 290);
            this.heightMapLabel.Name = "heightMapLabel";
            this.heightMapLabel.Size = new System.Drawing.Size(87, 22);
            this.heightMapLabel.TabIndex = 11;
            this.heightMapLabel.Text = "Height map";
            // 
            // heightMapPanel
            // 
            this.heightMapPanel.Location = new System.Drawing.Point(165, 264);
            this.heightMapPanel.Name = "heightMapPanel";
            this.heightMapPanel.Size = new System.Drawing.Size(49, 48);
            this.heightMapPanel.TabIndex = 12;
            // 
            // heightMapButton
            // 
            this.heightMapButton.Location = new System.Drawing.Point(232, 291);
            this.heightMapButton.Name = "heightMapButton";
            this.heightMapButton.Size = new System.Drawing.Size(75, 23);
            this.heightMapButton.TabIndex = 13;
            this.heightMapButton.Text = "Choose";
            this.heightMapButton.UseVisualStyleBackColor = true;
            this.heightMapButton.Click += new System.EventHandler(this.heightMapButton_Click);
            // 
            // pyramidCheckBox
            // 
            this.pyramidCheckBox.AutoSize = true;
            this.pyramidCheckBox.Location = new System.Drawing.Point(31, 240);
            this.pyramidCheckBox.Name = "pyramidCheckBox";
            this.pyramidCheckBox.Size = new System.Drawing.Size(135, 17);
            this.pyramidCheckBox.TabIndex = 14;
            this.pyramidCheckBox.Text = "Pyramid normal vectors";
            this.pyramidCheckBox.UseVisualStyleBackColor = true;
            // 
            // FillingDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 373);
            this.Controls.Add(this.pyramidCheckBox);
            this.Controls.Add(this.heightMapButton);
            this.Controls.Add(this.heightMapPanel);
            this.Controls.Add(this.heightMapLabel);
            this.Controls.Add(this.normalVectorsButton);
            this.Controls.Add(this.normalVectorsPanel);
            this.Controls.Add(this.normalVectorsLabel);
            this.Controls.Add(this.texturePanel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.textureButton);
            this.Controls.Add(this.colorPanel);
            this.Controls.Add(this.textureLabel);
            this.Controls.Add(this.lightColorLabel);
            this.Controls.Add(this.lightColorButton);
            this.Name = "FillingDialog";
            this.Text = "FillingDialog";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FillingDialog_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.Button lightColorButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label lightColorLabel;
        private System.Windows.Forms.Label textureLabel;
        private System.Windows.Forms.Panel colorPanel;
        private System.Windows.Forms.Button textureButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Panel texturePanel;
        private System.Windows.Forms.Label normalVectorsLabel;
        private System.Windows.Forms.Panel normalVectorsPanel;
        private System.Windows.Forms.Button normalVectorsButton;
        private System.Windows.Forms.Label heightMapLabel;
        private System.Windows.Forms.Panel heightMapPanel;
        private System.Windows.Forms.Button heightMapButton;
        private System.Windows.Forms.CheckBox pyramidCheckBox;
    }
}