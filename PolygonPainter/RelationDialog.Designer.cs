namespace PolygonPainter
{
    partial class RelationDialog
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
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.verticalButton = new System.Windows.Forms.RadioButton();
            this.horizontalButton = new System.Windows.Forms.RadioButton();
            this.lengthButton = new System.Windows.Forms.RadioButton();
            this.lengthTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(88, 110);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(232, 110);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 0;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // verticalButton
            // 
            this.verticalButton.AutoSize = true;
            this.verticalButton.Location = new System.Drawing.Point(78, 12);
            this.verticalButton.Name = "verticalButton";
            this.verticalButton.Size = new System.Drawing.Size(106, 17);
            this.verticalButton.TabIndex = 1;
            this.verticalButton.TabStop = true;
            this.verticalButton.Text = "Set a vertical line";
            this.verticalButton.UseVisualStyleBackColor = true;
            // 
            // horizontalButton
            // 
            this.horizontalButton.AutoSize = true;
            this.horizontalButton.Location = new System.Drawing.Point(78, 35);
            this.horizontalButton.Name = "horizontalButton";
            this.horizontalButton.Size = new System.Drawing.Size(117, 17);
            this.horizontalButton.TabIndex = 1;
            this.horizontalButton.TabStop = true;
            this.horizontalButton.Text = "Set a horizontal line";
            this.horizontalButton.UseVisualStyleBackColor = true;
            // 
            // lengthButton
            // 
            this.lengthButton.AutoSize = true;
            this.lengthButton.Location = new System.Drawing.Point(78, 58);
            this.lengthButton.Name = "lengthButton";
            this.lengthButton.Size = new System.Drawing.Size(73, 17);
            this.lengthButton.TabIndex = 1;
            this.lengthButton.TabStop = true;
            this.lengthButton.Text = "Set length";
            this.lengthButton.UseVisualStyleBackColor = true;
            // 
            // lengthTextBox
            // 
            this.lengthTextBox.Location = new System.Drawing.Point(98, 81);
            this.lengthTextBox.Name = "lengthTextBox";
            this.lengthTextBox.Size = new System.Drawing.Size(74, 20);
            this.lengthTextBox.TabIndex = 2;
            // 
            // RelationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 163);
            this.Controls.Add(this.lengthTextBox);
            this.Controls.Add(this.lengthButton);
            this.Controls.Add(this.horizontalButton);
            this.Controls.Add(this.verticalButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Name = "RelationForm";
            this.Text = "Which relation would you like to set?";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.RadioButton verticalButton;
        private System.Windows.Forms.RadioButton horizontalButton;
        private System.Windows.Forms.RadioButton lengthButton;
        private System.Windows.Forms.TextBox lengthTextBox;
    }
}