namespace gsm2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.reader_select = new System.Windows.Forms.Label();
            this.apdu_command = new System.Windows.Forms.Label();
            this.commandTextBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.responseTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(189, 75);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(300, 28);
            this.comboBox1.TabIndex = 0;
            // 
            // reader_select
            // 
            this.reader_select.AutoSize = true;
            this.reader_select.Location = new System.Drawing.Point(35, 78);
            this.reader_select.Name = "reader_select";
            this.reader_select.Size = new System.Drawing.Size(115, 20);
            this.reader_select.TabIndex = 1;
            this.reader_select.Text = "Wybór czytnika";
            // 
            // apdu_command
            // 
            this.apdu_command.AutoSize = true;
            this.apdu_command.Location = new System.Drawing.Point(35, 149);
            this.apdu_command.Name = "apdu_command";
            this.apdu_command.Size = new System.Drawing.Size(126, 20);
            this.apdu_command.TabIndex = 2;
            this.apdu_command.Text = "Komenda APDU";
            // 
            // commandTextBox
            // 
            this.commandTextBox.Location = new System.Drawing.Point(189, 149);
            this.commandTextBox.Name = "commandTextBox";
            this.commandTextBox.Size = new System.Drawing.Size(300, 26);
            this.commandTextBox.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(278, 181);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 43);
            this.button1.TabIndex = 4;
            this.button1.Text = "Wyślij";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // responseTextBox
            // 
            this.responseTextBox.Location = new System.Drawing.Point(189, 262);
            this.responseTextBox.Multiline = true;
            this.responseTextBox.Name = "responseTextBox";
            this.responseTextBox.ReadOnly = true;
            this.responseTextBox.Size = new System.Drawing.Size(300, 171);
            this.responseTextBox.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 336);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Odpowiedź";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 472);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.responseTextBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.commandTextBox);
            this.Controls.Add(this.apdu_command);
            this.Controls.Add(this.reader_select);
            this.Controls.Add(this.comboBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "SIM CARD READER";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label reader_select;
        private System.Windows.Forms.Label apdu_command;
        private System.Windows.Forms.TextBox commandTextBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox responseTextBox;
        private System.Windows.Forms.Label label1;
    }
}