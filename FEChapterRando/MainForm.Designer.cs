
namespace FEChapterRando
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.StartRandomization = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // StartRandomization
            // 
            this.StartRandomization.Location = new System.Drawing.Point(12, 12);
            this.StartRandomization.Name = "StartRandomization";
            this.StartRandomization.Size = new System.Drawing.Size(234, 23);
            this.StartRandomization.TabIndex = 0;
            this.StartRandomization.Text = "The button that does the randomization";
            this.StartRandomization.UseVisualStyleBackColor = true;
            this.StartRandomization.Click += new System.EventHandler(this.StartRandomization_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 45);
            this.Controls.Add(this.StartRandomization);
            this.Name = "MainForm";
            this.Text = "FE Chapter Randomizer";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button StartRandomization;
    }
}

