namespace individualProject440
{
    partial class ImportForm
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
            this.feedbackLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.browseButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.allowImportLabel = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.filesLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.folderLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // feedbackLabel
            // 
            this.feedbackLabel.AutoSize = true;
            this.feedbackLabel.BackColor = System.Drawing.Color.LightCyan;
            this.feedbackLabel.ForeColor = System.Drawing.Color.Red;
            this.feedbackLabel.Location = new System.Drawing.Point(343, 309);
            this.feedbackLabel.Name = "feedbackLabel";
            this.feedbackLabel.Size = new System.Drawing.Size(0, 16);
            this.feedbackLabel.TabIndex = 31;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.LightCyan;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F);
            this.label2.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label2.Location = new System.Drawing.Point(615, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(333, 58);
            this.label2.TabIndex = 30;
            this.label2.Text = "ImportGrades";
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cancelButton.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.cancelButton.Location = new System.Drawing.Point(297, 378);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(146, 86);
            this.cancelButton.TabIndex = 28;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // browseButton
            // 
            this.browseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.browseButton.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.browseButton.Location = new System.Drawing.Point(1243, 366);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(146, 47);
            this.browseButton.TabIndex = 27;
            this.browseButton.Text = "Browse";
            this.browseButton.UseVisualStyleBackColor = false;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.LightCyan;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label1.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label1.Location = new System.Drawing.Point(603, 175);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(345, 29);
            this.label1.TabIndex = 26;
            this.label1.Text = "Press Browse to Search Files";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightCyan;
            this.panel1.Controls.Add(this.allowImportLabel);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.filesLabel);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.folderLabel);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.feedbackLabel);
            this.panel1.Controls.Add(this.browseButton);
            this.panel1.Controls.Add(this.cancelButton);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(170, 90);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1700, 800);
            this.panel1.TabIndex = 32;
            // 
            // allowImportLabel
            // 
            this.allowImportLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.allowImportLabel.AutoSize = true;
            this.allowImportLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.allowImportLabel.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.allowImportLabel.Location = new System.Drawing.Point(548, 426);
            this.allowImportLabel.Name = "allowImportLabel";
            this.allowImportLabel.Size = new System.Drawing.Size(155, 25);
            this.allowImportLabel.TabIndex = 38;
            this.allowImportLabel.Text = "Folder Selected:";
            this.allowImportLabel.Visible = false;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBox2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.textBox2.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.textBox2.Location = new System.Drawing.Point(553, 263);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox2.Size = new System.Drawing.Size(612, 98);
            this.textBox2.TabIndex = 37;
            // 
            // filesLabel
            // 
            this.filesLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.filesLabel.AutoSize = true;
            this.filesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.filesLabel.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.filesLabel.Location = new System.Drawing.Point(583, 259);
            this.filesLabel.Name = "filesLabel";
            this.filesLabel.Size = new System.Drawing.Size(59, 25);
            this.filesLabel.TabIndex = 36;
            this.filesLabel.Text = "None";
            this.filesLabel.Visible = false;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label5.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label5.Location = new System.Drawing.Point(381, 259);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(141, 25);
            this.label5.TabIndex = 35;
            this.label5.Text = "Files Selected:";
            // 
            // folderLabel
            // 
            this.folderLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.folderLabel.AutoSize = true;
            this.folderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.folderLabel.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.folderLabel.Location = new System.Drawing.Point(548, 228);
            this.folderLabel.Name = "folderLabel";
            this.folderLabel.Size = new System.Drawing.Size(59, 25);
            this.folderLabel.TabIndex = 34;
            this.folderLabel.Text = "None";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label3.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label3.Location = new System.Drawing.Point(381, 228);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(155, 25);
            this.label3.TabIndex = 33;
            this.label3.Text = "Folder Selected:";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.button1.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.button1.Location = new System.Drawing.Point(1243, 419);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(146, 45);
            this.button1.TabIndex = 32;
            this.button1.Text = "Import";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ImportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1389, 697);
            this.Controls.Add(this.panel1);
            this.Name = "ImportForm";
            this.Text = "ImportForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label feedbackLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label filesLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label folderLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label allowImportLabel;
    }
}