﻿namespace StructuredFileDiff.Gui
{
    partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openRightFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openLeftFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.switchSidesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.panelLeft = new System.Windows.Forms.Panel();
			this.buttonLeftFile = new System.Windows.Forms.Button();
			this.panelRight = new System.Windows.Forms.Panel();
			this.buttonRightFile = new System.Windows.Forms.Button();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(12, 6);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(304, 20);
			this.textBox1.TabIndex = 0;
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(11, 6);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(295, 20);
			this.textBox2.TabIndex = 1;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(800, 24);
			this.menuStrip1.TabIndex = 2;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openLeftFileToolStripMenuItem,
            this.openRightFileToolStripMenuItem,
            this.switchSidesToolStripMenuItem,
            this.quitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// openRightFileToolStripMenuItem
			// 
			this.openRightFileToolStripMenuItem.Name = "openRightFileToolStripMenuItem";
			this.openRightFileToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.openRightFileToolStripMenuItem.Text = "Open right file";
			this.openRightFileToolStripMenuItem.Click += new System.EventHandler(this.openRightFileToolStripMenuItem_Click);
			// 
			// openLeftFileToolStripMenuItem
			// 
			this.openLeftFileToolStripMenuItem.Name = "openLeftFileToolStripMenuItem";
			this.openLeftFileToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.openLeftFileToolStripMenuItem.Text = "Open left file";
			this.openLeftFileToolStripMenuItem.Click += new System.EventHandler(this.openLeftFileToolStripMenuItem_Click);
			// 
			// switchSidesToolStripMenuItem
			// 
			this.switchSidesToolStripMenuItem.Name = "switchSidesToolStripMenuItem";
			this.switchSidesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.switchSidesToolStripMenuItem.Text = "Switch sides";
			this.switchSidesToolStripMenuItem.Click += new System.EventHandler(this.switchSidesToolStripMenuItem_Click);
			// 
			// quitToolStripMenuItem
			// 
			this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
			this.quitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.quitToolStripMenuItem.Text = "Quit";
			this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.IsSplitterFixed = true;
			this.splitContainer1.Location = new System.Drawing.Point(0, 24);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.panelLeft);
			this.splitContainer1.Panel1.Controls.Add(this.buttonLeftFile);
			this.splitContainer1.Panel1.Controls.Add(this.textBox1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.panelRight);
			this.splitContainer1.Panel2.Controls.Add(this.buttonRightFile);
			this.splitContainer1.Panel2.Controls.Add(this.textBox2);
			this.splitContainer1.Size = new System.Drawing.Size(800, 426);
			this.splitContainer1.SplitterDistance = 400;
			this.splitContainer1.SplitterWidth = 1;
			this.splitContainer1.TabIndex = 3;
			// 
			// panelLeft
			// 
			this.panelLeft.BackColor = System.Drawing.Color.White;
			this.panelLeft.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelLeft.Location = new System.Drawing.Point(0, 32);
			this.panelLeft.Name = "panelLeft";
			this.panelLeft.Size = new System.Drawing.Size(400, 394);
			this.panelLeft.TabIndex = 2;
			// 
			// buttonLeftFile
			// 
			this.buttonLeftFile.Location = new System.Drawing.Point(322, 3);
			this.buttonLeftFile.Name = "buttonLeftFile";
			this.buttonLeftFile.Size = new System.Drawing.Size(75, 23);
			this.buttonLeftFile.TabIndex = 1;
			this.buttonLeftFile.Text = "Left file";
			this.buttonLeftFile.UseVisualStyleBackColor = true;
			this.buttonLeftFile.Click += new System.EventHandler(this.button1_Click);
			// 
			// panelRight
			// 
			this.panelRight.BackColor = System.Drawing.Color.White;
			this.panelRight.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelRight.Location = new System.Drawing.Point(0, 32);
			this.panelRight.Name = "panelRight";
			this.panelRight.Size = new System.Drawing.Size(399, 394);
			this.panelRight.TabIndex = 3;
			// 
			// buttonRightFile
			// 
			this.buttonRightFile.Location = new System.Drawing.Point(321, 3);
			this.buttonRightFile.Name = "buttonRightFile";
			this.buttonRightFile.Size = new System.Drawing.Size(75, 23);
			this.buttonRightFile.TabIndex = 2;
			this.buttonRightFile.Text = "Right File";
			this.buttonRightFile.UseVisualStyleBackColor = true;
			this.buttonRightFile.Click += new System.EventHandler(this.button2_Click);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.Text = "Form1";
			this.Resize += new System.EventHandler(this.Form1_Resize);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openRightFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openLeftFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem switchSidesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button buttonLeftFile;
        private System.Windows.Forms.Button buttonRightFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panelRight;
    }
}

