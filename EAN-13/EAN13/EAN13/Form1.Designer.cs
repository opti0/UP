namespace EAN13
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
            this.inputBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.generateButton = new System.Windows.Forms.Button();
            this.controlBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBoxBarCode = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBarCode)).BeginInit();
            this.SuspendLayout();
            // 
            // inputBox
            // 
            this.inputBox.Location = new System.Drawing.Point(276, 63);
            this.inputBox.Name = "inputBox";
            this.inputBox.Size = new System.Drawing.Size(162, 26);
            this.inputBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(155, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Kod EAN-13";
            // 
            // generateButton
            // 
            this.generateButton.Location = new System.Drawing.Point(224, 187);
            this.generateButton.Name = "generateButton";
            this.generateButton.Size = new System.Drawing.Size(162, 36);
            this.generateButton.TabIndex = 2;
            this.generateButton.Text = "Generuj kod";
            this.generateButton.UseVisualStyleBackColor = true;
            this.generateButton.Click += new System.EventHandler(this.generateButton_Click);
            // 
            // controlBox
            // 
            this.controlBox.Enabled = false;
            this.controlBox.Location = new System.Drawing.Point(276, 130);
            this.controlBox.Name = "controlBox";
            this.controlBox.Size = new System.Drawing.Size(162, 26);
            this.controlBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(155, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Cyfra kontrolna";
            // 
            // pictureBoxBarCode
            // 
            this.pictureBoxBarCode.Location = new System.Drawing.Point(16, 279);
            this.pictureBoxBarCode.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBoxBarCode.Name = "pictureBoxBarCode";
            this.pictureBoxBarCode.Size = new System.Drawing.Size(560, 398);
            this.pictureBoxBarCode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxBarCode.TabIndex = 5;
            this.pictureBoxBarCode.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 698);
            this.Controls.Add(this.pictureBoxBarCode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.controlBox);
            this.Controls.Add(this.generateButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.inputBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Kody EAN-13";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBarCode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox inputBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button generateButton;
        private System.Windows.Forms.TextBox controlBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBoxBarCode;
    }
}

