﻿namespace Paint_Program
{
    partial class BrushSettings
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lPrime = new System.Windows.Forms.Label();
            this.lSec = new System.Windows.Forms.Label();
            this.pPrime = new System.Windows.Forms.Panel();
            this.pSec = new System.Windows.Forms.Panel();
            this.tbSize = new System.Windows.Forms.TrackBar();
            this.lSize = new System.Windows.Forms.Label();
            this.lHard = new System.Windows.Forms.Label();
            this.tbHardness = new System.Windows.Forms.TrackBar();
            this.cdPicker = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)(this.tbSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbHardness)).BeginInit();
            this.SuspendLayout();
            // 
            // lPrime
            // 
            this.lPrime.AutoSize = true;
            this.lPrime.Location = new System.Drawing.Point(59, 46);
            this.lPrime.Name = "lPrime";
            this.lPrime.Size = new System.Drawing.Size(106, 20);
            this.lPrime.TabIndex = 0;
            this.lPrime.Text = "Primary Color:";
            // 
            // lSec
            // 
            this.lSec.AutoSize = true;
            this.lSec.Location = new System.Drawing.Point(35, 103);
            this.lSec.Name = "lSec";
            this.lSec.Size = new System.Drawing.Size(130, 20);
            this.lSec.TabIndex = 1;
            this.lSec.Text = "Secondary Color:";
            // 
            // pPrime
            // 
            this.pPrime.Location = new System.Drawing.Point(189, 35);
            this.pPrime.Name = "pPrime";
            this.pPrime.Size = new System.Drawing.Size(50, 50);
            this.pPrime.TabIndex = 2;
            this.pPrime.Click += new System.EventHandler(this.pPrime_Click);
            this.pPrime.Paint += new System.Windows.Forms.PaintEventHandler(this.pPrime_Paint);
            // 
            // pSec
            // 
            this.pSec.Location = new System.Drawing.Point(189, 91);
            this.pSec.Name = "pSec";
            this.pSec.Size = new System.Drawing.Size(50, 50);
            this.pSec.TabIndex = 0;
            this.pSec.Click += new System.EventHandler(this.pSec_Click);
            // 
            // tbSize
            // 
            this.tbSize.Location = new System.Drawing.Point(39, 219);
            this.tbSize.Maximum = 100;
            this.tbSize.Minimum = 1;
            this.tbSize.Name = "tbSize";
            this.tbSize.Size = new System.Drawing.Size(231, 69);
            this.tbSize.TabIndex = 0;
            this.tbSize.Value = 1;
            this.tbSize.ValueChanged += new System.EventHandler(this.tbSize_ValueChanged);
            // 
            // lSize
            // 
            this.lSize.AutoSize = true;
            this.lSize.Location = new System.Drawing.Point(35, 185);
            this.lSize.Name = "lSize";
            this.lSize.Size = new System.Drawing.Size(103, 20);
            this.lSize.TabIndex = 3;
            this.lSize.Text = "Brush Size: 1";
            // 
            // lHard
            // 
            this.lHard.AutoSize = true;
            this.lHard.Location = new System.Drawing.Point(35, 291);
            this.lHard.Name = "lHard";
            this.lHard.Size = new System.Drawing.Size(141, 20);
            this.lHard.TabIndex = 4;
            this.lHard.Text = "Brush Hardness: 0";
            // 
            // tbHardness
            // 
            this.tbHardness.Location = new System.Drawing.Point(39, 339);
            this.tbHardness.Maximum = 255;
            this.tbHardness.Minimum = 1;
            this.tbHardness.Name = "tbHardness";
            this.tbHardness.Size = new System.Drawing.Size(231, 69);
            this.tbHardness.TabIndex = 5;
            this.tbHardness.Value = 1;
            this.tbHardness.ValueChanged += new System.EventHandler(this.tbHardness_ValueChanged);
            // 
            // cdPicker
            // 
            this.cdPicker.AnyColor = true;
            this.cdPicker.FullOpen = true;
            // 
            // BrushSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tbHardness);
            this.Controls.Add(this.lHard);
            this.Controls.Add(this.lSize);
            this.Controls.Add(this.tbSize);
            this.Controls.Add(this.pSec);
            this.Controls.Add(this.pPrime);
            this.Controls.Add(this.lSec);
            this.Controls.Add(this.lPrime);
            this.Name = "BrushSettings";
            this.Size = new System.Drawing.Size(358, 423);
            ((System.ComponentModel.ISupportInitialize)(this.tbSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbHardness)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lPrime;
        private System.Windows.Forms.Label lSec;
        private System.Windows.Forms.Panel pPrime;
        private System.Windows.Forms.Panel pSec;
        private System.Windows.Forms.TrackBar tbSize;
        private System.Windows.Forms.Label lSize;
        private System.Windows.Forms.Label lHard;
        private System.Windows.Forms.TrackBar tbHardness;
        private System.Windows.Forms.ColorDialog cdPicker;
    }
}