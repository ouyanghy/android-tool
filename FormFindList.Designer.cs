namespace android_tool
{
    partial class FormFindList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFindList));
            this.listBoxFind = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // listBoxFind
            // 
            this.listBoxFind.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxFind.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBoxFind.FormattingEnabled = true;
            this.listBoxFind.ItemHeight = 20;
            this.listBoxFind.Location = new System.Drawing.Point(0, 0);
            this.listBoxFind.Name = "listBoxFind";
            this.listBoxFind.Size = new System.Drawing.Size(689, 322);
            this.listBoxFind.TabIndex = 0;
            // 
            // FormFindList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 322);
            this.Controls.Add(this.listBoxFind);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormFindList";
            this.Text = "查找列表";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxFind;
    }
}