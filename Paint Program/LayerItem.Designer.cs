﻿namespace Paint_Program
{
    partial class LayerItem
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
            this.cbVisible = new System.Windows.Forms.CheckBox();
            this.pbLayerPreview = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbLayerPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // cbVisible
            // 
            this.cbVisible.AutoSize = true;
            this.cbVisible.Location = new System.Drawing.Point(180, 36);
            this.cbVisible.Name = "cbVisible";
            this.cbVisible.Size = new System.Drawing.Size(81, 24);
            this.cbVisible.TabIndex = 1;
            this.cbVisible.Text = "Visible";
            this.cbVisible.UseVisualStyleBackColor = true;
            this.cbVisible.CheckedChanged += new System.EventHandler(this.cbVisible_CheckedChanged);
            // 
            // pbLayerPreview
            // 
            this.pbLayerPreview.Location = new System.Drawing.Point(3, 3);
            this.pbLayerPreview.Name = "pbLayerPreview";
            this.pbLayerPreview.Size = new System.Drawing.Size(126, 94);
            this.pbLayerPreview.TabIndex = 2;
            this.pbLayerPreview.TabStop = false;
            // 
            // LayerItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pbLayerPreview);
            this.Controls.Add(this.cbVisible);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "LayerItem";
            this.Size = new System.Drawing.Size(350, 100);
            this.Click += new System.EventHandler(this.LayerForm_Click);
            ((System.ComponentModel.ISupportInitialize)(this.pbLayerPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox cbVisible;
        private System.Windows.Forms.PictureBox pbLayerPreview;
    }
}
