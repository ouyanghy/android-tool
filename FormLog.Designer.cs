namespace android_tool
{
    partial class FormLog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLog));
            this.tv = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tv
            // 
            this.tv.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.tv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv.ForeColor = System.Drawing.SystemColors.Info;
            this.tv.Location = new System.Drawing.Point(0, 0);
            this.tv.Multiline = true;
            this.tv.Name = "tv";
            this.tv.ReadOnly = true;
            this.tv.Size = new System.Drawing.Size(581, 426);
            this.tv.TabIndex = 0;
            // 
            // FormLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 426);
            this.Controls.Add(this.tv);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormLog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "日志";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox tv;
    }
}